using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool isGameOver = false;

    public static GameManager sharedInstance;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGameOver()
    {
        isGameOver = true;
        //TODO: mostrar el game over (panel / otra escena)
        StartCoroutine(GoToEndMenu());
    }


    IEnumerator GoToEndMenu()
    {
        yield return new WaitForSeconds(2.1f);

        SceneManager.LoadScene(6);
    }
}
