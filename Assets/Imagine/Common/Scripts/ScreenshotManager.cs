using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using FUTUREVISION.WebAR;


namespace Imagine.WebAR
{
    public enum EScreenShotEventType
    {
        None,
        Prepare,
        Capture,
        Release,
    }

    public class ScreenshotManager : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")] private static extern void ShowWebGLScreenshot(string dataUrl);
#endif
        public ARTrackerModel ARTrackerModel;
        private ARCamera arCamera;

        [SerializeField] private AudioClip shutterSound;
        [SerializeField] private AudioSource shutterSoundSource;

        public Texture2D screenShot;


        void Start(){
            arCamera = GameObject.FindObjectOfType<ARCamera>();
        }


        public void GetScreenShot()
        {
            StartCoroutine(CaptureScreenshot());
        }

        private IEnumerator CaptureScreenshot()
        {
            yield return new WaitForEndOfFrame();

            ARTrackerModel.OnScreenShotEvent.Invoke(EScreenShotEventType.Prepare);

            // ���� �������� ��� �׷��� ������ ��ٸ��ϴ�.
            yield return new WaitForEndOfFrame();

            ARTrackerModel.OnScreenShotEvent.Invoke(EScreenShotEventType.Capture);

            // ���� �ؽ�ó �����Ͽ� �޸� ������ �����մϴ�.
            if (screenShot != null)
            {
                Destroy(screenShot);
            }
            // ��ũ�� ����� ���� Texture2D ����
            screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);

            // ȭ�� ������ �ȼ��� �о�ɴϴ�.
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenShot.Apply();

            //// �׽�Ʈ�� ���� ���� ����
            //string path = Application.dataPath + "/screenshot.png";
            //System.IO.File.WriteAllBytes(path, screenShot.EncodeToPNG());

            // ���� ��ũ���� ���� �� �ļ� ó��
            ARTrackerModel.OnScreenShotEvent.Invoke(EScreenShotEventType.Release);

            if (shutterSoundSource != null && shutterSound != null)
            {
                shutterSoundSource.PlayOneShot(shutterSound);
            }

#if UNITY_EDITOR
            Debug.Log("Screenshots are only displayed on WebGL builds");
#elif UNITY_WEBGL && !UNITY_EDITOR
    byte[] textureBytes = screenShot.EncodeToJPG();
    string dataUrlStr = "data:image/jpeg;base64," + System.Convert.ToBase64String(textureBytes);
    ShowWebGLScreenshot(dataUrlStr);
#endif
        }
    }
}

