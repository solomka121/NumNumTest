using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    [SerializeField] private BoardTile _tilePrefab;
    [SerializeField] private BricksPile _bricksPilePrefab;
    [SerializeField] private MaterialsPallet _materialsPallet;
    
    public float tilesOffset;
    [SerializeField] private float _wallsPadding = 1;
    
    public BoardTile[,] Grid;

    public void Init()
    {
        Grid = new BoardTile[_boardData.columns, _boardData.rows];
        SpawnGrid();
        SpawnBricksPiles();
    }

    private void SpawnGrid()
    {
        if (_boardData != null)
        {
            Vector3 TileSize = _tilePrefab.transform.localScale;
            
            Vector3 FieldSize = GetFieldSize(TileSize);
            
            Vector3 firstTilePosition = new Vector3(FieldSize.x / 2 , 0 , FieldSize.z / 2);

            Vector3 currentPosition = -firstTilePosition;

            for (int z = 0; z < _boardData.rows; z++)
            {
                currentPosition.z += TileSize.z / 2;
                currentPosition.x = -firstTilePosition.x;
                
                for (int x = 0; x < _boardData.columns; x++)
                {
                    currentPosition.x += TileSize.x / 2;
                    
                    BoardTile tile = Instantiate(_tilePrefab , transform);
                    tile.SetIndexes(new Vector2Int(x, z));
                    
                    tile.transform.localPosition = currentPosition;
                    // bricks.transform.localScale = TileScale / _brickPrefab.BackGround.sprite.bounds.size.x;

                    // bricks.Text.text = _boardData.Board[x].Row[y];
                    Grid[x, z] = tile;
                    // bricks.PositionOnGird = new Vector2Int(x , y);
                    
                    currentPosition.x += TileSize.x / 2 + tilesOffset;
                }
                currentPosition.z += TileSize.z / 2 + tilesOffset;
            }
        }
    }

    private void SpawnBricksPiles()
    {
        for (int i = 0; i < _boardData.bricks.Length; i++)
        {
            Vector2Int position = _boardData.bricks[i].position;
            BoardTile tile = Grid[position.x, position.y];
            
            tile.bricks = Instantiate(_bricksPilePrefab , tile.transform);
            tile.bricks.count = _boardData.bricks[i].count;
            tile.bricks.mainMaterial = _materialsPallet.GetUniqueMaterial();
            tile.isEmpty = false;
            tile.bricks.Init();
        }
    }

    private Vector3 GetFieldSize(Vector3 tileSize)
    {
        Vector3 size = new Vector3();

        size.x = _boardData.columns * tileSize.z + tilesOffset * (_boardData.columns - 1);
        size.y = 1;
        size.z = _boardData.rows * tileSize.x + tilesOffset * (_boardData.rows - 1);

        return size;
    }
    
    //Bounds for camera in orthographic mode.
    private Vector3 GetScreenSize()
    {
        Vector3 screenWidth = GetScreenWidthInWorldUnits();

        screenWidth.x = screenWidth.x - _wallsPadding * 2;
        screenWidth.y = 1;
        screenWidth.z = screenWidth.x;

        return screenWidth;
    }
    
    private Vector3 GetScreenWidthInWorldUnits()
    {
        // get the screen height & width in world space units
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = (worldScreenHeight / Screen.height) * Screen.width;
        
        return new Vector3(worldScreenWidth , worldScreenHeight , 1);
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
            return;
        
        if(_boardData == null)
            return;
        
        if(_boardData.columns <= 0 || _boardData.rows <= 0)
            return;

        Vector3 startPoint = transform.localPosition;
        Vector3 BoxScale = GetFieldSize(_tilePrefab.transform.localScale);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPoint , BoxScale);
        
    }
}