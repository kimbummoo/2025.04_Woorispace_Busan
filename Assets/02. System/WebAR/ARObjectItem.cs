/*
 * 작성자: Kim Bummoo
 * 작성일: 2025.03.03
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUTUREVISION.WebAR
{
    public class ARObjectItem : MonoBehaviour
    {
        [Space]
        public UnityEvent OnClickMouseButton;

        public virtual void Initialize()
        {

        }

        // Touch 이벤트 처리를 위한 메소드 (2D Box Collider가 터치되었을 때 호출됨)
        private void OnMouseDown()
        {
            OnClickMouseButton.Invoke();
        }
    }
}
