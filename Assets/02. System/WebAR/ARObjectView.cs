/*
 * 작성자: Kim Bummoo
 * 작성일: 2025.03.03
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUTUREVISION.WebAR
{
    public class ARObjectView : BaseView
    {
        [Header("Object")]
        public GameObject ParentObject;
        public List<GameObject> ObjectList = new List<GameObject>();
        protected int CurrentObjectIndex = 0;

        public override void Initialize()
        {
            base.Initialize();

            // 초기화
            SetCurrentObject(GlobalManager.Instance.DataModel.Step);
        }

        protected virtual void Update()
        {
            var arTrackerModel = WebARManager.Instance.ARTrackerModel;
            switch (arTrackerModel.GetARTrackerState())
            {
                case EARTrackerState.ScreenState:
                    ParentObject.SetActive(true);
                    break;
                case EARTrackerState.WorldState:
                    ParentObject.SetActive(arTrackerModel.IsPlacement);
                    break;
                case EARTrackerState.None:
                    Debug.LogWarning($"ARObjectView: 정의되지 않음");
                    break;
            }

            var targetObject = arTrackerModel.GetCurruentObject();

            if (targetObject == null)
            {
                Debug.LogWarning($"ARObjectView: targetObject is null");
                return;
            }

            ParentObject.transform.position = targetObject.transform.position;
            ParentObject.transform.rotation = targetObject.transform.rotation;
            ParentObject.transform.localScale = targetObject.transform.localScale;
        }   

        public void SetCurrentObject(int index, bool isLoop = true)
        {
            if (isLoop)
            {
                // 순환하도록 처리
                index = (index + ObjectList.Count) % ObjectList.Count;
            }

            // 범위를 벗어나면 경고 후 종료
            if (index < 0 || index >= ObjectList.Count)
            {
                Debug.LogWarning("Index out of range");
                return;
            }

            CurrentObjectIndex = index;

            for (int i = 0; i < ObjectList.Count; i++)
            {
                ObjectList[i].SetActive(i == index);
            }
        }

        public int GetCurrentObjectIndex()
        {
            return CurrentObjectIndex;
        }
    }
}
