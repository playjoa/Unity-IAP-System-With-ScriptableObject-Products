using System;
using System.Collections.Generic;
using IAP_System.Base.Products;
using UnityEngine;
using UnityEngine.Purchasing;

namespace IAP_System.Base.Controller
{
    public class IAPController : MonoBehaviour, IStoreListener
    {   
        [SerializeField] private List<InAppPurchaseProduct> productsToInitialize;
        
        public static IAPController ME { get; private set; }
        public static Action<InAppPurchaseProduct> OnBuyProduct;

        public List<InAppPurchaseProduct> ProductsInDisplay { set; private get; }
        public bool InAppleDevice => Application.platform == RuntimePlatform.IPhonePlayer ||
                                     Application.platform == RuntimePlatform.OSXPlayer;

        private static IStoreController _mStoreController;
        private static IExtensionProvider _mStoreExtensionProvider;
        private static bool IsInitialized => _mStoreController != null && _mStoreExtensionProvider != null;
        private static Dictionary<string, InAppPurchaseProduct> _availableProducts;
        
        private void Awake()
        {
            if (!ME)
            {
                ME = this;
                InitializeProducts();
                DontDestroyOnLoad(gameObject);
                return;
            }
    
            DestroyImmediate(gameObject);
        }

        private void Start()
        {
            if (_mStoreController == null)
                InitializePurchasing();
        }

        private void InitializeProducts()
        {
            _availableProducts = new Dictionary<string, InAppPurchaseProduct>();
            ProductsInDisplay = new List<InAppPurchaseProduct>();
            
            foreach (var product in productsToInitialize)
            {
                if (_availableProducts.ContainsKey(product.ProductKey))
                {
                    Debug.LogError($"Trying to Initialize Product with duplicated key {product}");
                    continue;
                }
                
                ProductsInDisplay.Add(product);
                _availableProducts.Add(product.ProductKey, product);
            }
            Debug.Log($"Initialized Products {ProductsInDisplay}");
        }

        private void InitializePurchasing()
        {
            if (IsInitialized)
                return;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var currentProduct in _availableProducts)
                builder.AddProduct(currentProduct.Key, currentProduct.Value.ProductType);
            
            UnityPurchasing.Initialize(this, builder);
        }

        public InAppPurchaseProduct GetProduct(string productKey)
        {
            return _availableProducts.TryGetValue(productKey, out var productBought) ? productBought : null;
        }

        public string GetPrice(InAppPurchaseProduct product)
        {
            if (_mStoreController == null)
                InitializePurchasing();

            return _mStoreController != null ? _mStoreController.products.WithID(product.ProductKey).metadata.localizedPriceString : "$ 0.00";
        }

        public void RestorePurchases()
        {
            if (!IsInitialized)
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (InAppleDevice)
            {
                Debug.Log("RestorePurchases started ...");

                var apple = _mStoreExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result) =>
                {
                    Debug.Log($"RestorePurchases continuing: {result}. If no further messages, no purchases available to restore.");
                });
            }
            else
                Debug.Log($"RestorePurchases FAIL. Not supported on this platform. Current = {Application.platform}");
        }

        public void BuyProductID(InAppPurchaseProduct productToBuy)
        {
            if (IsInitialized)
            {
                var product = _mStoreController.products.WithID(productToBuy.ProductKey);

                if (product is { availableToPurchase: true })
                {
                    Debug.Log($"Purchasing product asynchronously: '{product.definition.id}'");
                    _mStoreController.InitiatePurchase(product);
                }
                else
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
            else
                Debug.Log("BuyProductID FAIL. Not initialized.");
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");
        
            _mStoreController = controller;
            _mStoreExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            var productBoughtId = args.purchasedProduct.definition.id;

            if (_availableProducts.TryGetValue(productBoughtId, out var productBought))
            {
                productBought.AwardPlayer();
                OnBuyProduct?.Invoke(productBought);
            }
            else
                Debug.Log($"ProcessPurchase: FAIL. Unrecognized product: '{args.purchasedProduct.definition.id}'");

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}");
        }
    }
}
