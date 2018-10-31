using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScreenLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadAsync(index));
        StartCoroutine(LoadingText());
    }

    IEnumerator LoadingText()
    {
        loadingText.text = "Loading";
        yield return new WaitForSeconds(0.3f);
        loadingText.text = "Loading.";
        yield return new WaitForSeconds(0.3f);
        loadingText.text = "Loading..";
        yield return new WaitForSeconds(0.3f);
        loadingText.text = "Loading...";
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator LoadAsync(int index)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(index);

        loadingScreen.SetActive(true);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);

            loadingSlider.value = progress;

            yield return null;
        }
    }
}
