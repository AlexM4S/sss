using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Este script controla el final de la partida al recibir una animación de muerte y la pantalla de muerte posterior a este 

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

    
    // Al morir se dejan unos segundos para que ocurra la animación de muerte y seguidamente, muestra el menú de reintento o salir
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
