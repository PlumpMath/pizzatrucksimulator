using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsHoldingCookedPizzaState : ArmsState {
    Animator animator;
    RaycastHit objectInfo;
    Transform pizzaObject;

    public ArmsHoldingCookedPizzaState(Arms _arms, Transform _pizzaObject) : base(_arms) {
        pizzaObject = _pizzaObject;
    }

    public override void OnEnter() {
    }

    public override void Tick() {
        if (Input.GetMouseButtonDown(0)) {
            arms.StartCoroutine(FirePizza());
        }
    }

    public override void OnExit() {
    }

    IEnumerator FirePizza() {
        float pizzaPower = 0f;
        while (Input.GetMouseButton(0)) {
            pizzaPower += 1.25f;
            yield return null;
        }

        // pizzaPower = Mathf.Clamp(pizzaPower, 1f, 5f);
        // Debug.Log("FIRING PIZZA AT: " + pizzaPower);

        Rigidbody pizzaBody = pizzaObject.GetComponent<Rigidbody>();
        Vector3 originalPosition = pizzaObject.transform.localPosition;
        BoxCollider pizzaCollider = pizzaObject.GetComponent<BoxCollider>();

        pizzaBody.transform.SetParent(null);
        pizzaBody.AddForce(arms.mainCamera.transform.forward * pizzaPower, ForceMode.Impulse);
        pizzaBody.useGravity = true;
        // pizzaBody.velocity = Vector3.back * pizzaPower;
        pizzaBody.constraints = RigidbodyConstraints.None;
        pizzaCollider.enabled = true;

        if (GameManager.Instance.debugMode) {
            yield return new WaitForSeconds(3);
            pizzaBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            pizzaBody.transform.SetParent(arms.holdingArea.transform);
            pizzaBody.velocity = Vector3.zero;
            pizzaBody.angularVelocity = Vector3.zero;
            pizzaBody.useGravity = false;
            pizzaObject.localPosition = originalPosition;
            pizzaObject.localRotation = Quaternion.Euler(-120, 0, 0);
            pizzaCollider.enabled = false;
        }
    }
}
