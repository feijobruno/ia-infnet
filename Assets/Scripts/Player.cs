using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    public float speed;
    private float score;

    private Vector3 verticalTargetPostion;

    public GameManager gameManager;

    void Start()
    {
        verticalTargetPostion = transform.position;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (animator.GetInteger("Start") == 0)
            return;

        animator.SetFloat("Speed", speed);

        score += Time.deltaTime * speed;
        gameManager.UpdateScore((int)score);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MudarFaixa(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MudarFaixa(1);
        }

       Vector3 targetPosition = new Vector3(verticalTargetPostion.x, verticalTargetPostion.y, transform.position.z);
       transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Correndo") && animator.GetInteger("Start") == 1)
            rb.velocity = Vector3.forward * (speed * 5);
    }

    private void MudarFaixa(int direcao)
    {
        if ((transform.position.x + direcao) < -1 || (transform.position.x + direcao) > 1)
            return;

        Vector3 vector3 = transform.position;
        vector3.x += direcao;

        verticalTargetPostion = vector3;
    }

    public void AumentarVelocidade()
    {
        if (speed <= 6)
            speed *= 1.15f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstaculo"))
        {
            animator.SetInteger("Start", 0);
            animator.SetBool("Morreu", true);
            rb.velocity = Vector3.zero;
            gameManager.GameOver();
        }
    }
}
