using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour {

public Ingredient[] ingredients;
public GameObject viewportShop;
public GameObject viewportTruck;
public GameObject ItemChoicePrefab;
Button marketUIButton;
public GameObject truckView;
List<int> usedValues = new List<int>();

    TruckControl truck;
    GameObject shop;
// events exploration

public delegate void BroadcastEvent(GameObject yeah);
public static event BroadcastEvent brotcast;
    
    void Start () {

        marketUIButton = this.GetComponentInChildren<Button>();
        GameObject ItemChoicePlaceholder = GameObject.Find("ItemChoicePrefab");
		ItemChoicePlaceholder.SetActive(false);
        truck = GameObject.Find("TruckUI").GetComponent<TruckControl>();
        shop = GameObject.Find("ShopUI");
        ListIngredients(0, 1, true, false);  //lists first ingredients ingredients
        ListIngredients(2, 7, false, true);
	}

    private void Update()
    {
        if (truck.truckIngredientsCounter < truck.counterLimit)
        {
            shop.GetComponent<CanvasGroup>().interactable = true;
            marketUIButton.interactable = false;
        }
        else
        {
            shop.GetComponent<CanvasGroup>().interactable = false;
            marketUIButton.interactable = true;
        }
    }

    public void ListIngredients(int min, int max, bool preloaded, bool freshness)
    {

        for (int i = min; i <= max; i++)
        {
            Ingredient ingredient = null;
            GameObject newItemChoiceObject = Instantiate(ItemChoicePrefab, Vector3.zero, Quaternion.identity);
            newItemChoiceObject.transform.SetParent(viewportShop.transform);
            newItemChoiceObject.transform.localScale = Vector3.one;
            ItemChoice newItemChoice = newItemChoiceObject.GetComponent<ItemChoice>();
            if (preloaded == true)
            {
                ingredient = ingredients[i];
            }
            if (preloaded == false)
            {
                ingredient = ingredients[UniqueRandomInt(min, ingredients.Length)];
            }

            newItemChoice.SetIcon(ingredient.icon);
            newItemChoice.SetDescription(ingredient.description);
            newItemChoice.SetTitle(ingredient.title);
            newItemChoiceObject.name = ingredient.title;
            newItemChoice.SetFreshness(freshness);

            if (preloaded == true)
            {
                newItemChoice.TruckTransfer();
                newItemChoice.GetComponentInChildren<Button>().interactable = false;
            }
            DoIt(newItemChoiceObject);
        }
    }

    void StoreDisplay(GameObject obj){
    }



    void DoIt(GameObject obj)
    {
        if (brotcast != null)
        {
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
