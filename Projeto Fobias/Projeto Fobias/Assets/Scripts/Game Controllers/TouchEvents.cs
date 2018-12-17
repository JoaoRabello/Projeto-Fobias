using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTouchEvent : UnityEvent
{
}

public class TouchEvents : MonoBehaviour {

    public enum Event { enemy, cutscene, text, evento, multiplosEventos};
    public Event eventTag;

    public GameObject enemyPool;
    private DialogueTrigger dlgTrigger;

    public bool startRandomize;
    private float timeToPlay;

    private GameController gc;

    [SerializeField] private OnTouchEvent myEvent;
    [SerializeField] private OnTouchEvent randomEvent1;
    [SerializeField] private OnTouchEvent randomEvent2;
    [SerializeField] private OnTouchEvent randomEvent3;
    [SerializeField] private OnTouchEvent randomEvent4;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

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
            case Event.multiplosEventos:
                startRandomize = true;
                break;
        }
    }

    private void Update()
    {
        if (startRandomize)
        {
            RandomizeSounds();
        }
    }

    void RandomizeSounds()
    {
        if(timeToPlay >= 0)
        {
            timeToPlay -= Time.deltaTime;
        }
        else
        {
            timeToPlay = Random.Range(5f, 10f);
            int randomOp = Random.Range(1, 4);
            switch (randomOp)
            {
                case 1:
                    randomEvent1.Invoke();
                    break;
                case 2:
                    randomEvent2.Invoke();
                    break;
                case 3:
                    randomEvent3.Invoke();
                    break;
                case 4:
                    randomEvent4.Invoke();
                    break;
            }
        }
    }

    public void SetRandomize(bool value)
    {
        startRandomize = value;
    }

    void Enemies()
    {
        Instantiate(enemyPool,transform);
        Destroy(this);
    }

    void Cutscenes()
    {
        print("Roda cutscene");
    }

    void Text()
    {
        if(gc.GetActiveChar() == 0)
        {
            dlgTrigger.TriggerDialogue();
        }
        else
        {
            if (gc.GetActiveChar() == 2)
                GetComponentInChildren<DialogueTrigger>().TriggerDialogue();
        }
        Destroy(this);
    }
}
