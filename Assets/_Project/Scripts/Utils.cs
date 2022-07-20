using System.Threading;
using UnityEngine;

public static class Utils
{
    public static void SetStartAndEnd(this LineRenderer renderer, Vector3 start, Vector3 end)
    {
        renderer.SetPositions(new Vector3[] {start, end});
    }
}
