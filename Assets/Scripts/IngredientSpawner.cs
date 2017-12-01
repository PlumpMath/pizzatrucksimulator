using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one IngredientSpawner instance!");
            return;
        }
        Instance = this;
        pizzaTruck = PizzaTruck.Instance;
    }
    #endregion

    public void SpawnTruckIngredients() {
        SpawnDough();

        int baseIngredientQuantity = 1;
        for (int i = 0; i < baseIngredientQuantity; i++) {
            Instantiate(saucePrefab, sauceSpawnpoint.position + Vector3.up * 0.15f * i, Quaternion.Euler(-90, 0, 0), sauceSpawnpoint);
            Instantiate(cheesePrefab, cheeseSpawnpoint.position + Vector3.up * 0.15f * i, Quaternion.Euler(-90, 0, 0), cheeseSpawnpoint);
        }

        int spawnIndex = 0;
        int toppingQuantity = 1;

        foreach (IngredientBundle ingredientBundle in pizzaTruck.ingredientList) {
            for (int i = 0; i < toppingQuantity; i++) {
                Instantiate(
                    ingredientBundle.ingredient, // prefab
                    ingredientSpawningPoints[spawnIndex].position + Vector3.up * .15f * i, //position 
                    Quaternion.Euler(-90, 0, 0), // rotation
                    ingredientSpawningPoints[spawnIndex] // parent
                    
                );
                GameObject label = ingredientBundle.ingredient.label;
                
                if (label != null)
                {
                 
                label.GetComponent<Text>().enabled =false;
                }
            }
            spawnIndex++;
        }
    }

    public void SpawnDough() {
        Instantiate(doughPrefab, doughSpawnpoint);
    }

    void Start() {

    }

    void Update() {

    }
}
