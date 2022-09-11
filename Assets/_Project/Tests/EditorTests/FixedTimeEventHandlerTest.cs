using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;


public class FixedTimeEventHandlerTest
{
    private class TestAffectedClass
    {
        public float CurrentPercentage => _currentPercentage;

        private float _currentPercentage = 0f;
        private bool _completed = false;
        private bool _hasProgressed = true;

        public bool Completed => _completed;

        public bool HasProgressed => _hasProgressed;

        public void OnComplete()
        {
            _completed = true;
        }
        public void OnChanged(float percentage)
        {
            _hasProgressed = percentage > _currentPercentage;
            _currentPercentage = percentage;
        }
    }

    [SetUp]
    public void OnSetUp()
    {
        Time.timeScale = 100f;
    }
    [TearDown]
    public void OnTeardown()
    {
        Time.timeScale = 100f;
    }

    [UnityTest]
    public IEnumerator FixedTimeEventHandlerTestWithEnumeratorPasses()
    {
        for (int i = 0; i < 100; i++)
        {
            float timeToPass = i/10f;
            float timePassed = 0f;
            FixedTimeEventHandler fixedTimeEventHandler = new FixedTimeEventHandler(timeToPass);
            TestAffectedClass testAffectedClass = new TestAffectedClass();
            fixedTimeEventHandler.OnCountdownCompleted += testAffectedClass.OnComplete;
            fixedTimeEventHandler.OnCountdownChanged += testAffectedClass.OnChanged;
            yield return null;
            while (!testAffectedClass.Completed)
            {
                timePassed += Time.deltaTime;
                fixedTimeEventHandler.ProgressTimer();
                yield return null;
                Assert.IsTrue(testAffectedClass.HasProgressed);
                float calculatedPercentage = Mathf.Clamp01(timePassed / timeToPass);
                float difference = Mathf.Abs(testAffectedClass.CurrentPercentage - calculatedPercentage);
                Assert.IsTrue(difference < 1f || timeToPass == 0);
            }
        }

    }
}
