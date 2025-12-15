using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Handles jewelry item behavior including grabbing, displaying info, and purchasing.
/// 
/// ATTACH THIS TO:
/// - Each jewelry GameObject/prefab that can be purchased
/// 
/// IN INSPECTOR, ASSIGN:
/// - Data: The JewelleryData ScriptableObject with item name and price
/// 
/// REQUIREMENTS:
/// - GameObject must have XRGrabInteractable component
/// </summary>
public class JewelleryItem : MonoBehaviour
{
    [Header("Item Data")]
    [Tooltip("The ScriptableObject containing item name and price")]
    public JewelleryData data;

    // Track if this item has been purchased
    private bool isPurchased = false;

    XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrabbed);
            grab.selectExited.AddListener(OnReleased);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (data != null && ShopUIManager.Instance != null)
        {
            // Pass this item reference so UI can handle purchase
            ShopUIManager.Instance.ShowItemInfo(
                this,
                data.itemName,
                data.price
            );
        }
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (ShopUIManager.Instance != null)
        {
            ShopUIManager.Instance.HideItemInfo();
        }
    }

    /// <summary>
    /// Check if this item has been purchased
    /// </summary>
    public bool IsPurchased()
    {
        return isPurchased;
    }

    /// <summary>
    /// Try to purchase this item. Returns true if successful, false if insufficient funds.
    /// </summary>
    public bool TryPurchase()
    {
        // Can't purchase if already purchased
        if (isPurchased)
        {
            return false;
        }

        // Check if data exists
        if (data == null)
        {
            Debug.LogWarning("JewelleryItem: No data assigned!");
            return false;
        }

        // Check if wallet exists
        if (PlayerWallet.Instance == null)
        {
            Debug.LogWarning("JewelleryItem: PlayerWallet not found!");
            return false;
        }

        // Try to spend money
        if (PlayerWallet.Instance.SpendMoney(data.price))
        {
            // Purchase successful!
            isPurchased = true;
            OnItemPurchased();
            return true;
        }
        else
        {
            // Insufficient funds
            return false;
        }
    }

    /// <summary>
    /// Called when item is successfully purchased.
    /// Override this method to add custom behavior (e.g., visual changes, sound effects).
    /// </summary>
    protected virtual void OnItemPurchased()
    {
        Debug.Log($"Item '{data.itemName}' purchased for ₹{data.price:N0}!");
        
        // Optional: You can add visual feedback here
        // For example: change material, add particle effect, disable collider, etc.
    }

    /// <summary>
    /// Reset purchase state (useful for testing or resetting shop)
    /// </summary>
    public void ResetPurchase()
    {
        isPurchased = false;
    }
}