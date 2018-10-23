using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharMovement : MonoBehaviour {


    #region Variáveis

    GameController gc;

    //Inventario
    Inventario inventario;
    
    //Movimento
    Rigidbody2D     rgb;
    public  float   speed;
    private float   cansaco;
    public bool     cansado;
    private bool    podeRespirar;
    private bool    canMove;
    public bool     moving;
    Vector3         diagSupEsq;
    Vector3         diagSupDir;
    Vector3         diagInfEsq;
    Vector3         diagInfDir;
    public Vector3  directionExport;

    //Portas
    bool canUseDoor;
    DoorSystem door;

    //Camera
    CameraControl camArea;

    //Pânico
    [SerializeField]
    private float panico;
    private bool  emPanico;

    //Diálogo
    DialogueTrigger readTrigger;

    public bool canDialogue;

    //Animação
    Animator anim;

    //UI
    public Slider cansacoSlider;
    public Slider panicoSlider;
    #endregion

    #region Main
    void Start () {
        gc = FindObjectOfType<GameController>();

        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inventario = GetComponent<Inventario>();

        speed   = 2f;
        cansaco = 0f;
        canMove = true;
        canUseDoor = false;
        panico  = 0f;

        //Vetores para guiarem o char para as diagonais (como na rosa dos ventos NO, NE, SO, SE)
        
        diagSupEsq = new Vector3(-1f, 0.5f);
        diagSupDir = new Vector3(1f, 0.5f);
        diagInfEsq = new Vector3(-1f, -0.5f);
        diagInfDir = new Vector3(1f, -0.5f);

    }
	
	void Update () {

        bool xKey               = Input.GetKey(KeyCode.X);
        bool xDownKey           = Input.GetKeyDown(KeyCode.X);
        bool joyInteractKey     = Input.GetKey("joystick button 1");
        bool joyInteractDownKey = Input.GetKeyDown("joystick button 1");

        Corre();

        //Sistema de Pânico
        if (emPanico)
        {
            EntraEmPanico(0.005f);
        }

        if ((xKey || joyInteractKey) && canDialogue)
        {
            readTrigger.TriggerDialogue();
        }

        if(canUseDoor && (xDownKey || joyInteractDownKey))
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
        if (col.gameObject.CompareTag("Item"))      if(col.gameObject.GetComponent<ItemImageSpawner>() == true) col.gameObject.GetComponent<ItemImageSpawner>().canImageSpawn = true;
        if (col.gameObject.CompareTag("Guardian"))  emPanico = true;


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
        //ALTERAR TODA ESSA BAGUNÇA


        if (col.gameObject.CompareTag("Door"))
        {
            door = col.gameObject.GetComponent<DoorSystem>();

            if (door.GetIsOpen() == false)
            {
                if (inventario.GetItemInventário("Chave") == true)
                {
                    canUseDoor = true;
                }
            }
            else
                canUseDoor = true;
        }

        if (col.gameObject.CompareTag("CameraArea"))
        {
            camArea = col.gameObject.GetComponent<CameraControl>();
            camArea.MoveCamera();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item")) if (col.gameObject.GetComponent<ItemImageSpawner>() == true) col.gameObject.GetComponent<ItemImageSpawner>().canImageSpawn = false;
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
    public void setCanMove(bool value)
    {
        canMove = value;
    }

    private void AnimateAndMove(Vector3 direction, int x, int y)
    {
        anim.SetBool("moving", true);       //Muda o parâmetro moving para verdadeiro, assim o animador começa a rodar animações de movimento
        moving = true;

        anim.SetFloat("x", x);  //Coloca no parâmetro x o valor recebido como x
        anim.SetFloat("y", y);

        rgb.transform.Translate(direction * speed * Time.deltaTime);    //Move na direção recebida com a velocidade atual arrumada com deltatime (evita diferenças de velocidade por causa de FPS)
    }

    private void MoveInputCheck()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");
        
        if (vertical > 0)      
        {
            if (horizontal < 0)   
            {
                AnimateAndMove(diagSupEsq, -1, 1);
                directionExport = diagSupEsq;
            }
            else
            {
                if (horizontal > 0)  
                {
                    AnimateAndMove(diagSupDir, 1, 1);
                    directionExport = diagSupDir;
                }
                else        
                {
                    AnimateAndMove(Vector3.up, 0, 1);
                    directionExport = Vector3.up;
                }
            }
        }
        else
        {
            if (vertical < 0)       
            {
                if (horizontal < 0)  
                {
                    AnimateAndMove(diagInfEsq, -1, -1);
                    directionExport = diagInfEsq;
                }
                else
                {
                    if (horizontal > 0)   
                    {
                        AnimateAndMove(diagInfDir, 1, -1);
                        directionExport = diagInfDir;
                    }
                    else        
                    {
                        AnimateAndMove(Vector3.down, 0, -1);
                        directionExport = Vector3.down;
                    }

                }
            }
            else
            {
                if (horizontal > 0)       
                {
                    AnimateAndMove(Vector3.right, 1, 0);
                    directionExport = Vector3.right;
                }
                else
                {
                    if (horizontal < 0)        
                    {
                        AnimateAndMove(Vector3.left, -1, 0);
                        directionExport = Vector3.left;
                    }
                    else
                    {
                        anim.SetBool("moving", false);
                        moving = false;
                    }
                }
            }
        }
    }

    private void RecuperaFolego()
    {
        cansaco -= 1f * Time.deltaTime;
        cansacoSlider.value = cansaco;
    }

    private void Corre()
    {
        bool botaoCorrida = Input.GetKey(KeyCode.Z);
        bool joyCorrida = Input.GetKey("joystick button 2");
        

        if ((botaoCorrida || joyCorrida) && moving && cansaco < 3f && cansado == false)    //Se o shift esquerdo está pressionado e o cansaço não está completo, então a velocidade aumenta para a corrida
        {
            speed = 4f;
            anim.SetBool("running", true);
            Cansa();
        }
        else
        {
            if (moving == false && (botaoCorrida || joyCorrida))
            {
                anim.SetBool("running", false);
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp("joystick button 2"))      
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
    }

    public void Cansa()
    {
        if(cansaco <= 3f)
        {
            cansaco += 1f * Time.deltaTime;
            cansacoSlider.value = cansaco;
        }
    }

    private void EntraEmPanico(float valorPanico)
    {
        panico += valorPanico;
        panicoSlider.value = panico;
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
