using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;
using Xunit.Abstractions;

public class RewritingMethods :
    VerifyBase
{
   public RewritingMethods(ITestOutputHelper output) :
        base(output)
    {
    }

   [Fact]
   public void RequiresNonNullArgumentWhenNullableReferenceTypeNotUsedInClassWithNullableContext1()
   {
       var sample = new ClassWithNullableContext1();
       var exception = Assert.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
       Assert.Equal("nonNullArg", exception.ParamName);
   }

   [Fact]
   public void RequiresNonNullArgumentWhenNullableReferenceTypeNotUsedInClassWithNullableContext2()
   {
       var sample = new ClassWithNullableContext2();
       var exception = Assert.Throws<ArgumentNullException>(() => { sample.SomeMethod(null, ""); });
       Assert.Equal("nonNullArg", exception.ParamName);
   }

   [Fact]
   public void AllowsNullWhenNullableReferenceTypeUsed()
   {
       var sample = new ClassWithNullableContext1();
       sample.SomeMethod("", null);
   }

   [Fact]
   public void AllowsNullWhenNullableReferenceTypeUsedInClassWithNullableContext2()
   {
       var sample = new ClassWithNullableContext2();
       sample.SomeMethod("", null);
   }

   [Fact]
   public void AllowsNullWithoutAttributeWhenNullableReferenceTypeUsedInClassWithNullableContext2()
   {
       var sample = new ClassWithNullableContext2();
       sample.AnotherMethod(null);
   }

   [Fact]
   public Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsedInClassWithNullableContext1()
   {
       var sample = new ClassWithNullableContext1();
       var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
       return Verify(exception.Message);
   }

   [Fact]
   public Task RequiresNonNullMethodReturnValueWhenNullableReferenceTypeNotUsedInClassWithNullableContext2()
   {
       var sample = new ClassWithNullableContext2();
       var exception = Assert.Throws<InvalidOperationException>(() => sample.MethodWithReturnValue(true));
       return Verify(exception.Message);
   }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        sample.MethodAllowsNullReturnValue();
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        sample.MethodAllowsNullReturnValue();
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableReferenceTypeUsedInClassWithNullableReferenceMethod()
    {
        var sample = new ClassWithNullableReferenceMethod();
        sample.MethodAllowsNullReturnValue("");
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableDisabledInClassWithNullableContext1()
    {
        var sample = new ClassWithNullableContext1();
        Assert.Null(sample.MethodWithNullableContext0());
    }

    [Fact]
    public void AllowsNullReturnValueWhenNullableDisabledInClassWithNullableContext2()
    {
        var sample = new ClassWithNullableContext2();
        Assert.Null(sample.MethodWithNullableContext0());
    }

    [Fact]
    public void AllowsNullReturnValueWhenStaticNullableReferenceTypeUsedInClassWithNullableContext1()
    {
        Assert.Null(ClassWithNullableContext1.StaticMethodAllowsNullReturnValue(""));
    }

    [Fact]
    public void AllowsNullReturnValueWhenStaticNullableReferenceTypeUsedInClassWithNullableContext2()
    {
        Assert.Null(ClassWithNullableContext2.StaticMethodAllowsNullReturnValue(""));
    }
}