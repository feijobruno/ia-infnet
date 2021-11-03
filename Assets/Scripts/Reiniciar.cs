using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
    string nomeDaCenaAtual;

    void Start()
    {
        nomeDaCenaAtual = SceneManager.GetActiveScene().name;
        Invoke(nameof(Reload), 3);
    }

    private void Reload()
    {
        SceneManager.LoadScene(nomeDaCenaAtual);
    }
}
