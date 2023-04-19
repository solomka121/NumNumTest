using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu]
public class BoardData : ScriptableObject
{
    public int columns = 0;
    public int rows = 0;

    public BrickPositionOnGrid[] bricks;

    [Serializable]
    public struct BrickPositionOnGrid
    {
        public Vector2Int position;
        public int count;
    }
}