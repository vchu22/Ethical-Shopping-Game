using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Screen3_UI : MonoBehaviour
{
    public Transform cartProductParent;
    public GameObject cartProductPrefab;
    public TextMeshProUGUI ethicInfoText;
    public TextMeshProUGUI summeryInfoText;

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
        HashSet<string> ethicInfoList = new HashSet<string>();
        foreach (var item in GameState.shoppingCartItems)
        {
            foreach (string desc in item.positiveEthicCategories)
            {
                ethicInfoList.Add("+ " + desc);
                posCount++;
            }
            foreach (string desc in item.negativeEthicCategories)
            {
                ethicInfoList.Add("- " + desc);
                negCount++;
            }
        }
        foreach (string item in ethicInfoList) {
            ethicInfoText.text += (item + "\n");
        }
        
        if (negCount <= 1)
        {
            ethicInfoText.text = "<b>The product you picked has 1 or no problematic brands." +
                                 " The brands are associated with the following:</b>" + "\n" + ethicInfoText.text;
        }
        else
        {
            ethicInfoText.text = "<b>The products you picked contain more than 1 problematic brand." +
                                 "The brands are associated with the following:</b>" + "\n" + ethicInfoText.text;
        }
        var score = posCount - negCount;
        summeryInfoText.text = GetSummaryText(negCount,score);
        GameState.lastRoundEthicalProductCount = score;

    }

    public void NextRound()
    {
        GameState.currentRound++;
        if (GameState.currentRound < 3)
        {
            Helpers.GoToScreen(1);
        }
    }

    private string GetSummaryText(int negCount, int score)
    {
        if(GameState.currentRound < 1)
        {
            if (negCount <= 1)
            {
                return
                    "“Interesting - seems like you have a good eye for ethical products”, she said and made some notes. " +
                    "Most customers are distracted by the packaging or the famous brands, but I guess you did your research this time”.";
            }

            return
                "”Interesting - seems like you went with quite problematic products. " +
                "What made you pick these? Did you recognize a brand you like? Or was the price important for you?”";
        }

        if (GameState.lastRoundEthicalProductCount < score)
        {
            return "“Now your shopping cart is looking better”.  She checked something on her laptop. " +
                   "“I can see that the glasses were fetching the brands database and displaying the information, so everything worked well.”";
        }
        else
        {
            return "“Now your shopping cart has more or same amount of problematic products - interesting.” She checked something on her laptop, " +
                   "“I can see that the glasses were fetching the brands database and displaying the information, so it was technically working. " +
                   "Seems like you made your choices regardless of the extra information.”"; 
        }
    }
}
