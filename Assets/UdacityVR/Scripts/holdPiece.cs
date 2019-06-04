using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdPiece : MonoBehaviour {
    public GameObject GameLogic;
    public GameObject raycastHolder;
    public GameObject player;
    public GameObject pieceBeingHeld;
	public GameObject gravityAttractor;

    public bool holdingPiece = false;
    public float hoverHeight = 0.3f;

    RaycastHit hit;

    // Use this for initialization
    void Start () {
		
	}
	public void grabPiece(GameObject selectedPiece) {
		pieceBeingHeld = selectedPiece;
		holdingPiece = true;
    }

	public void putPiece(GameObject selectedPlate) {
		if (pieceBeingHeld == null)
			return;
		holdingPiece = false;
		pieceBeingHeld.GetComponent<PlayerPiece>().playPiece(selectedPlate);
		pieceBeingHeld = null;
	}

	void Update () {
		if (holdingPiece == true) {
			Vector3 forwardDir = raycastHolder.transform.TransformDirection (Vector3.forward) * 100;
			Debug.DrawRay (raycastHolder.transform.position, forwardDir, Color.green);

			if (Physics.Raycast (raycastHolder.transform.position, (forwardDir), out hit)) {

				pieceBeingHeld.transform.position = new Vector3 (hit.point.x, hit.point.y + hoverHeight, hit.point.z);

			}
		}
	}

}








