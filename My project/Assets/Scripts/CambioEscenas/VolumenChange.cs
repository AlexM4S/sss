using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumenChange : MonoBehaviour
{
    // Este script contiene la función que permite cargar la escena Música
    public void Volumen()
    {
        SceneManager.LoadScene("Música");
    }
}
