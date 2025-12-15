using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages all shop UI elements including wallet display, item info, and purchase button.
/// 
/// ATTACH THIS TO:
/// - An empty GameObject named "ShopUIManager" in your scene
/// 
/// IN INSPECTOR, ASSIGN:
/// - Wallet Text: The TextMeshProUGUI that shows player money (always visible)
/// - Item Info Panel: The panel that shows when item is grabbed
/// - Item Name Text: TextMeshProUGUI for item name
/// - Price Text: TextMeshProUGUI for item price
/// - Purchase Button: The Button component for purchasing
/// - Error Message Text: TextMeshProUGUI for "insufficient funds" message
/// </summary>
public class ShopUIManager : MonoBehaviour
{
    public static ShopUIManager Instance;

    [Header("Wallet UI (Always Visible)")]
    [Tooltip("Text that shows current wallet balance - should always be visible")]
    public TextMeshProUGUI walletText;

    [Header("Item Info UI")]
    [Tooltip("Panel that appears when item is grabbed")]
    public GameObject itemInfoPanel;
    [Tooltip("Text showing the item name")]
    public TextMeshProUGUI itemNameText;
    [Tooltip("Text showing the item price")]
    public TextMeshProUGUI priceText;
    [Tooltip("Button to purchase the item")]
    public Button purchaseButton;

    [Header("Error Message")]
    [Tooltip("Text that shows error messages (e.g., insufficient funds)")]
    public TextMeshProUGUI errorMessageText;

    // Track the current item being viewed (for purchase)
    private JewelleryItem currentItem;

    void Awake()
    {
        Instance = this;
        HideItemInfo();
        HideErrorMessage();
    }

    void Start()
    {
        // Subscribe to wallet changes to update UI
        if (PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.OnMoneyChanged += UpdateWalletDisplay;
            UpdateWalletDisplay(PlayerWallet.Instance.GetMoney());
        }

        // Set up purchase button click handler
        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.OnMoneyChanged -= UpdateWalletDisplay;
        }
    }

    /// <summary>
    /// Update the wallet display text
    /// </summary>
    void UpdateWalletDisplay(float newAmount)
    {
        if (walletText != null)
        {
            walletText.text = "₹ " + newAmount.ToString("N0");
        }
    }

    /// <summary>
    /// Show item info when player grabs an item
    /// </summary>
    public void ShowItemInfo(JewelleryItem item, string itemName, float price)
    {
        currentItem = item;

        if (itemNameText != null)
        {
            itemNameText.text = itemName;
        }

        if (priceText != null)
        {
            priceText.text = "₹ " + price.ToString("N0");
        }

        // Show purchase button only if item is not already purchased
        if (purchaseButton != null)
        {
            purchaseButton.gameObject.SetActive(!item.IsPurchased());
            purchaseButton.interactable = !item.IsPurchased();
        }

        if (itemInfoPanel != null)
        {
            itemInfoPanel.SetActive(true);
        }

        HideErrorMessage();
    }

    /// <summary>
    /// Hide item info panel when item is released
    /// </summary>
    public void HideItemInfo()
    {
        currentItem = null;

        if (itemInfoPanel != null)
        {
            itemInfoPanel.SetActive(false);
        }

        HideErrorMessage();
    }

    /// <summary>
    /// Called when purchase button is clicked
    /// This is automatically connected in Start(), but can also be connected manually via Inspector
    /// </summary>
    public void OnPurchaseButtonClicked()
    {
        if (currentItem == null || currentItem.data == null)
        {
            Debug.LogWarning("No item selected for purchase!");
            return;
        }

        // Try to purchase the item
        if (currentItem.TryPurchase())
        {
            // Purchase successful - hide purchase button
            if (purchaseButton != null)
            {
                purchaseButton.gameObject.SetActive(false);
            }
            HideErrorMessage();
        }
        else
        {
            // Purchase failed - show error message
            ShowErrorMessage("You don't have enough money");
        }
    }

    /// <summary>
    /// Show error message
    /// </summary>
    void ShowErrorMessage(string message)
    {
        if (errorMessageText != null)
        {
            errorMessageText.text = message;
            errorMessageText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Hide error message
    /// </summary>
    void HideErrorMessage()
    {
        if (errorMessageText != null)
        {
            errorMessageText.gameObject.SetActive(false);
        }
    }
}