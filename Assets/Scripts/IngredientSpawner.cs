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

    public List<Transform> toppingPrefabs;

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

        AddSauce();
        AddCheese();

        int spawnIndex = 0;
        int toppingQuantity = 1;

        foreach (IngredientBundle ingredientBundle in pizzaTruck.ingredientList) {
            for (int i = 0; i < toppingQuantity; i++) {
                ingredientBundle.ingredient.spawnIndex = spawnIndex;
                AddIngredient(ingredientBundle.ingredient);
            }
            spawnIndex++;
        }
    }

    public void SpawnDough() {
        Instantiate(doughPrefab, doughSpawnpoint);
    }

    public void AddSauce() {
        Instantiate(saucePrefab, sauceSpawnpoint.position + Vector3.up, Quaternion.Euler(-90, 0, 0), sauceSpawnpoint);
    }

    public void AddCheese() {
        Instantiate(cheesePrefab, cheeseSpawnpoint.position + Vector3.up * 0.15f, Quaternion.Euler(-90, 0, 0), cheeseSpawnpoint);
    }

    public void AddIngredient(Ingredient ingredient) {
        Transform prefab = toppingPrefabs.Find(t => t.GetComponent<Ingredient>().ingredientID == ingredient.ingredientID);
        Instantiate(
            prefab, // prefab
            ingredientSpawningPoints[ingredient.spawnIndex].position + Vector3.up * .15f, //position 
            Quaternion.Euler(-90, 0, 0), // rotation
            ingredientSpawningPoints[ingredient.spawnIndex] // parent
        );
        GameObject label = ingredient.label;
        if (label != null) {
            label.GetComponent<Text>().enabled = false;
        }
    }

    void Start() {

    }

    void Update() {

    }
}
