# Unity-IAP-ScriptableObject-Products

<strong> - Obs: </strong> Tested with "com.unity.purchasing": "3.2.3"!

### Easily create custom products
- Make your custom type of product types!
- Easily create custom views for your products to be displayed in game!
- Full IAP Controller.

```C#
namespace IAP_System.Base.Products
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "IAP Custom Product", fileName = "New Custom IAP Product")]
    public class MyCustomIAPProduct : InAppPurchaseProduct
    {
        public override string ProductQuantityText => "$ " + productQty;

        public override void AwardPlayer()
        {
            Debug.Log($"Add {productQty} to my custom product type");
        }
    }
}
```

### Customize your IAP products 
```C#
namespace IAP_System.Base.Products
{
    public abstract class InAppPurchaseProduct : ScriptableObject
    {
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
```
