using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
    public bool playerTurn = true;
    public bool gameEnded = false;
    public bool playerWon = false;
    public bool drawGame = false;

    public GameObject restartPanel;
    public Text restartText;

    public Text AIFace;

    //For our board we will just have a simple array of 9 integers scanning from top left to bottom right.  0 Represents an open slot, 1 represents a player piece, 2 represents an AI piece
    public int[] boardRepresentation;
    public GameObject[] PlayerPieces;
    public GameObject[] AIPieces;



    public Vector3[] PlayerPiecePositions;
    public Vector3[] AIPiecePositions;
    public int AIMoveCount = 0;

    public GameObject[] gridPlates;

    // Use this for initialization
    void Start() {
        AIFace.text = ":)";

        /*for (int i = 0; i < 9; i++) {
            gridPlates[i].GetComponent<ParticleSystem>().Stop();
        }*/

        //Save our intial positions for a reset
        for (int i =0; i<5; i++) {
            PlayerPiecePositions[i] = PlayerPieces[i].transform.position;
            AIPiecePositions[i] = AIPieces[i].transform.position;
        }

        initBoard();
    }

    public void initBoard() {
        //Initialize our board array to be full of 'empty' 0s
        boardRepresentation = new int[9];
        for (int i = 0; i < 9; i++) {
            boardRepresentation[i] = 0;
            gridPlates[i].SetActive(true);
            gridPlates[i].GetComponent<MeshRenderer>().enabled = false;

        }

        for (int i = 0; i < 5; i++) {
            PlayerPieces[i].transform.position = PlayerPiecePositions[i];
            AIPieces[i].transform.position = AIPiecePositions[i];

            PlayerPieces[i].GetComponent<PlayerPiece>().hasBeenPlayed = false;
            
        }
    }

    public void AIMove() {
        //If it is the AI's turn to move
        //Randomly select an open slot on the board
        //Place a piece there
        //Check for Victory
        //Switch it to the player's turn.
        if (playerTurn == false && gameEnded == false) {

            for (int i = 0; i > -1; i++) {
                int movePosition = Random.Range(0, 9); //Generate a random movement position
                if (boardRepresentation[movePosition] == 0) { //See if that slot is open
                    boardRepresentation[movePosition] = 2; //If it's open, set it to our AI move

                    AIPieces[AIMoveCount].transform.position = new Vector3(gridPlates[movePosition].transform.position.x, gridPlates[movePosition].transform.position.y + 0.3f, gridPlates[movePosition].transform.position.z);
					iTween.MoveTo (AIPieces[AIMoveCount], iTween.Hash ("position", gridPlates[movePosition].transform.position, "time", .2f, "easetype", "linear"));
                    AIMoveCount++;
					gridPlates[movePosition].SetActive(false);
                    i = -5; //Escape the for loop
                }
            }


            Invoke("checkForVictory", 1);
            playerTurn = true; //Set the player turn again.
            AIFace.text = ":)";
        }
    }

    public void gameOver() {
        //Display victory or defeat message
        //Give the player the chance to play again
        gameEnded = true;
        if (playerWon == true) {
            AIFace.text = ":(";
            restartText.text = "You won!";
        } else if (playerWon == false && drawGame == false) {
            AIFace.text = ":D";
            restartText.text = "You lost!";
        } else {
            AIFace.text = ":/";
            restartText.text = "Draw!";
        }
        

        //Right now just loop but make this contingent on play again action
        
        restartPanel.SetActive(true);
        

    }
    public void newGame() {
        gameEnded = false;
        AIMoveCount = 0;
        initBoard();
        playerTurn = true;
        drawGame = false;
        playerWon = false;
        restartPanel.SetActive(false);

    }

    public void playerMove(GameObject selectedPlate) {
        //Match the numbered plate and update our board representation to match it.
        for(int i =0; i <9;i++) {
            if (selectedPlate == gridPlates[i]) {
                boardRepresentation[i] = 1;
				gridPlates[i].SetActive(false);
                
            }
            //Debug.Log(boardRepresentation[i]);
        }
        playerTurn = false;
        Invoke("checkForVictory", 1);
        if (gameEnded == false) {
            AIFace.text = ":/";
            Invoke("AIMove", 2);
            //AIMove();
        }
        
    }



    public void checkForVictory() {
        //We are doing this elegantly, but that's fine.  There are only 8 possible win conditions in tic tac toe
        if (boardRepresentation[0] == boardRepresentation[1] && boardRepresentation[1] == boardRepresentation[2] && boardRepresentation[0] != 0 && boardRepresentation[1] != 0 && boardRepresentation[2] != 0) { //Victory Detected spanning across top.
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[0] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        }
        else if (boardRepresentation[3] == boardRepresentation[4] && boardRepresentation[4] == boardRepresentation[5] && boardRepresentation[3] != 0 && boardRepresentation[4] != 0 && boardRepresentation[5] != 0) { //Victory Detected spanning across middle
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[3] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        }
        else if (boardRepresentation[6] == boardRepresentation[7] && boardRepresentation[7] == boardRepresentation[8] && boardRepresentation[6] != 0 && boardRepresentation[7] != 0 && boardRepresentation[8] != 0) { //Victory Detected spanning across bottom
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[6] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        }
        else if (boardRepresentation[0] == boardRepresentation[3] && boardRepresentation[3] == boardRepresentation[6] && boardRepresentation[0] != 0 && boardRepresentation[3] != 0 && boardRepresentation[6] != 0) { //Victory Detected spanning across left
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[0] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        }
        else if (boardRepresentation[1] == boardRepresentation[4] && boardRepresentation[4] == boardRepresentation[7] && boardRepresentation[1] != 0 && boardRepresentation[4] != 0 && boardRepresentation[7] != 0) { //Victory Detected spanning across middle
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[1] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        }
        else if (boardRepresentation[2] == boardRepresentation[5] && boardRepresentation[5] == boardRepresentation[8] && boardRepresentation[2] != 0 && boardRepresentation[5] != 0 && boardRepresentation[8] != 0) { //Victory Detected spanning across right
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[2] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        }
        else if (boardRepresentation[0] == boardRepresentation[4] && boardRepresentation[4] == boardRepresentation[8] && boardRepresentation[0] != 0 && boardRepresentation[4] != 0 && boardRepresentation[8] != 0) { //Victory Detected diagonal down
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[0] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        }
        else if (boardRepresentation[2] == boardRepresentation[4] && boardRepresentation[4] == boardRepresentation[6] && boardRepresentation[2] != 0 && boardRepresentation[4] != 0 && boardRepresentation[6] != 0) { //Victory Detected diagonal up
            //if the items are 1s, the AI lost, if the items are 2s the player lost
            gameEnded = true;
            if (boardRepresentation[2] == 1) {
                playerWon = true;
                gameOver();
            }
            else {
                playerWon = false;
                gameOver();
            }
        } else if (boardRepresentation[0] != 0 && boardRepresentation[1] != 0 && boardRepresentation[2] != 0 && boardRepresentation[3] != 0 && boardRepresentation[4] != 0 && boardRepresentation[5] != 0 && boardRepresentation[6] != 0 && boardRepresentation[7] != 0 && boardRepresentation[8] !=0) {
            //If it's a draw
            playerWon = false;
            drawGame = true;
            gameOver();

        }
    }

}
