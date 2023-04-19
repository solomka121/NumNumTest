using System;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldController : MonoBehaviour
{
    public GameField gameField;
    public SelectionOutline SelectionOutline;

    private BricksPile _selectedPile;
    private SelectionZone _selectedZone;
    private bool _isSelecting;
    
    private void Start()
    {
        gameField.Init();
        Init();
        SelectionOutline.Init(gameField.tilesOffset);
    }
    
    private void Init()
    {
        GameEvents.OnSelectionEnable += StartSelection;
        GameEvents.OnTileSelect += SelectTile;
        GameEvents.OnSelectionDisable += EndSelection;
        
        _selectedZone = new SelectionZone();
    }

    private void StartSelection(BoardTile tile)
    {
        if(tile.bricks == null)
            return;
        
        if(tile.bricks.isAnimating)
            return;
        
        if (tile.bricks.isDecomposed)
        {
            tile.bricks.Compose();
            return;
        }

        _selectedPile = tile.bricks;
        _selectedZone.start = tile.positionOnGrid;

        SelectionOutline.Activate(tile, _selectedZone.GetSize());
    }

    private void SelectTile(BoardTile tile)
    {
        _selectedZone.end = tile.positionOnGrid;
        SelectionOutline.SelectNewTile(tile, _selectedZone.GetSize());
    }

    private void EndSelection()
    {
        CheckSelectedZone();
        SelectionOutline.DiActivate();
    }

    private void CheckSelectedZone()
    {
        if(_selectedPile == null)
            return;
        
        if (GetSelectedTiles(out BoardTile[] tiles))
        {
            _selectedPile.Decompose(tiles);
        }

        _selectedPile = null;
    }

    private bool GetSelectedTiles(out BoardTile[] emptyTiles)
    {
        emptyTiles = new BoardTile[_selectedZone.GetLenght()];
        
        if (_selectedZone.GetLenght() != _selectedPile.count)
            return false;
        
        Vector2Int zoneSize = _selectedZone.GetSize();
        Vector2Int checkIndex = _selectedZone.start;
        Vector2Int direction = _selectedZone.GetDirection(true);

        int tileIndex = 0;
        
        for (int x = 0; x < zoneSize.x; x++)
        {
            checkIndex.y = _selectedZone.start.y;
            
            for (int y = 0; y < zoneSize.y; y++)
            {
                BoardTile tile = gameField.Grid[checkIndex.x , checkIndex.y];
                
                if (checkIndex != _selectedZone.start)
                {
                    if (tile.isEmpty == false)
                    {
                        return false;
                    }
                }
                
                emptyTiles[tileIndex] = tile;
                tileIndex++;
                
                checkIndex.y += direction.y;
            }
            checkIndex.x += direction.x;
        }

        return true;
    }
}

public struct SelectionZone
{
    public Vector2Int start;
    public Vector2Int end;
    
    public Vector2Int GetDirection()
    {
        Vector2Int size = Vector2Int.zero;
        
        size.x = end.x - start.x; // 1 - 3 = -2
        size.y = end.y - start.y; // 2 - 2 = 0
        
        return size;
    }
    
    public Vector2Int GetDirection(bool normalized)
    {
        Vector2Int size = GetDirection();

        size.x = Mathf.Clamp(size.x, -1, 1);
        size.y = Mathf.Clamp(size.y, -1, 1);
        
        return size;
    }

    public Vector2Int GetSize()
    {
        Vector2Int size = Vector2Int.zero;
        
        size.x = Mathf.Abs(end.x - start.x) + 1;
        size.y = Mathf.Abs(end.y - start.y) + 1;
        
        return size;
    }

    public int GetLenght()
    {
        Vector2Int size = GetSize();
        return size.x * size.y;
    }
    
    // GRID GENERATOR ( DOESNT WORK ).
    
    // [Serializable]
    // public struct GridZone
    // {
    //     public Vector2Int start;
    //     public Vector2Int end;
    //
    //     public Vector2Int GetSize()
    //     {
    //         Vector2Int size = Vector2Int.zero;
    //     
    //         size.x = Mathf.Abs(end.x - start.x) + 1;
    //         size.y = Mathf.Abs(end.y - start.y) + 1;
    //     
    //         return size;
    //     }
    // }
    //
    // private void GenerateField()
    // {
    //     int width = 6;
    //     int height = 4;
    //     int numRectangles = 5;
    //     Vector2Int positionOnGrid = new Vector2Int(-1 , -1);
    //
    //     // Random Zones.
    //     SplitRectangle(width, height, numRectangles , positionOnGrid);
    // }
    //
    // private void SplitRectangle(int width, int height, int numRectangles , Vector2Int positionOnGrid)
    // {
    //     positionOnGrid += Vector2Int.one;
    //     if (positionOnGrid.x > width - 1)
    //         positionOnGrid.x = 0;
    //             
    //     if (positionOnGrid.y > width - 1)
    //         positionOnGrid.y = 0;
    //     
    //     GridZone zone = new GridZone();
    //     if (numRectangles == 1)
    //     {
    //         zone.end = new Vector2Int(width - 1 , height - 1);
    //         _zones.Add(zone);
    //         positionOnGrid += zone.end;
    //         Debug.Log(width + "x" + height);
    //     }
    //     else if (numRectangles > 1 && numRectangles <= 8)
    //     {
    //         int maxSplit = Mathf.Min(Mathf.Min(width, height) - 1, numRectangles - 1); // 4 , 6 , 3  = 3
    //         int split = Random.Range(1, maxSplit + 1); // = 3
    //
    //         int remainingRectangles = numRectangles - 1;
    //         
    //         zone.start = positionOnGrid;
    //         if (width > height)
    //         {
    //             zone.end = positionOnGrid + new Vector2Int(split - 1 , height - 1);
    //             _zones.Add(zone);
    //             positionOnGrid += zone.end;
    //             if (positionOnGrid.x > width - 1)
    //                 positionOnGrid.x = 0;
    //             
    //             if (positionOnGrid.y > width - 1)
    //                 positionOnGrid.y = 0;
    //             
    //             Debug.Log(split + "x" + height);
    //             SplitRectangle(width - split, height, remainingRectangles , positionOnGrid);
    //         }
    //         else
    //         {
    //             zone.end = positionOnGrid + new Vector2Int(width - 1 , split - 1);
    //             _zones.Add(zone);
    //             positionOnGrid += zone.end;
    //             Debug.Log(width + "x" + split); // x4 y3
    //             SplitRectangle(width, height - split, remainingRectangles , positionOnGrid);
    //         }
    //     }
    // }

}
