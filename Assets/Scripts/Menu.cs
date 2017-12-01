using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public AudioSource introMusic;
	public UnityEngine.UI.Text startText;
	public float startTextBlinkSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		StartCoroutine(BlinkStartText());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene("Game");
		}
		
	}

	IEnumerator BlinkStartText() {
		bool displayText = true;
		while (true) {
			startText.enabled = displayText;
			displayText = !displayText;
			yield return new WaitForSeconds(startTextBlinkSpeed);
		}
	}
}
