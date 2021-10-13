using TranslationSystem.Base;
using UnityEngine;

namespace IAP_System.Base.Products
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "IAP Coin Product", fileName = "New Coin IAP Product")]
    public class CoinIAP : InAppPurchaseProduct
    {
        /*public override string ProductName => Translate.GetTranslatedText(productName);
        public override string ProductDescription => Translate.GetTranslatedText(productDescription);*/
        public override string ProductQuantityText => "$ " + productQty;

        public override void AwardPlayer()
        {
            Debug.Log($"Add {productQty} to player coins");
        }
    }
}