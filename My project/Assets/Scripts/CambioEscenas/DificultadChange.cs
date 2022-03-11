using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DificultadChange : MonoBehaviour
{
    // Este script contiene la función que permite cargar la escena Dificultad
    public void ChangeDifficult()
    {
        SceneManager.LoadScene("Dificultad");
    }
}
