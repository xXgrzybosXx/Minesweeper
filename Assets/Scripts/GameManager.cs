using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int minesAmount;
    public int width, height;

    public TileLibrary tileLibrary;

    public Tile[,] grid = new Tile[9,9];
    public List<Tile> tilesToCheck = new List<Tile>();

    void Start()
    {
        for (int i = 0; i < minesAmount; i++)
            PlaceMine();
        PlaceClues();
        PlaceBlanks();
    }

    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(mousePosition.x);
            int y = Mathf.RoundToInt(mousePosition.y);

            Tile tile = grid[x, y];

            if (tile.tileState == Tile.TileState.Normal)
            {
                if (tile.isCovered)
                {
                    if (tile.tileType == Tile.TileType.Mine)
                    {
                        GameOver(tile);
                    }
                    else
                    {
                        tile.SetIsCovered(false);
                    }

                    if (tile.tileType == Tile.TileType.Blank)
                    {
                    RevealAdjacentTilesForTileAt(x, y);
                    }
                }
            }
        }
    }

    private void GameOver(Tile tile)
    {
        tile.SetClickedMine();

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Mine");

        foreach (GameObject go in gameObjects)
        {
            Tile t = go.GetComponent<Tile>();

            if (t != tile)
            {
                if (t.tileState == Tile.TileState.Normal)
                {
                t.SetIsCovered(false);
                }
            }
        }

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                Tile t = grid[x, y];

                if (t.tileState == Tile.TileState.Flagged)
                {
                    if (t.tileType != Tile.TileType.Mine)
                    {
                        //Pokazuje graczowi że postawił flage nie na minie
                        t.SetNotAMineFlagged();
                    }
                }
            }
        }
    }

    void PlaceMine()
    {
        int x = UnityEngine.Random.Range(0, 9);
        int y = UnityEngine.Random.Range(0, 9);

        GameObject mineTile = Instantiate(tileLibrary.Mine, new Vector3(x, y, 0), Quaternion.identity);

        grid[x, y] = mineTile.GetComponent<Tile>();
    }

    void PlaceClues()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (grid[x, y] == null)
                {
                    int numMines = 0;

                    //StraightOnes
                    //Up
                    if (y + 1 < 9)
                    {
                        if (grid[x, y + 1] != null && grid[x, y + 1].tileType == Tile.TileType.Mine)
                            numMines++;
                    }
                    //Right
                    if (x + 1 < 9)
                    {
                        if (grid[x + 1, y] != null && grid[x + 1, y].tileType == Tile.TileType.Mine)
                            numMines++;
                    }
                    //Down
                    if (y - 1 >= 0)
                    {
                        if (grid[x, y - 1] != null && grid[x, y - 1].tileType == Tile.TileType.Mine)
                            numMines++;
                    }
                    //Left
                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, y] != null && grid[x - 1, y].tileType == Tile.TileType.Mine)
                            numMines++;
                    }
                    //CornerOnes
                    //UpRight
                    if (x + 1 < 9 && y + 1 < 9)
                    {
                        if (grid[x + 1, y + 1] != null && grid[x + 1, y + 1].tileType == Tile.TileType.Mine)
                            numMines++;
                    }
                    //UpLeft
                    if (x - 1 >= 0 && y + 1 < 9)
                    {
                        if (grid[x - 1, y + 1] != null && grid[x - 1, y + 1].tileType == Tile.TileType.Mine)
                            numMines++;
                    }
                    //DownRight
                    if (x + 1 < 9 && y - 1 >= 0)
                    {
                        if (grid[x + 1, y - 1] != null && grid[x + 1, y - 1].tileType == Tile.TileType.Mine)
                            numMines++;
                    }
                    //DownLeft
                    if (x - 1 >= 0 && y - 1 >= 0)
                    {
                        if (grid[x - 1, y - 1] != null && grid[x - 1, y - 1].tileType == Tile.TileType.Mine)
                            numMines++;
                    }

                    GameObject clueTile;
                    switch (numMines)
                    {
                        case 1: clueTile = Instantiate(tileLibrary.One,     new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        case 2: clueTile = Instantiate(tileLibrary.Two,     new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        case 3: clueTile = Instantiate(tileLibrary.Three,   new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        case 4: clueTile = Instantiate(tileLibrary.Four,    new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        case 5: clueTile = Instantiate(tileLibrary.Five,    new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        case 6: clueTile = Instantiate(tileLibrary.Six,     new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        case 7: clueTile = Instantiate(tileLibrary.Seven,   new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        case 8: clueTile = Instantiate(tileLibrary.Eight,   new Vector3(x, y, 0), Quaternion.identity);
                            grid[x, y] = clueTile.GetComponent<Tile>();
                            break;
                        default: break;
                    }
                    
                }
            }
        }
    }

    void PlaceBlanks()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (grid[x, y] == null)
                {
                    GameObject blankTile = Instantiate(tileLibrary.BlankTile, new Vector3(x, y, 0), Quaternion.identity);
                    grid[x, y] = blankTile.GetComponent<Tile>();
                }
            }
        }
    }

    void RevealAdjacentTilesForTileAt(int x, int y)
    {
        //Up
        if (y + 1 < 9)
        {
            CheckTileAt(x, y+1);
        }
        //Right
        if (x + 1 < 9)
        {
            CheckTileAt(x+1, y);
        }
        //Down
        if (y - 1 >= 0)
        {
            CheckTileAt(x, y-1);
        }
        //Left
        if (x - 1 >= 0)
        {
            CheckTileAt(x-1, y);
        }
        //CornerOnes
        //UpRight
        if (x + 1 < 9 && y + 1 < 9)
        {
            CheckTileAt(x+1, y+1);
        }
        //UpLeft
        if (x - 1 >= 0 && y + 1 < 9)
        {
            CheckTileAt(x-1, y+1);
        }
        //DownRight
        if (x + 1 < 9 && y - 1 >= 0)
        {
            CheckTileAt(x+1, y-1);
        }
        //DownLeft
        if (x - 1 >= 0 && y - 1 >= 0)
        {
            CheckTileAt(x-1, y-1);
        }

        //Sprawdzamy od tyłu bo usuwamy elementy na bieżąco, więc będzie coraz mniej elementów i moglibyśmy zbliżyć się do "Out of index"
        for (int i = tilesToCheck.Count - 1; i >= 0; i--)
        {
            if (tilesToCheck[i].didCheck)
            {
                tilesToCheck.RemoveAt(i);
            }
        }

        if (tilesToCheck.Count>0)
        {
            RevealAdjacentTilesForTiles();
        }
    }

    private void RevealAdjacentTilesForTiles()
    {
        for (int i = 0; i < tilesToCheck.Count; i++)
        {
            Tile tile = tilesToCheck[i];

            int x = (int)tile.gameObject.transform.localPosition.x;
            int y = (int)tile.gameObject.transform.localPosition.y;

            tile.didCheck = true;

            if (tile.tileState != Tile.TileState.Flagged)
            {
            tile.SetIsCovered(false);
            }

            RevealAdjacentTilesForTileAt(x, y);
        }
    }

    private void CheckTileAt(int x, int y)
    {
        Tile tile = grid[x, y];

        if (tile.tileType == Tile.TileType.Blank)
        {
            tilesToCheck.Add(tile);
        }
        else if (tile.tileType == Tile.TileType.Clue)
        {
            tile.SetIsCovered(false);
        }
        else if (tile.tileType == Tile.TileType.Mine)
        {

        }
    }
}
