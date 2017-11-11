using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour {

public Ingredient[] ingredients;

public GameObject viewportShop;
public GameObject viewportTruck;

public GameObject ItemChoicePrefab;

public GameObject truckView;
List<int> usedValues = new List<int>();

// learning events or something


public delegate void BroadcastEvent(GameObject yeah);

public static event BroadcastEvent brotcast;



//


    // Use this for initialization
    void Start () {

        GameObject ItemChoicePlaceholder = GameObject.Find("ItemChoicePrefab");
		ItemChoicePlaceholder.SetActive(false);

		ListIngredients(0, 1, false, false);  //lists first ingredients ingredients
        ListIngredients(2, 7, true, true);
	}


    public void ListIngredients(int min, int max, bool random, bool freshness)
    {

        for (int i = min; i <= max; i++)
        {
            Ingredient ingredient = null;
            GameObject newItemChoiceObject = Instantiate(ItemChoicePrefab, Vector3.zero, Quaternion.identity);
            newItemChoiceObject.transform.SetParent(viewportShop.transform);
            newItemChoiceObject.transform.localScale = Vector3.one;
            ItemChoice newItemChoice = newItemChoiceObject.GetComponent<ItemChoice>();
            if (random == false)
            {
                ingredient = ingredients[i];
            }
            if (random == true)
            {
                ingredient = ingredients[UniqueRandomInt(min, ingredients.Length)];
            }

            newItemChoice.SetIcon(ingredient.icon);
            newItemChoice.SetDescription(ingredient.description);
            newItemChoice.SetTitle(ingredient.title);
            newItemChoice.SetFreshness(freshness);

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
