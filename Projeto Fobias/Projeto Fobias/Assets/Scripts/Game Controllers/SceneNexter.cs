using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNexter : MonoBehaviour {

	public void _GoToNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
