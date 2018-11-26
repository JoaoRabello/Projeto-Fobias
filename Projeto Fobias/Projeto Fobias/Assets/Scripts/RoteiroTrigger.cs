using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteiroTrigger : MonoBehaviour {

    ProvisoryDialogueTrigger dlgTrigger;

    private void Awake()
    {
        dlgTrigger = GetComponent<ProvisoryDialogueTrigger>();
    }

    private void Start()
    {
        dlgTrigger.TriggerDialogue();
    }
}
