using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Blank,
        Mine,
        Clue
    }

    public enum TileState
    {
        Normal,
        Flagged
    }

    public bool isCovered = true;
    public bool didCheck = false;

    public Sprite coveredSprite;
    public Sprite flagSprite;
    public Sprite mineClicked;
    public Sprite noMineWasHere;

    public TileType tileType = TileType.Blank;

    public TileState tileState = TileState.Normal;

    private Sprite defaultSprite;

    void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;

        GetComponent<SpriteRenderer>().sprite = coveredSprite;
    }

    public void SetClickedMine()
    {
        GetComponent<SpriteRenderer>().sprite = mineClicked;
    }

    public void SetNotAMineFlagged()
    {
        GetComponent<SpriteRenderer>().sprite = noMineWasHere;
    }

    private void OnMouseOver()
    {
        if(isCovered)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (tileState == TileState.Normal)
                {
                    tileState = TileState.Flagged;
                    GetComponent<SpriteRenderer>().sprite = flagSprite;
                }
                else
                {
                    tileState = TileState.Normal;
                    GetComponent<SpriteRenderer>().sprite = coveredSprite;
                }
            }
        }
    }

    public void SetIsCovered (bool covered)
    {
        isCovered = covered;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;

    }
}
