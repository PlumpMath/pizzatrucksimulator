using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour {

public GameObject viewportShop;
public GameObject ItemChoicePrefab;

public List<IngredientBundle> marketList;

public Transform marketIngredientsArea;
public Transform truckIngredientsArea;

CanvasGroup marketIngredientsAreaCanvas;

public Button nextPhaseButton;

PizzaTruck pizzaTruck;

GameObject shop;

    public void Begin() {
        pizzaTruck = PizzaTruck.Instance;
        marketIngredientsAreaCanvas = marketIngredientsArea.GetComponent<CanvasGroup>();
        AddItemChoices();
    }

    private void Update() {
        marketIngredientsAreaCanvas.interactable = pizzaTruck.HasIngredientSpace;
        nextPhaseButton.interactable = !pizzaTruck.HasIngredientSpace;
    }

    void AddItemChoices() {
        foreach (IngredientBundle ingredientBundle in marketList) {
            AddItemChoice(ingredientBundle);
        }
    }

    void AddItemChoice(IngredientBundle ingredient) {
        GameObject newItemChoiceObject = Instantiate(ItemChoicePrefab, Vector3.zero, Quaternion.identity);
        newItemChoiceObject.transform.SetParent(viewportShop.transform);
        newItemChoiceObject.transform.localScale = Vector3.one;
        
        ItemChoice newItemChoice = newItemChoiceObject.GetComponent<ItemChoice>();

        newItemChoice.ingredientBundle = ingredient;
        newItemChoice.marketIngredientsArea = marketIngredientsArea;
        newItemChoice.truckIngredientsArea = truckIngredientsArea;

        newItemChoice.Setup(true);
    }
}