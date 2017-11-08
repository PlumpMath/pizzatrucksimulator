using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour {
	public float reputation = 0f;
	public float money = 0f;
	Turn currentTurn;
	public int lastTurnNumber= 7;
	int currentTurnNumber = 0;

	void Start () {
		print("_GameManager Start()");
		NewTurn();
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			currentTurn.NextPhase();
		}
	}

	void NewTurn() {
		currentTurnNumber++;
		print("_GameManager NewTurn() " + currentTurnNumber);
		GameObject currentTurnObject = new GameObject();
		currentTurnObject.name = "Turn " + currentTurnNumber;
		currentTurnObject.transform.SetParent(transform);
        currentTurn = currentTurnObject.gameObject.AddComponent<Turn>();
        currentTurn.OnFinish += MoveToNextTurn;
		currentTurn.Begin();
	}

	void MoveToNextTurn() {
		// ...
		print("_GameManager MoveToNextTurn()");
		if (currentTurnNumber != lastTurnNumber) {
			NewTurn();
		} else {
			EndGame();
		}
	}

	void EndGame() {
		print("_GameManager EndGame()");
	}
}
