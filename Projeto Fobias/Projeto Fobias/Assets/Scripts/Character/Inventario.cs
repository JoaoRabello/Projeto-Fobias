using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryEvent : UnityEvent{
}

public class Inventario : MonoBehaviour
{
    String[] invSpaces = new String[2];

    public InventoryEvent keyEvent;
    public InventoryEvent key2Event;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            Tag.Tags tag = col.GetComponent<Tag>().GetMyTag();
            switch (tag)
            {
                case Tag.Tags.chave1:
                    if (!InventarioFull())
                    {
                        EncheInventário("Chave1");
                        col.GetComponent<DialogueTrigger>().TriggerDialogue();
                        Destroy(col.gameObject);
                        keyEvent.Invoke();
                    }
                    break;
                case Tag.Tags.chave2:
                    if (!InventarioFull())
                    {
                        EncheInventário("Chave2");
                        Destroy(col.gameObject);
                        key2Event.Invoke();
                    }
                    break;
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

    public void RemoveItem(String nomeItem)
    {
        int i;

        for (i = 0; i < invSpaces.Length; i++)
        {
            if (invSpaces[i] == nomeItem)
            {
                invSpaces[i] = null;
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
