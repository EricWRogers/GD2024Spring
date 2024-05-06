using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueBox : MonoBehaviour
{
    public GameObject trigger;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogue;
    public int index;
    public GameObject nextTrigger;
    public float wordSpeed;
    public GameObject contButton;

 // this does not work the way I hoped it would, but I have found the jankiest way to make it work. it's so ugly its almost pretty
 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            zeroText();
            index = 0;
            Destroy(trigger);
            nextTrigger.SetActive(true);

        }

    }
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
            contButton.SetActive(false);
        }
        contButton.SetActive(true);
    }
    public void NextLine()
    {
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
}
