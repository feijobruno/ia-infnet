using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class IA : Agent
{
    private Rigidbody rb;
    private Animator animator;

    public float speed = 1.5f;
    private float score;

    private Vector3 verticalTargetPostion;

    public GameManager gameManager;

    public override void Initialize()
    {
        verticalTargetPostion = transform.position;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if(Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            MudarFaixa(-1);
        }
        else if (Mathf.FloorToInt(vectorAction[0]) == 2)
        {
            MudarFaixa(1);
        }
    }

    public override void OnEpisodeBegin()
    {

    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            actionsOut[0] = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            actionsOut[0] = 2;
        }
    }

    void Update()
    {
        if (animator.GetInteger("Start") == 0)
            return;

        animator.SetFloat("Speed", speed);

        score += Time.deltaTime * speed;
        gameManager.UpdateScore((int)score);

        Vector3 targetPosition = new Vector3(verticalTargetPostion.x, verticalTargetPostion.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Correndo") && animator.GetInteger("Start") == 1)
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