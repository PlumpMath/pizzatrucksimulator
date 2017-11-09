using System.Collections;
using UnityEngine;

public class Phase : MonoBehaviour, IPhase {
    public event System.Action OnPhaseFinish;

    public virtual void Begin() {
    }

    public virtual void End() {
        Debug.Log("Phase End()");
        if (OnPhaseFinish != null) {
            OnPhaseFinish();
        }
    }
}
