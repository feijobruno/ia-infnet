using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    public List<Vector3> posicoes = new List<Vector3>();
    public List<Transform> obstaculos = new List<Transform>();

    private void Start()
    {
        GerarObstaculos();
    }

    public void GerarObstaculos()
    {
        List<Vector3> _posicoes = new List<Vector3>();
        _posicoes.AddRange(posicoes);

        foreach (Transform obstaculo in obstaculos)
        {
            int x = Random.Range(0, _posicoes.Count);
            obstaculo.localPosition = _posicoes[x];
            _posicoes.RemoveAt(x);
        }
    }
}
