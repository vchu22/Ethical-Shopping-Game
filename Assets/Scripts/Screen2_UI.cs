using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen2_UI : MonoBehaviour
{
    public Button checkoutButton;

    public TextMeshProUGUI aisleNameText;
    public GridLayoutGroup aisleProductsUI;

    private static int currentAisleIdx;
    public void Awake()
    {
        Debug.Log("Screen 2 Round " + GameState.currentRound);
        Debug.Log("Aisle Index " + currentAisleIdx);
    }
    public void PopulateAislesUI()
    {
        
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
        Debug.Log("Aisle Index " + currentAisleIdx);

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
        Debug.Log("Aisle Index " + currentAisleIdx);
    }
    public void CheckOut()
    {
        Helpers.NextScreen();
    }
}
