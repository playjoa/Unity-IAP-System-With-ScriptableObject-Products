using IAP_System.Base.Products;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IAP_System.Base.Tools
{
    public class InAppPurchaseProductView : MonoBehaviour
    {
        [Header("Product to display")]
        [SerializeField] private InAppPurchaseProduct productToDisplayOnStart;
        
        [Header("Buttons")]
        [SerializeField] private Button btnBuyProduct;
        
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI txtProductName;
        [SerializeField] private TextMeshProUGUI txtProductDesc;
        [SerializeField] private TextMeshProUGUI txtProductQty;
        [SerializeField] private TextMeshProUGUI txtProductPrice;
        
        [Header("Image")]
        [SerializeField] private Image imgProduct;
    
        private InAppPurchaseProduct productInDisplay;
        
        private void Awake()
        {
            if (btnBuyProduct != null)
                btnBuyProduct.onClick.AddListener(OnBuyProductClick);
        }

        private void Start()
        {
            if (productToDisplayOnStart != null)
                DisplayProduct(productToDisplayOnStart);
        }

        private void OnDestroy()
        {
            if (btnBuyProduct != null)
                btnBuyProduct.onClick.RemoveAllListeners();
        }

        public void DisplayProduct(InAppPurchaseProduct productToDisplay)
        {
            if (productToDisplay == null)
            {
                Debug.LogError("Trying to display null product");
                return;
            }
            productInDisplay = productToDisplay;
            SetProductTexts();
            SetProductImage();
        }

        private void OnBuyProductClick() => productInDisplay.BuyProduct();

        private void SetProductTexts()
        {
            FillText(txtProductName, productInDisplay.ProductName);
            FillText(txtProductDesc, productInDisplay.ProductDescription);
            FillText(txtProductQty, productInDisplay.ProductQuantityText);
            FillText(txtProductPrice, productInDisplay.ProductNicePrice);
        }

        private void SetProductImage()
        {
            if(imgProduct == null || productInDisplay.ProductSprite == null) return;
            imgProduct.sprite = productInDisplay.ProductSprite;
        }

        private void FillText(TMP_Text textFieldTarget, string content, string prefix = "", string suffix ="")
        {
            if(textFieldTarget == null) return;

            textFieldTarget.text = $"{prefix}{content}{suffix}";
        }
    }
}
