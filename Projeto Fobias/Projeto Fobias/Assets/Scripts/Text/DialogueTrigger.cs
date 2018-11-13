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
    public bool hasSound;
    [SerializeField] private SoundEvents som;

	public void TriggerDialogue()
    {
        if (hasSound)
        {
            som.Invoke();
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
