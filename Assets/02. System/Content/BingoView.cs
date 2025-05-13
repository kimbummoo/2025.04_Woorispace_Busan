/*
 * 작성자: 김범무
 * 작성일: 2025.05.11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FUTUREVISION.Content
{
    public class BingoView : MonoBehaviour
    {
        [SerializeField] protected GameObject BingoPanel;
        [SerializeField] protected Button OpenBingoButton;
        [SerializeField] protected Button CloseBingoButton;

        [SerializeField] protected GameObject Bingo1;
        [SerializeField] protected GameObject Bingo2;
        [SerializeField] protected GameObject Bingo3;
        [SerializeField] protected GameObject Bingo4;
        [SerializeField] protected GameObject Bingo5;
        [SerializeField] protected GameObject Bingo6;

        public virtual void Initialize()
        {
            OpenBingoButton.onClick.AddListener(OpenBingo);
            CloseBingoButton.onClick.AddListener(CloseBingo);

            BingoPanel.SetActive(false);
        }

        public void OpenBingo()
        {
            BingoPanel.SetActive(true);

            UpdateBingoState();

            GlobalManager.Instance.SoundModel.PlayButtonClickSound();
        }

        public void CloseBingo()
        {
            BingoPanel.SetActive(false);

            GlobalManager.Instance.SoundModel.PlayButtonClickSound();
        }

        protected void UpdateBingoState()
        {
            var dataModel = GlobalManager.Instance.DataModel;

            Bingo1.SetActive(dataModel.ClearState.ClearState1);
            Bingo2.SetActive(dataModel.ClearState.ClearState2);
            Bingo3.SetActive(dataModel.ClearState.ClearState3);
            Bingo4.SetActive(dataModel.ClearState.ClearState4);
            Bingo5.SetActive(dataModel.ClearState.ClearState5);
            Bingo6.SetActive(dataModel.ClearState.ClearState6);
        }
    }
}
