using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChanger : MonoBehaviour {

    bool canChange;
    public int charNumber;
    int lastCharNumber;
    int tempCharNumber;

    GameObject ariel;
    GameObject clarice;
    GameObject ezekiel;
    GameObject pamela;

    private void Awake()
    {
        ariel = GameObject.FindGameObjectWithTag("Ariel");
        clarice = GameObject.FindGameObjectWithTag("Clarice");
        ezekiel = GameObject.FindGameObjectWithTag("Ezekiel");
        pamela = GameObject.FindGameObjectWithTag("Pamela");
    }

    // Use this for initialization
    void Start () {
        canChange = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C) && canChange) ChangeChar(charNumber);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ariel"))
        {
            canChange = true;
            lastCharNumber = 0;
            Debug.Log(lastCharNumber);
        }
        else
        {
            if (other.gameObject.CompareTag("Ezekiel"))
            {
                canChange = true;
                lastCharNumber = 1;
            }
            else
            {
                if (other.gameObject.CompareTag("Clarice"))
                {
                    canChange = true;
                    lastCharNumber = 2;
                    Debug.Log(lastCharNumber);
                }
                else
                {
                    if (other.gameObject.CompareTag("Pamela"))
                    {
                        canChange = true;
                        lastCharNumber = 3;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Ariel") || other.gameObject.CompareTag("Ezekiel") || other.gameObject.CompareTag("Clarice") || other.gameObject.CompareTag("Pamela")))
        {
            canChange = false;
        }
    }

    void ChangeChar(int i)
    {
        switch (i)
        {
            case 0:
                //Muda para Ariel
                ariel.SetActive(true);
                clarice.SetActive(false);
                charNumber = 0;
                tempCharNumber = charNumber;
                charNumber = lastCharNumber;
                lastCharNumber = tempCharNumber;
                Debug.Log(charNumber);
                break;
            case 1:
                //Muda para Ezekiel
                break;
            case 2:
                //Muda para Clarice
                ariel.SetActive(false);
                clarice.SetActive(true);
                clarice.GetComponent<SpriteRenderer>().enabled = true;
                charNumber = 2;
                tempCharNumber = charNumber;
                charNumber = lastCharNumber;
                lastCharNumber = tempCharNumber;
                Debug.Log(charNumber);
                break;
            case 3:
                //Muda para Pamela
                break;
            default:
                break;
        }
    }
}
