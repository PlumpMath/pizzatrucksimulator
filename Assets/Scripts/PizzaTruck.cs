using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[CreateAssetMenu(fileName = "Pizza Truck", menuName = "Assets/Create/Pizza Truck")]
public class PizzaTruck : ScriptableObject {
	public List<Ingredient> ingredientList;
	public List<TruckUpgrade> upgradeList;
	public enum TruckLocation {School, Park, HipsterStore, OfficeDistrict, RetirementHome}

	public void AddIngredient (Ingredient ingredient) {
		ingredientList.Add(ingredient);
	}

	public void RemoveIngredient (Ingredient ingredient) {
		ingredientList.Remove(ingredient);
	}
}