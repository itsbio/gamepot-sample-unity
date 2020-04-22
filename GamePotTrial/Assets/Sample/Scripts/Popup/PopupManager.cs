using UnityEngine;

namespace GamePotSample
{
    public class PopupManager : MonoBehaviour
    {
        public enum CommonPopupType
        {
            SMALL_SIZE,
            LARGE_SIZE
        }

        public static GameObject ShowPopup(GameObject parent, string popupName)
        {
            GameObject popupGameObject = Resources.Load<GameObject>(popupName);

			if (popupGameObject == null)
			{
                Debug.LogError("GamePot Sample - Could not found " + popupName + " in Resources folder");
			}

            popupGameObject = Instantiate(popupGameObject) as GameObject;
            popupGameObject.transform.parent = parent.transform;
            popupGameObject.transform.localScale = new Vector3(1, 1, 1);
            popupGameObject.transform.localPosition = Vector3.zero;

            popupGameObject.transform.SetAsLastSibling();

            return popupGameObject;
        }

        public static void ShowCommonPopup(GameObject parent, CommonPopupType popupType, string title, string message, string okText, System.Action okCallback, string cancelText = null, System.Action cancelCallback = null)
        {
            string prefabName;
            if (CommonPopupType.SMALL_SIZE == popupType)
            {
                prefabName = "GamePotSampleSPopup";
            }
            else
            {
                prefabName = "GamePotSampleLPopup";
            }
            CommonPopup commonPopup = ShowPopup(parent, prefabName).GetComponent<CommonPopup>();
            if (commonPopup != null)
            {
                commonPopup.SetPopup(
                    title,
                    message,
                    okText,
                    okCallback,
                    cancelText,
                    cancelCallback
                );
            }
        }
    }
}