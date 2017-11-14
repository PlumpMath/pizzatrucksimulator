using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[CreateAssetMenu(fileName = "Pizza Truck", menuName = "Assets/Create/Pizza Truck")]
public class PizzaTruck : SingletonScriptableObject<PizzaTruck> {
	public List<Ingredient> baseIngredientList;
	public List<Ingredient> ingredientList;

	public List<TruckUpgrade> upgradeList;
	public enum TruckLocation {School, Park, HipsterStore, OfficeDistrict, RetirementHome}
	public int ingredientLimit = 4;

	public void Init() {
		IngredientsList masterIngredientsList = GameManager.Instance.masterIngredientsList;
        baseIngredientList.Clear();
        AddBaseIngredient(masterIngredientsList.ingredientList[0]); // Add Dough
        AddBaseIngredient(masterIngredientsList.ingredientList[1]); // Add Sauce
        AddBaseIngredient(masterIngredientsList.ingredientList[2]); // Add Cheese
		ingredientList.Clear();
    }

	public void AddIngredient (Ingredient ingredient) {
		ingredientList.Add(ingredient);
	}

	public void AddBaseIngredient (Ingredient ingredient) {
		baseIngredientList.Add(ingredient);
	}

	public void RemoveIngredient (Ingredient ingredient) {
		ingredientList.Remove(ingredient);
	}

	public bool HasIngredientSpace {
		get { return ingredientList.Count < ingredientLimit; }
	}
}