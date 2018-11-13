using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI text;
    public bool dialogueEnded;
    private Queue<string> argumentos;

    GameController gc;
    public Animator boxAnim;
    public Animator arielImage;
    public Animator clariceImage;

    CharMovement player;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        argumentos = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        player = gc.GetActiveCharGO();
        player.SetIsDialoguing(true);

        Time.timeScale = 0;

        dialogueEnded = false;
        DialogueButton.canPress = true;

        AnimateBox(1);

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
        player.SetIsDialoguing(false);
        Time.timeScale = 1;
        AnimateBox(0);
        dialogueEnded = true;
        DialogueButton.canPress = false;
    }

    void AnimateBox(int op)
    {
        
        if(op == 1)
        {
            
            AnimatorTriggerSet(1);
        }
        else
        {
   
            AnimatorTriggerSet(0);
        }
    }

    void AnimatorTriggerSet(int op)
    {
        if(op == 1)
        {
            boxAnim.SetBool("Open", true);
            if (gc.GetActiveChar() == 0)
            {
                arielImage.SetBool("Open", true);
            }
            else
            {
                if(gc.GetActiveChar() == 1)
                {
                    //ezekielImage.SetTrigger("Open");
                }
                else
                {
                    if (gc.GetActiveChar() == 2)
                    {
                        clariceImage.SetBool("Open", true);
                    }
                }
            }
        }
        else
        {
            boxAnim.SetBool("Open", false);
            if (gc.GetActiveChar() == 0)
            {
                arielImage.SetBool("Open", false);
            }
            else
            {
                if (gc.GetActiveChar() == 1)
                {
                    //ezekielImage.SetTrigger("Open");
                }
                else
                {
                    if (gc.GetActiveChar() == 2)
                    {
                        clariceImage.SetBool("Open", false);
                    }
                }
            }
        }

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
