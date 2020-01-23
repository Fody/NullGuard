﻿#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class ClassWithAsyncMethods
{
    public string? InstanceProperty { get; set; }

    public static async Task<string> GetNonNullAsync()
    {
        return null!;
    }

    public static async Task<string?> GetNullAsync()
    {
        return null;
    }

    public static async Task<T> GetNonNullAsyncWithDelay<T>() where T : notnull
    {
        await Task.Delay(1);
        return default!;
    }

    public static async Task<T?> GetNullAsyncWithDelay<T>() where T : class
    {
        await Task.Delay(1);
        return null;
    }

    public static async Task<T> GetNullAsyncWithDelay2<T>() where T : class?
    {
        await Task.Delay(1);
        return default!;
    }
}
