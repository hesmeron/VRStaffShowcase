using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

public class ApperanceControllerTest
{
    private ApperanceController _apperanceController;
    private Material _material;
    //OIn the future this script will uise configuation files used in gameplay instead of mock data
    [SetUp]
    public void OnSetup()
    {
        _material = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshRenderer>().CloneAndAssignMaterial();
        _apperanceController = new ApperanceController(_material, 
                                            Color.black,
                                            Color.white,
                                            "_EmissionColor");
    }
    
    [Test]
    public void AppearanceControllerSinglePassTest()
    {
        for (int i = 0; i < 100; i++)
        {
            float percentage = i / 100f;
            _apperanceController.Visualize(percentage);
            Color _materialColor = _material.GetColor("_EmissionColor");
            Assert.IsTrue(AreColorsSimilarEnough(
                _materialColor,
                new Color(percentage, percentage, percentage, 1f)));
        }
    }

    private bool AreColorsSimilarEnough(Color a, Color b, float acceptedDifferece = 0.01f)
    {
        float differece = 0f;
        differece += Mathf.Abs(a.r - b.r);
        differece += Mathf.Abs(a.g - b.g);
        differece += Mathf.Abs(a.b - b.b);
        return differece < acceptedDifferece;
    }
}
