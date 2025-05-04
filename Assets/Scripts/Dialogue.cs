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

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        continueButton.onClick.AddListener(NextLine);
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (GameState.currentRound == 1)
        {
            foreach (char c in linesRoundOne[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeedInSeconds);
            }
        }
        else if (GameState.currentRound == 2)
        {
            foreach (char c in linesRoundTwo[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeedInSeconds);
            }
        }
    }

    public void NextLine()
    {
        StopAllCoroutines();

        if (index < linesRoundOne.Length - 1)
        {
            index++;
            textComponent.text = string.Empty; // Clear old text before typing the next line
            StartCoroutine(TypeLine());
        }
        else
        {
            // transition to Screen 2
            Helpers.NextScreen();
        }
    }
}
