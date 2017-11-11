using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour {
	public float reputation = 0f;
	public float money = 0f;
	Turn currentTurn;
	public int numberOfTurnsInGame= 7;
	int currentTurnNumber = 0;

	void Start () {
		print("_GameManager Start()");
		NewTurn();
	}

	void Update () {
	}

	void NewTurn() {
		currentTurnNumber++;
		print("_GameManager NewTurn() " + currentTurnNumber);
		GameObject currentTurnObject = new GameObject();
		currentTurnObject.name = "Turn " + currentTurnNumber;
		currentTurnObject.transform.SetParent(transform);
        currentTurn = currentTurnObject.gameObject.AddComponent<Turn>();
        currentTurn.OnTurnFinish += MoveToNextTurn;
		currentTurn.Begin();
	}

	void MoveToNextTurn() {
		// ...
		print("_GameManager MoveToNextTurn()");
		if (currentTurnNumber == numberOfTurnsInGame) {
			EndGame();
		} else {
			NewTurn();
		}
	}

	void EndGame() {
		print("_GameManager EndGame()");
	}
}
