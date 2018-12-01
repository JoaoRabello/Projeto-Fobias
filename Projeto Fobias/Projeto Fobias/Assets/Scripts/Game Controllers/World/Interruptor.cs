using UnityEngine;

public class Interruptor : MonoBehaviour {

    Light luz;
    private bool acesa = false;

    private void Start()
    {
        luz = GetComponent<Light>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.X) && !acesa)
        {
            luz.intensity = 5;
            acesa = true;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X) && acesa)
            {
                luz.intensity = 0;
                acesa = false;
            }
        }
	}
}
