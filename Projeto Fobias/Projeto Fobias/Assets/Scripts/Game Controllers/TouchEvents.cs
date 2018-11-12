using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTouchEvent : UnityEvent
{
}

public class TouchEvents : MonoBehaviour {

    public enum Event { enemy, cutscene, text, evento};
    public Event eventTag;

    public GameObject enemyPool;
    private DialogueTrigger dlgTrigger;
    
    private CharMovement player;

    [SerializeField] private OnTouchEvent myEvent;

    private void Start()
    {
        if (GetComponent<DialogueTrigger>())
        {
            dlgTrigger = GetComponent<DialogueTrigger>();
        }
        else
        {
            print("Evento sem dialogue trigger!");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel"))
        {
            player = col.GetComponent<CharMovement>();
            PlayEvent();
        }
    }

    void PlayEvent()
    {
        switch (eventTag)
        {
            case Event.enemy:
                Enemies();
                break;
            case Event.cutscene:
                Cutscenes();
                break;
            case Event.text:
                Text();
                break;
            case Event.evento:
                myEvent.Invoke();
                break;
        }
    }

    void Enemies()
    {
        Instantiate(enemyPool,transform);
    }

    void Cutscenes()
    {
        print("Roda cutscene");
    }

    void Text()
    {
        dlgTrigger.TriggerDialogue();
        Destroy(this);
    }
}
