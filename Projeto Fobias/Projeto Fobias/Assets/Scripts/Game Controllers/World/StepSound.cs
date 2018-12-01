using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AudioEvent : UnityEvent
{
}

public class StepSound : MonoBehaviour {

    CharMovement player = null;
    float timeToStep;
    [SerializeField] AudioEvent[] walkStep;
    [SerializeField] AudioEvent[] runStep;

	void Update () {
		if(player != null)
        {
            if (player.running)
            {
                if (timeToStep > 0.35f)
                {
                    int randomOp = Random.Range(1, 4);
                    runStep[randomOp].Invoke();
                    timeToStep = 0;
                }
                else
                {
                    timeToStep += Time.deltaTime;
                }
            }
            else
            {
                if (player.moving)
                {
                    if (timeToStep > 0.4f)
                    {
                        int randomOp = Random.Range(1, 14);
                        walkStep[randomOp].Invoke();
                        timeToStep = 0;
                    }
                    else
                    {
                        timeToStep += Time.deltaTime;
                    }
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel"))
        {
            player = col.gameObject.GetComponent<CharMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel"))
        {
            player = null;
        }
    }
}
