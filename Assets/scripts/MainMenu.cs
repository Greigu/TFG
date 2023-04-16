using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Carreguem l'escena del bosc
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Tanquem l'aplicació
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
