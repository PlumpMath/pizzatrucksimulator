using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {
	public List<Ingredient> ingredientsList;

    public bool doughRolled = false;
    public bool sauceAdded = false;
    public bool cheeseAdded = false;
    public bool gettable = true;
    public bool holdingAPizza = false;
    public SkinnedMeshRenderer doughMesh;
    public float blendShapeWeightCurrent = 100;

    float blendShapeWeightTarget= 100;

    void Start()
    {
        doughMesh = GetComponent<SkinnedMeshRenderer>();
    }



    public int Reputation { get; set; }
	// public Customer customer;


    public void RollDough()
    {
        doughRolled = true;
        blendShapeWeightTarget = 0;
    }
    
	
	
	void Update () {

        blendShapeWeightCurrent = Mathf.Lerp(blendShapeWeightCurrent, blendShapeWeightTarget, Time.deltaTime);
        doughMesh.SetBlendShapeWeight(0, blendShapeWeightCurrent);
    }
}
