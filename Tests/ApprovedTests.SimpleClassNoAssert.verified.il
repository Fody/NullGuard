﻿.class public auto ansi beforefieldinit SimpleClass
	extends [mscorlib]System.Object
{
	// Fields
	.field private valuetype [mscorlib]System.Nullable`1<int32> '<NonNullNullableProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<NonNullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<NullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<PropertyAllowsNullGetButDoesNotAllowNullSet>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<PropertyAllowsNullSetButDoesNotAllowNullGet>k__BackingField'
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
	} // end of method SimpleClass::.ctor

	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			[out] string& nonNullOutArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 25 (0x19)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ldarg.1
		IL_0007: ldnull
		IL_0008: stind.ref
		IL_0009: ldarg.1
		IL_000a: ldind.ref
		IL_000b: brtrue.s IL_0018

		IL_000d: ldstr "[NullGuard] Out parameter 'nonNullOutArg' is null."
		IL_0012: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0017: throw

		IL_0018: ret
	} // end of method SimpleClass::.ctor

	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			string nonNullArg,
			string nullArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 43 (0x2b)
		.maxstack 3

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: call instance void [mscorlib]System.Object::.ctor()
		IL_0019: ldarg.1
		IL_001a: ldstr " "
		IL_001f: ldarg.2
		IL_0020: call string [mscorlib]System.String::Concat(string, string, string)
		IL_0025: call void [mscorlib]System.Console::WriteLine(string)
		IL_002a: ret
	} // end of method SimpleClass::.ctor

	.method public hidebysig specialname 
		instance string get_GetterReturnValueChecksWithBranchToRetInstruction () cil managed 
	{
		// Header size: 12
		// Code size: 44 (0x2c)
		.maxstack 2
		.locals init (
			[0] string
		)

		IL_0000: ldnull
		IL_0001: stloc.0
		IL_0002: ldstr ""
		IL_0007: call instance int32 [mscorlib]System.String::get_Length()
		IL_000c: ldc.i4.s 42
		IL_000e: bne.un.s IL_001b

		IL_0010: ldstr "Not reachable"
		IL_0015: newobj instance void [mscorlib]System.Exception::.ctor(string)
		IL_001a: throw

		IL_001b: ldloc.0
		IL_001c: dup
		IL_001d: brtrue.s IL_002b

		IL_001f: pop
		IL_0020: ldstr "[NullGuard] Return value of property 'System.String SimpleClass::GetterReturnValueChecksWithBranchToRetInstruction()' is null."
		IL_0025: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_002a: throw

		IL_002b: ret
	} // end of method SimpleClass::get_GetterReturnValueChecksWithBranchToRetInstruction

	.method public hidebysig specialname 
		instance valuetype [mscorlib]System.Nullable`1<int32> get_NonNullNullableProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld valuetype [mscorlib]System.Nullable`1<int32> SimpleClass::'<NonNullNullableProperty>k__BackingField'
		IL_0006: ret
	} // end of method SimpleClass::get_NonNullNullableProperty

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
		IL_0001: ldfld string SimpleClass::'<NonNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String SimpleClass::NonNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method SimpleClass::get_NonNullProperty

	.method public hidebysig specialname 
		instance string get_NullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld string SimpleClass::'<NullProperty>k__BackingField'
		IL_0006: ret
	} // end of method SimpleClass::get_NullProperty

	.method public hidebysig specialname 
		instance string get_PropertyAllowsNullGetButDoesNotAllowNullSet () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: ldfld string SimpleClass::'<PropertyAllowsNullGetButDoesNotAllowNullSet>k__BackingField'
		IL_0006: ret
	} // end of method SimpleClass::get_PropertyAllowsNullGetButDoesNotAllowNullSet

	.method public hidebysig specialname 
		instance string get_PropertyAllowsNullSetButDoesNotAllowNullGet () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string SimpleClass::'<PropertyAllowsNullSetButDoesNotAllowNullGet>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String SimpleClass::PropertyAllowsNullSetButDoesNotAllowNullGet()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method SimpleClass::get_PropertyAllowsNullSetButDoesNotAllowNullGet

	.method public hidebysig 
		instance string MethodAllowsNullReturnValue () cil managed 
	{
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 1

		IL_0000: ldnull
		IL_0001: ret
	} // end of method SimpleClass::MethodAllowsNullReturnValue

	.method public hidebysig 
		instance void MethodWithAllowedNullOutValue (
			[out] string& nonNullOutArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 4 (0x4)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldnull
		IL_0002: stind.ref
		IL_0003: ret
	} // end of method SimpleClass::MethodWithAllowedNullOutValue

	.method public hidebysig 
		instance string MethodWithCanBeNullResult () cil managed 
	{
		.custom instance void CanBeNullAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 1

		IL_0000: ldnull
		IL_0001: ret
	} // end of method SimpleClass::MethodWithCanBeNullResult

	.method public hidebysig 
		instance void MethodWithExistingArgumentGuard (
			string x
		) cil managed 
	{
		// Header size: 12
		// Code size: 50 (0x32)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "x"
		IL_0008: ldstr "[NullGuard] x is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.1
		IL_0014: call bool [mscorlib]System.String::IsNullOrEmpty(string)
		IL_0019: brfalse.s IL_002b

		IL_001b: ldstr "x is null or empty."
		IL_0020: ldstr "x"
		IL_0025: newobj instance void [mscorlib]System.ArgumentException::.ctor(string, string)
		IL_002a: throw

		IL_002b: ldarg.1
		IL_002c: call void [mscorlib]System.Console::WriteLine(string)
		IL_0031: ret
	} // end of method SimpleClass::MethodWithExistingArgumentGuard

	.method public hidebysig 
		instance void MethodWithExistingArgumentNullGuard (
			string x
		) cil managed 
	{
		// Header size: 12
		// Code size: 26 (0x1a)
		.maxstack 1

		IL_0000: ldarg.1
		IL_0001: call bool [mscorlib]System.String::IsNullOrEmpty(string)
		IL_0006: brfalse.s IL_0013

		IL_0008: ldstr "x"
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string)
		IL_0012: throw

		IL_0013: ldarg.1
		IL_0014: call void [mscorlib]System.Console::WriteLine(string)
		IL_0019: ret
	} // end of method SimpleClass::MethodWithExistingArgumentNullGuard

	.method public hidebysig 
		instance void MethodWithExistingArgumentNullGuardWithMessage (
			string x
		) cil managed 
	{
		// Header size: 12
		// Code size: 31 (0x1f)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: call bool [mscorlib]System.String::IsNullOrEmpty(string)
		IL_0006: brfalse.s IL_0018

		IL_0008: ldstr "x"
		IL_000d: ldstr "x is null or empty."
		IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0017: throw

		IL_0018: ldarg.1
		IL_0019: call void [mscorlib]System.Console::WriteLine(string)
		IL_001e: ret
	} // end of method SimpleClass::MethodWithExistingArgumentNullGuardWithMessage

	.method public hidebysig 
		instance void MethodWithGeneric<T> (
			!!T returnNull
		) cil managed 
	{
		// Header size: 12
		// Code size: 25 (0x19)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: box !!T
		IL_0006: brtrue.s IL_0018

		IL_0008: ldstr "returnNull"
		IL_000d: ldstr "[NullGuard] returnNull is null."
		IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0017: throw

		IL_0018: ret
	} // end of method SimpleClass::MethodWithGeneric

	.method public hidebysig 
		instance void MethodWithGenericOut<T> (
			[out] !!T& item
		) cil managed 
	{
		// Header size: 12
		// Code size: 32 (0x20)
		.maxstack 1

		IL_0000: ldarg.1
		IL_0001: initobj !!T
		IL_0007: ldarg.1
		IL_0008: ldobj !!T
		IL_000d: box !!T
		IL_0012: brtrue.s IL_001f

		IL_0014: ldstr "[NullGuard] Out parameter 'item' is null."
		IL_0019: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_001e: throw

		IL_001f: ret
	} // end of method SimpleClass::MethodWithGenericOut

	.method public hidebysig 
		instance void MethodWithGenericRef<T> (
			!!T& returnNull
		) cil managed 
	{
		// Header size: 12
		// Code size: 30 (0x1e)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldobj !!T
		IL_0006: box !!T
		IL_000b: brtrue.s IL_001d

		IL_000d: ldstr "returnNull"
		IL_0012: ldstr "[NullGuard] returnNull is null."
		IL_0017: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_001c: throw

		IL_001d: ret
	} // end of method SimpleClass::MethodWithGenericRef

	.method public hidebysig 
		instance !!T MethodWithGenericReturn<T> (
			bool returnNull
		) cil managed 
	{
		// Header size: 12
		// Code size: 59 (0x3b)
		.maxstack 2
		.locals init (
			[0] !!T
		)

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_001d

		IL_0003: call !!0 [mscorlib]System.Activator::CreateInstance<!!T>()
		IL_0008: dup
		IL_0009: box !!T
		IL_000e: brtrue.s IL_001c

		IL_0010: pop
		IL_0011: ldstr "[NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null."
		IL_0016: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_001b: throw

		IL_001c: ret

		IL_001d: ldloca.s 0
		IL_001f: initobj !!T
		IL_0025: ldloc.0
		IL_0026: dup
		IL_0027: box !!T
		IL_002c: brtrue.s IL_003a

		IL_002e: pop
		IL_002f: ldstr "[NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null."
		IL_0034: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0039: throw

		IL_003a: ret
	} // end of method SimpleClass::MethodWithGenericReturn

	.method public hidebysig 
		instance void MethodWithOptionalParameter (
			[opt] string optional
		) cil managed 
	{
		.param [1] = nullref
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0

		IL_0000: ret
	} // end of method SimpleClass::MethodWithOptionalParameter

	.method public hidebysig 
		instance void MethodWithOptionalParameterWithNonNullDefaultValue (
			[opt] string optional
		) cil managed 
	{
		.param [1] = "default"
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "optional"
		IL_0008: ldstr "[NullGuard] optional is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ret
	} // end of method SimpleClass::MethodWithOptionalParameterWithNonNullDefaultValue

	.method public hidebysig 
		instance void MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute (
			[opt] string optional
		) cil managed 
	{
		.param [1] = "default"
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0

		IL_0000: ret
	} // end of method SimpleClass::MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute

	.method public hidebysig 
		instance object MethodWithOutAndReturn (
			[out] string& prefix
		) cil managed 
	{
		// Header size: 12
		// Code size: 35 (0x23)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldnull
		IL_0002: stind.ref
		IL_0003: ldnull
		IL_0004: ldarg.1
		IL_0005: ldind.ref
		IL_0006: brtrue.s IL_0013

		IL_0008: ldstr "[NullGuard] Out parameter 'prefix' is null."
		IL_000d: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0012: throw

		IL_0013: dup
		IL_0014: brtrue.s IL_0022

		IL_0016: pop
		IL_0017: ldstr "[NullGuard] Return value of method 'System.Object SimpleClass::MethodWithOutAndReturn(System.String&)' is null."
		IL_001c: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0021: throw

		IL_0022: ret
	} // end of method SimpleClass::MethodWithOutAndReturn

	.method public hidebysig 
		instance void MethodWithOutValue (
			[out] string& nonNullOutArg
		) cil managed 
	{
		// Header size: 12
		// Code size: 19 (0x13)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldnull
		IL_0002: stind.ref
		IL_0003: ldarg.1
		IL_0004: ldind.ref
		IL_0005: brtrue.s IL_0012

		IL_0007: ldstr "[NullGuard] Out parameter 'nonNullOutArg' is null."
		IL_000c: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0011: throw

		IL_0012: ret
	} // end of method SimpleClass::MethodWithOutValue

	.method public hidebysig 
		instance void MethodWithRef (
			object& returnNull
		) cil managed 
	{
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldind.ref
		IL_0002: brtrue.s IL_0014

		IL_0004: ldstr "returnNull"
		IL_0009: ldstr "[NullGuard] returnNull is null."
		IL_000e: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0013: throw

		IL_0014: ret
	} // end of method SimpleClass::MethodWithRef

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
		IL_000c: ldstr "[NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null."
		IL_0011: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0016: throw

		IL_0017: ret

		IL_0018: ldnull
		IL_0019: dup
		IL_001a: brtrue.s IL_0028

		IL_001c: pop
		IL_001d: ldstr "[NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null."
		IL_0022: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0027: throw

		IL_0028: ret
	} // end of method SimpleClass::MethodWithReturnValue

	.method public hidebysig 
		instance void MethodWithTwoOuts (
			[out] string& first,
			[out] string& second
		) cil managed 
	{
		// Header size: 12
		// Code size: 37 (0x25)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldnull
		IL_0002: stind.ref
		IL_0003: ldarg.2
		IL_0004: ldnull
		IL_0005: stind.ref
		IL_0006: ldarg.1
		IL_0007: ldind.ref
		IL_0008: brtrue.s IL_0015

		IL_000a: ldstr "[NullGuard] Out parameter 'first' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ldarg.2
		IL_0016: ldind.ref
		IL_0017: brtrue.s IL_0024

		IL_0019: ldstr "[NullGuard] Out parameter 'second' is null."
		IL_001e: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0023: throw

		IL_0024: ret
	} // end of method SimpleClass::MethodWithTwoOuts

	.method public hidebysig 
		instance void MethodWithTwoRefs (
			string& first,
			string& second
		) cil managed 
	{
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldind.ref
		IL_0002: brtrue.s IL_0014

		IL_0004: ldstr "first"
		IL_0009: ldstr "[NullGuard] first is null."
		IL_000e: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0013: throw

		IL_0014: ldarg.2
		IL_0015: ldind.ref
		IL_0016: brtrue.s IL_0028

		IL_0018: ldstr "second"
		IL_001d: ldstr "[NullGuard] second is null."
		IL_0022: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0027: throw

		IL_0028: ret
	} // end of method SimpleClass::MethodWithTwoRefs

	.method public hidebysig 
		instance void OutValueChecksWithBranchToRetInstruction (
			[out] string& outParam
		) cil managed 
	{
		// Header size: 12
		// Code size: 44 (0x2c)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldnull
		IL_0002: stind.ref
		IL_0003: ldstr ""
		IL_0008: call instance int32 [mscorlib]System.String::get_Length()
		IL_000d: ldc.i4.s 42
		IL_000f: bne.un.s IL_001c

		IL_0011: ldstr "Not reachable"
		IL_0016: newobj instance void [mscorlib]System.Exception::.ctor(string)
		IL_001b: throw

		IL_001c: ldarg.1
		IL_001d: ldind.ref
		IL_001e: brtrue.s IL_002b

		IL_0020: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_0025: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_002a: throw

		IL_002b: ret
	} // end of method SimpleClass::OutValueChecksWithBranchToRetInstruction

	.method public hidebysig 
		instance void OutValueChecksWithRetInstructionAsSwitchCase (
			int32 i,
			[out] string& outParam
		) cil managed 
	{
		// Header size: 12
		// Code size: 125 (0x7d)
		.maxstack 2

		IL_0000: ldarg.2
		IL_0001: ldnull
		IL_0002: stind.ref
		IL_0003: ldarg.1
		IL_0004: switch (IL_0029, IL_0039, IL_0053, IL_0063)

		IL_0019: ldarg.2
		IL_001a: ldind.ref
		IL_001b: brtrue.s IL_0028

		IL_001d: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_0022: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0027: throw

		IL_0028: ret

		IL_0029: ldarg.2
		IL_002a: ldind.ref
		IL_002b: brtrue.s IL_0038

		IL_002d: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_0032: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0037: throw

		IL_0038: ret

		IL_0039: ldstr "1"
		IL_003e: call void [mscorlib]System.Console::WriteLine(string)
		IL_0043: ldarg.2
		IL_0044: ldind.ref
		IL_0045: brtrue.s IL_0052

		IL_0047: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_004c: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0051: throw

		IL_0052: ret

		IL_0053: ldarg.2
		IL_0054: ldind.ref
		IL_0055: brtrue.s IL_0062

		IL_0057: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_005c: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0061: throw

		IL_0062: ret

		IL_0063: ldstr "3"
		IL_0068: call void [mscorlib]System.Console::WriteLine(string)
		IL_006d: ldarg.2
		IL_006e: ldind.ref
		IL_006f: brtrue.s IL_007c

		IL_0071: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_0076: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_007b: throw

		IL_007c: ret
	} // end of method SimpleClass::OutValueChecksWithRetInstructionAsSwitchCase

	.method public hidebysig 
		instance void PublicWrapperOfPrivateMethod () cil managed 
	{
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldnull
		IL_0002: call instance void SimpleClass::SomePrivateMethod(string)
		IL_0007: ret
	} // end of method SimpleClass::PublicWrapperOfPrivateMethod

	.method public hidebysig 
		instance string ReturnValueChecksWithBranchToRetInstruction () cil managed 
	{
		// Header size: 12
		// Code size: 44 (0x2c)
		.maxstack 2
		.locals init (
			[0] string
		)

		IL_0000: ldnull
		IL_0001: stloc.0
		IL_0002: ldstr ""
		IL_0007: call instance int32 [mscorlib]System.String::get_Length()
		IL_000c: ldc.i4.s 42
		IL_000e: bne.un.s IL_001b

		IL_0010: ldstr "Not reachable"
		IL_0015: newobj instance void [mscorlib]System.Exception::.ctor(string)
		IL_001a: throw

		IL_001b: ldloc.0
		IL_001c: dup
		IL_001d: brtrue.s IL_002b

		IL_001f: pop
		IL_0020: ldstr "[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null."
		IL_0025: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_002a: throw

		IL_002b: ret
	} // end of method SimpleClass::ReturnValueChecksWithBranchToRetInstruction

	.method public hidebysig specialname 
		instance void set_NonNullNullableProperty (
			valuetype [mscorlib]System.Nullable`1<int32> 'value'
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
		IL_0002: stfld valuetype [mscorlib]System.Nullable`1<int32> SimpleClass::'<NonNullNullableProperty>k__BackingField'
		IL_0007: ret
	} // end of method SimpleClass::set_NonNullNullableProperty

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
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String SimpleClass::NonNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string SimpleClass::'<NonNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method SimpleClass::set_NonNullProperty

	.method public hidebysig specialname 
		instance void set_NullProperty (
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
		IL_0002: stfld string SimpleClass::'<NullProperty>k__BackingField'
		IL_0007: ret
	} // end of method SimpleClass::set_NullProperty

	.method public hidebysig specialname 
		instance void set_PropertyAllowsNullGetButDoesNotAllowNullSet (
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
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String SimpleClass::PropertyAllowsNullGetButDoesNotAllowNullSet()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string SimpleClass::'<PropertyAllowsNullGetButDoesNotAllowNullSet>k__BackingField'
		IL_001a: ret
	} // end of method SimpleClass::set_PropertyAllowsNullGetButDoesNotAllowNullSet

	.method public hidebysig specialname 
		instance void set_PropertyAllowsNullSetButDoesNotAllowNullGet (
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
		IL_0002: stfld string SimpleClass::'<PropertyAllowsNullSetButDoesNotAllowNullGet>k__BackingField'
		IL_0007: ret
	} // end of method SimpleClass::set_PropertyAllowsNullSetButDoesNotAllowNullGet

	.method public hidebysig 
		instance void SomeMethod (
			string nonNullArg,
			string nullArg
		) cil managed 
	{
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
	} // end of method SimpleClass::SomeMethod

	.method private hidebysig 
		instance void SomePrivateMethod (
			string x
		) cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: call void [mscorlib]System.Console::WriteLine(string)
		IL_0006: ret
	} // end of method SimpleClass::SomePrivateMethod

	// Properties
	.property instance string GetterReturnValueChecksWithBranchToRetInstruction()
	{
		.get instance string SimpleClass::get_GetterReturnValueChecksWithBranchToRetInstruction()
	}
	.property instance valuetype [mscorlib]System.Nullable`1<int32> NonNullNullableProperty()
	{
		.get instance valuetype [mscorlib]System.Nullable`1<int32> SimpleClass::get_NonNullNullableProperty()
		.set instance void SimpleClass::set_NonNullNullableProperty(valuetype [mscorlib]System.Nullable`1<int32>)
	}
	.property instance string NonNullProperty()
	{
		.get instance string SimpleClass::get_NonNullProperty()
		.set instance void SimpleClass::set_NonNullProperty(string)
	}
	.property instance string NullProperty()
	{
		.get instance string SimpleClass::get_NullProperty()
		.set instance void SimpleClass::set_NullProperty(string)
	}
	.property instance string PropertyAllowsNullGetButDoesNotAllowNullSet()
	{
		.get instance string SimpleClass::get_PropertyAllowsNullGetButDoesNotAllowNullSet()
		.set instance void SimpleClass::set_PropertyAllowsNullGetButDoesNotAllowNullSet(string)
	}
	.property instance string PropertyAllowsNullSetButDoesNotAllowNullGet()
	{
		.get instance string SimpleClass::get_PropertyAllowsNullSetButDoesNotAllowNullGet()
		.set instance void SimpleClass::set_PropertyAllowsNullSetButDoesNotAllowNullGet(string)
	}

} // end of class SimpleClass
