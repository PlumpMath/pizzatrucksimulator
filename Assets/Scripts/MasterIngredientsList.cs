using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Master Ingredients List", menuName = "Assets/Create/Master Ingredients List")]
public class MasterIngredientsList : SingletonScriptableObject<MasterIngredientsList> {
	public List<IngredientBundle> list;
}
