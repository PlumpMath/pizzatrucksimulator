using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    public enum Type { Teenager, Hipster, Professional, Senior }
    public Type type;
    public List<Ingredient> ingredientNeeds;

    public Canvas textCanvas;
    public UnityEngine.UI.Text textObject;

    public Transform explosionPrefab;

    public AudioClip pizzaSuccessAudio;
    public AudioClip pizzaLaunch;
    public AudioClip sadEater;

    public event System.Action OnPizzaReceive;
    public event System.Action OnSuccess;
    public event System.Action OnFailure;

    static System.Random rng = new System.Random();

    float satisfactionLevel;
    float freshnessBias;
    float priceBias;

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    CharacterAnimator characterAnimator;
    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsuleCollider;

    Pizza pizza;

    public Customer(Type _type) {
        type = _type;
    }

    void Awake() {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterAnimator = GetComponent<CharacterAnimator>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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

    public void OnPizzaHit(Pizza _pizza) {
        pizza = _pizza;
        bool matchesNeeds = CheckNeeds();

        PrepForAnimation();

        if (matchesNeeds) {
            PizzaSuccess();
        } else {
            PizzaFailure();
        }

        if (OnPizzaReceive != null) {
            OnPizzaReceive();
        }
    }

    bool CheckNeeds() {
        int needsFulfilled = 0;

        foreach (Ingredient ingredient in pizza.ingredientsList) {
            if (ingredientNeeds.Find(x => x.ingredientID == ingredient.ingredientID) != null) {
                needsFulfilled += 1;
            } else {
                needsFulfilled -= 1;
            }
        }

        return needsFulfilled == ingredientNeeds.Count;
    }

    void SetNeeds() {
        int numToppings = Customer.rng.Next(1, 3);

        for (int i = 0; i < numToppings; i++) {
            Ingredient randomTopping = PizzaTruck.Instance.GetRandomIngredient();
            while (ingredientNeeds.Contains(randomTopping)) {
                randomTopping = PizzaTruck.Instance.GetRandomIngredient();
            }
            ingredientNeeds.Add(randomTopping);
        }
    }

    void ShowNeeds() {
        textCanvas.transform.localPosition = new Vector3(0f, 2f, 0f);
        List<string> ingredientNames = new List<string>();

        foreach (Ingredient ingredient in ingredientNeeds) {
            ingredientNames.Add(ingredient.Name);
        }

        textObject.text = string.Join("\n", ingredientNames.ToArray());
    }

    void PizzaSuccess() {
        GetComponent<AudioSource>().clip = pizzaSuccessAudio;
        GetComponent<AudioSource>().pitch = Random.Range(.8f,1.1f);
        GetComponent<AudioSource>().Play();

        StartCoroutine(HappyLaunch());
    }

    IEnumerator HappyLaunch() {
        float customerRotation = 0f;
        pizza.transform.parent = transform;
        pizza.transform.localPosition = new Vector3(0, 1.48f, 0.45f);
        pizza.transform.rotation = Quaternion.Euler(-90, 0, 0);
        pizza.GetComponent<BoxCollider>().enabled = false;
        pizza.GetComponent<Rigidbody>().isKinematic = true;

        transform.rotation = Quaternion.identity;
        rigidBody.isKinematic = true;
        rigidBody.drag = 0;
        rigidBody.angularDrag = 0;

        while (customerRotation < 1080f) {
            transform.Rotate(0, 30, 0);
            customerRotation += 30;
            yield return null;
        }

        foreach (Rigidbody ragdollRigidbody in characterAnimator.ragdollRigidbodies) {
            ragdollRigidbody.isKinematic = false;
            ragdollRigidbody.mass = .05f;
            ragdollRigidbody.drag = 0f;
            ragdollRigidbody.angularDrag = 0f;
            ragdollRigidbody.useGravity = false;
            ragdollRigidbody.AddForce(0, 10f, 0, ForceMode.VelocityChange);
        }

        pizza.GetComponent<Rigidbody>().isKinematic = false;
        pizza.GetComponent<Rigidbody>().AddForce(0, 10f, 0, ForceMode.VelocityChange);

        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = pizzaLaunch;
        audio.pitch = Random.Range(.8f, 1.5f);
        audio.Play();

        Transform explosion = Instantiate(explosionPrefab, transform.position - Vector3.down, Quaternion.identity);
        Destroy(explosion.gameObject, 1f);

        // Destroy(pizza.gameObject);
        Destroy(gameObject, 1f);

        if (OnSuccess != null) {
            OnSuccess();
        }
    }


    void PrepForAnimation() {
        textObject.text = "";
        textCanvas.enabled = false;

        animator.enabled = false;
        Vector3 currentPosition = transform.position;
        navMeshAgent.enabled = false;
        transform.position = currentPosition;

    }


    void PizzaFailure() {
         GetComponent<AudioSource>().clip=sadEater;
         GetComponent<AudioSource>().pitch = Random.Range(.8f,1.3f);
        GetComponent<AudioSource>().Play();
        foreach(Rigidbody ragdollRigidbody in characterAnimator.ragdollRigidbodies) {

            ragdollRigidbody.isKinematic = false;
            ragdollRigidbody.mass = .05f;
            ragdollRigidbody.drag = 0f;
            ragdollRigidbody.angularDrag = 0f;
        }

        Destroy(gameObject, 3f);
        Destroy(pizza.gameObject, 3f);

        if (OnFailure != null) {
            OnFailure();
        }
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
