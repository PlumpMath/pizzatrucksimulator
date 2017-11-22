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

    public SkinnedMeshRenderer doughMesh;
    public float blendShapeWeightCurrent = 100;

    float blendShapeWeightTarget = 100;

    void Start() {
        doughMesh = GetComponent<SkinnedMeshRenderer>();
    }


    public int Reputation { get; set; }
    // public Customer customer;

    public void RollDough() {
        if (!doughRolled) {
            doughRolled = true;
            blendShapeWeightTarget = 0;
        }
    }

    void Update() {
        blendShapeWeightCurrent = Mathf.Lerp(blendShapeWeightCurrent, blendShapeWeightTarget, Time.deltaTime);
        doughMesh.SetBlendShapeWeight(0, blendShapeWeightCurrent);
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
        ingredient.gameObject.GetComponent<MeshRenderer>().enabled = false;
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

    public string GetIngredientType(Transform ingredient) {
        if (ingredient.tag == "Sauce" || ingredient.tag == "Cheese") {
            return ingredient.tag;
        } else if (ingredient.GetComponent<Ingredient>() != null) {
            return "Topping";
        }
        return null;
    }
}
