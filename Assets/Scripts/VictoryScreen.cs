using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("UI Titulo");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
