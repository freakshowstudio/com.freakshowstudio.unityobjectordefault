
using FreakshowStudio.UnityObjectOrDefault.Runtime;
using NUnit.Framework;
using UnityEngine;


namespace FreakshowStudio.UnityObjectOrDefault.Tests.Runtime
{
    internal class TestUnityObjectOrDefault
    {
        private interface ITestInterface { }
        private class TestBehaviour : MonoBehaviour, ITestInterface { }
        private class DerivedTestBehaviour : TestBehaviour { }
        private class NonUnityTestClass : ITestInterface { }
        private class TestScriptableObject : ScriptableObject { }

        [Test]
        public void NonUnityObject_WhenNull_ShouldReturnNull()
        {
            object? obj = null;
            Assert.That(obj.UnityObjectOrDefault(), Is.Null);
        }

        [Test]
        public void NonUnityObject_WhenNotNull_ShouldReturnNotNull()
        {
            object obj = new object();
            Assert.That(obj.UnityObjectOrDefault(), Is.Not.Null);
        }

        [Test]
        public void UnityObject_WhenNotNull_ShouldReturnNotNull()
        {
            var go = new GameObject();
            Assert.That(go.UnityObjectOrDefault(), Is.Not.Null);
        }

        [Test]
        public void UnityObject_WhenDestroyed_ShouldReturnNull()
        {
            var go = new GameObject();
            Object.DestroyImmediate(go);
            Assert.That(go == null);
            Assert.That(go.UnityObjectOrDefault(), Is.Null);
        }

        [Test]
        public void Interface_WhenNotNull_ShouldReturnNotNull()
        {
            var go = new GameObject();
            ITestInterface i = go.AddComponent<TestBehaviour>();
            Assert.That(i.UnityObjectOrDefault(), Is.Not.Null);
        }

        [Test]
        public void Interface_WhenDestroyed_ShouldReturnNull()
        {
            var go = new GameObject();
            var b = go.AddComponent<TestBehaviour>();
            var i = b as ITestInterface;
            Object.DestroyImmediate(b);
            // Don't use Is.Null here as it bypasses the Unity null check
            Assert.That(b == null, Is.True);
            Assert.That(i.UnityObjectOrDefault(), Is.Null);
        }

        [Test]
        public void Component_WhenGameObjectDestroyed_ShouldReturnNull()
        {
            var go = new GameObject();
            var component = go.AddComponent<TestBehaviour>();
            Object.DestroyImmediate(go);
            Assert.That(component.UnityObjectOrDefault(), Is.Null);
        }

        [Test]
        public void UnityObject_WhenExplicitlyNull_ShouldReturnNull()
        {
            GameObject? go = null;
            Assert.That(go.UnityObjectOrDefault(), Is.Null);
        }

        [Test]
        public void Interface_WhenPureCSharpObject_ShouldReturnNotNull()
        {
            ITestInterface i = new NonUnityTestClass();
            Assert.That(i.UnityObjectOrDefault(), Is.Not.Null);
        }

        [Test]
        public void Interface_WhenPureCSharpObjectNull_ShouldReturnNull()
        {
            ITestInterface? i = null;
            Assert.That(i.UnityObjectOrDefault(), Is.Null);
        }

        [Test]
        public void DerivedComponent_WhenNotNull_ShouldReturnNotNull()
        {
            var go = new GameObject();
            var component = go.AddComponent<DerivedTestBehaviour>();
            Assert.That(component.UnityObjectOrDefault(), Is.Not.Null);
        }

        [Test]
        public void DerivedComponent_WhenDestroyed_ShouldReturnNull()
        {
            var go = new GameObject();
            var component = go.AddComponent<DerivedTestBehaviour>();
            Object.DestroyImmediate(component);
            Assert.That(component.UnityObjectOrDefault(), Is.Null);
        }

        [Test]
        public void ScriptableObject_WhenNotNull_ShouldReturnNotNull()
        {
            var so = ScriptableObject.CreateInstance<TestScriptableObject>();
            Assert.That(so.UnityObjectOrDefault(), Is.Not.Null);
        }

        [Test]
        public void ScriptableObject_WhenDestroyed_ShouldReturnNull()
        {
            var so = ScriptableObject.CreateInstance<TestScriptableObject>();
            Object.DestroyImmediate(so);
            Assert.That(so.UnityObjectOrDefault(), Is.Null);
        }
    }
}
