using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public Transform customerPrefab;
    public Transform spawnArea;
    public Transform truckWindow;

    Turn currentTurn;

    #region Singleton
    public static CustomerSpawner Instance;

    void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("More than one CustomerSpawner instance!");
            return;
        }
        Instance = this;
    }
    #endregion

    public void Begin() {
        Debug.Log("CustomerSpawner Begin()");
        currentTurn = GameManager.Instance.currentTurn;
        StartCoroutine(SpawnCustomers());
        // get turn info
        // start spawning customers
    }

    IEnumerator SpawnCustomers() {
        Debug.Log("CustomerSpawner SpawnCustomers(), spawning customers: " + currentTurn.customerCount);
        float spawningRadius = spawnArea.localScale.y;

        for(int i = 0; i < currentTurn.customerCount; i++) {
            Debug.Log("CustomerSpawner Spawning Customer " + i);
            Vector3 randomPosition = new Vector3(
                                         Random.Range(-7f, 7f), 
                                         0, 
                                         Random.Range(-1f, 4f)
                                     );
            Transform customerObject = Instantiate(customerPrefab);
            customerObject.parent = transform;
            customerObject.localPosition = randomPosition;
            customerObject.GetComponent<Customer>().SetDestination(truckWindow.position);
            yield return new WaitForSeconds(1);
        }
    }
}