﻿using UnityEngine;
using UnityEngine.UI;

namespace GamePotSample
{
    public class CommonPopup : MonoBehaviour
    {
        [SerializeField]
        private Text title;
        [SerializeField]
        private Text message;
        [SerializeField]
        private Text okButtonText;
        [SerializeField]
        private Text cancelButtonText;
        [SerializeField]
        private GameObject okGameObject;
        [SerializeField]
        private GameObject closeGameObject;

        private System.Action okCallback;
        private System.Action cancelCallback;

        public void SetPopup(string title, string message, string okText, System.Action okCallback, string cancelText = null, System.Action cancelCallback = null)
        {
            SetText(title, message);
            SetButton(okText, okCallback, cancelText, cancelCallback);
        }

        private void SetButton(string okText, System.Action okCallback, string cancelText, System.Action cancelCallback = null)
        {
            this.okCallback = okCallback;
            this.cancelCallback = cancelCallback;

            if (false == string.IsNullOrEmpty(okText))
            {
                okButtonText.text = okText;
            }

            if (false == string.IsNullOrEmpty(cancelText))
            {
                cancelButtonText.text = cancelText;
            }

            if (null == this.cancelCallback)
            {
                closeGameObject.SetActive(false);
                title.transform.localPosition = new Vector3(0f, title.transform.localPosition.y, 0f);
            }
        }

        private void SetText(string title, string message)
        {
            this.title.text = title;
            this.message.text = message;
        }

        #region UIButton.onClick
        public void ClickOKButton()
        {
            if (null != okCallback)
            {
                okCallback();
            }

            Destroy(gameObject);
        }

        public void ClickCloseButton()
        {
            if (null != cancelCallback)
            {
                cancelCallback();
            }

            Destroy(gameObject);
        }
        #endregion
    }
}