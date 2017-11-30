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

    void Start() {
        doughMesh = GetComponent<SkinnedMeshRenderer>();
    }


    public int Reputation { get; set; }
    // public Customer customer;

    public void RollDough() {
        if (!doughRolled) {
            StartCoroutine(RollOutDough());
            //            blendShapeWeightTarget = 0;
        }
    }

    void OnCollisionEnter(Collision col) {

        CharacterAnimator anim = col.gameObject.GetComponent<CharacterAnimator>();

        if (anim != null)
        {
            anim.MakeRagdoll();
        }
 
    }

    IEnumerator RollOutDough() {
        float blendShapeWeightCurrent = 100;
        float interpolation = 0f;
        Arms.Instance.GetComponent<Animator>().SetTrigger("Roll");
        while (blendShapeWeightCurrent > 0f) {
            blendShapeWeightCurrent = Mathf.Floor(Mathf.Lerp(blendShapeWeightCurrent, 0f, interpolation));
            doughMesh.SetBlendShapeWeight(0, blendShapeWeightCurrent);
            interpolation += Time.deltaTime * .1f;
            yield return null;
        }
        doughRolled = true;
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
