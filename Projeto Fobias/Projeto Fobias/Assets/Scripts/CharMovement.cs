using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharMovement : MonoBehaviour {


    #region Variáveis

    GameController gc;
    
    //Movimento
    Rigidbody2D     rgb;
    public  float   speed;
    private float   cansaco;
    private bool    cansado;
    private bool    podeRespirar;
    private bool    canMove;
    Vector3         diagSupEsq;
    Vector3         diagSupDir;
    Vector3         diagInfEsq;
    Vector3         diagInfDir;
    public Vector3  directionExport;

    //Portas
    bool canUseDoor = false;
    DoorSystem door;

    //Camera
    CameraControl camArea;

    //Pânico
    [SerializeField]
    private float panico;
    private bool  emPanico;
    public float tempo = 3f;

    //EXCLUIR ESTA PARTE
    DialogueTrigger readTrigger;

    public bool canDialogue;

    //Animação
    Animator anim;

    //UI
    public Text cansacoText;
    public Slider cansacoSlider;
    public Slider panicoSlider;
    #endregion

    #region Main
    void Start () {
        gc = FindObjectOfType<GameController>();

        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        speed   = 2f;
        cansaco = 0f;
        canMove = true;
        panico  = 0f;

        //Vetores para guiarem o char para as diagonais (como na rosa dos ventos NO, NE, SO, SE)
        
        diagSupEsq = new Vector3(-1f, 0.5f, 0);
        diagSupDir = new Vector3(1f, 0.5f, 0);
        diagInfEsq = new Vector3(-1f, -0.5f, 0);
        diagInfDir = new Vector3(1f, -0.5f, 0);

    }
	
	// Update é chamado a cada frame
	void Update () {

        Corre();

        //Sistema de Pânico
        if (emPanico)
        {
            EntraEmPanico();
        }

        if (Input.GetKey(KeyCode.S) && canDialogue)
        {
            readTrigger.TriggerDialogue();
        }

        if(canUseDoor && Input.GetKeyDown(KeyCode.X))
        {
            door.DoorEnter(gameObject);
        }
    }

    private void FixedUpdate()
    {

        if (canMove)  MoveInputCheck();
  
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))     col.gameObject.GetComponent<ItemImageSpawner>().canImageSpawn = true;
        if (col.gameObject.CompareTag("Guardian")) emPanico = true;

        //ALTERAR TODA ESSA BAGUNÇA
        if (col.gameObject.CompareTag("Legivel") && gc.GetActiveChar() == 0)
        {
            readTrigger = col.gameObject.GetComponent<DialogueTrigger>();
            canDialogue = true;
        }
        if (col.gameObject.CompareTag("LegivelClarice") && gc.GetActiveChar() == 2)
        {
            readTrigger = col.gameObject.GetComponent<DialogueTrigger>();
            canDialogue = true;
        }

        if (col.gameObject.CompareTag("Door"))
        {
            door = col.gameObject.GetComponent<DoorSystem>();

            if (door.GetIsOpen() == true)
            {
                canUseDoor = true;
            }
        }

        if (col.gameObject.CompareTag("CameraArea"))
        {
            camArea = col.gameObject.GetComponent<CameraControl>();
            camArea.MoveCamera();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item")) col.gameObject.GetComponent<ItemImageSpawner>().canImageSpawn = false;
        if (col.gameObject.CompareTag("Guardian")) emPanico = false;

        //ALTERAR TODA ESSA BAGUNÇA
        if (col.gameObject.CompareTag("Legivel") && gc.GetActiveChar() == 0)
        {
            readTrigger = null;
            canDialogue = false;
        }
        if (col.gameObject.CompareTag("LegivelClarice") && gc.GetActiveChar() == 2)
        {
            readTrigger = null;
            canDialogue = false;
        }

        if (col.gameObject.CompareTag("Door"))
        {
            canUseDoor = false;   
        }
    }
    #endregion

    #region Métodos
    private void AnimateAndMove(Vector3 direction, int x, int y)
    {
        anim.SetBool("moving", true);       //Muda o parâmetro moving para verdadeiro, assim o animador começa a rodar animações de movimento

        anim.SetFloat("x", x);  //Coloca no parâmetro x o valor recebido como x
        anim.SetFloat("y", y);

        rgb.transform.Translate(direction * speed * Time.deltaTime);    //Move na direção recebida com a velocidade atual arrumada com deltatime (evita diferenças de velocidade por causa de FPS)
    }

    private void MoveInputCheck()
    {
        if (!Input.anyKey)
        {
            anim.SetBool("moving", false);
        }

        //Sistema de Movimentação

        if (Input.GetKey(KeyCode.UpArrow))      //Se pressiona o botão para cima, então anda para cima
        {
            if (Input.GetKey(KeyCode.LeftArrow))   //Pressionando também o botão esquerdo, segue para a diagonal superior esquerda
            {
                AnimateAndMove(diagSupEsq, -1, 1);
                directionExport = diagSupEsq;
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))   //Pressionando também o botão direito, segue para a diagonal superior direita
                {
                    AnimateAndMove(diagSupDir, 1, 1);
                    directionExport = diagSupDir;
                }
                else        //Se só pressionar o botão para cima, então segue para cima
                {
                    AnimateAndMove(Vector3.up, 0, 1);
                    directionExport = Vector3.up;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.DownArrow))        //Se pressiona o botão para baixo, então anda para baixo
            {
                if (Input.GetKey(KeyCode.LeftArrow))   //Pressionando também o botão esquerdo, segue para a diagonal inferior esquerda
                {
                    AnimateAndMove(diagInfEsq, -1, -1);
                    directionExport = diagInfEsq;
                }
                else
                {
                    if (Input.GetKey(KeyCode.RightArrow))   //Pressionando também o botão direito, segue para a diagonal inferior direita
                    {
                        AnimateAndMove(diagInfDir, 1, -1);
                        directionExport = diagInfDir;
                    }
                    else        //Se só pressionar o botão para baixo, então segue para baixo
                    {
                        AnimateAndMove(Vector3.down, 0, -1);
                        directionExport = Vector3.down;
                    }

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))       //Se pressiona o botão para direita, então anda para direita
                {
                    AnimateAndMove(Vector3.right, 1, 0);
                    directionExport = Vector3.right;
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftArrow))        //Se pressiona o botão para esquerda, então anda para esquerda
                    {
                        AnimateAndMove(Vector3.left, -1, 0);
                        directionExport = Vector3.left;
                    }
                }
            }
        }
    }

    private void RecuperaFolego()
    {
        cansaco -= 1f * Time.deltaTime;
        cansacoSlider.value = cansaco;
        cansacoText.text = "Cansaço: " + cansaco.ToString("F1");
        Debug.Log("Recuperando folego");
    }

    private void Corre()
    {
        if (Input.GetKey(KeyCode.Z) && cansaco < 3f && cansado == false)    //Se o shift esquerdo está pressionado e o cansaço não está completo, então a velocidade aumenta para a corrida
        {
            speed = 4f;
            anim.SetBool("running", true);
            cansaco += 1f * Time.deltaTime;     //Aqui o cansaço aumenta com o tempo de uso da corrida
            cansacoSlider.value = cansaco;
            cansacoText.text = "Cansaço: " + cansaco.ToString("F1");
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Z))      //Se o shift esquerdo é liberado, a velocidade volta ao normal
            {
                speed = 2f;
                anim.SetBool("running", false);
            }
            else
            {
                if (cansaco >= 3f && cansado == false)      //Se o cansaço alcança o máximo, a velocidade também volta ao normal, não podendo correr
                {
                    speed = 2f;
                    cansado = true;

                    StartCoroutine(EsperaParaRespirar());

                    anim.SetBool("running", false);
                }
                else
                {
                    if (cansado && podeRespirar)
                    {
                        RecuperaFolego();
                        if (cansaco <= 0)
                        {
                            cansado = false;
                            podeRespirar = false;
                        }
                    }
                }
            }
        }
    }

    private void EntraEmPanico()
    {
        if (tempo >= 0)
        {
            panico += 0.005f;
            panicoSlider.value = panico;
            tempo -= Time.deltaTime;
        }
    }

    public Vector3 GetDirection()
    {
        return directionExport;
    }
    #endregion

    #region Coroutines
    IEnumerator EsperaParaRespirar()
    {
        yield return new WaitForSeconds(2);
        podeRespirar = true;
    }
    
    #endregion
}
