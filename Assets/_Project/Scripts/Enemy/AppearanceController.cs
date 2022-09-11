using UnityEngine;

public class AppearanceController
{
    private readonly Material _material;
    private readonly Color _inactiveColor;
    private readonly Color _activeColor;
    private readonly int EmissionColor;

    public AppearanceController(Material material, Color inactiveColor, Color activeColor, string propertyName)
    {
        _inactiveColor = inactiveColor;
        _activeColor = activeColor;
        _material = material;
        EmissionColor = Shader.PropertyToID(propertyName);
    }
    
    public void Visualize(float timePassedPercentage)
    {
        Color color = Utils.LerpColor(_inactiveColor, _activeColor, timePassedPercentage);
        _material.SetColor(EmissionColor, color);
    }
}
