using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event System.Action<BoardTile> OnSelectionEnable;
    
    public static void OnSelectionEnableMethod(BoardTile tile)
    {
        OnSelectionEnable?.Invoke(tile);
    }
    
    public static event System.Action<BoardTile> OnTileSelect;
    
    public static void OnTileSelectMethod(BoardTile tile)
    {
        OnTileSelect?.Invoke(tile);
    }
    
    public static event System.Action OnSelectionDisable;

    public static void OnSelectionDisableMethod()
    {
        OnSelectionDisable?.Invoke();
    }

}
