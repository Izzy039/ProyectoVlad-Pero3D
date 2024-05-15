using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;

public class MainMenu : MonoBehaviour
{
   public void PlayGame ()
   {
    SceneManager.LoadScreen(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void QuitGame ()
   {
    Debug.Log ("QUIT!");
    Application.Quit();
   }
}
