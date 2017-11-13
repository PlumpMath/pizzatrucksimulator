using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChoice : MonoBehaviour {



	public GameObject icon;
	public GameObject description;
	public GameObject freshness;
	public GameObject title;

    public Button button;

    public bool inStore = true;

    public PizzaTruck pizzaTruck;
    public Ingredient ingredient;

    TruckControl truck;
    GameObject shop;

    
    
    void Start()
    {
        truck = GameObject.Find("TruckUI").GetComponent<TruckControl>();
        shop = GameObject.Find("ShopUI");

        MarketUI.brotcast += this.StoreDisplay;

        button.onClick.AddListener(AddToTruck);
    }

    public void Setup(bool freshness) {
        gameObject.name = ingredient.title;
        SetIcon(ingredient.icon);
        SetDescription(ingredient.description);
        SetTitle(ingredient.title);
        SetFreshness(freshness);
    }

    void StoreDisplay(GameObject obj)
    {
  //      print("Display ze " + obj.GetComponent<ItemChoice>().title.GetComponent<Text>().text + " to pizza shop");  
    }

    public void SetIcon(Sprite sprite){
		icon.GetComponent<Image>().sprite = sprite;
	}

	public void SetDescription(string text)
	{
		description.GetComponent<Text>().text = text.ToString();
	}

	public void SetTitle(string text){

		title.GetComponent<Text>().text = text.ToString();

	}

    public void SetFreshness(bool random)
    {
        if (random == true)
        {
            int i = Random.Range(0, 4);
            if (i == 0)
            {
                freshness.GetComponent<Text>().text = "Almost Spoiled";
            }
            if (i == 1)
            {
                freshness.GetComponent<Text>().text = "Stale";
            }
            if (i == 2)
            {
                freshness.GetComponent<Text>().text = "Past Fresh";
            }
            if (i == 3)
            {
                freshness.GetComponent<Text>().text = "Fresh";
            }
        }
            else
            {
                freshness.GetComponent<Text>().text = "So Fresh";
            }
    }

   public void AddToTruck() {
        this.transform.SetParent(truck.transform.GetChild(0));

        pizzaTruck.AddIngredient(ingredient);

        button.onClick.RemoveListener(AddToTruck);
        button.onClick.AddListener(RemoveFromTruck);

        Debug.Log("--- TRUCK Inventory ---");
        for(int i = 0; i < pizzaTruck.ingredientList.Count; i++) {
            Debug.Log(i+1 + ": " + pizzaTruck.ingredientList[i].title);
        }
    }

    public void RemoveFromTruck() {
        this.transform.SetParent(shop.transform.GetChild(0));
        
        pizzaTruck.RemoveIngredient(ingredient);
        
        button.onClick.AddListener(AddToTruck);
        button.onClick.RemoveListener(RemoveFromTruck);

        Debug.Log("--- TRUCK Inventory ---");
        for(int i = 0; i < pizzaTruck.ingredientList.Count; i++) {
            Debug.Log(i+1 + ": " + pizzaTruck.ingredientList[i].title);
        }
    }
    
}
