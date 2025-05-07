using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Screen2_UI : MonoBehaviour
{
    public Button checkoutButton;

    public TextMeshProUGUI aisleNameText;
    public GridLayoutGroup aisleProductsGridLayout;
    public GameObject aisleProductsPrefab;

    public ProductInfoUI productInfoUI;

    private int currentAisleIdx;

    public GameObject aisleSlotContainer;

    public ShoppingListUI shoppingListUI;

    private AisleSlot[] aisleSlots;

    // Saves the assigned aisle items to their slots so they can be restored when the aisle is changed
    // int is the aisle index
    private Dictionary<int, AssignedAisleItem[]> assignedAisleItems = new Dictionary<int, AssignedAisleItem[]>();

    private HashSet<int> checkedAisles = new HashSet<int>();

    public void Awake()
    {
        Debug.Log("Screen 2 Round " + GameState.currentRound);
        Debug.Log("Aisle Index " + currentAisleIdx);
        aisleSlots = aisleSlotContainer.GetComponentsInChildren<AisleSlot>();
        AssignSlots();
        PopulateAislesUI();
        GameState.shoppingCartItems.Clear();
    }

    private void AssignSlots()
    {
        var aisles = GameState.itemCatelog;
        var aisleIndex = 0;
        foreach (var aisle in aisles)
        {
            AssignedAisleItem[] assignedItems = new AssignedAisleItem[aisle.items.Length];
            for (int i = 0; i < aisle.items.Length; i++)
            {
                assignedItems[i] = new AssignedAisleItem
                {
                    aisleItem = aisle.items[i],
                    slot = aisleSlots[i >= aisleSlots.Length ? aisleSlots.Length - 1 : i] // we could randomize this
                };
            }
            assignedAisleItems.Add(aisleIndex, assignedItems);
            aisleIndex++;
        }
    }

    private void CleanUpAisleSlots()
    {
        foreach (var slot in aisleSlots)
        {
            if (slot.aisleItem != null)
            {
                slot.DeleteSlotItem();
            }
        }
    }

    public void PopulateAislesUI()
    {
        CleanUpAisleSlots();
        ChangeAisleName();

        var aisle = assignedAisleItems[currentAisleIdx];
        foreach (var assigned in aisle)
        {
            if (assigned.aisleItem != null)
            {
                var aisleSlot = assigned.slot;
                var aisleProductObject = Instantiate(aisleProductsPrefab, aisleSlot.transform);
                var aisleProduct = aisleProductObject.GetComponent<AisleProduct>();
                aisleProduct.aisleItem = assigned.aisleItem;
                aisleProduct.infoClicked += OnAisleInfoClicked;
                aisleProduct.clicked += OnProductClicked;

                aisleSlot.SetSlotItem(assigned.aisleItem, aisleProduct);

            }
        }
    }
    private void ChangeAisleName()
    {
        aisleNameText.text = GameState.itemCatelog[currentAisleIdx].name;
    }
    public void PrevAisle()
    {
        if (currentAisleIdx <= 0)
        {
            currentAisleIdx = GameState.getNumberOfAisles() - 1;
        }
        else
        {
            currentAisleIdx--;
        }
        ChangeAisleName();
        PopulateAislesUI();
    }
    public void NextAisle()
    {
        if (GameState.getNumberOfAisles() - 1 <= currentAisleIdx)
        {
            currentAisleIdx = 0;
        }
        else
        {
            currentAisleIdx++;
        }
        ChangeAisleName();
        PopulateAislesUI();
    }
    public void CheckOut()
    {
        Helpers.NextScreen();
    }

    private void OnAisleInfoClicked(AisleProduct aisleProduct)
    {
        productInfoUI.ShowProductInfo(aisleProduct);
        Debug.Log("Product Info Clicked: " + aisleProduct.aisleItem.name);
    }
    public void OnProductClicked(AisleProduct aisleProduct)
    {
        Debug.Log("Product Clicked: " + aisleProduct.aisleItem.name);
        var cardItem = GameState.shoppingCartItems.Where(item => item.name == aisleProduct.aisleItem.name).FirstOrDefault();

        if (cardItem != null)
        {
            GameState.shoppingCartItems.Remove(cardItem);
            checkedAisles.Remove(currentAisleIdx);
            shoppingListUI.RemoveCrossedOffItem(GameState.itemCatelog[currentAisleIdx].name);
        }
        else
        {
            GameState.shoppingCartItems.Add(aisleProduct.aisleItem);
            checkedAisles.Add(currentAisleIdx);
            shoppingListUI.AddCrossedOffItem(GameState.itemCatelog[currentAisleIdx].name);
        }

    }
}

public class AssignedAisleItem
{
    public AisleSlot slot;
    public AisleItem aisleItem;
}