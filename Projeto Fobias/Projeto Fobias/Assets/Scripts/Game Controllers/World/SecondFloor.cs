using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloor : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ChangeLayerOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            Debug.Log("Para baixo");
            ChangeLayerOrder(-1);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            ChangeLayerOrder(1);
            Debug.Log("Para cima");
        }
    }
}
