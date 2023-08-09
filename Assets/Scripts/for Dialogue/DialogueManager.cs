using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text DialougeText;
    public Image Icon;
    public Animator animator;
    public DezScript Dez;



    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();

        // Lock the cursor in place and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Dez.StopAnimation();

        Dez.DisableMovement();

        Dez.DisableAnimation();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        animator.SetBool("IsOpen", true);

        Icon.sprite = dialogue.Icon;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string phrase = sentences.Dequeue();
        //DialougeText.text = phrase;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(phrase));

    }

    IEnumerator TypeSentence (string sentence)
    {
        DialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialougeText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }



    void EndDialogue()
    {
        Debug.Log("End of Conversation");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator.SetBool("IsOpen", false);

        Dez.EnableAnimation();

        Dez.EnableMovement();
    }





}
