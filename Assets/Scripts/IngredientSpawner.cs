using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {
    public Transform[] baseIngredientSpawningPoints;
    public Transform[] ingredientSpawningPoints;
    #region Singleton
    public static IngredientSpawner Instance;

    PizzaTruck pizzaTruck;

    void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("More than one IngredientSpawner instance!");
            return;
        }
        Instance = this;
        pizzaTruck = PizzaTruck.Instance;
    }
    #endregion

    public void SpawnTruckIngredients() {
        foreach (Ingredient ingredient in pizzaTruck.baseIngredientList) {
            Transform newIngredientObject = Instantiate(ingredient.ingredientObject, ingredient.spawnPoint);
            newIngredientObject.SetParent(ingredient.spawnPoint);
        }

        int spawnIndex = 0;
        foreach (Ingredient ingredient in pizzaTruck.ingredientList) {

            for (int i = 0; i < ingredient.quantity; i++)
            {
                Transform newIngredientObject = Instantiate(ingredient.ingredientObject, ingredientSpawningPoints[spawnIndex].position+Vector3.up*.3f*i, Quaternion.identity);
                newIngredientObject.SetParent(ingredientSpawningPoints[spawnIndex]);

            }
            spawnIndex++;

        }
    }
	void Start () {
		
	}
	
	void Update () {
		
	}
}
