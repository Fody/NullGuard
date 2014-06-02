using System;
using System.Reflection;
using System.Runtime.ExceptionServices;

internal static class Reflection
{
    public static void SetPublicPropertyValue(this Type type, object instance, string propertyName, object value)
    {
        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
        InvokeAndRethrowTargetInvocationInnerException(() => propertyInfo.SetValue(instance, value));
    }

    public static void SetNonPublicPropertyValue(this Type type, object instance, string propertyName, object value)
    {
        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
        InvokeAndRethrowTargetInvocationInnerException(() => propertyInfo.SetValue(instance, value));
    }

    public static void InvokePublicMethod(this Type type, object instance, string methodName, object[] parameters)
    {
        var methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
        InvokeAndRethrowTargetInvocationInnerException(() => methodInfo.Invoke(instance, parameters));
    }

    public static void InvokeNonPublicMethod(this Type type, object instance, string methodName, object[] parameters)
    {
        var methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
        InvokeAndRethrowTargetInvocationInnerException(() => methodInfo.Invoke(instance, parameters));
    }

    private static void InvokeAndRethrowTargetInvocationInnerException(Action action)
    {
        try
        {
            action();
        }
        catch (TargetInvocationException ex)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
        }
    }
}