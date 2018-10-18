using System;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    String[] invSpaces = new String[2];


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            if (col.GetComponent<Tag>().GetMyTag() == Tag.Tags.chave)
            {
                if (!InventarioFull())
                {
                    EncheInventário("Chave");
                    Destroy(col.gameObject);
                }
            }
        }
    }

    public bool GetItemInventário(String item)
    {
        int i;

        for (i = 0; i < invSpaces.Length; i++)
        {
            if (invSpaces[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    public void EncheInventário(String nomeItem)
    {
        int i;

        for (i = 0; i < invSpaces.Length; i++)
        {
            if (invSpaces[i] == null)
            {
                invSpaces[i] = nomeItem;
                return;
            }
        }
    }

    private bool InventarioFull()
    {
        int i;
        int count = 0;

        for (i = 0; i<invSpaces.Length; i++)
        {
            if(invSpaces[i] != null)
            {
                count++;
            }
        }

        if (count == invSpaces.Length)
            return true;
        else
            return false;
    }
}
