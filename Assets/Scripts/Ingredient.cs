using System.Collections;
using UnityEngine;

[System.Serializable]
public class Ingredient {
	public string title;
	public string description;
	public bool isBaseIngredient;
	public int quantity = 5;
	public Sprite icon;
	public Rigidbody ingredientObject;  // The prefab for the ingredient
}