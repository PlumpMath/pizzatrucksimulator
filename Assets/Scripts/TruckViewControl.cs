using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckViewControl : MonoBehaviour {


    void Start()
    {

        MarketUI.brotcast += this.HotPants;

    }

   void  HotPants(GameObject whatever)
    {
        print("eh?");
    }
}
