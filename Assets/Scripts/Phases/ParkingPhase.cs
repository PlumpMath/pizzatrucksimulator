using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ParkingPhase : Phase
{
    GameObject parkingUI;
    Button parkingUIButton;


    void Start()
    {
        // parkingUI = GameObject.Find("Phase 2 - Parking");
        // parkingUI.GetComponent<CanvasGroup>().alpha = 0f;
        // parkingUI.GetComponent<CanvasGroup>().interactable = false;
        // parkingUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
        // parkingUIButton = GameObject.Find("MarketButtonEnd").GetComponent<Button>();
        // parkingUIButton.onClick.AddListener(End);
    }

    private void Update()
    {

    }

    public override void Begin()
    {
        parkingUI = GameObject.Find("Phase 2 - Parking");
        print("Parking Begin()- vroom vroom!");
        parkingUI.GetComponent<CanvasGroup>().alpha = 1f;
        parkingUI.GetComponent<CanvasGroup>().interactable = true;
        parkingUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
        parkingUIButton.onClick.AddListener(End);
        base.Begin();
    }

    public override void End()
    {
        print("Parking PHase End()");
        /*
        
        parkingUI.GetComponent<CanvasGroup>().alpha = 0f;
        parkingUI.GetComponent<CanvasGroup>().interactable = false;
        parkingUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
        parkingUIButton.onClick.RemoveAllListeners();
        base.End();
        */
    }

}