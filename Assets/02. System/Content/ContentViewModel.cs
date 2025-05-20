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
    public enum ContentState
    {
        None,
        Intro,
        Quiz,
        Finding,
        Bingo,
    }

    public class ContentViewModel : MonoBehaviour
    {
        [Space]
        [SerializeField] protected BingoView BingoView;
        [SerializeField] protected QuizView QuizView;
        [SerializeField] protected IntroView IntroView;
        [SerializeField] protected GuidePopup GuidePopup;
        [SerializeField] protected MissionCompletePopup MissionCompletePopup;

        private ContentState currentState = ContentState.None;
        public ContentState CurrentState => currentState;

        public virtual void Initialize()
        {
            BingoView.Initialize();
            QuizView.Initialize();
            IntroView.Initialize();
            GuidePopup.Initialize();
            MissionCompletePopup.Initialize();

            ShowGuide(false);
            ShowMissionComplete(false);
        }

        public void SetContentState(ContentState newContentState)
        {
            currentState = newContentState;

            switch (newContentState)
            {
                case ContentState.Intro:
                    BingoView.gameObject.SetActive(false);
                    QuizView.gameObject.SetActive(false);
                    IntroView.gameObject.SetActive(true);

                    WebARManager.Instance.ARViewModel.SetActiveObjectView(false);
                    WebARManager.Instance.ARViewModel.SetActiveUIView(false);
                    break;
                case ContentState.Quiz:
                    BingoView.gameObject.SetActive(false);
                    QuizView.gameObject.SetActive(true);
                    IntroView.gameObject.SetActive(false);

                    WebARManager.Instance.ARViewModel.SetActiveObjectView(false);
                    WebARManager.Instance.ARViewModel.SetActiveUIView(false);

                    GlobalManager.Instance.SoundModel.PlayPopupSound();
                    break;
                case ContentState.Finding:
                    BingoView.gameObject.SetActive(false);
                    QuizView.gameObject.SetActive(false);
                    IntroView.gameObject.SetActive(false);

                    ShowGuide(true);
                    ShowMissionComplete(false);
                    //WebARManager.Instance.ARViewModel.SetActiveObjectView(true); // Show Guide가 끝난 후 보여줌
                    WebARManager.Instance.ARViewModel.SetActiveUIView(false);

                    GlobalManager.Instance.SoundModel.PlayPopupSound();
                    break;
                case ContentState.Bingo:
                    BingoView.gameObject.SetActive(true);
                    QuizView.gameObject.SetActive(false);
                    IntroView.gameObject.SetActive(false);

                    ShowGuide(false);
                    ShowMissionComplete(false);
                    WebARManager.Instance.ARViewModel.SetActiveObjectView(true);
                    WebARManager.Instance.ARViewModel.SetActiveUIView(true);

                    GlobalManager.Instance.SoundModel.PlayPopupSound();
                    break;
                default:
                    break;
            }
        }

        public void ShowGuide(bool newActive/*, string text = null*/)
        {
            string text = GlobalManager.Instance.DataModel.GetGuideText();
            GuidePopup.ShowGuide(newActive, text);
        }

        public void ShowMissionComplete(bool newActive, string text = null)
        {
            MissionCompletePopup.ShowGuide(newActive, text);
        }

        public void ShowBingoPanel(bool newState)
        {
            if (newState)
            {
                BingoView.OpenBingo();
            }
            else
            {
                BingoView.CloseBingo();
            }
        }
    }
}
