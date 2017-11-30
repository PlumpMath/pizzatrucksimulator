using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenConveyor : MonoBehaviour {

    AudioSource audio;

    public GameObject destinationPoint;
    public float conveyorSpeed;
    [HideInInspector] public bool isCooking = false;
    GameObject placedPizza;

    void Update() {
    }

    public void CookPizza(GameObject pizza) {
        placedPizza = pizza;
        pizza.transform.SetParent(transform.GetChild(0));
        pizza.transform.localPosition = new Vector3(0, 0, 0);
        pizza.transform.localRotation = Quaternion.Euler(-90, 0, 0);

        audio = GetComponent<AudioSource>();
        audio.Play();


        StartCoroutine(MoveOnOven());
    }

    IEnumerator MoveOnOven() {
        isCooking = true;
        
        Pizza pizza = placedPizza.GetComponent<Pizza>();

        while (!pizza.cooked) {
            placedPizza.transform.position = Vector3.MoveTowards(
                                                 placedPizza.transform.position,
                                                 destinationPoint.transform.position,
                                                 conveyorSpeed * Time.deltaTime
                                             );

            bool atDestination = Vector3.Distance(placedPizza.transform.position, destinationPoint.transform.position) < .01f; 
            if (atDestination) {
                pizza.cooked = true;
                placedPizza.GetComponent<BoxCollider>().enabled = true;
                
            }
            yield return null;
    
        }

        isCooking = false;
    }
}
