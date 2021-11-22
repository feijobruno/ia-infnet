using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    public float speed;

    private int currentLane = 0;
    private Vector3 verticalTargetPosition;

    public GerarCenario gerarCenario;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetInteger("Start") == 0)
            return;

        animator.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MudarFaixa(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MudarFaixa(1);
        }

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.localPosition.z);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, (speed * 5) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * (speed * 5);
    }

    private void MudarFaixa(int direcao)
    {
        int targetLane = currentLane + direcao;
        if (targetLane < -1 || targetLane > 1)
            return;

        currentLane = targetLane;

        verticalTargetPosition = new Vector3(currentLane, 0, 0);
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
            gerarCenario.Reset(this.transform);
        }
    }
}
