/*
 * 작성자: 김범무
 * 작성일: 2025.05.11
 */

using FUTUREVISION.WebAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUTUREVISION.Content
{
    public class ContentViewModel : MonoBehaviour
    {
        [SerializeField] protected BingoView BingoView;
        [SerializeField] protected QuizView QuizView;
        [SerializeField] protected GuidePopup GuidePopup;
        public virtual void Initialize()
        {
            BingoView.Initialize();
            QuizView.Initialize();
            GuidePopup.Initialize();
        }
    }
}
