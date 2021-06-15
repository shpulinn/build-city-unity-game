using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Building")]
public class BuildingScriptableObj : ScriptableObject {

    public string type; // type of house (decorative, money, wood, etc.)
    public int moneyCost; // cost of money player should spent to build that house
    public int woodCost; // cost of wood ==//==
    public int brickCost; // cost of brick ==//==
    public float buildTime; // time from starting building that house to it built 
    public float produceTime; // time from starting to produce resource to it producing
    public int amountOfResource; // amount of resource that building produces each "produceTime"


}
