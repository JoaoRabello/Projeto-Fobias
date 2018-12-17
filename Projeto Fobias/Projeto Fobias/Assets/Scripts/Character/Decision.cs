using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Decision : MonoBehaviour {

    public bool answer { get; set; }
    private bool hasAnswered = false;
    private bool canReturn = false;

    [SerializeField] private TextMeshProUGUI baloonText;
    [SerializeField] string questionText;
    [SerializeField] private Animator baloonAnim;

    public void SetQuestionText(string txt)
    {
        questionText = txt;
    }

    public void Ask()
    {
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
        yield return new WaitUntil(() => hasAnswered == true);
        canReturn = true;
        baloonAnim.SetBool("Open",false);
    }
}
