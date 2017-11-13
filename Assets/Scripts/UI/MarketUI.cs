using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour {

public GameObject viewportShop;
public GameObject ItemChoicePrefab;

public List<Ingredient> marketList;

public PizzaTruck pizzaTruck;

public Transform marketIngredientsArea;
public Transform truckIngredientsArea;

CanvasGroup marketIngredientsAreaCanvas;

public Button nextPhaseButton;

GameObject shop;

    public void Begin() {
        marketIngredientsAreaCanvas = marketIngredientsArea.GetComponent<CanvasGroup>();
        AddItemChoices();
    }

    private void Update() {
        marketIngredientsAreaCanvas.interactable = pizzaTruck.HasIngredientSpace;
        nextPhaseButton.interactable = !pizzaTruck.HasIngredientSpace;
    }

    void AddItemChoices() {
        foreach (Ingredient ingredient in marketList) {
            AddItemChoice(ingredient);
        }
    }

    void AddItemChoice(Ingredient ingredient) {
        GameObject newItemChoiceObject = Instantiate(ItemChoicePrefab, Vector3.zero, Quaternion.identity);
        newItemChoiceObject.transform.SetParent(viewportShop.transform);
        newItemChoiceObject.transform.localScale = Vector3.one;
        
        ItemChoice newItemChoice = newItemChoiceObject.GetComponent<ItemChoice>();

        newItemChoice.pizzaTruck = pizzaTruck;
        newItemChoice.ingredient = ingredient;
        newItemChoice.marketIngredientsArea = marketIngredientsArea;
        newItemChoice.truckIngredientsArea = truckIngredientsArea;

        newItemChoice.Setup(true);
    }
}