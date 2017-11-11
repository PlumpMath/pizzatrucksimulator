using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MarketPhase : Phase {
	GameObject marketUI;
	Button marketUIButton;
    TruckControl truck;
    GameObject shop;

	void Start() {
		marketUI = GameObject.Find("MarketUI");
		marketUI.GetComponent<CanvasGroup>().alpha = 1f;
		marketUI.GetComponent<CanvasGroup>().interactable = true;
		marketUI.GetComponent<CanvasGroup>().blocksRaycasts = true;

        marketUIButton = GameObject.Find("MarketButtonEnd").GetComponent<Button>();
        marketUIButton.onClick.AddListener(End);
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
