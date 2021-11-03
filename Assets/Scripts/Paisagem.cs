using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paisagem : MonoBehaviour
{
    public List<GameObject> ilhas = new List<GameObject>();

    private void Start()
    {
        GerarPaisagens();
    }

    public void GerarPaisagens()
    {
        foreach(GameObject gameObject in ilhas)
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        int x = Random.Range(0, ilhas.Count);
        ilhas[x].SetActive(true);
    }
}
