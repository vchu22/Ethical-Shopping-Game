using UnityEngine;
using UnityEngine.UI;

public class Screen2_UI : MonoBehaviour
{
    public Button checkoutButton;
    public void Awake()
    {
        Debug.Log("Screen 2 Round " + GameState.currentRound);
    }

    public void CheckOut()
    {
        Helpers.NextScreen();
    }
}
