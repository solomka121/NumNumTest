using System;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public BricksStack bricks;
    public bool isEmpty;
    
    public Vector2Int positionOnGrid;
    
    public void SetIndexes(Vector2Int indexes)
    {
        positionOnGrid = indexes;
    }

    private void OnMouseDown()
    {
        Clicked();
    }
    
    private void Clicked()
    { 
        Debug.Log("Clicked on " + positionOnGrid);
        
        GameEvents.OnSelectionEnableMethod(this);
    }

    private void OnMouseEnter()
    {
        Hovered();
    }
    
    private void Hovered()
    {
        // Debug.Log("Hovered on " + positionOnGrid);
        
        GameEvents.OnTileSelectMethod(this);
    }

    private void OnMouseUp()
    {
        MouseUp();
    }
    
    private void MouseUp()
    {
        Debug.Log("Upped on " + positionOnGrid);
        
        GameEvents.OnSelectionDisableMethod();
    }
}
