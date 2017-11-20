using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDatabase {
    static private List<Ingredient> ingredientList;
    static private bool isDatabaseLoaded;

    static public void LoadDatabase() {
        if (isDatabaseLoaded) return;
        isDatabaseLoaded = true;
        _LoadDatabase();
    }

    static void _LoadDatabase() {
        if (ingredientList == null) {
            ingredientList = new List<Ingredient>(); 
        }
        foreach (Ingredient ingredient in GameManager.Instance.availableToppings) {
            if (!ingredientList.Contains(ingredient)) {
                int i = ingredient.ingredientID;
                ingredientList.Add(ingredient);
            }
        }
    }

    static public void ClearDatabase() {
        isDatabaseLoaded = false;
        ingredientList.Clear();
    }

    static public Ingredient GetIngredient(int id) {
        return ingredientList.Find(i => i.ingredientID == id);
    }

    static public List<Ingredient> GetList() {
        return ingredientList;
    }

}