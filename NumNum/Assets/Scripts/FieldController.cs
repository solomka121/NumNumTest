using System;
using System.Security.Principal;
using UnityEditor;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public GameField gameField;

    private BricksPile _selectedPile;
    private SelectionZone _selectedZone;
    private bool _isSelecting;

    private void Start()
    {
        gameField.Init();
        Init();
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

        _selectedPile = tile.bricks;
        _selectedZone.start = tile.positionOnGrid;
    }

    private void SelectTile(BoardTile tile)
    {
        _selectedZone.end = tile.positionOnGrid;
    }

    private void EndSelection()
    {
        CheckSelectedZone();
    }

    private void CheckSelectedZone()
    {
        if(_selectedPile == null)
            return;

        if (_selectedPile.IsDecomposed)
        {
            _selectedPile.Compose();
        }
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
        Debug.Log("\nZone: " + zoneSize + "\nStart: " + checkIndex + "\nDir: " + direction);

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
}
