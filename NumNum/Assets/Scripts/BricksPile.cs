using System.Collections;
using System.Collections.Generic;
using FlexusTween;
using TMPro;
using UnityEngine;

public class BricksPile : MonoBehaviour
{
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private float _stackOffset = 0.2f;
    [SerializeField] private float _counterOffset = 0.1f;

    [SerializeField] private float _decomposeTileTime = 0.2f;
    [SerializeField] private float _decomposeJumpHeight = 0.6f;
    [SerializeField] private float _composeTileDelay = 0.1f;
    
    [SerializeField] private TMP_Text _counter;
    public Material mainMaterial;
    public int count;

    private List<Brick> _bricks;
    private BoardTile[] takenTiles;

    public bool isDecomposed = false;
    public bool isAnimating = false;

    public void Init()
    {
        _bricks = new List<Brick>();

        for (int i = 0; i < count; i++)
        {
            _bricks.Add(Instantiate(_brickPrefab , transform));
            
            _bricks[i].transform.localPosition = GetPositionForBrick(i + 1);
            _bricks[i].SetMaterial(mainMaterial);
        }
        
        _counter.text = count.ToString();
        UpdateCounterPosition();
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
            //_bricks[count - tileIndex].transform.position = takenTiles[tileIndex].transform.position;
            takenTiles[tileIndex].isEmpty = false;
        }

        StartCoroutine(DecomposeCoroutine());
        isDecomposed = true;
    }

    private IEnumerator DecomposeCoroutine()
    {
        isAnimating = true;
        HideCounter();
        
        for (int brick = 1; brick < _bricks.Count; brick++)
        {
            float jumpProgress = 0;
            
            while (jumpProgress <= 1)
            {
                Vector3 startPosition = _bricks[brick].transform.position;
                Vector3 targetPosition = takenTiles[brick].transform.position;
            
                Vector3 lerpPosition;
                lerpPosition = Vector3.Lerp(startPosition, targetPosition, FTween.InOutCubic(jumpProgress));
                lerpPosition.y += FTween.JumpSine(jumpProgress) * _decomposeJumpHeight;

                _bricks[brick].transform.position = lerpPosition;
                
                for (int bricksAbove = brick + 1; bricksAbove < _bricks.Count; bricksAbove++)
                {
                    _bricks[bricksAbove].transform.position = lerpPosition + new Vector3(0, (bricksAbove - brick) * _stackOffset, 0);
                }

                jumpProgress += Time.deltaTime * (1 / _decomposeTileTime);
                yield return null;
            }
        }

        isAnimating = false;
        UpdateCounterPosition();
    }
    

    public void Compose()
    {
        for (int tileIndex = 1; tileIndex < count; tileIndex++)
        {
            // _bricks[tileIndex].transform.localPosition = GetPositionForBrick(tileIndex + 1);
            takenTiles[tileIndex].isEmpty = true;
        }

        StartCoroutine(ComposeCoroutine());
        isDecomposed = false;
    }

    private IEnumerator ComposeCoroutine()
    {
        isAnimating = true;
        HideCounter();
        
        float flyProgress = 0;
        float targetTime = 1 + (_bricks.Count - 1) * _composeTileDelay;

        while (flyProgress < targetTime)
        {
            for (int brick = 0; brick < _bricks.Count; brick++)
            {
                Vector3 startPosition = _bricks[brick].transform.localPosition;
                Vector3 targetPosition = GetPositionForBrick(brick + 1);
            
                Vector3 lerpPosition;
                lerpPosition = Vector3.Lerp(startPosition, targetPosition, FTween.InOutCubic(flyProgress - brick * _composeTileDelay));

                _bricks[brick].transform.localPosition = lerpPosition;
            }

            flyProgress += Time.deltaTime * (1 / _decomposeTileTime);
            yield return null;
        }
        
        isAnimating = false;
        UpdateCounterPosition();
    }

    private void HideCounter()
    {
        _counter.gameObject.SetActive(false);
    }

    private void UpdateCounterPosition()
    {
        _counter.gameObject.SetActive(true);
        
        Vector3 counterPosition = Vector3.zero;
        
        if (isDecomposed)
        {
            counterPosition.y = _stackOffset + _counterOffset;
        }
        else
        {
            counterPosition.y = count * _stackOffset + _counterOffset;
        }

        _counter.transform.localPosition = counterPosition;
    }
}
