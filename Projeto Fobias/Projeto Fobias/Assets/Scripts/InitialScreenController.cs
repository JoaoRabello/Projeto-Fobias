using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScreenController : MonoBehaviour {

	public void Play()
    {
        SceneManager.LoadScene(1);
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
