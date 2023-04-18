using System;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public Vector2Int positionOnGrid;
    
    public void SetIndexes(Vector2Int indexes)
    {
        positionOnGrid = indexes;
    }
    
    private void OnMouseDown()
    {
        Debug.Log("CLicked on " + positionOnGrid);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Hovered on " + positionOnGrid);
    }

    private void OnMouseUp()
    {
        Debug.Log("Upped on " + positionOnGrid);
    }
}
