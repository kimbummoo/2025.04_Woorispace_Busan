/*
 * 작성자: Kim Bummoo
 * 작성일: 2025.05.10
 *
 */

using FUTUREVISION;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUTUREVISION.WebAR
{
    public class MissionCompletePopup : MonoBehaviour
    {
        [SerializeField] protected TextBoxUIItem GuideText;
        [Space(10)]
        public UnityEvent OnClickGuidePopup;

        public virtual void Initialize()
        {
            GuideText.Button.onClick.AddListener(() =>
            {
                // 자동으로 넘어가도록 설정함 Manager 참고
                //// 팝업 닫기
                //gameObject.SetActive(false);

                GlobalManager.Instance.SoundModel.PlayButtonClickSound();
            });
        }

        public void ShowGuide(string text)
        {
            // 팝업 열기
            gameObject.SetActive(true);

            // 텍스트 설정
            GuideText.Text.text = text;
        }
    }
}
