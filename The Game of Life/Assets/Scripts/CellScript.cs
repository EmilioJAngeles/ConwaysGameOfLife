using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{ 
    public class CellScript : MonoBehaviour
    {
        private int row;
        private int col;

        private GameOfLifeBoard myBoard;

        public Renderer rend;

        // Update is called once per frame
        void Update()
        {
            rend = GetComponent<Renderer>();

            if (myBoard.GetCell(row, col) == true)
            {
                rend.enabled = true;
            } else {
                rend.enabled = false;
            }
        }

        public void SetRowCol(int i, int j)
        {
            row = i;
            col = j;
        }

        public void SetGameOfLifeBoard(GameOfLifeBoard myBoard)
        {
            this.myBoard = myBoard;
        }

        private void OnMouseOver()
        {
            //Debug.Log("Cursor Over [" + row + "," + col + "]");
            if (GameMaster.gamePlaying == false)
            {
                if (Input.GetMouseButtonDown(0) && myBoard.GetCell(row, col) == true)
                {
                    myBoard.ChangeCellState(true, row, col);
                    //Debug.Log("Cursor Over [" + row + "," + col + "] which was on but is now off");
                }
                else if (Input.GetMouseButtonDown(0) && myBoard.GetCell(row, col) == false)
                {
                    myBoard.ChangeCellState(false, row, col);
                    //Debug.Log("Cursor Over [" + row + "," + col + "] which was off but is now on");
                }
            }
        }
    }
}