using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class GameMaster : MonoBehaviour {

    // reference to board data structure
    public GameOfLifeBoard myBoard;

    // Grid size (will be 30x30)
    private int boardSize = 30;       

    // Holds the prefab to create. In Inspector, drag and drop 
    // prefab onto Master Controller game object to set this 
    // Use this for initialization
    public Transform masterCell;   
    
    public static bool gamePlaying;
    public float timeForNextBoard;
    
    void Start()
    {
        timeForNextBoard = 0.25F;
        gamePlaying = true;

        InitializeBoard();
    }

    private void Update()
    {
        // Press space to pause or resume the game
        if (Input.GetKeyDown(KeyCode.Space) && gamePlaying == true)
        {
            Debug.Log("Space Pressed so Game Paused");
            gamePlaying = false;
        }else if (Input.GetKeyDown(KeyCode.Space) && gamePlaying == false)
        {
            Debug.Log("Space Pressed so Game Resumed");
            gamePlaying = true;
        }

        // Press comma key to slow down update time 
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.fixedDeltaTime <= 1.0F)
        {
            timeForNextBoard += 0.05F;
            Debug.Log("Fixed Time: " + timeForNextBoard);
        }

        // Press period key to speed up update time
        if (Input.GetKeyDown(KeyCode.RightArrow) && Time.fixedDeltaTime >= 0.05F)
        {
            timeForNextBoard += -0.05F;
            Debug.Log("Fixed Time: " + timeForNextBoard);
        }

        // Press R to reset the board
        if (Input.GetKeyDown(KeyCode.R))
        {
            myBoard.ClearCurrentBoard();
            InitializeBoard();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if the game is playing go through the board and set up the next frame
        if(gamePlaying == true)
        {
            Time.fixedDeltaTime = timeForNextBoard;
            myBoard.WalkThrough();
            myBoard.SetNewBoard();
        }
    }

    void InitializeBoard()
    {
        myBoard = new GameOfLifeBoard(boardSize);
        Transform newCell;
        CellScript myCellScript;

        for (int i = 0; i < boardSize; i++) // starts in bottom left corner goes right to left
        {
            for (int j = 0; j < boardSize; j++) // starts in bottom left corner goes bottom to top
            {
                // Create a new instance of the cell prefab
                newCell = Instantiate(masterCell, new Vector3(i * 1.5F, j * 1.5F, 0.0F), Quaternion.identity) as Transform;

                // Get a reference to the cells script
                myCellScript = newCell.gameObject.GetComponent<CellScript>();

                // Set the row and col value for the cell
                myCellScript.SetRowCol(i, j);

                // Create the intitial board
                myCellScript.SetGameOfLifeBoard(myBoard);
            }
        }
    }
}
