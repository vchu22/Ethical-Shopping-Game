using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShoppingListUI : MonoBehaviour
{
    public TextMeshProUGUI shoppingListText;

    public string headerText = "Shopping List:";

    private HashSet<string> crossedOffItems = new HashSet<string>();

    public void Start()
    {
        UpdateShoppingListText();
    }

    public void AddCrossedOffItem(string itemName)
    {
        Debug.Log("Adding crossed off item: " + itemName);
        if (!crossedOffItems.Contains(itemName))
        {
            crossedOffItems.Add(itemName);
            UpdateShoppingListText();
        }
    }

    public void RemoveCrossedOffItem(string itemName)
    {
        Debug.Log("Removing crossed off item: " + itemName);
        if (crossedOffItems.Contains(itemName))
        {
            crossedOffItems.Remove(itemName);
            UpdateShoppingListText();
        }
    }

    void UpdateShoppingListText()
    {
        var items = GameState.itemCatelog
            .Select(item => $" - {(crossedOffItems.Contains(item.name) ? $"<s>{item.name}</s>" : item.name)}");
        shoppingListText.text = $"{headerText}\n{string.Join("\n", items)}";
    }


}
