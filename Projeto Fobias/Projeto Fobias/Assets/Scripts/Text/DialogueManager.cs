using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI text;
    public bool dialogueEnded;
    private Queue<string> argumentos;

    private void Start()
    {
        argumentos = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueEnded = false;

        argumentos.Clear();

        foreach(string argumento in dialogue.argumentos)
        {
            argumentos.Enqueue(argumento);
        }

        DisplayNextSequence();
    }


    public void DisplayNextSequence()
    {
        if(argumentos.Count == 0)
        {
            EndDialogue();
            return;
        }

        string argumento = argumentos.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSequence(argumento));
    }

    void EndDialogue()
    {
        dialogueEnded = true;
    }

    IEnumerator TypeSequence(string argumento)
    {
        text.text = "";
        foreach(char letra in argumento.ToCharArray())
        {
            text.text += letra;
            yield return null;
        }
    }
}
