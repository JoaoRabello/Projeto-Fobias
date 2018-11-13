using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnTouch : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("Colide");
            float rand = Random.Range(0, 5);
            if (rand >= 2.5f)
            {
                
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                col.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
