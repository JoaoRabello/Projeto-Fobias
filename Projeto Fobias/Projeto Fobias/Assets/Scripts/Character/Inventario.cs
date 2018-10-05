using UnityEngine;

public class Inventario : MonoBehaviour
{
    int[] invSpaces = new int[2];


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            if (col.GetComponent<Tag>().GetMyTag() == Tag.Tags.chave)
            {
                if (!InventarioFull())
                {
                    //Pega chave
                }
            }
        }
    }

    private bool InventarioFull()
    {
        int i;
        int count = 0;

        for (i = 0; i<=invSpaces.Length; i++)
        {
            if(invSpaces[i] != 0)
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
