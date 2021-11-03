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
            transform.position = new Vector3(0, 0, transform.position.z + 105 * 2);
            other.GetComponent<Player>().AumentarVelocidade();

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
}
