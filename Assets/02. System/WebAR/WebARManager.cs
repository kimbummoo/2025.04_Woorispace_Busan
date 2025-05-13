/*
 * 작성자: Kim Bummoo
 * 작성일: 2024.12.11
 */

using FUTUREVISION.Content;
using UnityEngine;

namespace FUTUREVISION.WebAR
{
    public enum ECameraState
    {
        None,

        Front,
        Back,
    }

    public enum EARTrackerState
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

        public ECameraState StartCameraState = ECameraState.Back;
        public EARTrackerState StartObjectState = EARTrackerState.ScreenState;

        public override void Initialize()
        {
            base.Initialize();

            ARTrackerModel.Initialize();
            ARViewModel.Initialize();
            ContentViewModel.Initialize();

            ARTrackerModel.SetCameraState(StartCameraState);
            ARTrackerModel.SetARTrackerState(StartObjectState);
        }

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
