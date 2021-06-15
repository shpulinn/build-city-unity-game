using UnityEngine;

public class BuildButton : MonoBehaviour {

    //script for showing and hiding build screen by pressing "Build" button

    [SerializeField] private GameObject buildScreen;

    public void OnBuildButtonClick() {
        buildScreen.SetActive(!buildScreen.activeSelf);
        GetComponent<CameraController>().canControl = !buildScreen.activeSelf; // making unable to mve camera while build screen is on
    }

}
