using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blackScreen : MonoBehaviour {

	private float alpha;
	private CanvasGroup cg;

	public bool active;
	public bool fadeOut;

	void Start()
	{
		cg = this.gameObject.GetComponent<CanvasGroup> ();
		active = true;
		alpha = 1f;
	}

	void Update()
	{
		if (active) {
			cg.alpha = alpha -= 0.5f * Time.deltaTime;
		}

		if (alpha <= 0f)
		{
			alpha = 0f;
		}
		if (alpha >= 1f) {
			alpha = 1f;
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

		}
		if (!active) {
			cg.alpha = alpha += 0.5f * Time.deltaTime;
		}
	}
}
