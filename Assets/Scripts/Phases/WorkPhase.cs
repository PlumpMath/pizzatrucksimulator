using System.Collections;
using UnityEngine;

public class WorkPhase : Phase {

	public override void Begin() {
		print("WorkPhase Begin()");
		base.Begin();
		GameManager.Instance.firstPersonController.enabled = true;
		ArmsController.instance.controlsEnabled = true;
	}

	public override void End() {
		print("WorkPhase End()");
		GameManager.Instance.firstPersonController.enabled = false;
		ArmsController.instance.controlsEnabled = false;
		base.End();
	}
}
