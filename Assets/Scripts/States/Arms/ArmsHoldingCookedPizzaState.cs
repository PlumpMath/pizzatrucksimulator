using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsHoldingCookedPizzaState : ArmsState {
    Animator animator;
    RaycastHit objectInfo;
    Transform pizzaObject;
    AudioSource audio;

    public ArmsHoldingCookedPizzaState(Arms _arms, Transform _pizzaObject) : base(_arms) {
        pizzaObject = _pizzaObject;
    }

    public override void OnEnter() {
    }

    public override void Tick() {
        if (Input.GetMouseButtonDown(0)) {
            audio = Arms.Instance.GetComponent<AudioSource>();
            audio.clip = Arms.Instance.AudioClipWindUp;
            audio.Play();
            arms.StartCoroutine(FirePizza());
        }
    }

    public override void OnExit() {
    }

    IEnumerator FirePizza() {
        float pizzaPower = 0f;
        Rigidbody pizzaBody = pizzaObject.GetComponent<Rigidbody>();
        pizzaBody.maxAngularVelocity = 75f;
        pizzaBody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        while (Input.GetMouseButton(0)) {
            if (GameManager.Instance.debugMode) {
                pizzaPower += 100f;
                pizzaBody.AddTorque(10f, 0f, 0f, ForceMode.VelocityChange);
            } else {
                pizzaPower += 1.25f;
                pizzaBody.AddTorque(.1f, 0f, 0f, ForceMode.VelocityChange);
            }

            yield return null;
        }


        pizzaPower = Mathf.Clamp(pizzaPower, 1f, 150f);
        Debug.Log("FIRING PIZZA AT: " + pizzaPower);

        Vector3 originalPosition = pizzaObject.transform.localPosition;
        BoxCollider pizzaCollider = pizzaObject.GetComponent<BoxCollider>();

        pizzaBody.transform.SetParent(null);
        audio.clip = Arms.Instance.AudioClipWoosh;
        audio.Play();
        pizzaBody.AddForce(arms.mainCamera.transform.forward * pizzaPower, ForceMode.Impulse);
        pizzaBody.useGravity = true;
        // pizzaBody.velocity = Vector3.back * pizzaPower;
        pizzaBody.constraints = RigidbodyConstraints.None;
        pizzaCollider.enabled = true;

        arms.SetState(new ArmsEmptyState(arms));
    }
}
