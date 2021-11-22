using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerarCenario : MonoBehaviour
{
    private List<Paisagem> paisagems = new List<Paisagem>();
    private List<Obstaculos> obstaculos = new List<Obstaculos>();

    private void Start()
    {
        paisagems.AddRange(GetComponentsInChildren<Paisagem>());
        obstaculos.AddRange(GetComponentsInChildren<Obstaculos>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AumentarVelocidade();
            Reset(other.transform);
        }

        if (other.CompareTag("IA"))
        {
            other.GetComponent<IA>().AumentarVelocidade();
            Reset(other.transform);
        }
    }

    public void Reset(Transform jogador)
    {
        jogador.localPosition = new Vector3(jogador.localPosition.x, jogador.localPosition.y, -1.5f);

        foreach (Paisagem paisagem in paisagems)
        {
            paisagem.GerarPaisagens();
        }

        foreach (Obstaculos obstaculo in obstaculos)
        {
            obstaculo.GerarObstaculos();
        }
    }
}
