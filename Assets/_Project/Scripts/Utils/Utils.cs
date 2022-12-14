using System;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// This is a class miscellaneous static utility functions,
/// as the project matures this should be seperated into more specific classes 
/// </summary>
public static class Utils
{
    static Random _R = new Random();

    public static void SetStartAndEnd(this LineRenderer renderer, Vector3 start, Vector3 end)
    {
        renderer.SetPositions(new Vector3[] { start, end });
    }

    public static Vector3 LerpVector(Vector3 from, Vector3 to, float amount)
    {
        float x = Mathf.Lerp(from.x, to.x, amount);
        float y = Mathf.Lerp(from.y, to.y, amount);
        float z = Mathf.Lerp(from.z, to.z, amount);
        return new Vector3(x, y, z);
    }

    public static T RandomEnumValue<T>() where T : Enum
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(_R.Next(v.Length));
    }

    public static Color LerpColor(Color from, Color to, float amount)
    {
        float r = Mathf.Lerp(from.r, to.r, amount);
        float g = Mathf.Lerp(from.g, to.g, amount);
        float b = Mathf.Lerp(from.g, to.b, amount);
        float a = Mathf.Lerp(from.a, to.a, amount);
        return new Color(r, g, b, a);
    }

    public static T RandomElement<T>(this T[] array)
    {
        return array[(_R.Next(array.Length))];
    }

    public static Material CloneAndAssignMaterial(this MeshRenderer renderer)
    {
        Material material = renderer.material;
        return renderer.CloneAndAssignMaterial(material);

    }
    
    public static Material CloneAndAssignMaterial(this MeshRenderer renderer, Material material)
    {
        material = new Material(material);
        renderer.material = material;
        return material;
    }
    
    
}
