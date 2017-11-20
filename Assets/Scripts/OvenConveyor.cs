using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenConveyor : MonoBehaviour {


    public GameObject placedPizza;
    public GameObject destinationPoint;
    public float conveyorSpeed;
    public bool placed = false;
    public bool cooked = false;
    public float distance;


    void Update () {

        if (placed == true && cooked == false)
        {
            placedPizza.transform.position = Vector3.MoveTowards(placedPizza.transform.position,
                destinationPoint.transform.position,
                conveyorSpeed * Time.deltaTime);

            if (Vector3.Distance(placedPizza.transform.position, destinationPoint.transform.position) < .01f)
                {
                cooked = true;
                placedPizza.GetComponent<Pizza>().cooked = true;
                placedPizza.GetComponent<BoxCollider>().enabled = true;
                }
             
        }
    }


}
