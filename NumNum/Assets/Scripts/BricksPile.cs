using System;
using System.Collections.Generic;
using UnityEngine;

public class BricksPile : MonoBehaviour
{
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private float _stackOffset = 0.2f;
    public int count;

    private List<Brick> _bricks;
    private BoardTile[] takenTiles;
    public bool IsDecomposed = false;

    public void Init()
    {
        _bricks = new List<Brick>();
        
        for (int i = 0; i < count; i++)
        {
            _bricks.Add(Instantiate(_brickPrefab , transform));
            
            _bricks[i].transform.localPosition = GetPositionForBrick(i + 1);
        }
    }
        
    private Vector3 GetPositionForBrick(int number)
    {
        return new Vector3(0, (number - 1) * _stackOffset , 0);
    }

    public void Decompose(BoardTile[] tiles)
    {
        takenTiles = tiles;
        
        // Skip first.
        for (int tileIndex = 1; tileIndex < takenTiles.Length; tileIndex++)
        {
            _bricks[count - tileIndex].transform.position = takenTiles[tileIndex].transform.position;
            takenTiles[tileIndex].isEmpty = false;
        }

        IsDecomposed = true;
    }

    public void Compose()
    {
        for (int tileIndex = 1; tileIndex < count; tileIndex++)
        {
            _bricks[count - tileIndex].transform.localPosition = GetPositionForBrick(tileIndex);
            takenTiles[tileIndex].isEmpty = true;
        }
        
        IsDecomposed = false;
    }
}
