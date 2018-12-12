using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SoundEvents : UnityEvent
{
}

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
    public Dialogue english_Dialogue;
    public bool hasSound;
    [SerializeField] private SoundEvents som;

	public void TriggerDialogue()
    {
        if (hasSound)
        {
            som.Invoke();
        }

        switch (FindObjectOfType<LanguageManager>().Language)
        {
            case 0: //portugues
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                break;
            case 1: //ingles
                FindObjectOfType<DialogueManager>().StartDialogue(english_Dialogue);
                break;
        }
        
    }
}
