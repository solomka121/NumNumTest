using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MaterialsPallet : MonoBehaviour
{
    public Material[] materials;
    public List<Material> uniqueMaterials;

    private void Start()
    {
        uniqueMaterials.AddRange(materials);
    }

    public Material GetRandomMaterial()
    {
        return materials[Random.Range(0, materials.Length)];
    }

    public Material GetUniqueMaterial()
    {
        int i = Random.Range(0, uniqueMaterials.Count);
        Material uniqueMaterial = uniqueMaterials[i];
        uniqueMaterials.RemoveAt(i);
        
        return uniqueMaterial;
    }
}
