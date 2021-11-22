using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class IA : Agent
{
    public Rigidbody rb;
    public Animator animator;

    public float speed = 1.5f;

    public MeshRenderer chao;

    private int currentLane = 0;
    private Vector3 verticalTargetPosition;

    public GerarCenario gerarCenario;

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 1)
        {
            MudarFaixa(-1);
        }
        else if (actions.DiscreteActions[0] == 2)
        {
            MudarFaixa(1);
        }

        AddReward(rb.velocity.z * .001f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition.x);
    }

    public override void OnEpisodeBegin()
    {
        gerarCenario.Reset(this.transform);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discretActions = actionsOut.DiscreteActions;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            discretActions[0] = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            discretActions[0] = 2;
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", speed);

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
        //if (speed <= 6)
        //    speed *= 1.15f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstaculo"))
        {
            AddReward(-100.0f);
            //chao.material.color = Color.red;
            EndEpisode();
        }

        if (other.CompareTag("recompensa"))
        {
            AddReward(100f);
            //chao.material.color = Color.green;
            EndEpisode();
        }

        if (other.CompareTag("reward"))
        {
            AddReward(1f);
            //chao.material.color = Color.yellow;
        }
    }
}
