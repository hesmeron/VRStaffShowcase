using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{
    private Movement _movement;
    private Transform _transform;
    
    [SetUp]
    public void OnSetup()
    {
        _movement = new Movement();
         _transform = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        _movement.Initialize(_transform, 300f);
    }
    
    [UnityTest]
    public IEnumerator MovementTestWithEnumeratorPasses()
    {
        Vector3 translation = new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        Vector3 targetPosition = _transform.position + translation * Random.Range(0, 2000f);
        float lastDistance = translation.magnitude;
        float lastAngle = Vector3.Angle(_transform.forward, translation);
        float distance = lastDistance;
        yield return null;
        while (distance > 0.01f )
        {
            _movement.MoveTowards(targetPosition);
            yield return null;
            distance = Vector3.Distance(_transform.position, targetPosition);
            float angle = Vector3.Angle(_transform.forward, translation);
            Assert.IsTrue(distance < lastDistance || angle < lastAngle);
            lastAngle = angle;
            lastDistance = distance;
        }
    }
}
