﻿.class public auto ansi beforefieldinit ClassWithNullableContext2
	extends [mscorlib]System.Object
{
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 02 00 00
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
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 01 00 00
	)
	.field private string '<NullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
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
	} // end of method ClassWithNullableContext2::.ctor

	.method public hidebysig 
		instance void FillerMethod1 (
			string 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0

		IL_0000: ret
	} // end of method ClassWithNullableContext2::FillerMethod1

	.method public hidebysig 
		instance void FillerMethod2 (
			string 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0

		IL_0000: ret
	} // end of method ClassWithNullableContext2::FillerMethod2

	.method public hidebysig 
		instance void FillerMethod3 (
			string 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0

		IL_0000: ret
	} // end of method ClassWithNullableContext2::FillerMethod3

	.method public hidebysig 
		instance !!T GenericClassWithNullableParam<class T> (
			!!T 'value'
		) cil managed 
	{
		.param type T
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 01 00 00
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
	} // end of method ClassWithNullableContext2::GenericClassWithNullableParam

	.method public hidebysig 
		instance !!T GenericNotNullReturnValue<T> () cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
		.param type T
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		.param [0]
			.custom instance void System.Diagnostics.CodeAnalysis.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 30 (0x1e)
		.maxstack 2
		.locals init (
			[0] !!T
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj !!T
		IL_0008: ldloc.0
		IL_0009: dup
		IL_000a: box !!T
		IL_000f: brtrue.s IL_001d

		IL_0011: pop
		IL_0012: ldstr "[NullGuard] Return value of method 'T ClassWithNullableContext2::GenericNotNullReturnValue()' is null."
		IL_0017: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_001c: throw

		IL_001d: ret
	} // end of method ClassWithNullableContext2::GenericNotNullReturnValue

	.method public hidebysig 
		instance !!T GenericNullableClassWithNotNullableParam<class T> (
			!!T 'value'
		) cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
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
	} // end of method ClassWithNullableContext2::GenericNullableClassWithNotNullableParam

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
		IL_0001: ldfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext2::'<MixedNonNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.Tuple`2<System.String,System.String> ClassWithNullableContext2::MixedNonNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ClassWithNullableContext2::get_MixedNonNullProperty

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
		IL_0001: ldfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext2::'<MixedNullProperty>k__BackingField'
		IL_0006: ret
	} // end of method ClassWithNullableContext2::get_MixedNullProperty

	.method public hidebysig specialname 
		instance string get_NonNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string ClassWithNullableContext2::'<NonNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String ClassWithNullableContext2::NonNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ClassWithNullableContext2::get_NonNullProperty

	.method public hidebysig specialname 
		instance string get_NullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: ldfld string ClassWithNullableContext2::'<NullProperty>k__BackingField'
		IL_0006: ret
	} // end of method ClassWithNullableContext2::get_NullProperty

	.method public hidebysig 
		instance string MethodAllowsNullReturnValue () cil managed 
	{
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 1

		IL_0000: ldnull
		IL_0001: ret
	} // end of method ClassWithNullableContext2::MethodAllowsNullReturnValue

	.method public hidebysig 
		instance void MethodWillNullableArg (
			string nullArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0

		IL_0000: ret
	} // end of method ClassWithNullableContext2::MethodWillNullableArg

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
	} // end of method ClassWithNullableContext2::MethodWithNullableContext0

	.method public hidebysig 
		instance string MethodWithReturnValue (
			bool returnNull
		) cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0018

		IL_0003: ldstr ""
		IL_0008: dup
		IL_0009: brtrue.s IL_0017

		IL_000b: pop
		IL_000c: ldstr "[NullGuard] Return value of method 'System.String ClassWithNullableContext2::MethodWithReturnValue(System.Boolean)' is null."
		IL_0011: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0016: throw

		IL_0017: ret

		IL_0018: ldnull
		IL_0019: dup
		IL_001a: brtrue.s IL_0028

		IL_001c: pop
		IL_001d: ldstr "[NullGuard] Return value of method 'System.String ClassWithNullableContext2::MethodWithReturnValue(System.Boolean)' is null."
		IL_0022: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0027: throw

		IL_0028: ret
	} // end of method ClassWithNullableContext2::MethodWithReturnValue

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
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.Tuple`2<System.String,System.String> ClassWithNullableContext2::MixedNonNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext2::'<MixedNonNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method ClassWithNullableContext2::set_MixedNonNullProperty

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
		IL_0002: stfld class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext2::'<MixedNullProperty>k__BackingField'
		IL_0007: ret
	} // end of method ClassWithNullableContext2::set_MixedNullProperty

	.method public hidebysig specialname 
		instance void set_NonNullProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "value"
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String ClassWithNullableContext2::NonNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string ClassWithNullableContext2::'<NonNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method ClassWithNullableContext2::set_NonNullProperty

	.method public hidebysig specialname 
		instance void set_NullProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld string ClassWithNullableContext2::'<NullProperty>k__BackingField'
		IL_0007: ret
	} // end of method ClassWithNullableContext2::set_NullProperty

	.method public hidebysig 
		instance void SomeMethod (
			string nonNullArg,
			string nullArg
		) cil managed 
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
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
	} // end of method ClassWithNullableContext2::SomeMethod

	.method public hidebysig static 
		string StaticMethodAllowsNullReturnValue (
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

		IL_0000: ldarg.0
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldnull
		IL_0014: ret
	} // end of method ClassWithNullableContext2::StaticMethodAllowsNullReturnValue

	// Properties
	.property instance class [mscorlib]System.Tuple`2<string, string> MixedNonNullProperty()
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
			01 00 03 00 00 00 01 02 01 00 00
		)
		.get instance class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext2::get_MixedNonNullProperty()
		.set instance void ClassWithNullableContext2::set_MixedNonNullProperty(class [mscorlib]System.Tuple`2<string, string>)
	}
	.property instance class [mscorlib]System.Tuple`2<string, string> MixedNullProperty()
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
			01 00 03 00 00 00 02 02 01 00 00
		)
		.get instance class [mscorlib]System.Tuple`2<string, string> ClassWithNullableContext2::get_MixedNullProperty()
		.set instance void ClassWithNullableContext2::set_MixedNullProperty(class [mscorlib]System.Tuple`2<string, string>)
	}
	.property instance string NonNullProperty()
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
		.get instance string ClassWithNullableContext2::get_NonNullProperty()
		.set instance void ClassWithNullableContext2::set_NonNullProperty(string)
	}
	.property instance string NullProperty()
	{
		.get instance string ClassWithNullableContext2::get_NullProperty()
		.set instance void ClassWithNullableContext2::set_NullProperty(string)
	}

} // end of class ClassWithNullableContext2
