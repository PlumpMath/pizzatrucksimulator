using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChoice : MonoBehaviour {



	public GameObject icon;
	public GameObject description;
	public GameObject freshness;
	public GameObject title;

    public Button thisButton;

    //public MarketUI marketUI;

    void Start()
    {

        MarketUI.brotcast += this.TruckTransfer;
        MarketUI.brotcast += this.HotPants;

        Button btn = thisButton.GetComponent<Button>();
        btn.onClick.AddListener(GatherIngredients);

    }

    void TruckTransfer(GameObject obj)
    {
        print("Transferred ze " + obj.GetComponent<ItemChoice>().title.GetComponent<Text>().text + " to pizza shop");  
    }

    public void SetIcon(Sprite sprite)
	{
		icon.GetComponent<Image>().sprite = sprite;
	}

	public void SetDescription(string text)
	{
		description.GetComponent<Text>().text = text.ToString();
	}


	public void SetTitle(string text){

		title.GetComponent<Text>().text = text.ToString();

	}

    public void SetFreshness(bool random)
    {
        if (random == true)
        {
            int i = Random.Range(0, 4);
            if (i == 0)
            {
                freshness.GetComponent<Text>().text = "Almost Spoiled";
            }
            if (i == 1)
            {
                freshness.GetComponent<Text>().text = "Stale";
            }
            if (i == 2)
            {
                freshness.GetComponent<Text>().text = "Past Fresh";
            }
            if (i == 3)
            {
                freshness.GetComponent<Text>().text = "Fresh";
            }
        }
            else
            {
                freshness.GetComponent<Text>().text = "So Fresh";
            }
    }

    void GatherIngredients()
    {
        print("Bravo, you took the " + title.GetComponent<Text>().text);
        thisButton.interactable = false;

    }

    void HotPants(GameObject obj)
    {
    
    }
    
}
