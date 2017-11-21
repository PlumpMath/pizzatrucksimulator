using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {
    public Transform[] ingredientSpawningPoints;

    public Transform doughPrefab;
    public Transform doughSpawnpoint;
    public Transform saucePrefab;
    public Transform sauceSpawnpoint;
    public Transform cheesePrefab;
    public Transform cheeseSpawnpoint;

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
        // For now, just spawn 1 dough, 4 sauce, 4 cheese
        Instantiate(doughPrefab, doughSpawnpoint);
        for (int i = 0; i < 4; i++) {
            Instantiate(saucePrefab, sauceSpawnpoint.position + Vector3.up * 0.15f * i, Quaternion.Euler(-90,0,0), sauceSpawnpoint);
            Instantiate(cheesePrefab, cheeseSpawnpoint.position + Vector3.up * 0.15f * i, Quaternion.Euler(-90,0,0), cheeseSpawnpoint);
        }

        int spawnIndex = 0;
        foreach (IngredientBundle ingredientBundle in pizzaTruck.ingredientList) {

            for (int i = 0; i < ingredientBundle.quantity; i++)
            {
                Instantiate(
                    ingredientBundle.ingredient, // prefab
                    ingredientSpawningPoints[spawnIndex].position + Vector3.up * .15f * i, //position 
                    Quaternion.Euler(-90,0,0), // rotation
                    ingredientSpawningPoints[spawnIndex] // parent
                );
            }
            spawnIndex++;
        }
    }

	void Start () {
		
	}
	
	void Update () {
		
	}
}
