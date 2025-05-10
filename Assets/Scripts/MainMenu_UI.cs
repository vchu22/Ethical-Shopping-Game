using UnityEngine;

public class MainMenu_UI : MonoBehaviour
{
    public void StartGame()
    {
        Helpers.NextScreen();
    }
    public void GotoOptionsPage()
    {
        Helpers.GoToScreen(4);
    }
    public void BackToMainMenu()
    {
        Helpers.GoToScreen(0);
    }
}
