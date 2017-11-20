using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {
    public enum Type {Base, Topping}

    public Type type;
    public string Name;
    public int ingredientID;
	public string description;
	public bool isBaseIngredient;
	public Sprite icon;
}