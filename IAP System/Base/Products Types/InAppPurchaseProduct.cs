using IAP_System.Base.Controller;
using UnityEngine;
using UnityEngine.Purchasing;

namespace IAP_System.Base.Products
{
    public abstract class InAppPurchaseProduct : ScriptableObject
    {
        [Tooltip("Your AppStore/GooglePlay IAP Key Here")]
        [SerializeField] protected string productKey = "myGame500Coins";
        [SerializeField] protected ProductType productType = ProductType.Consumable;
        [SerializeField] protected string productName = "500 Coins";
        [TextArea] public string productDescription = "Get 500 coins";
        [SerializeField] protected int productQty = 500;
        [SerializeField] protected Sprite productSprite;
        
        public virtual string ProductKey => productKey;
        public virtual ProductType ProductType => productType;
        public virtual string ProductName => productName;
        public virtual string ProductDescription => productDescription;
        public virtual int ProductQuantity => productQty;
        public virtual string ProductQuantityText => productQty.ToString();
        public virtual Sprite ProductSprite => productSprite;
        public virtual string ProductNicePrice => IAPController.ME.GetPrice(this);

        public virtual void BuyProduct() => IAPController.ME.BuyProductID(this);
        public abstract void AwardPlayer();
    }
}
