using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPhase : Phase {
    const int itemsPerTurn = 4;
	GameObject marketUIObject;
	MarketUI marketUI;
	Button marketUIButton;
    TruckControl truck;
    GameObject shop;
	GameManager gameManager;
	List<Ingredient> marketList;

    public override void Begin() {
		print("MarketPhase Begin()");
		base.Begin();

		marketUIObject = GameObject.Find("MarketUI");
		marketUIObject.GetComponent<CanvasGroup>().alpha = 1f;
		marketUIObject.GetComponent<CanvasGroup>().interactable = true;
		marketUIObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        marketUIButton = GameObject.Find("MarketButtonEnd").GetComponent<Button>();
        marketUIButton.onClick.AddListener(End);

		marketUI = marketUIObject.GetComponent<MarketUI>();

		gameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();

		SetupMarketList();

		marketUI.Begin();
	}

    void SetupMarketList() {
        for (int i = 0; i < itemsPerTurn; i++) {
            Ingredient ingredient = gameManager.marketIngredientsDeck.Dequeue();
			print("Adding ingredient: " + ingredient.title);
			marketUI.marketList.Add(ingredient);
        }
    }
	public override void End() {
		print("MarketPhase End()");
		marketUI.GetComponent<CanvasGroup>().alpha = 0f;
		marketUI.GetComponent<CanvasGroup>().interactable = false;
		marketUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
		marketUIButton.onClick.RemoveAllListeners();
		base.End();
	}

}
