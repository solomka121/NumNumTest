using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    public Color GetColor => _meshRenderer.material.color;

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
