# VR Jewelry Store - Wallet & Purchase System Setup Guide

## 📋 Overview

This guide will help you set up the wallet and purchase system in your VR jewelry store. The system includes:
- **PlayerWallet**: Global wallet that tracks player money
- **ShopUIManager**: Manages all UI elements (wallet display, item info, purchase button)
- **JewelleryItem**: Handles item grabbing and purchase logic

---

## 🎯 Step-by-Step Setup

### **STEP 1: Create PlayerWallet GameObject**

1. In Hierarchy, right-click → **Create Empty**
2. Name it: **"PlayerWallet"**
3. Add Component → **PlayerWallet** script
4. In Inspector:
   - **Starting Money**: Set to `100000` (or your desired starting amount)

**Note**: This is a singleton - only one should exist in the scene.

---

### **STEP 2: Set Up UI Canvas**

1. In Hierarchy, right-click → **UI → Canvas**
2. Name it: **"ShopCanvas"**
3. Set Canvas **Render Mode** to:
   - **Screen Space - Overlay** (recommended for VR)
   - OR **Screen Space - Camera** (if you prefer)

---

### **STEP 3: Create Wallet Display (Always Visible)**

1. Right-click on **ShopCanvas** → **UI → Text - TextMeshPro**
2. Name it: **"WalletText"**
3. Position it where you want (top-left, top-right, etc.)
4. In Inspector:
   - Set **Text** to: `₹ 100,000` (placeholder)
   - Adjust **Font Size** (e.g., 36)
   - Set **Alignment** to center
   - Style as desired

**This text will always be visible and update automatically.**

---

### **STEP 4: Create Item Info Panel**

1. Right-click on **ShopCanvas** → **UI → Panel**
2. Name it: **"ItemInfoPanel"**
3. Position it bottom-center or side (comfortable for VR)
4. **Uncheck** the panel in Inspector (disabled by default)

---

### **STEP 5: Add Text Elements to ItemInfoPanel**

#### **Item Name Text:**
1. Right-click on **ItemInfoPanel** → **UI → Text - TextMeshPro**
2. Name it: **"ItemNameText"**
3. Set **Text** to: `Item Name` (placeholder)
4. Adjust font size and styling

#### **Price Text:**
1. Right-click on **ItemInfoPanel** → **UI → Text - TextMeshPro**
2. Name it: **"PriceText"**
3. Set **Text** to: `₹ 0` (placeholder)
4. Adjust font size and styling

---

### **STEP 6: Add Purchase Button**

1. Right-click on **ItemInfoPanel** → **UI → Button - TextMeshPro**
2. Name it: **"PurchaseButton"**
3. Select the **Text** child object
4. Set button text to: **"Purchase"**
5. Style the button as desired

**The button will automatically show/hide when items are grabbed.**

---

### **STEP 7: Add Error Message Text**

1. Right-click on **ShopCanvas** → **UI → Text - TextMeshPro**
2. Name it: **"ErrorMessageText"**
3. Position it near the ItemInfoPanel
4. Set **Text** to: `You don't have enough money`
5. Set **Color** to red (or warning color)
6. **Uncheck** the GameObject (disabled by default)

---

### **STEP 8: Set Up ShopUIManager**

1. In Hierarchy, right-click → **Create Empty**
2. Name it: **"ShopUIManager"**
3. Add Component → **ShopUIManager** script
4. In Inspector, drag references:

   **Wallet UI:**
   - **Wallet Text** → Drag `WalletText` TextMeshProUGUI

   **Item Info UI:**
   - **Item Info Panel** → Drag `ItemInfoPanel` GameObject
   - **Item Name Text** → Drag `ItemNameText` TextMeshProUGUI
   - **Price Text** → Drag `PriceText` TextMeshProUGUI
   - **Purchase Button** → Drag `PurchaseButton` Button component

   **Error Message:**
   - **Error Message Text** → Drag `ErrorMessageText` TextMeshProUGUI

---

### **STEP 9: Set Up Jewelry Items**

For each jewelry GameObject/prefab:

1. Select the jewelry GameObject
2. Add Component → **JewelleryItem** script
3. In Inspector:
   - **Data** → Assign the correct `JewelleryData` ScriptableObject
4. Ensure the GameObject has:
   - **XRGrabInteractable** component (required for VR grabbing)

---

### **STEP 10: Create JewelleryData Assets**

1. Right-click in Project window → **Create → Jewellery → Jewellery Data**
2. Name each asset (e.g., `RingGold`, `NecklaceSilver`)
3. For each asset, set:
   - **Item Name**: e.g., "Gold Ring"
   - **Price**: e.g., `50000`
4. Assign these assets to your jewelry prefabs in the **JewelleryItem** component

---

## ✅ Testing Checklist

1. **Press Play**
2. **Check Wallet Display**: Should show starting money (₹ 100,000)
3. **Grab a jewelry item**:
   - ✅ Item name and price appear
   - ✅ Purchase button appears
4. **Click Purchase Button**:
   - ✅ If you have enough money: Money deducted, wallet updates, button disappears
   - ✅ If not enough money: Error message appears
5. **Release item**: UI hides
6. **Grab purchased item again**: Purchase button should NOT appear

---

## 🎨 UI Hierarchy Example

```
ShopCanvas
├── WalletText (always visible)
├── ItemInfoPanel (shows when item grabbed)
│   ├── ItemNameText
│   ├── PriceText
│   └── PurchaseButton
│       └── Text (child)
└── ErrorMessageText (shows on error)
```

---

## 🔧 Troubleshooting

### **Wallet doesn't update:**
- Check that `PlayerWallet` GameObject exists in scene
- Check that `ShopUIManager` has `WalletText` assigned

### **Purchase button doesn't appear:**
- Check that `ShopUIManager` has `PurchaseButton` assigned
- Check that `JewelleryItem` has `Data` assigned

### **Purchase doesn't work:**
- Check Console for error messages
- Verify `PlayerWallet` exists and has enough starting money
- Check that `JewelleryData` has a valid price set

### **Error message doesn't show:**
- Check that `ErrorMessageText` is assigned in `ShopUIManager`
- Verify the text GameObject is a child of Canvas

---

## 📝 Code Files Created

1. **PlayerWallet.cs** - Singleton wallet manager
2. **ShopUIManager.cs** (updated) - UI controller
3. **JewelleryItem.cs** (updated) - Item purchase logic
4. **JewelleryData.cs** (already existed) - Item data ScriptableObject

---

## 🚀 Next Steps (Optional Enhancements)

- Add visual feedback when item is purchased (particle effects, color change)
- Add sound effects for purchase success/failure
- Add purchase history/log
- Add "Sell" functionality
- Add discount system
- Add multiple currencies

---

## 💡 Tips

- **Starting Money**: Adjust in `PlayerWallet` Inspector
- **UI Positioning**: Position UI elements where they're comfortable in VR (not too close to edges)
- **Button Size**: Make purchase button large enough for VR controllers
- **Testing**: Use different starting money amounts to test insufficient funds scenario

---

**Setup Complete!** 🎉

Your VR jewelry store now has a complete wallet and purchase system!

