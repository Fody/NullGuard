﻿.class public auto ansi beforefieldinit ClassWithPrivateMethod
	extends [mscorlib]System.Object
{
	// Fields
	.field private string '<SomeProperty>k__BackingField'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method ClassWithPrivateMethod::.ctor

	.method public hidebysig specialname 
		instance string get_SomeProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld string ClassWithPrivateMethod::'<SomeProperty>k__BackingField'
		IL_0006: ret
	} // end of method ClassWithPrivateMethod::get_SomeProperty

	.method public hidebysig 
		instance void PublicWrapperOfPrivateMethod () cil managed 
	{
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 2

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldnull
		IL_0003: call instance void ClassWithPrivateMethod::SomePrivateMethod(string)
		IL_0008: nop
		IL_0009: ret
	} // end of method ClassWithPrivateMethod::PublicWrapperOfPrivateMethod

	.method public hidebysig specialname 
		instance void set_SomeProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld string ClassWithPrivateMethod::'<SomeProperty>k__BackingField'
		IL_0007: ret
	} // end of method ClassWithPrivateMethod::set_SomeProperty

	.method private hidebysig 
		instance void SomePrivateMethod (
			string x
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "x"
		IL_0008: ldstr "[NullGuard] x is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: nop
		IL_0014: ret
	} // end of method ClassWithPrivateMethod::SomePrivateMethod

	// Properties
	.property instance string SomeProperty()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string ClassWithPrivateMethod::get_SomeProperty()
		.set instance void ClassWithPrivateMethod::set_SomeProperty(string)
	}

} // end of class ClassWithPrivateMethod
