using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private new AudioSource audio;
    bool isNew;
    private void Start() {
        audio = GameObject.Find("BackgroundSound").GetComponent<AudioSource>();
        float vol;
        if (!PlayerPrefs.HasKey("Volume"))
        {
            vol = 0.5f;
        }
        else
        {
            vol = PlayerPrefs.GetFloat("Volume");
        }
        audio.volume = vol;
    }
    // Carreguem l'escena del bosc
    public void PlayGame()
    {
        isNew = true;
        PlayerPrefs.SetInt("isNew", (isNew ? 1 : 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Tanquem l'aplicació
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadGame(){
        isNew = false;
        PlayerPrefs.SetInt("isNew", (isNew ? 1 : 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
