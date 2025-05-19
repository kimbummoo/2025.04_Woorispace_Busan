/*
 * 작성자: Kim Bummoo
 * 작성일: 2025.03.03
 *
 */

using Imagine.WebAR;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FUTUREVISION.WebAR
{
    public class ARTrackerModel : BaseModel
    {
        [Header("AR Camera의 포지션과 이름이 하드코딩 되어있으니 변경하지 말것")]

        [Space(10)]
        [Header("Camera")]
        [SerializeField] protected ARCamera ARCamera;
        [SerializeField] protected ScreenshotManager ScreenshotManager;
        [SerializeField] protected GameObject Placement;

        [Space(10)]
        [SerializeField] protected Camera ScreenContentCamera;
        [SerializeField] protected Camera WorldContentsCamera;

        [Space(10)]
        [Header("Object")]
        [SerializeField] protected WorldTracker WorldTracker;

        [Space(10)]
        [SerializeField] protected GameObject ScreenController;
        [SerializeField] protected GameObject WorldController;

        [Space(10)]
        [SerializeField] protected GameObject ScreenObject;
        [SerializeField] protected GameObject WorldObject;

        [Space(10)]
        [Header("State")]
        [SerializeField] protected CameraState CameraState;
        [SerializeField] protected ARTrackerState ARTrackingState;

        [Space(10)]
        [Header("Event")]
        public UnityEvent<ARTrackerState> OnARTrackingStateChanged;

        public UnityEvent OnPlaced;
        public UnityEvent OnReset;
        public UnityEvent<bool> OnPlacementVisibilityChanged;
        public UnityEvent<EScreenShotEventType> OnScreenShotEvent;

        private bool isPlacement = false;
        public bool IsPlacement => isPlacement;

        public override void Initialize()
        {
            base.Initialize();

            // WorldTracker 초기화
            StartCoroutine(WaitForCameraPermission());
        }

        public IEnumerator WaitForCameraPermission()
        {
            Application.RequestUserAuthorization(UserAuthorization.WebCam);

            yield return new WaitUntil(() =>
            {
                return Application.HasUserAuthorization(UserAuthorization.WebCam);
            });
            yield return new WaitForSeconds(1.5f);
            WorldTracker.gameObject.SetActive(true);
        }

        public void SetCameraState(CameraState state)
        {
            CameraState = state;

            string settingParam = string.Empty;
            switch (state)
            {
                case CameraState.Front:
                    {
                        settingParam = "user";
                        ARCamera.isFlipped = true;
                    }
                    break;
                case CameraState.Back:
                    {
                        settingParam = "environment";
                        ARCamera.isFlipped = false;
                    }
                    break;
            }
            StartCoroutine(RestartCamera(settingParam));
        }

        IEnumerator RestartCamera(string settingParam)
        {
            Application.ExternalCall("StopWebcam");

            yield return new WaitForSeconds(0.5f);

            Application.ExternalCall("SetWebCamSetting", settingParam);
            Application.ExternalCall("StartWebcam");
        }

        public void SetARTrackerState(ARTrackerState state)
        {
            ARTrackingState = state;

            switch (ARTrackingState)
            {
                case ARTrackerState.ScreenState:
                    WorldTracker.ResetOrigin();

                    // 카메라 전환
                    ScreenContentCamera.gameObject.SetActive(true);
                    WorldContentsCamera.gameObject.SetActive(false);

                    // 
                    ScreenController.SetActive(true);
                    WorldController.SetActive(false);

                    // 오브젝트 전환
                    ScreenObject.SetActive(true);
                    WorldObject.SetActive(false);
                    break;

                case ARTrackerState.WorldState:
                    WorldTracker.ResetOrigin();

                    // 카메라 전환
                    ScreenContentCamera.gameObject.SetActive(false);
                    WorldContentsCamera.gameObject.SetActive(true);

                    //
                    ScreenController.SetActive(false);
                    WorldController.SetActive(true);

                    // 오브젝트 전환
                    ScreenObject.SetActive(false);
                    WorldObject.SetActive(true);

                    if (WebARManager.Instance.IsAutomaticPlacement)
                    {
                        // 1.5초 뒤에 자동으로 배치되도록 합니다.
                        StartCoroutine(DelayResetCoroutine());
                    }
                    break;
            }

            OnARTrackingStateChanged?.Invoke(ARTrackingState);
        }

        #region Getter

        public Camera GetScreenContentCamera()
        {
            return ScreenContentCamera;
        }

        public Camera GetWorldContentsCamera()
        {
            return WorldContentsCamera;
        }

        public CameraState GetCameraState()
        {
            return CameraState;
        }

        public ARTrackerState GetARTrackerState()
        {
            return ARTrackingState;
        }

        public GameObject GetCurruentObject()
        {
            switch (ARTrackingState)
            {
                case ARTrackerState.ScreenState:
                    return ScreenObject;
                case ARTrackerState.WorldState:
                    return WorldObject;
            }

            return null;
        }

        #endregion

        #region WorldTracker 제어

        /// <summary>
        /// 자동 배치를 위한 코루틴, 1.5초 뒤에 배치합니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerator DelayResetCoroutine()
        {
            yield return new WaitForSeconds(1.5f);
            WorldTracker.PlaceOrigin();
        }

        public bool GetPlacementVisibility()
        {
            return Placement.activeSelf;
        }

        public void SetPlacement()
        {
            // AR 오브젝트를 배치합니다.
            WorldTracker.PlaceOrigin();
            isPlacement = true;

            OnPlaced?.Invoke();
        }

        public void ResetPlacement()
        {
            WorldTracker.ResetOrigin();
            isPlacement = false;

            OnReset?.Invoke();
        }

        public void TakeScreenShot()
        {
            // 스크린샷 매니저를 통해 스크린샷을 찍습니다. WebGL에서 정상작동 합니다.
            ScreenshotManager.GetScreenShot();
        }

        // WorldTracker 이벤트 콜백

        public void OnPlacementVisibilityChangedCallback(bool isShow)
        {
            OnPlacementVisibilityChanged?.Invoke(isShow);
        }

        #endregion
    }
}
