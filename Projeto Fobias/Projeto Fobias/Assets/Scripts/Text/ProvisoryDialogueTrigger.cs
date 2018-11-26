using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvisoryDialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<ProvisoryDialogueManager>().StartDialogue(dialogue);
    }
}
