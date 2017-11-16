using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[CreateAssetMenu(fileName = "Pizza Truck", menuName = "Assets/Create/Pizza Truck")]
public class PizzaTruck : SingletonScriptableObject<PizzaTruck> {
	public List<IngredientBundle> baseIngredientList;
	public List<IngredientBundle> ingredientList;

	public List<TruckUpgrade> upgradeList;
	public enum TruckLocation {School, Park, HipsterStore, OfficeDistrict, RetirementHome}
	public int ingredientLimit = 4;

	public void Init() {
		List<IngredientBundle> masterIngredientsList = MasterIngredientsList.Instance.list;
        baseIngredientList.Clear();
        AddBaseIngredient(masterIngredientsList[0]); // Add Dough
        AddBaseIngredient(masterIngredientsList[1]); // Add Sauce
        AddBaseIngredient(masterIngredientsList[2]); // Add Cheese
		ingredientList.Clear();
    }

	public void AddIngredient (IngredientBundle ingredientBundle) {
		ingredientList.Add(ingredientBundle);
	}

	public void AddBaseIngredient (IngredientBundle ingredientBundle) {
		baseIngredientList.Add(ingredientBundle);
	}

	public void RemoveIngredient (IngredientBundle ingredientBundle) {
		ingredientList.Remove(ingredientBundle);
	}

	public bool HasIngredientSpace {
		get { return ingredientList.Count < ingredientLimit; }
	}
}