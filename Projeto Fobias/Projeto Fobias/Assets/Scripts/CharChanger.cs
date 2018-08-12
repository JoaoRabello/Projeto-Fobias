using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChanger : MonoBehaviour {

    public int charNumber;
    bool canChange;
    
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
        if ((other.gameObject.CompareTag("Ariel") || other.gameObject.CompareTag("Ezekiel") || other.gameObject.CompareTag("Clarice") || other.gameObject.CompareTag("Pamela")))
        {
            canChange = true;
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
                break;
            case 1:
                //Muda para Ezekiel
                break;
            case 2:
                //Muda para Clarice
                Debug.Log("Muda para clarice");
                break;
            case 3:
                //Muda para Pamela
                break;
            default:
                break;
        }
    }
}
