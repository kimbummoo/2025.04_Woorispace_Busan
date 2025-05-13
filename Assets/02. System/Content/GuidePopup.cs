/*
 * 작성자: Kim Bummoo
 * 작성일: 2025.05.10
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUTUREVISION.WebAR
{
    public class GuidePopup : MonoBehaviour
    {
        [SerializeField] protected TextBoxUIItem GuideText;

        public virtual void Initialize()
        {
            GuideText.Button.onClick.AddListener(() =>
            {
                // 팝업 닫기
                gameObject.SetActive(false);

                GlobalManager.Instance.SoundModel.PlayButtonClickSound();
            });
        }
    }
}
