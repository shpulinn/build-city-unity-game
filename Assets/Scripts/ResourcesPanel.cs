using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour {

    [SerializeField] private ResourcesScriptableObj ResourcesScriptableObj;

    [SerializeField] private Text moneyText;
    [SerializeField] private Text woodText;
    [SerializeField] private Text bricksText;

    private void Awake() {
        moneyText.text = ResourcesScriptableObj.money.ToString();
        woodText.text = ResourcesScriptableObj.wood.ToString();
        bricksText.text = ResourcesScriptableObj.bricks.ToString();
    }

    private void FixedUpdate() {
        moneyText.text = ResourcesScriptableObj.money.ToString();
        woodText.text = ResourcesScriptableObj.wood.ToString();
        bricksText.text = ResourcesScriptableObj.bricks.ToString();
    }

}
