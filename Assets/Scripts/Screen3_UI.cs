using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen3_UI : MonoBehaviour
{
    public Transform cartProductParent;
    public GameObject cartProductPrefab;
    public TextMeshProUGUI ethicInfoText;
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
        ethicInfoText.text = "";
        foreach (var item in GameState.shoppingCartItems)
        {
            foreach (string desc in item.positiveEthicCategories)
            {
                ethicInfoText.text += ("+ " + desc + "\n");
            }
            foreach (string desc in item.negativeEthicCategories)
            {
                ethicInfoText.text += ("- " + desc + "\n");
            }
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
