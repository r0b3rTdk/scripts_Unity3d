using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;	

public class DialogueControl : MonoBehaviour
{
    // declaracao de componentes
    [Header("components")]
    public GameObject dialogueBox;
    public Text actorNameText;
    public Text speetchText;
    // declaracao de settings
    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;
    private Coroutine typingCoroutine;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            NextSentence();
        }
    }
    public void Speech(string[] txt, string actorName)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogueBox.SetActive(true);
        speetchText.text = "";
        actorNameText.text = actorName;
        sentences = txt;
        index = 0;
        typingCoroutine = StartCoroutine(TypeSentence());
    }
    IEnumerator TypeSentence()
    {
        speetchText.text = "";
        foreach (char letter in sentences[index].ToCharArray())
        {
            speetchText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null;
    }
    public void NextSentence()
    {
        if(speetchText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speetchText.text = "";
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeSentence());
            }
            else
            {
                EndDialogue();
            }
        }
    }
    public void HidePanel()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        speetchText.text = "";
        actorNameText.text = "";
        index = 0;
        dialogueBox.SetActive(false);
    }
    public void EndDialogue()
    {
        speetchText.text = "";
        index = 0;
        dialogueBox.SetActive(false);
    }
}
