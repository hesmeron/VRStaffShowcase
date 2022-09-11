using UnityEngine;

/// <summary>
/// Creates instance of selected material and assigns it to renderer. 
/// </summary>
public class MaterialController
{
    private Material _material;

    public Material Material => _material;
    
    public MaterialController(MeshRenderer renderer, Material material)
    {
        _material = new Material(material);
        renderer.material = _material;
    }

    public MaterialController(MeshRenderer renderer)
    {
        _material = new Material(renderer.material);
        renderer.material = _material;
    }
}
