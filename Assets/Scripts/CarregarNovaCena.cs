using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarNovaCena : MonoBehaviour
{
    public void CarregarCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }
}
