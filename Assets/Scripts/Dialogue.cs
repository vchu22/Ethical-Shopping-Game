using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeedInSeconds = 0.1f;
    public string[] lines;

    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            textComponent.text += string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // transition to Screen 2
        }
    }
}
