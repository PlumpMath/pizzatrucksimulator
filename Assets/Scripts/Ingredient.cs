using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {
    public enum Type { Base, Topping }

    public Type type;
    public string Name;
    public int ingredientID;
    public string description;
    public bool isBaseIngredient;
    public Sprite icon;
    public float stackLayer; // where this ingredient pizza sits in the stack
    public GameObject label;
    public int spawnIndex;
    public Pizza pizza;

    void OnCollisionEnter(Collision col) {
        Ingredient ingredient = col.gameObject.GetComponent<Ingredient>();
        if (ingredient != null && ingredient.pizza != null && ingredient.pizza.CanReceiveIngredient(transform)) {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            ingredient.pizza.AddToPizza(transform);
            return;
        }

        Pizza pizza = col.gameObject.GetComponent<Pizza>();
        if (pizza != null && pizza.CanReceiveIngredient(transform)) {
            float magnitude = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            float pizzaMagnitude = pizza.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            pizza.AddToPizza(transform);
        }

    }
}