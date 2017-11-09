using System.Collections;
using UnityEngine;

public class CleanupPhase : Phase {
	public override void Begin() {
		print("CleanupPhase Begin()");
		base.Begin();
	}
}