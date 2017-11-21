using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour {
    private ArmsState currentState;

    public Transform armsHolder;

    public bool controlsEnabled = false;
    public Camera mainCamera;
    public Transform holdingArea;

    bool isHolding = false;
    public bool hoveringOverDough = false;
    Transform heldObject;
    Transform heldObjectParent;
    public Animator animator;
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




        LookForDough();
    }

    void LookForDough() {
        RaycastHit doughInfo;
        int layerMask = 1 << 10;
        // only then check if raycast hits anything
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out doughInfo, 10f, layerMask)) {
            // and the raycast hit object tag is this
            if (doughInfo.transform.CompareTag("Dough")) {
                hoveringOverDough = true;
            }
        } else {
            hoveringOverDough = false;
        }
    }

    void DropObject() {

        RaycastHit hitInfo;
        int layerMask = 1 << 10; // Layer 10, ingredients

        GameObject pizzaObject;


        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 10f, layerMask);

        if (hoveringOverDough) {
            print("Dough was hit!");
            pizzaObject = hitInfo.transform.gameObject;
            pizza = pizzaObject.GetComponent<Pizza>();

            if (pizza.doughRolled == true) {

                if (pizza.sauceAdded == true) {

                    if (pizza.cheeseAdded == true) {
                        pizza.ingredientsList.Add(heldObject.GetComponent<Ingredient>());
                        heldObject.GetComponent<Renderer>().enabled = false;
                        heldObject.SetParent(pizza.transform);
                        heldObject.localPosition = (new Vector3(0f, 0f, 0f));
                        heldObject.localRotation = Quaternion.Euler(-90, 0, 0);
                        isHolding = false;
                        print("You added " + heldObject.name + " to the pizza.");
                        heldObject = null;

                    } else {
                        if (heldObject.tag == "Cheese") {
                            pizza.ingredientsList.Add(heldObject.GetComponent<Ingredient>());
                            heldObject.GetComponent<Renderer>().enabled = false;
                            heldObject.SetParent(pizza.transform);
                            heldObject.localPosition = (new Vector3(0f, 0f, 0f));
                            heldObject.localRotation = Quaternion.Euler(-90, 0, 0);
                            isHolding = false;
                            heldObject = null;
                            pizza.cheeseAdded = true;
                            print("The cheese is spread.");
                        } else {
                            print("ya gonna sprinkle that cheese?!");
                        }
                    }

                } else {
                    if (heldObject.tag == "Sauce") {
                        pizza.ingredientsList.Add(heldObject.GetComponent<Ingredient>());
                        heldObject.GetComponent<Renderer>().enabled = false;
                        heldObject.SetParent(pizza.transform);
                        heldObject.localPosition = (new Vector3(0f, 0f, 0f));
                        heldObject.localRotation = Quaternion.Euler(-90, 0, 0);
                        isHolding = false;
                        heldObject = null;
                        pizza.sauceAdded = true;
                        print("the sauce is spread");
                    } else {
                        print("You gotta spread the sauce first, ya boob.");
                    }
                }
            } else {

                print("You gotta roll the dough first, bub.");

            }

        } else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 10f, layerMask)
              && hitInfo.transform.gameObject.tag == "Oven") {
            print("you hit the oven, dude");
            if (pizza.holdingAPizza && pizza.cooked == false) {
                print("dropping the pizza in the oven");

                OvenConveyor ovenConveyor = hitInfo.transform.GetComponent<OvenConveyor>();
                ovenConveyor.placedPizza = heldObject.transform.gameObject;

                heldObject.transform.GetComponent<Rigidbody>().useGravity = false;
                heldObject.SetParent(hitInfo.transform.GetChild(0));
                heldObject.localPosition = new Vector3(0f, 0f, 0f);
                heldObject.localRotation = Quaternion.Euler(-90, 0, 0);

                isHolding = false;
                heldObject = null;
                ovenConveyor.placed = true;
            }
            if (pizza.holdingAPizza && pizza.cooked == true) {
                print("It's already cooked, Paulie");
            }
        } else // just drop whatever you're holding
          {
            heldObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.gameObject.GetComponent<BoxCollider>().enabled = true;

            heldObject.parent = null;
            heldObject.position = holdingArea.position;
            heldObject.rotation = Quaternion.Euler(-90, 0, 0);

            armsHolder.localPosition = new Vector3(armsHolder.localPosition.x, armsHolder.localPosition.y, armsHolder.localPosition.z - .5f);
            isHolding = false;
            heldObject = null;
        }


    }
}
