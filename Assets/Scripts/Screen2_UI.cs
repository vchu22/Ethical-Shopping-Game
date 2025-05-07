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
    public void Awake()
    {
        Debug.Log("Screen 2 Round " + GameState.currentRound);
        Debug.Log("Aisle Index " + currentAisleIdx);
        PopulateAislesUI();
    }
    public void PopulateAislesUI()
    {
        ChangeAisleName();
        for (int i = 0; i < GameState.itemCatelog[currentAisleIdx].items.Length; i++)
        {
            GameObject product = Instantiate(aisleProductsPrefab);
            product.transform.parent = aisleProductsGridLayout.gameObject.transform;
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
    }
    public void NextAisle()
    {
        if (GameState.getNumberOfAisles()-1 <= currentAisleIdx)
        {
            currentAisleIdx = 0;
        }
        else
        {
            currentAisleIdx++;
        }
        ChangeAisleName();
    }
    public void CheckOut()
    {
        Helpers.NextScreen();
    }
}
