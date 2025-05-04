using UnityEngine;
using UnityEngine.UI;

public class Screen3_UI : MonoBehaviour
{
    public Button nextRoundButton;
    public void Awake()
    {
        Debug.Log("Screen 3 Round " + GameState.currentRound);
    }

    public void NextRound()
    {
        GameState.currentRound++;
        if (GameState.currentRound < 3)
        {
            Helpers.GoToScreen(0);
        }
        else
        {
            Helpers.PrevScreen();
        }
    }
}
