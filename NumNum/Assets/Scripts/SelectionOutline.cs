using System.Reflection;
using UnityEngine;

public class SelectionOutline : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Vector2 _bonusSize;
    [SerializeField] private float _yPosition;
    private float _tilesOffset;

    private BoardTile _startTile;
    private BoardTile _endTile;

    public void Init(float tilesOffset)
    {
        _tilesOffset = tilesOffset;
    }

    public void Activate(BoardTile tile , Vector2 selectionSize)
    {
        gameObject.SetActive(true);

        _renderer.color = tile.bricks.mainMaterial.color;
        
        _startTile = tile;
        _endTile = tile;

        CalculatePositionAndSize(selectionSize);
    }

    public void SelectNewTile(BoardTile tile , Vector2 selectionSize)
    {
        _endTile = tile;

        CalculatePositionAndSize(selectionSize);
    }

    public void DiActivate()
    {
        gameObject.SetActive(false);
    }

    public void CalculatePositionAndSize(Vector2 selectionSize)
    {
        if(gameObject.activeSelf == false)
            return;
        
        Vector3 position;
        position = (_startTile.transform.position + _endTile.transform.position) / 2;
        position.y = _yPosition;
        transform.position = position;
        
        Vector2 size;
        size.x = selectionSize.x + _tilesOffset * (selectionSize.x - 1);
        size.y = selectionSize.y + _tilesOffset * (selectionSize.y - 1);
        _renderer.size = size + _bonusSize;
    }
}
