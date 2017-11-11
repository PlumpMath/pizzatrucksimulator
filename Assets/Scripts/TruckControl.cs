using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckControl : MonoBehaviour {

    public List<GameObject> GatheredIngredients;

    public int truckIngredientsCounter = 0;

    public int counterLimit = 5;

    TruckControl truck;
    GameObject shop;

    void Start()
    {
        MarketUI.brotcast += this.DoIt;
        truck = GameObject.Find("TruckUI").GetComponent<TruckControl>();
        shop = GameObject.Find("ShopUI");

    }

    private void Update()
    {
        if (truckIngredientsCounter < counterLimit)
        {
            shop.GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            shop.GetComponent<CanvasGroup>().interactable = false;

        }
    }



    public void  DoIt(GameObject whatever)
    {
        print("yoooooooo?");
    }

    public void GiveToTruck()
    {

    }

}
