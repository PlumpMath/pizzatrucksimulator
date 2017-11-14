using System.Collections;
using UnityEngine;

public class WorkPhase : Phase {

	PizzaTruck pizzaTruck;

	public override void Begin() {
		print("WorkPhase Begin()");
		base.Begin();
		GameManager.Instance.firstPersonController.enabled = true;
		ArmsController.Instance.controlsEnabled = true;
		pizzaTruck = PizzaTruck.Instance;
		SetupWorkArea();
	}

	void SetupWorkArea() {
		IngredientSpawner.Instance.SpawnTruckIngredients();
	}

	public override void End() {
		print("WorkPhase End()");
		GameManager.Instance.firstPersonController.enabled = false;
		ArmsController.Instance.controlsEnabled = false;
		base.End();
	}
}