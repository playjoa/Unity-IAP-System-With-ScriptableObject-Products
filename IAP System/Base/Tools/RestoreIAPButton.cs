using IAP_System.Base.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace IAP_System.Base.Tools
{
    [RequireComponent(typeof(Button))]
    public class RestoreIAPButton : MonoBehaviour
    {
        private Button restoreButton;

        private void Awake()
        {
            if (restoreButton == null)
                restoreButton = GetComponent<Button>();
        
            restoreButton.onClick.AddListener(ClickHandler);
            
            if(!IAPController.ME.InAppleDevice)
                restoreButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            restoreButton.onClick.RemoveAllListeners();
        }

        private void ClickHandler()
        {
            IAPController.ME.RestorePurchases();
        }
    }
}
