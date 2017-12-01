using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour {
    private ArmsState currentState;

    public Transform armsHolder;

    public bool controlsEnabled = false;
    public Camera mainCamera;
    public Transform holdingArea;

    public bool hoveringOverDough = false;
    public Transform heldObject;
    Transform heldObjectParent;
    public Animator animator;

    public AudioClip AudioClipWindUp;
    public AudioClip AudioClipWoosh;

    Pizza pizza;

    #region Singleton
    public static Arms Instance;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one ArmsController instance!");
            return;
        }
        Instance = this;
        this.animator = this.GetComponent<Animator>();
        SetState(new ArmsEmptyState(this));
    }
    #endregion Singleton

    public void SetState(ArmsState state) {
        if (currentState != null) {
            currentState.OnExit();
        }

        currentState = state;

        gameObject.name = "Arms - " + state.GetType().Name;

        if (currentState != null) {
            currentState.OnEnter();
        }
    }

    void Start() {

    }

    void Update() {
        // Don't do anything if controls are disabled
        if (!controlsEnabled) {
            return;
        }
        if (currentState != null) {
            currentState.Tick();
        }

    }
}
