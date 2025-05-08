using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Button continueButton;
    public float textSpeedInSeconds = 0.1f;
    public string[] linesRoundOne;
    public string[] linesRoundTwo;
    public string[] linesRoundThree;

    private int index;
    string[][] lines = new string[3][];

    void Start()
    {
        textComponent.text = string.Empty;
        continueButton.onClick.AddListener(NextLine);
        lines[0] = linesRoundOne;
        lines[1] = linesRoundTwo;
        lines[2] = linesRoundThree;

        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        Debug.Log("Round: " + GameState.currentRound + ", Line " + index + " of " + lines[GameState.currentRound].Length);
        foreach (char c in lines[GameState.currentRound][index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeedInSeconds);
        }
    }

    public void NextLine()
    {
        StopAllCoroutines();
        if (index < lines[GameState.currentRound].Length - 1)
        {
            index++;
            textComponent.text = string.Empty; // Clear old text before typing the next line
            StartCoroutine(TypeLine());
        }
        else
        {
            if (GameState.currentRound < 2)
            {
                // transition to Screen 2
                Helpers.NextScreen();
            }
            else
            {
                GameState.currentRound = 1;
                Helpers.GoToScreen(0);
            }
        }
    }
}
