using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowControles : MonoBehaviour
{
    // Este script contiene la función que permite cargar la escena Controles
    public void Controles()
    {
        SceneManager.LoadScene("Controles");
    }
}
