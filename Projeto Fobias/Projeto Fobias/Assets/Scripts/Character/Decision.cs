using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Decision : MonoBehaviour {

    public bool answer { get; set; }
    private bool hasAnswered = false;
    private bool canReturn = false;

    [SerializeField] private TextMeshProUGUI baloonText;
    [SerializeField] string questionText;
    [SerializeField] private GameObject baloonGO;
    [SerializeField] private Animator baloonAnim;
    [SerializeField] private Button yesButton;

    public void SetQuestionText(string txt)
    {
        questionText = txt;
    }

    public void Ask()
    {
        baloonGO.SetActive(true);
        hasAnswered = false;
        baloonText.text = questionText;
        baloonAnim.SetBool("Open", true);
        StartCoroutine(WaitAnswer());
    }

    public void SetHasAnswered(bool op)
    {
        hasAnswered = op;
    }

    IEnumerator WaitAnswer()
    {
        yesButton.Select();
        Time.timeScale = 0;
        yield return new WaitUntil(() => hasAnswered == true);
        canReturn = true;
        baloonAnim.SetBool("Open",false);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);
        baloonGO.SetActive(false);
    }
}
