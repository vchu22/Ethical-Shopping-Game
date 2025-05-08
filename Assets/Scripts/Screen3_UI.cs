using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen3_UI : MonoBehaviour
{
    public Button nextRoundButton;
    
    public Transform cartProductParent;
    public GameObject cartProductPrefab;
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
    }

    public void NextRound()
    {
        GameState.currentRound++;
        if (GameState.currentRound <= 3)
        {
            Helpers.GoToScreen(1);
        }
    }
}
