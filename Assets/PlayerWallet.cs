using UnityEngine;
using System;

/// <summary>
/// Global wallet system that manages player money.
/// This is a singleton - only one instance should exist in the scene.
/// 
/// ATTACH THIS TO:
/// - An empty GameObject named "PlayerWallet" in your scene
/// - Or attach to the ShopUIManager GameObject
/// </summary>
public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;

    [Header("Starting Money")]
    [Tooltip("The amount of money the player starts with")]
    public float startingMoney = 100000f;

    // Current money amount (private, accessed through property)
    private float currentMoney;

    // Event that fires when money changes (for UI updates)
    public event Action<float> OnMoneyChanged;

    void Awake()
    {
        // Singleton pattern - ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            currentMoney = startingMoney;
            DontDestroyOnLoad(gameObject); // Optional: persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Notify UI of initial money amount
        OnMoneyChanged?.Invoke(currentMoney);
    }

    /// <summary>
    /// Get the current amount of money the player has
    /// </summary>
    public float GetMoney()
    {
        return currentMoney;
    }

    /// <summary>
    /// Check if the player has enough money for a purchase
    /// </summary>
    public bool CanAfford(float amount)
    {
        return currentMoney >= amount;
    }

    /// <summary>
    /// Try to spend money. Returns true if successful, false if insufficient funds.
    /// </summary>
    public bool SpendMoney(float amount)
    {
        if (CanAfford(amount))
        {
            currentMoney -= amount;
            OnMoneyChanged?.Invoke(currentMoney);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Add money to the wallet (for testing or rewards)
    /// </summary>
    public void AddMoney(float amount)
    {
        currentMoney += amount;
        OnMoneyChanged?.Invoke(currentMoney);
    }

    /// <summary>
    /// Reset wallet to starting amount (useful for testing)
    /// </summary>
    public void ResetWallet()
    {
        currentMoney = startingMoney;
        OnMoneyChanged?.Invoke(currentMoney);
    }
}

