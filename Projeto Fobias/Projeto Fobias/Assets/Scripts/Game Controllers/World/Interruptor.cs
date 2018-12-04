using UnityEngine;

public class Interruptor : MonoBehaviour {

    private Light luz;
    private bool acesa = false;
    private bool playerOnRange = false;

    [SerializeField] private SpriteRenderer candelabro;
    [SerializeField] private Sprite spriteCandelabroOn;
    [SerializeField] private Sprite spriteCandelabroOff;

    [SerializeField] private SpriteRenderer interruptor;
    [SerializeField] private Sprite spriteInterruptorOn;
    [SerializeField] private Sprite spriteInterruptorOff;

    private void Start()
    {
        luz = GetComponent<Light>();
    }

    void Update () {

        if (playerOnRange)
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            playerOnRange = true;
        }
    }
}
