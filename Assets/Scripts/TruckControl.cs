using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckControl : MonoBehaviour {

    public List<GameObject> GatheredIngredients;

    public int truckIngredientsCounter = 0;

    public int counterLimit = 5;

    TruckControl truck;
    GameObject shop;
    Button MarketButtonEnd;

    void Start()
    {
        MarketUI.brotcast += this.DoIt;
    }

    void  DoIt(GameObject whatever)
    {
    //    print("yoooooooo?" + whatever);
    }

}
