using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesdeMenu : MonoBehaviour
{
    // Este script contiene la funci�n que permite cargar la escena men�
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
