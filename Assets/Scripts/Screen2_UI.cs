using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen2_UI : MonoBehaviour
{
    public Button checkoutButton;

    public TextMeshProUGUI aisleNameText;
    public GridLayoutGroup aisleProductsGridLayout;
    public GameObject aisleProductsPrefab;

    private static int currentAisleIdx;

    public GameObject aisleSlotContainer;

    private AisleSlot[] aisleSlots;

    // Saves the assigned aisle items to their slots so they can be restored when the aisle is changed
    private Dictionary<int, AssignedAisleItem[]> assignedAisleItems = new Dictionary<int, AssignedAisleItem[]>();

    public void Awake()
    {
        Debug.Log("Screen 2 Round " + GameState.currentRound);
        Debug.Log("Aisle Index " + currentAisleIdx);
        aisleSlots = aisleSlotContainer.GetComponentsInChildren<AisleSlot>();
        AssignSlots();
        PopulateAislesUI();
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
                slot.DeleteAisleItem();
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
                var aisleItemObject = Instantiate(aisleProductsPrefab, aisleSlot.transform);
                aisleSlot.SetAisleItem(assigned.aisleItem, aisleItemObject);
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
}

public class AssignedAisleItem
{
    public AisleSlot slot;
    public AisleItem aisleItem;
}
