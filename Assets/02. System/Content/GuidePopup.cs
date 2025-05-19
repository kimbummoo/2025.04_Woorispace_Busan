/*
 * 작성자: Kim Bummoo
 * 작성일: 2025.05.10
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUTUREVISION.WebAR
{
    public class GuidePopup : MonoBehaviour
    {
        [SerializeField] protected TextBoxUIItem GuideText;
        //[Space(10)]
        //public UnityEvent OnClickGuidePopup;

        public virtual void Initialize()
        {
            //GuideText.Button.onClick.AddListener(() =>
            //{
            //    // 팝업 닫기
            //    gameObject.SetActive(false);

            //    GlobalManager.Instance.SoundModel.PlayButtonClickSound();
            //});
        }

        public void ShowGuide(bool newActive, string text = null)
        {
            // 팝업 열기
            gameObject.SetActive(newActive);

            // 텍스트 설정
            if (text != null)
            {
                GuideText.Text.text = text;
            }

            if (newActive == true)
            {
                WebARManager.Instance.EndGuide();
            }
        }
    }
}
