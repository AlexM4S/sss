using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AjustesChange : MonoBehaviour
{
    // Este script contiene las funciones necesarias para que el menu principal funcione

    public void EscenaAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }

    public void EmpezarJuego()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void Salir()
    {
        Application.Quit();
    }
}
