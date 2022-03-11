using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Este script controla el final de la partida al recibir una animaci�n de muerte y la pantalla de muerte posterior a este 

    public bool isGameOver = false;

    public static GameManager sharedInstance;
    
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    // Al morir se dejan unos segundos para que ocurra la animaci�n de muerte y seguidamente, muestra el men� de reintento o salir
    public void GoToGameOver()
    {
        isGameOver = true;
        StartCoroutine(GoToEndMenu());
    }


    IEnumerator GoToEndMenu()
    {
        yield return new WaitForSeconds(2.1f);

        SceneManager.LoadScene(6);
    }
}
