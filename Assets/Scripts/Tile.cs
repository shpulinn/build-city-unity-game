using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    Color highlightedColor = new Color(1, 2, 3);

    void Start() {

    }

    void Update() {

    }

    private void OnMouseEnter() {
        gameObject.GetComponent<MeshRenderer>().material.color = highlightedColor;
    }

    private void OnMouseExit() {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    private void OnMouseDown() {
        //Debug.Log(gameObject.name + " " + transform.position);
    }
}
