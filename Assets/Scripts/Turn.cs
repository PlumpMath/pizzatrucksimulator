﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
	public int turnNumber;
	// ReputationCalculator reputationCalculator;
	Queue<IPhase> phases;
	IPhase currentPhase;

	// CustomerQueue customerQueue;

	public event System.Action OnTurnFinish;

	public void Begin() {
		Debug.Log("Turn Begin()");
		// reputationCalculator = new ReputationCalculator();

		phases = new Queue<IPhase>(
					new IPhase[] { 
						gameObject.AddComponent<MarketPhase>(),
						gameObject.AddComponent<ParkingPhase>(),
						gameObject.AddComponent<WorkPhase>(),
						gameObject.AddComponent<SalesPhase>(),
						gameObject.AddComponent<CleanupPhase>()
					}
				);

		NextPhase();
	}
	
	void Update () {
	}

	public void NextPhase() {
		Debug.Log("Turn NextPhase()");
		if (phases.Count > 0) {
			currentPhase = phases.Dequeue();	
			currentPhase.Begin();
			currentPhase.OnPhaseFinish += HandlePhaseFinish;
		} else {
			FinishTurn();
		}
	}

	public void HandlePhaseFinish() {
		Debug.Log("Turn OnPhaseFinished()");
		NextPhase();
	}

	void FinishTurn() {
		Debug.Log("Turn FinishTurn()");
		if (OnTurnFinish != null) {
			OnTurnFinish();
		}
	}
}