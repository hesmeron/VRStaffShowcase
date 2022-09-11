using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApperanceController
{
    private Material _material;
    private Color _inactiveColor;
    private Color _activeColor;
    private readonly int EmissionColor;

    public ApperanceController(Material material, Color inactiveColor, Color activeColor, string propertyName)
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
