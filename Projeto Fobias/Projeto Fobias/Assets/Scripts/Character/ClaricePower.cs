using UnityEngine;

public class ClaricePower : MonoBehaviour {

    bool canUsePower;
    GameObject clarice;
    public Transform originHole;
    public Transform destinyHole;

    public bool hasDialogueBeforeChoice = false;
    public bool hasDialogueAfterChoice = false;
    private Decision decision;

    private void Awake()
    {
        clarice = GameObject.FindGameObjectWithTag("Clarice");
        decision = FindObjectOfType<Decision>();
        decision.SetQuestionText("Você deseja entrar no duto de ar?");
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.X) && canUsePower)
        {
            DialogueAndPlayerChoose();
        }
	}

    void DialogueAndPlayerChoose()
    {
        decision.Ask();
    }

    public void UsePower()
    {
        clarice.transform.position = destinyHole.position;
        SwapHole();
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
