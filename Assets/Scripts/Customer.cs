using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    public enum Type { Teenager, Hipster, Professional, Senior }
    public Type type;
    public List<Ingredient> ingredientNeeds;

    float satisfactionLevel;
    float freshnessBias;
    float priceBias;

    UnityEngine.AI.NavMeshAgent navMeshAgent;

    public Customer(Type _type) {
        type = _type;
    }

    void Awake() {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void SetDestination(Vector3 destination) {
        navMeshAgent.destination = destination;
    }

    public int[] PreferredToppings() {
        switch (type) {
            case Type.Teenager:
                return new int[] { 7 };
            case Type.Hipster:
                return new int[] { 3 };
            case Type.Professional:
                return new int[] { 11 };
            case Type.Senior:
                return new int[] { 6 };
            default:
                return null;
        }
    }

    public void OnPizzaHit(Pizza pizza) {

    }
    // Topping IDs:
    // 2  Arugula
    // 3  Broccoli
    // 4  Chicken Cutlet
    // 5  Mushroom
    // 6  Olives
    // 7  Pepperoni
    // 8  Peppers
    // 9  Pineapple
    // 10 Sausage
    // 11 Sun Dried Tomatoes
}
