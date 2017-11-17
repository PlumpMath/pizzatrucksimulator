using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmsController : MonoBehaviour
{
    public bool controlsEnabled = false;

    public Transform armsHolder;
    public Transform leftArm;
    public Transform rightArm;
    public Transform leftForearm;
    public Transform rightForearm;
    public Transform leftHand;
    public Transform rightHand;

    public Camera mainCamera;

    public Transform holdingArea;

    bool isHolding = false;
    public bool hoveringOverDough = false;
    Transform heldObject;
    Transform heldObjectParent;
    Animator animator;
    Pizza pizza;

    Vector3 initialArmsHolderPosition;
    Quaternion initialArmsHolderRotation;

    #region Singleton
    public static ArmsController Instance;


    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one ArmsController instance!");
            return;
        }
        Instance = this;
        this.animator = this.GetComponent<Animator>();
        initialArmsHolderPosition = armsHolder.position;
        initialArmsHolderRotation = armsHolder.rotation;
    }
    #endregion Singleton

    void Start()
    {

    }

    void Update()
    {

        if (!controlsEnabled)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isHolding)
            {
                DropObject();
            }
            else
            {
                LiftObject();
            }
            animator.SetTrigger("Lift");
        }

        LookForDough();

    }

    void LiftObject()
    {
        RaycastHit hitInfo;
        int layerMask = 1 << 10; // Layer 10, ingredients
        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 10f, layerMask);
        Color color = hitSomething ? Color.green : Color.red;

        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 10f, color);
        if (hitSomething)
        {
            {
                GameObject pizzaObject;
                if (hoveringOverDough)
                    {
                        print("Dough was hit!");
                        pizzaObject = hitInfo.transform.gameObject;
                        pizza = pizzaObject.GetComponent<Pizza>();

                        if (pizza.doughRolled == true)
                        {
                            {
                                if (pizza.sauceAdded == true)
                                {
                                    if (pizza.cheeseAdded == true)
                                    {

                                        if (pizza.gettable == true)
                                        {
                                            isHolding = true;
                                            pizza.holdingAPizza = true;
                                            heldObject = hitInfo.transform;
                                            heldObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
                                            heldObject.gameObject.GetComponent<BoxCollider>().enabled = false;
                                            heldObject.SetParent(holdingArea);
                                            heldObject.localPosition = Vector3.zero;
                                            armsHolder.localPosition = new Vector3(armsHolder.localPosition.x, armsHolder.localPosition.y, armsHolder.localPosition.z + .5f);

                                        }
                                        else
                                        {
                                            print("you gotta lay out the cheese, ya boob");
                                        }
                                    }
                                    else
                                    {
                                        print("you gotta spread the sauce, bub");
                                    }
                                }
                            }
                        }
                        else
                        {
                            pizza.RollDough();
                        }
                    }


                else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 10f, layerMask)
                   && hitInfo.transform.gameObject.tag == "Oven")

                {
                    print("That's the oven, mane.  Put a pizza in thurr.");
                }

                else
                {
                    isHolding = true;
                    heldObject = hitInfo.transform;
                    heldObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.gameObject.GetComponent<BoxCollider>().enabled = false;
                    heldObject.SetParent(holdingArea);
                    heldObject.localPosition = Vector3.zero;
                    armsHolder.localPosition = new Vector3(armsHolder.localPosition.x, armsHolder.localPosition.y, armsHolder.localPosition.z + .5f);

                }
                

            }
        }
    }


    void LookForDough()
    {
        RaycastHit doughInfo;
        int layerMask = 1 << 10;
        // only then check if raycast hits anything
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out doughInfo, 10f, layerMask))
            {
            // and the raycast hit object tag is this
            if (doughInfo.transform.CompareTag("Dough"))
                {
                hoveringOverDough = true;
                }
            }
        else
        {
            hoveringOverDough = false;
        }
     }

    void DropObject()
    {

        RaycastHit hitInfo;
        int layerMask = 1 << 10; // Layer 10, ingredients

        GameObject pizzaObject;


        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 10f, layerMask);

        if (hoveringOverDough)
        {
            print("Dough was hit!");
            pizzaObject = hitInfo.transform.gameObject;
            pizza = pizzaObject.GetComponent<Pizza>();

            if (pizza.doughRolled == true)
            {

                if (pizza.sauceAdded == true)
                {

                    if (pizza.cheeseAdded == true)
                    {
                        pizza.ingredientsList.Add(heldObject.GetComponent<Ingredient>());
                        heldObject.GetComponent<Renderer>().enabled = false;
                        heldObject.SetParent(pizza.transform);
                        heldObject.localPosition = (new Vector3(0f, 0f, 0f));
                        heldObject.localRotation = Quaternion.Euler(-90, 0, 0);
                        isHolding = false;
                        print("You added " + heldObject.name + " to the pizza.");
                        heldObject = null;
                        
                    }

                    else
                    {
                        if (heldObject.tag == "Cheese")
                        {
                            pizza.ingredientsList.Add(heldObject.GetComponent<Ingredient>());
                            heldObject.GetComponent<Renderer>().enabled = false;
                            heldObject.SetParent(pizza.transform);
                            heldObject.localPosition = (new Vector3(0f, 0f, 0f));
                            heldObject.localRotation = Quaternion.Euler(-90, 0, 0);
                            isHolding = false;
                            heldObject = null;
                            pizza.cheeseAdded = true;
                            print("The cheese is spread.");
                        }
                        else
                        {
                            print("ya gonna sprinkle that cheese?!");
                        }
                    }

                }

                else
                {
                    if (heldObject.tag == "Sauce")
                    {
                        pizza.ingredientsList.Add(heldObject.GetComponent<Ingredient>());
                        heldObject.GetComponent<Renderer>().enabled = false;
                        heldObject.SetParent(pizza.transform);
                        heldObject.localPosition = (new Vector3(0f, 0f, 0f));
                        heldObject.localRotation = Quaternion.Euler(-90, 0, 0);
                        isHolding = false;
                        heldObject = null;
                        pizza.sauceAdded = true;
                        print("the sauce is spread");
                    }
                    else
                    {
                        print("You gotta spread the sauce first, ya boob.");
                    }
                }
            }

            else
            {

                print("You gotta roll the dough first, bub.");

             }

        }
        
        else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 10f, layerMask) 
            && hitInfo.transform.gameObject.tag == "Oven")
            {
                print("you hit the oven, dude");
                    if (pizza.holdingAPizza)
                    {
                        print("dropping the pizza in the oven");
                        heldObject.SetParent(hitInfo.transform.GetChild(0));
                        heldObject.localPosition = holdingArea.localPosition;
                        heldObject.localRotation = Quaternion.Euler(-90, 0, 0);
                        isHolding = false;
                        heldObject = null;
            }
            }

        else // just drop whatever you're holding
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
