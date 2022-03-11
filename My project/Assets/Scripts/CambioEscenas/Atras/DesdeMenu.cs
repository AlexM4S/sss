using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesdeMenu : MonoBehaviour
{
    // Este script contiene la función que permite cargar la escena menú
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
