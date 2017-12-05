using System.Collections;
using UnityEngine;

public class WorkPhase : Phase {
    GameObject workdayUIObject;
    // WorkdayUI workdayUI;
    float timeLeft;
    UnityEngine.UI.Text countdownDisplay;
    UnityEngine.UI.Text timeBonusDisplay;
    int customersServed;
    int customersFailed;
    bool gameFinished;

    public override void Begin() {
        print("WorkPhase Begin()");
        base.Begin();
        timeLeft = GameManager.Instance.timePerGame;
        GameManager.Instance.firstPersonController.enabled = true;
        Arms.Instance.controlsEnabled = true;
        SetupWorkdayUI();
        SetupWorkArea();
    }

    void FixedUpdate() {
        if (!gameFinished && timeLeft <= 0) {
            gameFinished = true;
            GameOver();
        } else {
            timeLeft -= Time.deltaTime;
            countdownDisplay.text = Mathf.Ceil(timeLeft).ToString();

            if (Input.GetKeyDown(KeyCode.Alpha9)) {
                timeLeft -= 10f;
            } else if (Input.GetKeyDown(KeyCode.Alpha0)) {
                timeLeft += 10f;
            }
        }
    }

    void SetupWorkdayUI() {
        workdayUIObject = GameObject.Find("WorkdayUI");
        workdayUIObject.GetComponent<CanvasGroup>().alpha = 1f;
        workdayUIObject.GetComponent<CanvasGroup>().interactable = true;
        workdayUIObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // workdayUI = workdayUIObject.GetComponent<WorkdayUI>();
        countdownDisplay = GameObject.Find("Countdown").GetComponent<UnityEngine.UI.Text>();
        countdownDisplay.enabled = true;
        timeBonusDisplay = GameObject.Find("Time Bonus").GetComponent<UnityEngine.UI.Text>();
    }

    void SetupWorkArea() {
        IngredientSpawner.Instance.SpawnTruckIngredients();
        CustomerSpawner.Instance.OnCustomerServed += OnCustomerServed;
        CustomerSpawner.Instance.OnCustomerFailed += OnCustomerFailed;
        CustomerSpawner.Instance.Begin();
    }

    void GameOver() {
        GameManager.Instance.firstPersonController.enabled = false;
        Arms.Instance.controlsEnabled = false;
        GameObject gameOverObject = GameObject.Find("Game Over Screen");
        CanvasGroup gameOverCanvas = gameOverObject.GetComponent<CanvasGroup>();
        gameOverCanvas.alpha = 1f;

        UnityEngine.UI.Text gameOverText = GameObject.Find("Game Over Text").GetComponent<UnityEngine.UI.Text>();
        gameOverText.text += "\nHappily Served: " + customersServed;
        gameOverText.text += "\nThrown Back By Bad Pizza: " + customersFailed;

        StartCoroutine(ReturnToStart());
    }

    IEnumerator ReturnToStart() {
        yield return new WaitForSeconds(3.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public override void End() {
        print("WorkPhase End()");
        GameManager.Instance.firstPersonController.enabled = false;
        Arms.Instance.controlsEnabled = false;
        base.End();
    }

    void OnCustomerServed() {
        customersServed++;
		timeLeft += 10f;
		StartCoroutine(DisplayTimeBonus());
    }

	IEnumerator DisplayTimeBonus() {
		timeBonusDisplay.enabled = true;
		yield return new WaitForSeconds(2f);
		timeBonusDisplay.enabled = false;
	}

    void OnCustomerFailed() {
        customersFailed++;
    }
}