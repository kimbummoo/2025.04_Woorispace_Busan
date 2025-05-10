/*
 * 
 *
 */

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace FUTUREVISION
{
    public class WebCamViewModel : BaseViewModel
    {
        [Header("������ �𸣰�����, Canvas�� sorting order�� 0 ���� �۰� �ؾ� camera�� �ִ� physics raycaster �̺�Ʈ�� blocking���� ����.")]

        [Header("WebCamPresenter")]
        [SerializeField] protected WebCamView view;
        public WebCamView View { get => view; }

        public override void Initialize()
        {
            base.Initialize();

            view.Initialize();
        }

        public virtual void SetCamTexture(WebCamTexture webCamTexture)
        {
            view.SetCamTexture(webCamTexture);
        }

        public virtual void UpdateImageSize(int2 imageSize)
        {
            view.UpdateImageSize(imageSize);
        }
    }
}
