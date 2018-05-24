using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
	 public GUIText  EPTextMenu;
	private EPController epController;
     public void LoadGame(string name)
    {
        SceneManager.LoadScene(name); 
    }

     public void ExitGame()
    {
		
		Application.Quit ();



    } 



}
