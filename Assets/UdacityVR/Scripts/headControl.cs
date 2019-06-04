using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headControl : MonoBehaviour {
    public GameObject GameLogic;
    public GameObject Player;

    private float speed = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GameLogic.GetComponent<GameLogic>().playerTurn == true) {
            if (GameLogic.GetComponent<holdPiece>().holdingPiece == true) {
                Vector3 dir = GameLogic.GetComponent<holdPiece>().pieceBeingHeld.transform.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(-dir);
                // slerp to the desired rotation over time
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
            }
        } else {
            Vector3 dir = Player.transform.position - transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(-dir);
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
        }
        
	}
}
