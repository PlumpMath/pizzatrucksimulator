using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController firstPersonController;
    public float reputation = 0f;
    public float money = 0f;
    public Queue<Ingredient> marketIngredientsDeck;
    public IngredientsList masterIngredientsList;

    PizzaTruck pizzaTruck;
    Turn currentTurn;
    public int numberOfTurnsInGame = 7;
    int currentTurnNumber = 0;

    #region Singleton
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("More than one GameManager instance!");
            return;
        }
        Instance = this;
    }
    #endregion

    void Start()
    {
        print("GameManager Start()");
		firstPersonController.enabled = false;
        SetupMarketDeck();
        SetupPizzaTruck();
        NewTurn();
    }

    void Update()
    {
    }

    void SetupMarketDeck()
    {
        List<Ingredient> shuffledList = new List<Ingredient>();
        int ingredientQuantity = 4;
        foreach (Ingredient ingredient in masterIngredientsList.ingredientList)
        {
            if (!ingredient.isBaseIngredient)
            {
                for (int i = 0; i < ingredientQuantity; i++)
                {
                    shuffledList.Add(ingredient);
                }
            }
        }

        Shuffle(shuffledList);

        marketIngredientsDeck = new Queue<Ingredient>(shuffledList.ToArray());

        // Debug.Log("--- Market Deck ---");
        // for(int i = 0; i < shuffledList.Count; i++) {
        // 	Debug.Log(i + " " + shuffledList[i].title);
        // }
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void SetupPizzaTruck()
    {
        pizzaTruck = PizzaTruck.Instance;
        pizzaTruck.ingredientList.Clear();
        pizzaTruck.AddIngredient(masterIngredientsList.ingredientList[0]); // Add Dough
        pizzaTruck.AddIngredient(masterIngredientsList.ingredientList[1]); // Add Sauce
        pizzaTruck.AddIngredient(masterIngredientsList.ingredientList[2]); // Add Cheese
    }

    void NewTurn()
    {
        currentTurnNumber++;
        print("GameManager NewTurn() " + currentTurnNumber);
        GameObject currentTurnObject = new GameObject();
        currentTurnObject.name = "Turn " + currentTurnNumber;
        currentTurnObject.transform.SetParent(transform);
        currentTurn = currentTurnObject.gameObject.AddComponent<Turn>();
        currentTurn.OnTurnFinish += MoveToNextTurn;
        currentTurn.Begin();
    }

    void MoveToNextTurn()
    {
        print("GameManager MoveToNextTurn()");
        if (currentTurnNumber == numberOfTurnsInGame)
        {
            EndGame();
        }
        else
        {
            NewTurn();
        }
    }

    void EndGame()
    {
        print("GameManager EndGame()");
    }
}
