﻿.class public auto ansi beforefieldinit ClassWithNullableContext1
	extends [mscorlib]System.Object
{
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 01 00 00
	)
	// Fields
	.field private class [mscorlib]System.Tuple`2<string, string> '<MixedNonNullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
		01 00 03 00 00 00 01 02 01 00 00
	)
	.field private class [mscorlib]System.Tuple`2<string, string> '<MixedNullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
		01 00 03 00 00 00 02 02 01 00 00
	)
	.field private string '<NonNullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<NullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
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
	} // end of method ClassWithNullableContext1::.ctor

	.method public hidebysig 
		instance void AndAnotherMethod (
			string nonNullArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ret
	} // end of method ClassWithNullableContext1::AndAnotherMethod

	.method public hidebysig 
		instance string AnotherMethod (
			string nonNullArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 36 (0x24)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.1
		IL_0014: dup
		IL_0015: brtrue.s IL_0023

		IL_0017: pop
		IL_0018: ldstr "[NullGuard] Return value of method 'System.String ClassWithNullableContext1::AnotherMethod(System.String)' is null."
		IL_001d: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0022: throw

		IL_0023: ret
	} // end of method ClassWithNullableContext1::AnotherMethod

	.method public hidebysig 
		instance !!T GenericMaybeNullReturnValue<T> () cil managed 
	{
		.param [0]
			.custom instance void [mscorlib]System.Diagnostics.CodeAnalysis.MaybeNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 1
		.locals init (
			[0] !!T
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj !!T
		IL_0008: ldloc.0
		IL_0009: ret
	} // end of method ClassWithNullableContext1::GenericMaybeNullReturnValue

	.method public hidebysig specialname 
		instance class [mscorlib]System.Tuple`2<string, string> get_MixedNonNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.param [0]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
				01 00 03 00 00 00 01 02 01 00 00
			)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext1::'<MixedNonNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.Tuple`2<System.String,System.String> ClassWithNullableContext1::MixedNonNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ClassWithNullableContext1::get_MixedNonNullProperty

	.method public hidebysig specialname 
		instance class [mscorlib]System.Tuple`2<string, string> get_MixedNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.param [0]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
				01 00 03 00 00 00 02 02 01 00 00
			)
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: ldfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext1::'<MixedNullProperty>k__BackingField'
		IL_0006: ret
	} // end of method ClassWithNullableContext1::get_MixedNullProperty

	.method public hidebysig specialname 
		instance string get_NonNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string ClassWithNullableContext1::'<NonNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String ClassWithNullableContext1::NonNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ClassWithNullableContext1::get_NonNullProperty

	.method public hidebysig specialname 
		instance string get_NullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: ldfld string ClassWithNullableContext1::'<NullProperty>k__BackingField'
		IL_0006: ret
	} // end of method ClassWithNullableContext1::get_NullProperty

	.method public hidebysig 
		instance string MethodAllowsNullReturnValue () cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 1

		IL_0000: ldnull
		IL_0001: ret
	} // end of method ClassWithNullableContext1::MethodAllowsNullReturnValue

	.method public hidebysig 
		instance void MethodWithManyParameters (
			string nullArg1,
			string nonNullArg2,
			string nullArg3,
			string nonNullArg4
		) cil managed 
	{
		.param [1]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		.param [3]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Header size: 12
		// Code size: 40 (0x28)
		.maxstack 2

		IL_0000: ldarg.2
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg2"
		IL_0008: ldstr "[NullGuard] nonNullArg2 is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.s nonNullArg4
		IL_0015: brtrue.s IL_0027

		IL_0017: ldstr "nonNullArg4"
		IL_001c: ldstr "[NullGuard] nonNullArg4 is null."
		IL_0021: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0026: throw

		IL_0027: ret
	} // end of method ClassWithNullableContext1::MethodWithManyParameters

	.method public hidebysig 
		instance string MethodWithNullableContext0 () cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 1

		IL_0000: ldnull
		IL_0001: ret
	} // end of method ClassWithNullableContext1::MethodWithNullableContext0

	.method public hidebysig 
		instance string MethodWithReturnValue (
			bool returnNull
		) cil managed 
	{
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0018

		IL_0003: ldstr ""
		IL_0008: dup
		IL_0009: brtrue.s IL_0017

		IL_000b: pop
		IL_000c: ldstr "[NullGuard] Return value of method 'System.String ClassWithNullableContext1::MethodWithReturnValue(System.Boolean)' is null."
		IL_0011: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0016: throw

		IL_0017: ret

		IL_0018: ldnull
		IL_0019: dup
		IL_001a: brtrue.s IL_0028

		IL_001c: pop
		IL_001d: ldstr "[NullGuard] Return value of method 'System.String ClassWithNullableContext1::MethodWithReturnValue(System.Boolean)' is null."
		IL_0022: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0027: throw

		IL_0028: ret
	} // end of method ClassWithNullableContext1::MethodWithReturnValue

	.method public hidebysig 
		instance !!T NotNullGeneric<T> (
			!!T nonNullArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 54 (0x36)
		.maxstack 2
		.locals init (
			[0] !!T
		)

		IL_0000: ldarg.1
		IL_0001: box !!T
		IL_0006: brtrue.s IL_0018

		IL_0008: ldstr "nonNullArg"
		IL_000d: ldstr "[NullGuard] nonNullArg is null."
		IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0017: throw

		IL_0018: ldloca.s 0
		IL_001a: initobj !!T
		IL_0020: ldloc.0
		IL_0021: dup
		IL_0022: box !!T
		IL_0027: brtrue.s IL_0035

		IL_0029: pop
		IL_002a: ldstr "[NullGuard] Return value of method 'T ClassWithNullableContext1::NotNullGeneric(T)' is null."
		IL_002f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0034: throw

		IL_0035: ret
	} // end of method ClassWithNullableContext1::NotNullGeneric

	.method public hidebysig specialname 
		instance void set_MixedNonNullProperty (
			class [mscorlib]System.Tuple`2<string, string> 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.param [1]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
				01 00 03 00 00 00 01 02 01 00 00
			)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "value"
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.Tuple`2<System.String,System.String> ClassWithNullableContext1::MixedNonNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext1::'<MixedNonNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method ClassWithNullableContext1::set_MixedNonNullProperty

	.method public hidebysig specialname 
		instance void set_MixedNullProperty (
			class [mscorlib]System.Tuple`2<string, string> 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.param [1]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
				01 00 03 00 00 00 02 02 01 00 00
			)
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext1::'<MixedNullProperty>k__BackingField'
		IL_0007: ret
	} // end of method ClassWithNullableContext1::set_MixedNullProperty

	.method public hidebysig specialname 
		instance void set_NonNullProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "value"
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String ClassWithNullableContext1::NonNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string ClassWithNullableContext1::'<NonNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method ClassWithNullableContext1::set_NonNullProperty

	.method public hidebysig specialname 
		instance void set_NullProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld string ClassWithNullableContext1::'<NullProperty>k__BackingField'
		IL_0007: ret
	} // end of method ClassWithNullableContext1::set_NullProperty

	.method public hidebysig 
		instance void SomeMethod (
			string nonNullArg,
			string nullArg
		) cil managed 
	{
		.param [2]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Header size: 12
		// Code size: 26 (0x1a)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.1
		IL_0014: call void [mscorlib]System.Console::WriteLine(string)
		IL_0019: ret
	} // end of method ClassWithNullableContext1::SomeMethod

	.method public hidebysig static 
		string StaticMethodAllowsNullReturnValue (
			string nonNullArg
		) cil managed 
	{
		.param [0]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldnull
		IL_0014: ret
	} // end of method ClassWithNullableContext1::StaticMethodAllowsNullReturnValue

	.method public hidebysig 
		instance !!T UnconstrainedGeneric<T> (
			!!T 'value'
		) cil managed 
	{
		.param type T
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 1
		.locals init (
			[0] !!T
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj !!T
		IL_0008: ldloc.0
		IL_0009: ret
	} // end of method ClassWithNullableContext1::UnconstrainedGeneric

	// Properties
	.property instance class [mscorlib]System.Tuple`2<string, string> MixedNonNullProperty()
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
			01 00 03 00 00 00 01 02 01 00 00
		)
		.get instance class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext1::get_MixedNonNullProperty()
		.set instance void ClassWithNullableContext1::set_MixedNonNullProperty(class [mscorlib]System.Tuple`2<string, string>)
	}
	.property instance class [mscorlib]System.Tuple`2<string, string> MixedNullProperty()
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
			01 00 03 00 00 00 02 02 01 00 00
		)
		.get instance class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext1::get_MixedNullProperty()
		.set instance void ClassWithNullableContext1::set_MixedNullProperty(class [mscorlib]System.Tuple`2<string, string>)
	}
	.property instance string NonNullProperty()
	{
		.get instance string ClassWithNullableContext1::get_NonNullProperty()
		.set instance void ClassWithNullableContext1::set_NonNullProperty(string)
	}
	.property instance string NullProperty()
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		.get instance string ClassWithNullableContext1::get_NullProperty()
		.set instance void ClassWithNullableContext1::set_NullProperty(string)
	}

} // end of class ClassWithNullableContext1
