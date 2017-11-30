using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PizzaTruck {
    public List<IngredientBundle> baseIngredientList;
    public List<IngredientBundle> ingredientList;

    public List<TruckUpgrade> upgradeList;
    public enum TruckLocation { School, Park, HipsterStore, OfficeDistrict, RetirementHome }
    public int ingredientLimit = 4;

    static PizzaTruck _instance;

    static System.Random rng = new System.Random();

    private PizzaTruck() {
        baseIngredientList = new List<IngredientBundle>();
        ingredientList = new List<IngredientBundle>();
    }

    public static PizzaTruck Instance {
        get {
            if (_instance == null) {
                _instance = new PizzaTruck();
            }
            return _instance;
        }
    }

    public void Init() {
		// Add one of each ingredient to the truck's ingredient list
        int ingredientQuantity = 1;
        foreach (Ingredient ingredient in IngredientDatabase.GetList()) {
            IngredientBundle ingredientBundle = new IngredientBundle(ingredient, ingredientQuantity);
            ingredientBundle.quantity = ingredientQuantity;
			AddIngredient(ingredientBundle);
        }

        // Add Sauce
        // AddBaseIngredient(
        // 	new IngredientBundle(IngredientDatabase.GetIngredient(0), 4)
        // ); 

        // Add Cheese

        // AddBaseIngredient(
        // 	new IngredientBundle(IngredientDatabase.GetIngredient(0), 4)
        // ); 
    }

    public void AddIngredient(IngredientBundle ingredientBundle) {
        ingredientList.Add(ingredientBundle);
    }

    public void AddBaseIngredient(IngredientBundle ingredientBundle) {
        baseIngredientList.Add(ingredientBundle);
    }

    public void RemoveIngredient(IngredientBundle ingredientBundle) {
        ingredientList.Remove(ingredientBundle);
    }

    public Ingredient GetRandomIngredient() {
        int r = PizzaTruck.rng.Next(0, ingredientList.Count - 1);
        return ingredientList[r].ingredient;
    }

    public bool HasIngredientSpace {
        get { return ingredientList.Count < ingredientLimit; }
    }
}