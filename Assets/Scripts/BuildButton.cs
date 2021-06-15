using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour {

    [SerializeField] private GameObject buildScreen;

    public void OnBuildButtonClick() {
        buildScreen.SetActive(!buildScreen.activeSelf);
        GetComponent<CameraController>().canControl = !buildScreen.activeSelf;
    }

}
