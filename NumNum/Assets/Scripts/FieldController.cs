using System;
using System.Security.Principal;
using UnityEditor;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public GameField gameField;

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
        _selectedZone.start = tile.positionOnGrid;
    }

    private void SelectTile(BoardTile tile)
    {
        _selectedZone.end = tile.positionOnGrid;
    }

    private void EndSelection()
    {
        CheckSelectionZone();
    }

    private void CheckSelectionZone()
    {
        Debug.Log(_selectedZone.GetSize());
    }
    
}

public struct SelectionZone
{
    public Vector2Int start;
    public Vector2Int end;

    public Vector2 GetSize()
    {
        Vector2 size ;
        
        size.x = Mathf.Abs(start.x - end.x) + 1;
        size.y = Mathf.Abs(start.y - end.y) + 1;
        
        return size;
    }
}
