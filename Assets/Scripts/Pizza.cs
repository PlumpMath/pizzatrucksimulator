using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {
    public List<Ingredient> ingredientsList;

    public bool doughRolled = false;
    public bool sauceAdded = false;
    public bool cheeseAdded = false;
    public bool gettable = true;
    public bool holdingAPizza = false;
    public bool cooked = false;
    public bool rollingDough;

    public Material pizzaTop;
    public Material pizzaBottom;

    public SkinnedMeshRenderer doughMesh;

    void Start() {
        doughMesh = GetComponent<SkinnedMeshRenderer>();
    }


    public int Reputation { get; set; }
    // public Customer customer;

    public void RollDough() {
        if (!doughRolled) {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            StartCoroutine(RollOutDough());
            //            blendShapeWeightTarget = 0;
        }
    }

    void OnCollisionEnter(Collision col) {
        Customer customer = col.gameObject.GetComponent<Customer>();

        if (customer != null) {
            customer.OnPizzaHit(this);
        }
    }

    IEnumerator RollOutDough() {
        float blendShapeWeightCurrent = 100;
        float interpolation = 0f;
        Arms.Instance.GetComponent<Animator>().SetTrigger("Roll");
        rollingDough = true;
        while (blendShapeWeightCurrent > 0f) {
            blendShapeWeightCurrent = Mathf.Floor(Mathf.Lerp(blendShapeWeightCurrent, 0f, interpolation));
            doughMesh.SetBlendShapeWeight(0, blendShapeWeightCurrent);
            interpolation += Time.deltaTime * .1f;
            yield return null;
        }
        doughRolled = true;
        rollingDough = false;
    }

    void Update() {

    }

    public bool AddToPizza(Transform ingredient) {
        if (!CanReceiveIngredient(ingredient)) {
            // Show warning that this ingredient cannot be received
            return false;
        }

        string ingredientType = GetIngredientType(ingredient);
        switch (ingredientType) {
            case "Sauce":
                sauceAdded = true;
                break;
            case "Cheese":
                cheeseAdded = true;
                break;
            case "Topping":
                ingredientsList.Add(ingredient.GetComponent<Ingredient>());
                break;
            default:
                break;
        }

        ingredient.SetParent(null);
       // ingredient.gameObject.GetComponent<MeshRenderer>().enabled = false;
        return true;
    }

    public bool CanReceiveIngredient(Transform ingredient) {
        if (ingredient.tag == "Sauce") {
            return doughRolled && !sauceAdded;
        } else if (ingredient.tag == "Cheese") {
            return doughRolled && sauceAdded && !cheeseAdded;
        } else if (ingredient.GetComponent<Ingredient>() != null) {
            return doughRolled && sauceAdded && cheeseAdded;
        } else {
            Debug.Log("Not sauce or cheese or ingredient!");
            return false;
        }
    }

    public void ChangeMaterial(){

        Material[] mats = doughMesh.materials; 
        mats[0] = pizzaTop;
        mats[1] = pizzaBottom;
        doughMesh.materials = mats;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

    }

    public string GetIngredientType(Transform ingredient) {
        if (ingredient.tag == "Sauce" || ingredient.tag == "Cheese") {
            return ingredient.tag;
        } else if (ingredient.GetComponent<Ingredient>() != null) {
            return "Topping";
        }
        return null;
    }
}
