using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChoice : MonoBehaviour {



	public GameObject icon;
	public GameObject description;
	public GameObject freshness;
	public GameObject title;

    public Button thisButton;

    public bool inStore = true;
    
    void Start()
    {
        MarketUI.brotcast += this.StoreDisplay;
        Button btn = thisButton.GetComponent<Button>();
        btn.onClick.AddListener(TruckTransfer);
    }

    void StoreDisplay(GameObject obj)
    {
  //      print("Display ze " + obj.GetComponent<ItemChoice>().title.GetComponent<Text>().text + " to pizza shop");  
    }

    public void SetIcon(Sprite sprite)
	{
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

   public void TruckTransfer()
    {
        TruckControl truck = GameObject.Find("TruckUI").GetComponent<TruckControl>();
        GameObject shop = GameObject.Find("ShopUI"); 
        switch (inStore)
        {
            case true:
                truck.GatheredIngredients.Add(transform.gameObject);
                this.transform.SetParent(truck.transform.GetChild(0));
                truck.truckIngredientsCounter++;
                inStore = false;
                break;
            case false:
                truck.GatheredIngredients.Remove(transform.gameObject);
                this.transform.SetParent(shop.transform.GetChild(0));
                truck.truckIngredientsCounter--;
                inStore = true;
                break;
            default:
                print("hot pants");
                break;
        }
    }
    
}
