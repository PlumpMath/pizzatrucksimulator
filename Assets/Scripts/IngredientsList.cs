using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredients List", menuName = "Assets/Create/Ingredients List")]
public class IngredientsList : ScriptableObject {
	public List<Ingredient> ingredientList;
}
