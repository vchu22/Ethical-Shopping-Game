using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Screen3_UI : MonoBehaviour
{
    public Transform cartProductParent;
    public GameObject cartProductPrefab;
    public TextMeshProUGUI ethicInfoText;
    public TextMeshProUGUI summeryInfoText;

    public string summeryTextPositiveMore;
    public string summeryTextNegativeMore;
    public string summeryTextNeutral;

    public void Awake()
    {
        Debug.Log("Screen 3 Round " + GameState.currentRound);
    }

    public void Start()
    {
        foreach(var item in GameState.shoppingCartItems)
        {
            GameObject cartProduct = Instantiate(cartProductPrefab, cartProductParent);
            cartProduct.GetComponent<CartProduct>().SetItem(item);
        }

        // Display ethic info
        int posCount = 0, negCount = 0;
        ethicInfoText.text = "";
        foreach (var item in GameState.shoppingCartItems)
        {
            foreach (string desc in item.positiveEthicCategories)
            {
                ethicInfoText.text += ("+ " + desc + "\n");
                posCount++;
            }
            foreach (string desc in item.negativeEthicCategories)
            {
                ethicInfoText.text += ("- " + desc + "\n");
                negCount++;
            }
        }

        // Show summery text based on how many positive and negative categories
        if (posCount == negCount)
        {
            summeryInfoText.text = summeryTextNeutral;
        }
        else if (posCount > negCount)
        {
            summeryInfoText.text = summeryTextPositiveMore;
        }
        else 
        {
            summeryInfoText.text = summeryTextNegativeMore;
        }
    }

    public void NextRound()
    {
        GameState.currentRound++;
        if (GameState.currentRound < 3)
        {
            Helpers.GoToScreen(1);
        }
    }
}
