/*
 * 작성자: Kim Bummoo
 * 작성일: 2024.12.11
 */

using FUTUREVISION.Content;
using System;
using System.Collections;
using UnityEngine;

namespace FUTUREVISION.WebAR
{
    public enum CameraState
    {
        None,

        Front,
        Back,
    }

    public enum ARTrackerState
    {
        None,
        ScreenState,
        WorldState,
    }

    public class WebARManager : BaseManager<WebARManager>
    {
        [Header("WebAR Manager")]
        public ARTrackerModel ARTrackerModel;
        public ARViewModel ARViewModel;
        public ContentViewModel ContentViewModel;

        [Space(10)]
        public RenderTexture RenderTexture;

        [Header("WebAR Manager/Setting")]
        [Tooltip("TODO: WebAR의 카메라를 자동으로 배치할지 여부")]
        public bool IsAutomaticPlacement = false;

        public CameraState StartCameraState = CameraState.Back;
        public ARTrackerState StartObjectState = ARTrackerState.ScreenState;

        //[Space]
        private bool isFirstFind = false;

        public override void Initialize()
        {
            base.Initialize();

            ContentViewModel.SetContentState(ContentState.Intro);
            StartCoroutine(RequestCameraPermission(() =>
            {
                // 카메라 권한 요청 후 초기화
                InitializeWebAR();

                ContentViewModel.SetContentState(ContentState.Quiz);
            }));
        }

        public void InitializeWebAR()
        {
            ARTrackerModel.Initialize();
            ARViewModel.Initialize();
            ContentViewModel.Initialize();

            ARTrackerModel.SetCameraState(StartCameraState);
            ARTrackerModel.SetARTrackerState(StartObjectState);
        }

        public IEnumerator RequestCameraPermission(Action action)
        {
            Application.RequestUserAuthorization(UserAuthorization.WebCam);

            // 카메라 권한 요청

            if (Application.isEditor)
            {
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                yield return new WaitUntil(() =>
                {
                    return Application.HasUserAuthorization(UserAuthorization.WebCam);
                });
            }

            action?.Invoke();

            if (IsAutomaticPlacement)
            {
                ARTrackerModel.ResetPlacement();
                yield return new WaitForSeconds(1.5f);
                ARTrackerModel.SetPlacement();
            }
        }

        #region Content

        public void EndFindARObject()
        {
            ContentViewModel.ShowMissionComplete(true);

            StartCoroutine(WaitForMissionComplete());
        }

        private IEnumerator WaitForMissionComplete()
        {
            yield return new WaitForSeconds(3.0f);
            ContentViewModel.SetContentState(Content.ContentState.Bingo);
            ContentViewModel.ShowBingoPanel(true);
        }

        #endregion

        //public void Update()
        //{
        //    // 웹페이즈의 화면 크기에 맞춰 Render Texture의 크기를 업데이트합니다.
        //    // Render Texture의 크기를 스크린 크기에 맞춰 업데이트
        //    if (RenderTexture.width != Screen.width || RenderTexture.height != Screen.height)
        //    {
        //        RenderTexture.Release();
        //        RenderTexture.width = Screen.width;
        //        RenderTexture.height = Screen.height;
        //        RenderTexture.Create();
        //    }
        //}
    }
}
