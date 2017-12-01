using System.Collections;
using UnityEngine;

public class WorkPhase : Phase {
	GameObject workdayUIObject;
	WorkdayUI workdayUI;

	public override void Begin() {
		print("WorkPhase Begin()");
		base.Begin();
		GameManager.Instance.firstPersonController.enabled = true;
		Arms.Instance.controlsEnabled = true;
		SetupWorkdayUI();
		SetupWorkArea();
	}

	void SetupWorkdayUI() {
		workdayUIObject = GameObject.Find("WorkdayUI");
		workdayUIObject.GetComponent<CanvasGroup>().alpha = 1f;
		workdayUIObject.GetComponent<CanvasGroup>().interactable = true;
		workdayUIObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

		workdayUI = workdayUIObject.GetComponent<WorkdayUI>();
	}

	void SetupWorkArea() {
		IngredientSpawner.Instance.SpawnTruckIngredients();
		CustomerSpawner.Instance.Begin();
	}

	public override void End() {
		print("WorkPhase End()");
		GameManager.Instance.firstPersonController.enabled = false;
		Arms.Instance.controlsEnabled = false;
		base.End();
	}
}