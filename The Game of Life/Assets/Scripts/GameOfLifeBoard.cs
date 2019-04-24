using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class GameOfLifeBoard
    {
        // set board size
        public int boardSize = 30;

        // initialize 2D array
        public bool[,] CurrentBoardArray;
        public bool[,] NextBoardArray;

        // Constructor
        public GameOfLifeBoard(int boardSize)
        {
            CurrentBoardArray = new bool [boardSize, boardSize];
            NextBoardArray = new bool[boardSize, boardSize];

            // walk through the board cells and randomly set the cell to alive or dead
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (Random.Range(0.0F, 1.0F) >= 0.5F)
                    {
                        CurrentBoardArray[i, j] = false;
                    }
                    else
                    {
                        CurrentBoardArray[i, j] = true;
                    }
                }
            }
        }

        // Set cell to alive or dead
        public bool GetCell(int x, int y)
        {
            return CurrentBoardArray[x, y];
        }

        // Run through all cells and check neighbors then set 
        public void WalkThrough()
        {
            for(int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    SetCell(GetNeighbors(i, j), i, j);
                }
            }
        }

        public bool GetNeighbors(int x, int y)
        {
            int NeighborCount = 0;

            // Check cell on the right.
            if (x != boardSize-1)
                if (CurrentBoardArray[x + 1, y] == true)
                    NeighborCount++;

            // Check cell on the bottom right.
            if (x != boardSize-1 && y != boardSize-1)
                if (CurrentBoardArray[x + 1, y + 1] == true)
                    NeighborCount++;

            // Check cell on the bottom.
            if (y != boardSize-1)
                if (CurrentBoardArray[x, y + 1] == true)
                    NeighborCount++;

            // Check cell on the bottom left.
            if (x != 0 && y != boardSize-1)
                if (CurrentBoardArray[x - 1, y + 1] == true)
                    NeighborCount++;

            // Check cell on the left.
            if (x != 0)
                if (CurrentBoardArray[x - 1, y] == true)
                    NeighborCount++;

            // Check cell on the top left.
            if (x != 0 && y != 0)
                if (CurrentBoardArray[x - 1, y - 1] == true)
                    NeighborCount++;

            // Check cell on the top.
            if (y != 0)
                if (CurrentBoardArray[x, y - 1] == true)
                    NeighborCount++;

            // Check cell on the top right.
            if (x != boardSize-1 && y != 0)
                if (CurrentBoardArray[x + 1, y - 1] == true)
                    NeighborCount++;

            // Check condition for status in next frame
            if (CurrentBoardArray[x, y] == true)
            {
                if (NeighborCount == 3)
                { // Any live cell with two or three live neighbors lives on to the next generation.
                    return true;
                }
                else if(NeighborCount == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (CurrentBoardArray[x, y] == false)
            {
                // Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                if (NeighborCount == 3)
                { 
                    return true;
                }
                else
                {
                    return false;
                }
             
            }
            else // failsafe
            {
                Debug.Log("ERROR: NO CONIDTIONS MET");
                return false;
            }
        }

        public void ChangeCellState(bool isAlive, int x, int y)
        {
            if (isAlive == true)
            {
                Debug.Log("Cell [" + x + "," + y + "] is " + CurrentBoardArray[x, y]);
                CurrentBoardArray[x, y] = false;
                Debug.Log("Cell [" + x + "," + y + "] is " + CurrentBoardArray[x, y]);
            }
            else
            {
                Debug.Log("Cell [" + x + "," + y + "] is " + CurrentBoardArray[x, y]);
                CurrentBoardArray[x, y] = true;
                Debug.Log("Cell [" + x + "," + y + "] is " + CurrentBoardArray[x, y]);
            }
        }

        // Set the cell to alive or dead
        public void SetCell(bool isAlive, int x, int y)
        {
            if (isAlive == true)
            {
                NextBoardArray[x, y] = true;
            } else
            {
                NextBoardArray[x, y] = false;
            }
        }

        // Create the new board to be initialized in the next frame
        public void SetNewBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    CurrentBoardArray[i, j] = NextBoardArray[i, j];
                }
            }
        }

        public void ClearCurrentBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    CurrentBoardArray[i, j] = false;
                }
            }
        }
    }

