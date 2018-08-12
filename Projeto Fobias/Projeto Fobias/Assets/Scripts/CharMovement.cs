﻿using System.Collections;
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
            anim.SetBool("running", true);
            cansaco += 1f * Time.deltaTime;     //Aqui o cansaço aumenta com o tempo de uso da corrida
            Debug.Log(cansaco);
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))      //Se o shift esquerdo é liberado, a velocidade volta ao normal
            {
                speed = 2f;
                anim.SetBool("running", false);
            }
            else
            {
                if (cansaco >= 3f)      //Se o cansaço alcança o máximo, a velocidade também volta ao normal, não podendo correr
                {
                    speed = 2f;
                    anim.SetBool("running", false);
                }
            }
        }

        //Sistema de Respiração
        if(Input.GetKey (KeyCode.Space) && cansaco >= 0)    //Segurar espaço e ter o cansaço acima de zero reduz a velocidade, porém reduz o cansaço com tempo de uso da respiração
        {
            speed = 0f;
            anim.SetBool("moving", false);
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

        MoveInputCheck();
  
    }

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
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))   //Pressionando também o botão direito, segue para a diagonal superior direita
                {
                    AnimateAndMove(diagSupDir, 1, 1);
                }
                else        //Se só pressionar o botão para cima, então segue para cima
                {
                    AnimateAndMove(Vector3.up, 0, 1);
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
                }
                else
                {
                    if (Input.GetKey(KeyCode.RightArrow))   //Pressionando também o botão direito, segue para a diagonal inferior direita
                    {
                        AnimateAndMove(diagInfDir, 1, -1);
                    }
                    else        //Se só pressionar o botão para baixo, então segue para baixo
                    {
                        AnimateAndMove(Vector3.down, 0, -1);
                    }

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))       //Se pressiona o botão para direita, então anda para direita
                {
                    AnimateAndMove(Vector3.right, 1, 0);
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftArrow))        //Se pressiona o botão para esquerda, então anda para esquerda
                    {
                        AnimateAndMove(Vector3.left, -1, 0);
                    }
                }
            }
        }
    }
}
