﻿.class public auto ansi beforefieldinit ClassWithNullableReferenceMethod
	extends [netstandard]System.Object
{
	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: ret
	} // end of method ClassWithNullableReferenceMethod::.ctor

	.method public hidebysig 
		instance string MethodAllowsNullReturnValue (
			string nonNullArg
		) cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
		.param [0]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldnull
		IL_0014: ret
	} // end of method ClassWithNullableReferenceMethod::MethodAllowsNullReturnValue

} // end of class ClassWithNullableReferenceMethod
