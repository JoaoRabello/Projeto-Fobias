using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

    //Movimento
    Rigidbody2D rgb;
    public float speed;
    Vector3 diagSupEsq;
    Vector3 diagSupDir;
    Vector3 diagInfEsq;
    Vector3 diagInfDir;
    float cansaco;

    //Animação
    Animator anim;

    void Start () {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        speed = 2f;
        cansaco = 0f;

        //Vetores para guiarem o char para as diagonais (como na rosa dos ventos NO, NE, SO, SE)
        diagSupEsq = new Vector3(-0.75f, 0.75f, 0);
        diagSupDir = new Vector3(0.75f, 0.75f, 0);
        diagInfEsq = new Vector3(-0.75f, -0.75f, 0);
        diagInfDir = new Vector3(0.75f, -0.75f, 0);
    }
	
	// Update é chamado a cada frame
	void Update () {

        //Sistema de Corrida e Cansaço
        if (Input.GetKey(KeyCode.LeftShift) && cansaco < 3f)    //Se o shift esquerdo está pressionado e o cansaço não está completo, então a velocidade aumenta para a corrida
        {
            speed = 4f;
            cansaco += 1f * Time.deltaTime;     //Aqui o cansaço aumenta com o tempo de uso da corrida
            Debug.Log(cansaco);
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))      //Se o shift esquerdo é liberado, a velocidade volta ao normal
            {
                speed = 2f;
                Debug.Log("Parou de correr");
            }
            else
            {
                if (cansaco >= 3f)      //Se o cansaço alcança o máximo, a velocidade também volta ao normal, não podendo correr
                {
                    speed = 2f;
                }
            }
        }

        //Sistema de Respiração
        if(Input.GetKey (KeyCode.Space) && cansaco >= 0)    //Segurar espaço e ter o cansaço acima de zero reduz a velocidade, porém reduz o cansaço com tempo de uso da respiração
        {
            speed = 0f;
            cansaco -= 1f * Time.deltaTime;
            Debug.Log(cansaco);
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.Space) || cansaco <= 0)   //Liberar o espaço ou ter o cansaço zerado faz com que a velocidade retorne ao normal
            {
                speed = 2f;
            }
        }

    }

    // FixedUpdate é chamado mais vezes que o Update normal, servindo para cálculos físicos (ótimo para evitar colisões estranhas)
    private void FixedUpdate()
    {

        //Sistema de Movimentação
        if (Input.GetKey(KeyCode.UpArrow))      //Se pressiona o botão para cima, então anda para cima
        {
            if (Input.GetKey(KeyCode.LeftArrow))   //Pressionando também o botão esquerdo, segue para a diagonal superior esquerda
            {
                rgb.transform.Translate(diagSupEsq * speed * Time.deltaTime);       //Função que move o objeto em um sentido e direção, com velocidade e usando um vetor
                anim.SetFloat("x", -1);       anim.SetFloat("y", 1);
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))   //Pressionando também o botão direito, segue para a diagonal superior direita
                {
                    rgb.transform.Translate(diagSupDir * speed * Time.deltaTime);
                    anim.SetFloat("x", 1);      anim.SetFloat("y", 1);
                }
                else        //Se só pressionar o botão para cima, então segue para cima
                {
                    rgb.transform.Translate(Vector3.up * speed * Time.deltaTime);
                    anim.SetFloat("x", 0);      anim.SetFloat("y", 1);
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.DownArrow))        //Se pressiona o botão para baixo, então anda para baixo
            {
                if (Input.GetKey(KeyCode.LeftArrow))   //Pressionando também o botão esquerdo, segue para a diagonal inferior esquerda
                {
                    rgb.transform.Translate(diagInfEsq * speed * Time.deltaTime);
                    anim.SetFloat("x", -1);     anim.SetFloat("y", -1);
                }
                else
                {
                    if (Input.GetKey(KeyCode.RightArrow))   //Pressionando também o botão direito, segue para a diagonal inferior direita
                    {
                        rgb.transform.Translate(diagInfDir * speed * Time.deltaTime);
                        anim.SetFloat("x", 1);     anim.SetFloat("y", -1);
                    }
                    else        //Se só pressionar o botão para baixo, então segue para baixo
                    {
                        rgb.transform.Translate(Vector3.down * speed * Time.deltaTime);
                        anim.SetFloat("x", 0);      anim.SetFloat("y", -1);
                    }

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))       //Se pressiona o botão para direita, então anda para direita
                {
                    rgb.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    anim.SetFloat("x", 1);      anim.SetFloat("y", 0);
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftArrow))        //Se pressiona o botão para esquerda, então anda para esquerda
                    {
                        rgb.transform.Translate(Vector3.left * speed * Time.deltaTime);
                        anim.SetFloat("x", -1);     anim.SetFloat("y", 0);
                    }
                }
            }
        }
    }

}
