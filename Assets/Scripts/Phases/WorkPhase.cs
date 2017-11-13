using System.Collections;
using UnityEngine;

public class WorkPhase : Phase {

	public override void Begin() {
		print("WorkPhase Begin()");
		base.Begin();
		GameManager.instance.firstPersonController.enabled = true;
	}

	public override void End() {
		print("WorkPhase End()");
		base.End();
	}
}
