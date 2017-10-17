using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour {

	[Header("References")]
	public GameObject PauseUI;
	public KeyCode pauseButton;

	public KeyCode resumeKey;
	public KeyCode restartKey;
	public KeyCode quitKey;

	[Header("Bools")]
	public bool paused;


	void Start()
	{
		paused = false;
		PauseUI.SetActive (false);
	}

	void Update()
	{
		if (Input.GetKeyDown (pauseButton)) {
			paused = !paused;
		}


		if (paused) {
			PauseUI.SetActive (true);
			Time.timeScale = 0;

			if (Input.GetKeyDown (resumeKey)) {
				Resume ();
			}
			if (Input.GetKeyDown (restartKey)) {
				Restart ();
			}
			if (Input.GetKeyDown (quitKey)) {
				Quit ();
			}
		}
		if (!paused) {
			PauseUI.SetActive (false);
			Time.timeScale = 1;
		}
	
	}

	public void Resume()
	{
		paused = false;
	}
	public void Restart()
	{
		//Application.LoadLevel (Application.loadedLevel);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	public void Quit()
	{
		Application.Quit();
	}


}
