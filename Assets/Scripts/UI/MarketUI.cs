using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour {

public Ingredient[] marketIngredients;
public GameObject viewportShop;
public GameObject viewportTruck;
public GameObject ItemChoicePrefab;
public GameObject gameManagerObject;

public int itemsPerTurn = 4;

public GameManager gameManager;
PizzaTruck pizzaTruck;

Button marketUIButton;

List<int> usedValues = new List<int>();

    TruckControl truck;
    GameObject shop;
// events exploration

public delegate void BroadcastEvent(GameObject yeah);
public static event BroadcastEvent brotcast;

    
    void Start () {
	}

    public void Begin() {
        marketUIButton = this.GetComponentInChildren<Button>();
        truck = GameObject.Find("TruckUI").GetComponent<TruckControl>();
        shop = GameObject.Find("ShopUI");

        gameManager = gameManagerObject.GetComponent<GameManager>();
        pizzaTruck = gameManager.pizzaTruck;

        SetupMarketIngredients();
    }

    private void Update()
    {
        if (truck.truckIngredientsCounter < truck.counterLimit) {
            shop.GetComponent<CanvasGroup>().interactable = true;
            marketUIButton.interactable = false;
        } else {
            shop.GetComponent<CanvasGroup>().interactable = false;
            marketUIButton.interactable = true;
        }
    }

    void SetupMarketIngredients() {
        for (int i = 0; i < itemsPerTurn; i++) {
            // Debug.Log("Market Deck Count: " + gameManager.marketIngredientsDeck.Count);
            Ingredient ingredient = gameManager.marketIngredientsDeck.Dequeue();
            // Debug.Log("Adding Market Ingredient: " + ingredient.title);
            AddItemChoice(ingredient, true);
        }
    }

    void AddItemChoice(Ingredient ingredient, bool freshness) {
        GameObject newItemChoiceObject = Instantiate(ItemChoicePrefab, Vector3.zero, Quaternion.identity);
        newItemChoiceObject.transform.SetParent(viewportShop.transform);
        newItemChoiceObject.transform.localScale = Vector3.one;
        
        ItemChoice newItemChoice = newItemChoiceObject.GetComponent<ItemChoice>();

        newItemChoice.pizzaTruck = pizzaTruck;
        newItemChoice.ingredient = ingredient;

        newItemChoice.Setup(freshness);

        DoIt(newItemChoiceObject);
    }

    void DoIt(GameObject obj) {
        if (brotcast != null) {
            brotcast(obj);
        }
    }

    public int UniqueRandomInt(int min, int max)
		{
            int val = Random.Range(min, max);

			while(usedValues.Contains(val))
			{
				 val = Random.Range(min, max);
			}
			usedValues.Add(val);
			return val;
		}
}
