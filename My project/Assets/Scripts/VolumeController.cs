using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Este script permite controlar el volumen de la música de fondo del videojuego
public class VolumeController : MonoBehaviour
{
    public Slider controlVolumen;

    public GameObject[] audios;

    // En el start se definen que valores queremos guardar para el audio, en este caso los objectos con el tag "audio", y se guardan con PlayerPrefs
    private void Start()   
    {
        audios = GameObject.FindGameObjectsWithTag("audio");
        controlVolumen.value = PlayerPrefs.GetFloat("volumenSave", 1f);
    }

    // En el update se comprueba si hay mas de un audio para asignar
    private void Update()
    {
        foreach (GameObject au in audios)
            au.GetComponent<AudioSource>().volume = controlVolumen.value;
    }

    // En esta función se guarda el valor obtenido anteriormente
    public void guardarVolumen(float value)
    {
        foreach (GameObject au in audios)
            au.GetComponent<AudioSource>().volume = controlVolumen.value;

        PlayerPrefs.SetFloat("volumenSave", value);
    }

}
