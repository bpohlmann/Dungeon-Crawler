using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SmallMenu : MonoBehaviour {

	public Transform Canavas;

	public void quit (string name)
	{
        Application.Quit();
    }

	public void goOn()
	{


		Canavas.gameObject.SetActive(false);
		Time.timeScale = 1;

	}
}
