using UnityEngine;

public class ArmsHoldingCheeseOverDough : ArmsState {

    Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform cheese;
    Transform dough;
    Pizza pizza;


    public ArmsHoldingCheeseOverDough(Arms _arms, Transform _cheese, Transform _dough) : base(_arms) {

        cheese = _cheese;
        dough = _dough;

    }

    public override void OnEnter() {

        animator = arms.animator;
        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
    }

    public override void Tick() {

        RaycastHit objectInfo;
        int layerMask = 1 << 10;

        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out objectInfo, 10f, layerMask);

        if (!hitSomething) {
            arms.SetState (new ArmsHoldingCheese(arms, cheese));
            return;
        }

        if (Input.GetMouseButtonDown(0)){
            
            SprinkleCheese();
            
        }

    }

    public override void OnExit() {

    }

    public void SprinkleCheese()
    {
	    pizza = dough.GetComponent<Pizza>();
       // if (pizza.sauceAdded == true)
        {
            pizza.cheeseAdded = true;
             pizza.ingredientsList.Add(cheese.GetComponent<Ingredient>());
             Debug.Log("The cheese is sprlinkled.");
              arms.SetState(new ArmsEmptyState(arms));    

        }
    }

}
