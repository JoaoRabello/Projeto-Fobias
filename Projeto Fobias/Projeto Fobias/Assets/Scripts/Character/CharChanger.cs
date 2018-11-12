using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChanger : MonoBehaviour {

    #region Variaveis
    bool canChange;
    bool firstChange = true;
    public int charNumber;
    int lastCharNumber;
    int tempCharNumber;

    public GameObject cutscene2;
    public GameObject hud;
    public Animator fadeAnim;

    public Transform charPoolPosition;

    GameObject ariel;
    GameObject clarice;
    //GameObject ezekiel;
    //GameObject pamela;

    Animator anim;

    GameController gameController;
    #endregion

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();

        ariel = GameObject.FindGameObjectWithTag("Ariel");
        clarice = GameObject.FindGameObjectWithTag("Clarice");
        clarice.GetComponent<CharMovement>().enabled = false;
        //ezekiel = GameObject.FindGameObjectWithTag("Ezekiel");
        //ezekiel.GetComponent<CharMovement>().enabled = false;
        //pamela = GameObject.FindGameObjectWithTag("Pamela");
        //pamela.GetComponent<CharMovement>().enabled = false;
    }


    void Start () {
        canChange = false;
        anim = GetComponent<Animator>();
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.X) && canChange && firstChange)
        {
            fadeAnim.SetTrigger("Fade");
            StartCoroutine(Cutscene());
            firstChange = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X) && canChange)
            {
                ChangeChar(charNumber);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ariel"))
        {
            canChange = true;
            lastCharNumber = 0;
            Debug.Log("Char atual: " + lastCharNumber);
        }
        else
        {
            if (other.gameObject.CompareTag("Ezekiel"))
            {
                canChange = true;
                lastCharNumber = 1;
            }
            else
            {
                if (other.gameObject.CompareTag("Clarice"))
                {
                    canChange = true;
                    lastCharNumber = 2;
                    Debug.Log("Char atual: " + lastCharNumber);
                }
                else
                {
                    if (other.gameObject.CompareTag("Pamela"))
                    {
                        canChange = true;
                        lastCharNumber = 3;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Ariel") || other.gameObject.CompareTag("Ezekiel") || other.gameObject.CompareTag("Clarice") || other.gameObject.CompareTag("Pamela")))
        {
            canChange = false;
        }
    }

    #region Métodos
    void ChangeChar(int i)
    {
        switch (i)
        {
            case 0:
                //Muda para Ariel
                SwapChar(ariel, clarice);
                charNumber = 0;
                SwapCharNumber();

                gameController.SetActiveChar(0);

                anim.SetBool("ariel-clarice", false);
                break;
            case 1:
                //Muda para Ezekiel
                break;
            case 2:
                //Muda para Clarice
                SwapChar(clarice, ariel);
                charNumber = 2;
                SwapCharNumber();

                gameController.SetActiveChar(2);

                anim.SetBool("ariel-clarice", true);
                break;
            case 3:
                //Muda para Pamela
                break;
            default:
                break;
        }
    }

    void SwapChar(GameObject go1, GameObject go2)
    {
        go1.SetActive(true);
        go1.GetComponent<CharMovement>().enabled = true;
        go1.transform.position = go2.transform.position;

        go2.SetActive(false);
        go2.GetComponent<CharMovement>().enabled = true;
        go2.transform.position = charPoolPosition.position;
    }

    void SwapCharNumber()
    {
        tempCharNumber = charNumber;
        charNumber = lastCharNumber;
        lastCharNumber = tempCharNumber;
    }
    #endregion

    IEnumerator Cutscene()
    {
        hud.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        cutscene2.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        fadeAnim.SetTrigger("Fade");
        ChangeChar(charNumber);
        yield return new WaitForSecondsRealtime(11f);
        cutscene2.SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);
        hud.SetActive(true);
    }
}
