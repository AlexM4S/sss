using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumenChange : MonoBehaviour
{
    // Este script contiene la funci�n que permite cargar la escena M�sica
    public void Volumen()
    {
        SceneManager.LoadScene("M�sica");
    }
}
