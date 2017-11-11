using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour {

public Ingredient[] ingredients;

public GameObject viewportShop;
public GameObject viewportTruck;

public GameObject ItemChoicePrefab;

List<int> usedValues = new List<int>();

// learning events or something


public delegate void BroadcastEvent(GameObject yeah);

public static event BroadcastEvent brotcast;



//


    // Use this for initialization
    void Start () {

        GameObject ItemChoicePlaceholder = GameObject.Find("ItemChoicePrefab");
		ItemChoicePlaceholder.SetActive(false);

		ListIngredients();  //lists all ingredients



	}
	

	public void ListIngredients(){

		for (int i = 0; i <= 1; i++)
		{
			GameObject newItemChoiceObject = Instantiate(ItemChoicePrefab, Vector3.zero, Quaternion.identity);
			newItemChoiceObject.transform.SetParent(viewportShop.transform);
			newItemChoiceObject.transform.localScale = Vector3.one; 
			ItemChoice newItemChoice = newItemChoiceObject.GetComponent<ItemChoice>();
			Ingredient ingredient = ingredients[i];
			newItemChoice.SetIcon(ingredient.icon);
			newItemChoice.SetDescription(ingredient.description);
			newItemChoice.SetTitle(ingredient.title);
            newItemChoice.SetFreshness(false);

            DoIt(newItemChoiceObject);
        }

        for (int i = 2; i <= 7; i++)
		{
			GameObject newItemChoiceObject = Instantiate(ItemChoicePrefab, Vector3.zero, Quaternion.identity);
			newItemChoiceObject.transform.SetParent(viewportShop.transform);
			newItemChoiceObject.transform.localScale = Vector3.one; 
			ItemChoice newItemChoice = newItemChoiceObject.GetComponent<ItemChoice>();
			Ingredient ingredient = ingredients[UniqueRandomInt(2,10)];
			newItemChoice.SetIcon(ingredient.icon);
			newItemChoice.SetDescription(ingredient.description);
			newItemChoice.SetTitle(ingredient.title);
            newItemChoice.SetFreshness(true);

            DoIt(newItemChoiceObject);


        }
	}

    void HotPants(GameObject obj)
    {
       ///
    }

   void TruckTransfer(GameObject obj)
    {
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
