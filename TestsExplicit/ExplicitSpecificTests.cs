namespace TestsExplicit
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    [TestFixtureSource(nameof(FixtureArgs))]
    public class ExplicitSpecificTests
    {
        static object[] FixtureArgs = {
            new object[] { "DerivedClass", string.Empty },
            new object[] { "ImplementsInterface", string.Empty },
            new object[] { "ImplementsInheritedInterface", string.Empty },
            new object[] { "ImplementsInterfaceExplicit", "InterfaceWithAttributes." },
        };

        private readonly string _className;
        private readonly string _interfaceName;

        public ExplicitSpecificTests(string className, string interfaceName)
        {
            _className = className;
            _interfaceName = interfaceName;
        }

        [Test]
        public void InheritsNullabilityForMethodParameterAndThrowsOnNull()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            var exception = Assert.Throws<ArgumentNullException>(() => sample.MethodWithNotNullParameter((string)null, (string)null));
            Assert.AreEqual("[NullGuard] arg is null.\r\nParameter name: arg", exception.Message);
        }

        [Test]
        public void InheritsNullabilityForMethodParameterAndDoesNotThrowOnNotNull()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            Assert.DoesNotThrow(() => sample.MethodWithNotNullParameter((string)null, "Test"));
        }

        [Test]
        public void InheritsNullabilityForMethodReturnAndThrowsOnNull()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithNotNullReturnValue((string)null));
            Assert.AreEqual($"[NullGuard] Return value of method 'System.String {_className}::{_interfaceName}MethodWithNotNullReturnValue(System.String)' is null.", exception.Message);
        }

        [Test]
        public void InheritsNullabilityForMethodReturnAndDoesNotThrowOnNotNull()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            Assert.DoesNotThrow(() => sample.MethodWithNotNullReturnValue("Test"));
        }

        [Test]
        public void InheritsNullabilityForPropertyAndThrowsOnNullSet()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            var exception = Assert.Throws<ArgumentNullException>(() => sample.NotNullProperty = (string)null);
            Assert.AreEqual($"[NullGuard] Cannot set the value of property 'System.String {_className}::{_interfaceName}NotNullProperty()' to null.\r\nParameter name: value", exception.Message);
        }

        [Test]
        public void InheritsNullabilityForPropertyAndDoesNotThrowOnNotNullSet()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            Assert.DoesNotThrow(() => sample.NotNullProperty = "Test");
        }

        [Test]
        public void InheritsNullabilityForPropertyAndThrowsOnNullGet()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            string value;
            var exception = Assert.Throws<InvalidOperationException>(() => value = sample.NotNullProperty);
            Assert.AreEqual($"[NullGuard] Return value of property 'System.String {_className}::{_interfaceName}NotNullProperty()' is null.", exception.Message);
        }

        [Test]
        public void InheritsNullabilityForPropertyAndDoesNotThrowOnNotNullGet()
        {
            var type = AssemblyWeaver.Assembly.GetType(_className);
            var sample = (dynamic)Activator.CreateInstance(type);
            string value;
            Assert.DoesNotThrow(() =>
            {
                sample.NotNullProperty = "Test";
                value = sample.NotNullProperty;
            });
        }
    }
}
