﻿using UnityEngine;

public class Interruptor : MonoBehaviour {

    private Light luz;
    private bool acesa = false;
    private bool playerOnRange = false;
    private bool canInteract = false;

    [SerializeField] private SpriteRenderer candelabro;
    [SerializeField] private Sprite spriteCandelabroOn;
    [SerializeField] private Sprite spriteCandelabroOff;

    [SerializeField] private SpriteRenderer interruptor;
    [SerializeField] private Sprite spriteInterruptorOn;
    [SerializeField] private Sprite spriteInterruptorOff;

    public bool CanInteract
    {
        get
        {
            return canInteract;
        }

        set
        {
            canInteract = value;
        }
    }

    private void Start()
    {
        luz = GetComponent<Light>();
    }

    void Update () {

        if (playerOnRange && CanInteract)
        {
            if (!acesa && Input.GetKeyDown(KeyCode.X))
            {
                luz.intensity = 5;
                candelabro.sprite = spriteCandelabroOn;
                interruptor.sprite = spriteInterruptorOn;
                acesa = true;
            }
            else
            {
                if (acesa && Input.GetKeyDown(KeyCode.X))
                {
                    luz.intensity = 0;
                    candelabro.sprite = spriteCandelabroOff;
                    interruptor.sprite = spriteInterruptorOff;
                    acesa = false;
                }
            }
        }
	}

    public bool GetAcesa()
    {
        return acesa;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            playerOnRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            playerOnRange = false;
        }
    }

    
}
