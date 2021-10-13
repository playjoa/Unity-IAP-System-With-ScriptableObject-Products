using TranslationSystem.Base;
using UnityEngine;

namespace IAP_System.Base.Products
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "IAP Gem Product", fileName = "New Gem IAP Product")]
    public class GemIAP : InAppPurchaseProduct
    {
        /*public override string ProductName => Translate.GetTranslatedText(productName);
        public override string ProductDescription => Translate.GetTranslatedText(productDescription);*/
        public override string ProductQuantityText => "x " + productQty;

        public override void AwardPlayer()
        {
            Debug.Log($"Add {productQty} to player GEMS");
        }
    }
}