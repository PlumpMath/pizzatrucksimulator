using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MarketPhase : Phase {
	GameObject marketUIObject;
	MarketUI marketUI;
	Button marketUIButton;
    TruckControl truck;
    GameObject shop;

	void Start() {
		marketUIObject = GameObject.Find("MarketUI");
		marketUIObject.GetComponent<CanvasGroup>().alpha = 1f;
		marketUIObject.GetComponent<CanvasGroup>().interactable = true;
		marketUIObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        marketUIButton = GameObject.Find("MarketButtonEnd").GetComponent<Button>();
        marketUIButton.onClick.AddListener(End);

		marketUI = marketUIObject.GetComponent<MarketUI>();
		marketUI.Begin();
	}



    public override void Begin() {
		print("MarketPhase Begin()");
		base.Begin();
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
