using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public Image background;
    public Sprite[] backgroundImages;
    public Image aisleShelf;
    public Sprite[] aisleShelfImages;
    public RectTransform aisleNameRectTransform;

    // Saves the assigned aisle items to their slots so they can be restored when the aisle is changed
    // int is the aisle index
    private Dictionary<int, AssignedAisleItem[]> assignedAisleItems = new();

    public void Awake()
    {
        aisleSlots = aisleSlotContainer.GetComponentsInChildren<AisleSlot>();
        AssignSlots();
        PopulateAislesUI();
        GameState.shoppingCartItems.Clear();
        checkoutButton.gameObject.SetActive(false);
    }

    private void AssignSlots()
    {
        var aisles = GameState.itemCatelog;
        var aisleIndex = 0;
        foreach (var aisle in aisles)
        {
            AssignedAisleItem[] assignedItems = new AssignedAisleItem[aisle.items.Length];
            int slotIndex = 0;
            var availableSlots = new List<int>() {0, 1, 2, 3};
            
            for (int i = 0; i < aisle.items.Length; i++)
            {
                if (i == 2)
                {
                    availableSlots = new List<int>() {4, 5, 6, 7};
                }
                int randomIndex = Random.Range(0, availableSlots.Count);
                Debug.Log("Random index: " + randomIndex);
                Debug.Log("Available slots: " + string.Join(", ", availableSlots));
                slotIndex = availableSlots[randomIndex];
                availableSlots.Remove(slotIndex);

                
                
                assignedItems[i] = new AssignedAisleItem
                {
                    aisleItem = aisle.items[i],
                    slot = aisleSlots[slotIndex] // we could randomize this
                };
                slotIndex++;
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
        ChangeAisleBackground();

        var aisle = assignedAisleItems[currentAisleIdx];
        foreach (var assigned in aisle)
        {
            if (assigned.aisleItem != null)
            {
                var aisleSlot = assigned.slot;
                var aisleProductObject = Instantiate(aisleProductsPrefab, aisleSlot.transform);
                var aisleProduct = aisleProductObject.GetComponent<AisleProduct>();
                aisleProduct.SetAisleItem(assigned.aisleItem);
                aisleProduct.infoClicked += OnAisleInfoClicked;
                aisleProduct.clicked += OnProductClicked;

                aisleSlot.SetSlotItem(assigned.aisleItem, aisleProduct);
                

            }
        }
        UpdateItemHighlights();
        
        Debug.Log("Shopping card contents: " +  string.Join( ", ", GameState.shoppingCartItems.Select(item => item.name)));
    }

    private void ChangeAisleBackground()
    {
        background.sprite = backgroundImages[currentAisleIdx];
        aisleShelf.sprite = aisleShelfImages[currentAisleIdx];
        if (currentAisleIdx == 1) 
        {
            aisleNameRectTransform.Translate(new Vector2(455,-10));
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

        var cardItem = GameState.shoppingCartItems.FirstOrDefault(item => item.aisle.name == aisleProduct.aisleItem.aisle.name);

        // Check if the item is already in the cart and remove it if it is
        if (cardItem != null && cardItem.name == aisleProduct.aisleItem.name)
        {
            GameState.shoppingCartItems.Remove(cardItem);
            shoppingListUI.RemoveCrossedOffItem(aisleProduct.aisleItem.aisle.name);
            UpdateItemHighlights();
            checkoutButton.gameObject.SetActive(false);
            return;
        }
        
        shoppingListUI.AddCrossedOffItem(aisleProduct.aisleItem.aisle.name);

        GameState.shoppingCartItems.Remove(cardItem);
        GameState.shoppingCartItems.Add(aisleProduct.aisleItem);
        UpdateItemHighlights();
        
        Debug.Log("Shopping card contents: " +  string.Join( ", ", GameState.shoppingCartItems.Select(item => item.name)));
        
        checkoutButton.gameObject.SetActive(GameState.shoppingCartItems.Count == GameState.itemCatelog.Length);


    }

    public void UpdateItemHighlights()
    {
        foreach(var slot in aisleSlots)
        {
            if (slot.aisleItem != null)
            {
                var aisleProduct = slot.aisleProduct;
                
                if (GameState.shoppingCartItems.Contains(aisleProduct.aisleItem))
                {
                    aisleProduct.Highlight();
                    Debug.Log("Highlighting: " + aisleProduct.aisleItem.name);
                }
                else
                {
                    aisleProduct.RemoveHighlight();
                }
            }
        }
    }
}

public class AssignedAisleItem
{
    public AisleSlot slot;
    public AisleItem aisleItem;
}