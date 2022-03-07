using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AjustesChange : MonoBehaviour
{
    

    public void EscenaAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }

    public void EmpezarJuego()
    {
        SceneManager.LoadScene("Game");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
