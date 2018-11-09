using UnityEngine;

public class ClaricePower : MonoBehaviour {

    bool canUsePower;
    GameObject clarice;
    public Transform originHole;
    public Transform destinyHole;

    private void Awake()
    {
        clarice = GameObject.FindGameObjectWithTag("Clarice");
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.C) && canUsePower)
        {
            clarice.transform.position = destinyHole.position;
            SwapHole();
        }
	}

    void SwapHole()
    {
        Transform tempHole = destinyHole;
        destinyHole = originHole;
        originHole = tempHole;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Clarice"))
        {
            canUsePower = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Clarice"))
        {
            canUsePower = false;
        }
    }
}
