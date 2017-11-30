using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    public enum Type { Teenager, Hipster, Professional, Senior }
    public Type type;
    public List<Ingredient> ingredientNeeds;

    public Canvas textCanvas;
    public UnityEngine.UI.Text textObject;


    static System.Random rng = new System.Random();

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

    void Start() {
        SetNeeds();
        ShowNeeds();
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

    void SetNeeds() {
        Debug.Log("SetNeeds()");
        int numToppings = Customer.rng.Next(1,3);

        for (int i = 0; i < numToppings; i++) {
            Ingredient randomTopping = PizzaTruck.Instance.GetRandomIngredient();
            while(ingredientNeeds.Contains(randomTopping)) {
                randomTopping = PizzaTruck.Instance.GetRandomIngredient();
            }
            ingredientNeeds.Add(randomTopping);
            Debug.Log("Adding Topping: " + randomTopping.Name);
        }
    }

    void ShowNeeds() {
        textCanvas.transform.localPosition = new Vector3(0f,2f,0f);
        List<string> ingredientNames = new List<string>();

        foreach(Ingredient ingredient in ingredientNeeds) {
            ingredientNames.Add(ingredient.Name);
        }

        textObject.text = string.Join("\n", ingredientNames.ToArray());
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
