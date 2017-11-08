using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
	public int turnNumber;
	ReputationCalculator reputationCalculator;
	Queue<IPhase> phases;
	IPhase currentPhase;

	// CustomerQueue customerQueue;

	public event System.Action OnFinish;

	ParkingPhase parkingPhase;
	MarketPhase marketPhase;
	WorkPhase workPhase;
	SalesPhase salesPhase;
	CleanupPhase cleanupPhase;

	public void Begin() {
		Debug.Log("Turn Start()");
		reputationCalculator = new ReputationCalculator();

		phases = new Queue<IPhase>(
					new IPhase[] { 
						gameObject.AddComponent<ParkingPhase>(),
						gameObject.AddComponent<MarketPhase>(),
						gameObject.AddComponent<WorkPhase>(),
						gameObject.AddComponent<SalesPhase>(),
						gameObject.AddComponent<CleanupPhase>()
					}
				);

		NextPhase();
	}
	
	void Update () {
	}

	void OnPhaseFinished() {
	}

	public void NextPhase() {
		Debug.Log("Turn NextPhase()");
		if (phases.Count > 0) {
			currentPhase = phases.Dequeue();	
			currentPhase.Begin();
		} else {
			FinishTurn();
		}
	}

	void FinishTurn() {
		Debug.Log("Turn FinishTurn()");
		if (OnFinish != null) {
			OnFinish();
		}
	}
}
