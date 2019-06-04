using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour {
    public bool hasBeenPlayed = false;
	private bool holdingPiece;
	public GameObject gameLogic;
	private holdPiece holdpiece;
	private GameLogic gamelogic;




    // Use this for initialization
    void Start () {
		holdpiece = gameLogic.GetComponent<holdPiece> ();
		gamelogic = gameLogic.GetComponent<GameLogic> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void inPlay() {
        //If this piece has been selected
        //Make it hover above the raycast
		holdingPiece = holdpiece.holdingPiece;
		if (!holdingPiece && !hasBeenPlayed && gamelogic.playerTurn && !gamelogic.gameEnded) {
			iTween.MoveTo (this.gameObject, iTween.Hash ("position", transform.position + Vector3.up * .3f, "time", .2f, "easetype", "linear"));
			holdpiece.grabPiece (this.gameObject);
		}

        
    }
	public void playPiece(GameObject selectedPlate) {
        //If the player has selected a grid area
        //Animate the piece into position
        hasBeenPlayed = true;
		iTween.MoveTo (this.gameObject, iTween.Hash ("position", selectedPlate.transform.position, "time", .2f, "easetype", "linear"));
        //Tell our GameLogic script to occupy the game board array at the right location with a player piece
		gamelogic.playerMove(selectedPlate);
    }
}
