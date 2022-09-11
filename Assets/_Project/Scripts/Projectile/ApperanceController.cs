using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApperanceController
{
    private Material _material;
    private Color _inactiveColor;
    private Color _activeColor;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    public ApperanceController(Material material, Color inactiveColor, Color activeColor)
    {
        _inactiveColor = inactiveColor;
        _activeColor = activeColor;
        _material = material;
    }
    
    public void Visualize(float timePassedPercentage)
    {
        Color color = Utils.LerpColor(_inactiveColor, _activeColor, timePassedPercentage);
        _material.SetColor(EmissionColor, color);
    }
}
