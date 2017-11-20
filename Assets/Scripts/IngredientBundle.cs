using System.Collections;
using UnityEngine;

[System.Serializable]
public class IngredientBundle {
	public Ingredient ingredient;
	public int quantity;
	public Transform spawnPoint;
	// public string title;
	// public string description;
	// public bool isBaseIngredient;
	// public Sprite icon;
	// public Transform ingredientObject;  // The prefab for the ingredient

	public IngredientBundle(Ingredient _ingredient, int _quantity) {
		ingredient = _ingredient;
		quantity = _quantity;
	}
}