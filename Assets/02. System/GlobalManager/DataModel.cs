using Gpm.Common.ThirdParty.LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace FUTUREVISION
{
    [Serializable]
    public class ClearState
    {

        [SerializeField] private bool clearState1 = false;
        [SerializeField] private bool clearState2 = false;
        [SerializeField] private bool clearState3 = false;
        [SerializeField] private bool clearState4 = false;
        [SerializeField] private bool clearState5 = false;
        [SerializeField] private bool clearState6 = false;

        public bool ClearState1
        {
            get { return clearState1; }
            set
            {
                clearState1 = value;
                SaveData();
            }
        }
        public bool ClearState2
        {
            get { return clearState2; }
            set
            {
                clearState2 = value;
                SaveData();
            }
        }
        public bool ClearState3
        {
            get { return clearState3; }
            set
            {
                clearState3 = value;
                SaveData();
            }
        }
        public bool ClearState4
        {
            get { return clearState4; }
            set
            {
                clearState4 = value;
                SaveData();
            }
        }
        public bool ClearState5
        {
            get { return clearState5; }
            set
            {
                clearState5 = value;
                SaveData();
            }
        }
        public bool ClearState6
        {
            get { return clearState6; }
            set
            {
                clearState6 = value;
                SaveData();
            }
        }

        public void LoadData()
        {
            clearState1 = PlayerPrefs.GetInt("ClearState1", 0) == 1;
            clearState2 = PlayerPrefs.GetInt("ClearState2", 0) == 1;
            clearState3 = PlayerPrefs.GetInt("ClearState3", 0) == 1;
            clearState4 = PlayerPrefs.GetInt("ClearState4", 0) == 1;
            clearState5 = PlayerPrefs.GetInt("ClearState5", 0) == 1;
            clearState6 = PlayerPrefs.GetInt("ClearState6", 0) == 1;
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt("ClearState1", clearState1 ? 1 : 0);
            PlayerPrefs.SetInt("ClearState2", clearState2 ? 1 : 0);
            PlayerPrefs.SetInt("ClearState3", clearState3 ? 1 : 0);
            PlayerPrefs.SetInt("ClearState4", clearState4 ? 1 : 0);
            PlayerPrefs.SetInt("ClearState5", clearState5 ? 1 : 0);
            PlayerPrefs.SetInt("ClearState6", clearState6 ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void ClearData()
        {
            clearState1 = false;
            clearState2 = false;
            clearState3 = false;
            clearState4 = false;
            clearState5 = false;
            clearState6 = false;
            PlayerPrefs.DeleteKey("ClearState1");
            PlayerPrefs.DeleteKey("ClearState2");
            PlayerPrefs.DeleteKey("ClearState3");
            PlayerPrefs.DeleteKey("ClearState4");
            PlayerPrefs.DeleteKey("ClearState5");
            PlayerPrefs.DeleteKey("ClearState6");
            PlayerPrefs.Save();
        }
    }

    public class DataModel : BaseModel
    {
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();

        public int Step = 0;
        public bool IsOpenBingo = false;
        public ClearState ClearState = new ClearState();

        public override void Initialize()
        {
            InitializeParameters();
            Step = Parameters.ContainsKey("step") ? int.Parse(Parameters["step"]) : Step;
            string mode = Parameters.ContainsKey("mode") ? Parameters["mode"] : "";
            switch (mode)
            {
                case "bingo":
                    IsOpenBingo = true;
                    break;
            }

            ClearState.LoadData();
        }

        public void InitializeParameters()
        {
            // URL 파라미터에서 값 가져오기
            var url = Application.absoluteURL;

            if (url.Contains("?"))
            {
                var param = url.Split('?')[1];  // ? 뒤의 파라미터 부분만 가져오기
                var paramList = param.Split('&');
                Parameters = new Dictionary<string, string>();

                foreach (var p in paramList)
                {
                    var keyValue = p.Split('=');
                    if (keyValue.Length == 2)
                    {
                        // URL 디코딩 적용 (특수문자 및 한글 처리)
                        string key = WWW.UnEscapeURL(keyValue[0]);
                        string value = WWW.UnEscapeURL(keyValue[1]);
                        Parameters[key] = value;
                    }
                }
            }
        }

        public void SetCorrectState(int index, bool newState)
        {
            switch (index)
            {
                case 0:
                    ClearState.ClearState1 = newState;
                    break;
                case 1:
                    ClearState.ClearState2 = newState;
                    break;
                case 2:
                    ClearState.ClearState3 = newState;
                    break;
                case 3:
                    ClearState.ClearState4 = newState;
                    break;
                case 4:
                    ClearState.ClearState5 = newState;
                    break;
                case 5:
                    ClearState.ClearState6 = newState;
                    break;
                default:
                    Debug.LogWarning("Invalid index for setting clear state.");
                    break;
            }
        }

        [ContextMenu("Clear Data")]
        public void ResetClearState()
        {
            ClearState.ClearData();
        }

        public string GetGuideText()
        {
            switch (Step)
            {
                case 0:
                    return "용숙의 일기장 AR을 찾아보세요!";
                case 1:
                    return "짱구 왕사슴벌레 AR을 찾아보세요!";
                case 2:
                    return "황금매미 AR을 찾아보세요!";
                case 3:
                    return "버섯 옷을 입은 AR 짱구를 찾아보세요!";
                case 4:
                    return "야끼소바 AR을 찾아보세요!";
                case 5:
                    return "별똥별 AR을 찾아보세요!";
                default:
                    return "";
            }
        }
    }
}
