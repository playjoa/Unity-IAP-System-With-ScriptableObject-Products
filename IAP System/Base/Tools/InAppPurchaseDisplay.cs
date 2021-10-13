using System;
using System.Collections.Generic;
using IAP_System.Base.Controller;
using IAP_System.Base.Products;
using UnityEngine;

namespace IAP_System.Base.Tools
{
    public class InAppPurchaseDisplay : MonoBehaviour
    {
        [SerializeField] private List<ProductDisplay> productDisplays;

        private void Start()
        {
            SetUpDisplay();
        }

        private void SetUpDisplay()
        {
            foreach (var targetDisplay in productDisplays)
            {
                var product = IAPController.ME.GetProduct(targetDisplay.ProductKey);
                if(product == null) continue;
                
                targetDisplay.SetProductView(product);
            }
        }
    }

    [Serializable]
    public class ProductDisplay
    {
        [SerializeField] private string productKey;
        [SerializeField] private InAppPurchaseProductView productView;

        public string ProductKey => productKey;
        
        public void SetProductView(InAppPurchaseProduct productToDisplay)
        {
            productView.Initiate(productToDisplay);
        }
    }
}
