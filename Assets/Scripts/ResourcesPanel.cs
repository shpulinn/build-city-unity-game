using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour {

    // script for updating info about resources player have in current moment.

    [SerializeField] private ResourcesScriptableObj ResourcesScriptableObj;

    [SerializeField] private Text moneyText;
    [SerializeField] private Text woodText;
    [SerializeField] private Text bricksText;

    private void FixedUpdate() {
        moneyText.text = ResourcesScriptableObj.money.ToString();
        woodText.text = ResourcesScriptableObj.wood.ToString();
        bricksText.text = ResourcesScriptableObj.bricks.ToString();
    }

}
