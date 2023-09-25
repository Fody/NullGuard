using System;

[AttributeUsage (
    AttributeTargets.Method |
    AttributeTargets.Property |
    AttributeTargets.Field |
    AttributeTargets.Parameter |
    AttributeTargets.Delegate)]
public sealed class CanBeNullAttribute : Attribute;