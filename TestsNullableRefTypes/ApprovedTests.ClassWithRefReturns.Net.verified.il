﻿.class public auto ansi beforefieldinit ClassWithRefReturns
	extends [mscorlib]System.Object
{
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 01 00 00
	)
	// Nested Types
	.class nested public auto ansi beforefieldinit Generic`1<T>
		extends [mscorlib]System.Object
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		.param type T
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Fields
		.field private !T _value

		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 12
			// Code size: 7 (0x7)
			.maxstack 1

			IL_0000: ldarg.0
			IL_0001: call instance void [mscorlib]System.Object::.ctor()
			IL_0006: ret
		} // end of method Generic`1::.ctor

		.method public hidebysig 
			instance !T& GetMaybeNullUnconstrainedRef () cil managed 
		{
			// Header size: 12
			// Code size: 7 (0x7)
			.maxstack 1

			IL_0000: ldarg.0
			IL_0001: ldflda !0 class ClassWithRefReturns/Generic`1<!T>::_value
			IL_0006: ret
		} // end of method Generic`1::GetMaybeNullUnconstrainedRef

	} // end of class Generic`1

	.class nested public auto ansi beforefieldinit GenericNonNull`1<T>
		extends [mscorlib]System.Object
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		// Fields
		.field private !T _value

		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				!T 'value'
			) cil managed 
		{
			.param [1]
				.custom instance void System.Diagnostics.CodeAnalysis.AllowNullAttribute::.ctor() = (
					01 00 00 00
				)
			// Header size: 12
			// Code size: 14 (0xe)
			.maxstack 2

			IL_0000: ldarg.0
			IL_0001: call instance void [mscorlib]System.Object::.ctor()
			IL_0006: ldarg.0
			IL_0007: ldarg.1
			IL_0008: stfld !0 class ClassWithRefReturns/GenericNonNull`1<!T>::_value
			IL_000d: ret
		} // end of method GenericNonNull`1::.ctor

		.method public hidebysig 
			instance !T& GetNonNullRef () cil managed 
		{
			// Header size: 12
			// Code size: 32 (0x20)
			.maxstack 2

			IL_0000: ldarg.0
			IL_0001: ldflda !0 class ClassWithRefReturns/GenericNonNull`1<!T>::_value
			IL_0006: dup
			IL_0007: ldobj !T
			IL_000c: box !T
			IL_0011: brtrue.s IL_001f

			IL_0013: pop
			IL_0014: ldstr "[NullGuard] Return value of method 'T& ClassWithRefReturns/GenericNonNull`1::GetNonNullRef()' is null."
			IL_0019: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
			IL_001e: throw

			IL_001f: ret
		} // end of method GenericNonNull`1::GetNonNullRef

	} // end of class GenericNonNull`1


	// Fields
	.field private string _value
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 02 00 00
	)

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
	} // end of method ClassWithRefReturns::.ctor

	.method public hidebysig 
		instance string& GetNonNullRef () cil managed 
	{
		// Header size: 12
		// Code size: 23 (0x17)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldflda string ClassWithRefReturns::_value
		IL_0006: dup
		IL_0007: ldind.ref
		IL_0008: brtrue.s IL_0016

		IL_000a: pop
		IL_000b: ldstr "[NullGuard] Return value of method 'System.String& ClassWithRefReturns::GetNonNullRef()' is null."
		IL_0010: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0015: throw

		IL_0016: ret
	} // end of method ClassWithRefReturns::GetNonNullRef

	.method public hidebysig 
		instance string& GetNullRef () cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: ldflda string ClassWithRefReturns::_value
		IL_0006: ret
	} // end of method ClassWithRefReturns::GetNullRef

} // end of class ClassWithRefReturns
