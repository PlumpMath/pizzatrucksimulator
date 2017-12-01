using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController firstPersonController;
    // public float reputation = 0f;
    // public float money = 0f;
    public Queue<IngredientBundle> marketIngredientsDeck;

    public float timePerGame = 60f;
    public int numberOfTurnsInGame = 7;
    public List<Ingredient> availableToppings;

    public Turn currentTurn;

    public bool debugMode;

    int currentTurnNumber = 0;

    #region Singleton
    public static GameManager Instance;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one GameManager instance!");
            return;
        }
        Instance = this;
        firstPersonController.enabled = false;
        IngredientDatabase.LoadDatabase();
        // SetupMarketDeck();
        PizzaTruck.Instance.Init();
    }
    #endregion

    void Start() {
        print("GameManager Start()");
        NewTurn();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void SetupMarketDeck() {
        List<IngredientBundle> shuffledList = new List<IngredientBundle>();
        int ingredientQuantity = 4;
        foreach (Ingredient ingredient in IngredientDatabase.GetList()) {
            IngredientBundle ingredientBundle = new IngredientBundle(ingredient, ingredientQuantity);
            ingredientBundle.quantity = ingredientQuantity;
            shuffledList.Add(ingredientBundle);
        }
        Shuffle(shuffledList);
        marketIngredientsDeck = new Queue<IngredientBundle>(shuffledList.ToArray());
    }

    void Shuffle<T>(List<T> list) {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void NewTurn() {
        currentTurnNumber++;
        print("GameManager NewTurn() " + currentTurnNumber);
        GameObject currentTurnObject = new GameObject();
        currentTurnObject.name = "Turn " + currentTurnNumber;
        currentTurnObject.transform.SetParent(transform);

        currentTurn = currentTurnObject.gameObject.AddComponent<Turn>();
        currentTurn.turnNumber = currentTurnNumber;

        currentTurn.OnTurnFinish += MoveToNextTurn;
        currentTurn.Begin();
    }

    void MoveToNextTurn() {
        print("GameManager MoveToNextTurn()");
        if (currentTurnNumber == numberOfTurnsInGame) {
            EndGame();
        } else {
            NewTurn();
        }
    }

    void EndGame() {
        print("GameManager EndGame()");
    }

    public void DelayedReplenish(Transform t) {
        Vector3 position = t.position;
        Quaternion rotation = t.rotation;
        StartCoroutine(ReplenishObject(t, position, rotation));
    }

    IEnumerator ReplenishObject(Transform t, Vector3 position, Quaternion rotation) {
        yield return new WaitForSeconds(3f);
        Instantiate(t, position, rotation);
    }
}
