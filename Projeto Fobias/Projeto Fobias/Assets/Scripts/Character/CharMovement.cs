using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class LockedSoundEvents : UnityEvent
{
}


public class CharMovement : MonoBehaviour {


    #region Variáveis
    GameController gc;

    //Inventario
    Inventario inventario;
    
    //Movimento
    Rigidbody2D     rgb;
    public  float   speed;
    private bool    running;
    public bool    podeCorrer = true;
    private float   cansaco;
    public  bool    cansado;
    private bool    podeRespirar;
    private bool    podeRespirarUpdate;

    private bool    canMove;
    public bool     moving;

    Vector3         diagSupEsq;
    Vector3         diagSupDir;
    Vector3         diagInfEsq;
    Vector3         diagInfDir;
    public Vector3  directionExport;

    bool botaoCorrida;
    bool joyCorrida;

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
    public bool isDialoguing;

    //Animação
    Animator anim;

    //UI
    public Slider cansacoSlider;
    public Slider panicoSlider;

    [SerializeField] LockedSoundEvents somEvento;
    #endregion

    #region Main
    void Start () {
        gc = FindObjectOfType<GameController>();

        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inventario = GetComponent<Inventario>();

        speed       = 2f;
        cansaco     = 0f;
        canMove     = true;
        canUseDoor  = false;
        panico      = 0f;

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
        botaoCorrida = Input.GetKey(KeyCode.Z);
        joyCorrida = Input.GetKey("joystick button 2");

        if (Input.GetKeyUp(KeyCode.Z))
        {
            StopCoroutine(ParouDeCorrer());
            StartCoroutine(ParouDeCorrer());
        }
        else
        {
            if (podeRespirarUpdate)
            {
                RecuperaFolego();
            }
        }


        Corre();

        
        //Sistema de Pânico
        if (emPanico)
        {
            EntraEmPanico(0.005f);
        }

        if ((xDownKey || joyInteractKey) && canDialogue && isDialoguing == false)
        {
            readTrigger.TriggerDialogue();
        }

        if(canUseDoor && (xDownKey || joyInteractDownKey))
        {
            if (door.GetIsLocked())
            {
                somEvento.Invoke();
                string key = door.GetKeyName();
                if (inventario.GetItemInventário(key))
                {
                    door.Unlock();
                    door.DoorEnter(gameObject);
                }
            }
            else
            {
                door.DoorEnter(gameObject);
            }
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
            canUseDoor = true;
        }

        if (col.gameObject.CompareTag("CameraArea"))
        {
            camArea = col.gameObject.GetComponent<CameraControl>();
            camArea.GetTarget(this.transform);
            camArea.MoveCamera();
            camArea.Resize();
            camArea.follow = true;
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

        if (col.gameObject.CompareTag("CameraArea"))
        {
            camArea.follow = false;
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

        if (horizontal == 0 || vertical == 0)
        {
            moving = false;
            running = false;
        }

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
        if (!running)
        {
            cansaco -= 1f * Time.deltaTime;
            cansacoSlider.value = cansaco;
            if (cansaco <= 0f)
            {
                podeRespirarUpdate = false;
                podeCorrer = true;
                StopAllCoroutines();
            }
        }
    }

    private void Corre()
    {

        

        if ((botaoCorrida || joyCorrida) && moving && cansaco < 3f && cansado == false && podeCorrer) 
        {
            StopAllCoroutines();
            speed = 4f;
            anim.SetBool("running", true);
            running = true;
            Cansa();
        }
        else
        {
            if (moving == false && (botaoCorrida || joyCorrida))
            {
                anim.SetBool("running", false);
                running = false;
                podeCorrer = false;
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp("joystick button 2"))      
                {
                    speed = 2f;
                    anim.SetBool("running", false);
                    running = false;

                }
                else
                {
                    if (cansaco >= 3f && cansado == false)  
                    {
                        speed = 2f;
                        cansado = true;
                        anim.SetBool("running", false);
                        running = false;
                        podeCorrer = false;


                        StartCoroutine(EsperaParaRespirar());
                    }
                    else
                    {
                        if (cansado && podeRespirar)
                        {
                            RecuperaFolego();
                            podeCorrer = false;
                            podeRespirarUpdate = false;
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
        if (GetComponentInChildren<Push>() && cansaco >= 3f)
        {
            GetComponentInChildren<Push>().UnChild();
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

    public void SetIsDialoguing(bool value)
    {
        isDialoguing = value;
    }
    #endregion

    #region Coroutines
    IEnumerator EsperaParaRespirar()
    {
        print("Espera para respirar");
        StopCoroutine(ParouDeCorrer());
        yield return new WaitForSeconds(2);
        podeRespirar = true;
        print("FIM Espera para respirar");
    }

    IEnumerator ParouDeCorrer()
    {
        yield return new WaitForSeconds(1);
        podeRespirarUpdate = true;
        podeCorrer = false;
    }
    #endregion
}
