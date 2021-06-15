using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Buildings")]
public class BuildingScriptableObj : ScriptableObject {

    public string type;
    public int moneyCost;
    public int woodCost;
    public int brickCost;
    public float buildTime;


}
