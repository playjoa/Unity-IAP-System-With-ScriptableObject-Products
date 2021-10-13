# Unity-IAP-ScriptableObject-Products

## Easily create custom products

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
