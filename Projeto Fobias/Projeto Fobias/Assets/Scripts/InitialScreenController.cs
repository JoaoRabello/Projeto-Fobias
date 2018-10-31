using UnityEngine;

public class InitialScreenController : MonoBehaviour {

    public ScreenLoader sLoader;

	public void Play()
    {
        sLoader.LoadLevel(1);
    }

    public void Options()
    {
        //Opções
    }

    public void Quit()
    {
        Application.Quit();
    }
}