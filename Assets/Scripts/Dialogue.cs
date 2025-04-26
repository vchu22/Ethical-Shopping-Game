using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeedInSeconds = 0.1f;
    public string[] lines;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeedInSeconds);
        }
    }

    public void NextLine()
    {
        StopAllCoroutines();

        if (index < lines.Length - 1)
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
