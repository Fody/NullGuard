.class public auto ansi beforefieldinit SimpleClass
	extends [mscorlib]System.Object
{
	// Fields
	.field private valuetype [mscorlib]System.Nullable`1<int32> '<NonNullNullableProperty>k__BackingField'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<NonNullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<NullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<PropertyAllowsNullGetButDoesNotAllowNullSet>k__BackingField'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private string '<PropertyAllowsNullSetButDoesNotAllowNullGet>k__BackingField'
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
		// Code size: 9 (0x9)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: nop
		IL_0008: ret
	} // end of method SimpleClass::.ctor

	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			[out] string& nonNullOutArg
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: nop
		IL_0008: ldarg.1
		IL_0009: ldnull
		IL_000a: stind.ref
		IL_000b: ldarg.1
		IL_000c: ldind.ref
		IL_000d: brtrue.s IL_001a

		IL_000f: ldstr "[NullGuard] Out parameter 'nonNullOutArg' is null."
		IL_0014: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0019: throw

		IL_001a: ret
	} // end of method SimpleClass::.ctor

	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			string nonNullArg,
			string nullArg
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		.param [2]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 46 (0x2e)
		.maxstack 3

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: call instance void [mscorlib]System.Object::.ctor()
		IL_0019: nop
		IL_001a: nop
		IL_001b: ldarg.1
		IL_001c: ldstr " "
		IL_0021: ldarg.2
		IL_0022: call string [mscorlib]System.String::Concat(string, string, string)
		IL_0027: call void [mscorlib]System.Console::WriteLine(string)
		IL_002c: nop
		IL_002d: ret
	} // end of method SimpleClass::.ctor

	.method public hidebysig specialname 
		instance string get_GetterReturnValueChecksWithBranchToRetInstruction () cil managed 
	{
		// Header size: 12
		// Code size: 53 (0x35)
		.maxstack 2
		.locals init (
			[0] string,
			[1] bool,
			[2] string
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: stloc.0
		IL_0003: ldstr ""
		IL_0008: call instance int32 [mscorlib]System.String::get_Length()
		IL_000d: ldc.i4.s 42
		IL_000f: ceq
		IL_0011: stloc.1
		IL_0012: ldloc.1
		IL_0013: brfalse.s IL_0020

		IL_0015: ldstr "Not reachable"
		IL_001a: newobj instance void [mscorlib]System.Exception::.ctor(string)
		IL_001f: throw

		IL_0020: ldloc.0
		IL_0021: stloc.2
		IL_0022: br.s IL_0024

		IL_0024: ldloc.2
		IL_0025: dup
		IL_0026: brtrue.s IL_0034

		IL_0028: pop
		IL_0029: ldstr "[NullGuard] Return value of property 'System.String SimpleClass::GetterReturnValueChecksWithBranchToRetInstruction()' is null."
		IL_002e: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0033: throw

		IL_0034: ret
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
		// Code size: 7 (0x7)
		.maxstack 1
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: stloc.0
		IL_0003: br.s IL_0005

		IL_0005: ldloc.0
		IL_0006: ret
	} // end of method SimpleClass::MethodAllowsNullReturnValue

	.method public hidebysig 
		instance void MethodWithAllowedNullOutValue (
			[out] string& nonNullOutArg
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 5 (0x5)
		.maxstack 2

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldnull
		IL_0003: stind.ref
		IL_0004: ret
	} // end of method SimpleClass::MethodWithAllowedNullOutValue

	.method public hidebysig 
		instance string MethodWithCanBeNullResult () cil managed 
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: stloc.0
		IL_0003: br.s IL_0005

		IL_0005: ldloc.0
		IL_0006: ret
	} // end of method SimpleClass::MethodWithCanBeNullResult

	.method public hidebysig 
		instance void MethodWithExistingArgumentGuard (
			string x
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 54 (0x36)
		.maxstack 2
		.locals init (
			[0] bool
		)

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "x"
		IL_0008: ldstr "[NullGuard] x is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: nop
		IL_0014: ldarg.1
		IL_0015: call bool [mscorlib]System.String::IsNullOrEmpty(string)
		IL_001a: stloc.0
		IL_001b: ldloc.0
		IL_001c: brfalse.s IL_002e

		IL_001e: ldstr "x is null or empty."
		IL_0023: ldstr "x"
		IL_0028: newobj instance void [mscorlib]System.ArgumentException::.ctor(string, string)
		IL_002d: throw

		IL_002e: ldarg.1
		IL_002f: call void [mscorlib]System.Console::WriteLine(string)
		IL_0034: nop
		IL_0035: ret
	} // end of method SimpleClass::MethodWithExistingArgumentGuard

	.method public hidebysig 
		instance void MethodWithExistingArgumentNullGuard (
			string x
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 30 (0x1e)
		.maxstack 1
		.locals init (
			[0] bool
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: call bool [mscorlib]System.String::IsNullOrEmpty(string)
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: brfalse.s IL_0016

		IL_000b: ldstr "x"
		IL_0010: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string)
		IL_0015: throw

		IL_0016: ldarg.1
		IL_0017: call void [mscorlib]System.Console::WriteLine(string)
		IL_001c: nop
		IL_001d: ret
	} // end of method SimpleClass::MethodWithExistingArgumentNullGuard

	.method public hidebysig 
		instance void MethodWithExistingArgumentNullGuardWithMessage (
			string x
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 35 (0x23)
		.maxstack 2
		.locals init (
			[0] bool
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: call bool [mscorlib]System.String::IsNullOrEmpty(string)
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: brfalse.s IL_001b

		IL_000b: ldstr "x"
		IL_0010: ldstr "x is null or empty."
		IL_0015: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_001a: throw

		IL_001b: ldarg.1
		IL_001c: call void [mscorlib]System.Console::WriteLine(string)
		IL_0021: nop
		IL_0022: ret
	} // end of method SimpleClass::MethodWithExistingArgumentNullGuardWithMessage

	.method public hidebysig 
		instance void MethodWithGeneric<T> (
			!!T returnNull
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 26 (0x1a)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: box !!T
		IL_0006: brtrue.s IL_0018

		IL_0008: ldstr "returnNull"
		IL_000d: ldstr "[NullGuard] returnNull is null."
		IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0017: throw

		IL_0018: nop
		IL_0019: ret
	} // end of method SimpleClass::MethodWithGeneric

	.method public hidebysig 
		instance void MethodWithGenericOut<T> (
			[out] !!T& item
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 33 (0x21)
		.maxstack 1

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: initobj !!T
		IL_0008: ldarg.1
		IL_0009: ldobj !!T
		IL_000e: box !!T
		IL_0013: brtrue.s IL_0020

		IL_0015: ldstr "[NullGuard] Out parameter 'item' is null."
		IL_001a: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_001f: throw

		IL_0020: ret
	} // end of method SimpleClass::MethodWithGenericOut

	.method public hidebysig 
		instance void MethodWithGenericRef<T> (
			!!T& returnNull
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 31 (0x1f)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldobj !!T
		IL_0006: box !!T
		IL_000b: brtrue.s IL_001d

		IL_000d: ldstr "returnNull"
		IL_0012: ldstr "[NullGuard] returnNull is null."
		IL_0017: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_001c: throw

		IL_001d: nop
		IL_001e: ret
	} // end of method SimpleClass::MethodWithGenericRef

	.method public hidebysig 
		instance !!T MethodWithGenericReturn<T> (
			bool returnNull
		) cil managed 
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 45 (0x2d)
		.maxstack 2
		.locals init (
			[0] !!T,
			[1] !!T
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: brtrue.s IL_000b

		IL_0004: call !!0 [mscorlib]System.Activator::CreateInstance<!!T>()
		IL_0009: br.s IL_0014

		IL_000b: ldloca.s 0
		IL_000d: initobj !!T
		IL_0013: ldloc.0

		IL_0014: stloc.1
		IL_0015: br.s IL_0017

		IL_0017: ldloc.1
		IL_0018: dup
		IL_0019: box !!T
		IL_001e: brtrue.s IL_002c

		IL_0020: pop
		IL_0021: ldstr "[NullGuard] Return value of method 'T SimpleClass::MethodWithGenericReturn(System.Boolean)' is null."
		IL_0026: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_002b: throw

		IL_002c: ret
	} // end of method SimpleClass::MethodWithGenericReturn

	.method public hidebysig 
		instance void MethodWithOptionalParameter (
			[opt] string optional
		) cil managed 
	{
		.param [1] = nullref
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 0

		IL_0000: nop
		IL_0001: ret
	} // end of method SimpleClass::MethodWithOptionalParameter

	.method public hidebysig 
		instance void MethodWithOptionalParameterWithNonNullDefaultValue (
			[opt] string optional
		) cil managed 
	{
		.param [1] = "default"
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "optional"
		IL_0008: ldstr "[NullGuard] optional is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: nop
		IL_0014: ret
	} // end of method SimpleClass::MethodWithOptionalParameterWithNonNullDefaultValue

	.method public hidebysig 
		instance void MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute (
			[opt] string optional
		) cil managed 
	{
		.param [1] = "default"
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 0

		IL_0000: nop
		IL_0001: ret
	} // end of method SimpleClass::MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute

	.method public hidebysig 
		instance object MethodWithOutAndReturn (
			[out] string& prefix
		) cil managed 
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 40 (0x28)
		.maxstack 2
		.locals init (
			[0] object
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldnull
		IL_0003: stind.ref
		IL_0004: ldnull
		IL_0005: stloc.0
		IL_0006: br.s IL_0008

		IL_0008: ldloc.0
		IL_0009: ldarg.1
		IL_000a: ldind.ref
		IL_000b: brtrue.s IL_0018

		IL_000d: ldstr "[NullGuard] Out parameter 'prefix' is null."
		IL_0012: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0017: throw

		IL_0018: dup
		IL_0019: brtrue.s IL_0027

		IL_001b: pop
		IL_001c: ldstr "[NullGuard] Return value of method 'System.Object SimpleClass::MethodWithOutAndReturn(System.String&)' is null."
		IL_0021: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0026: throw

		IL_0027: ret
	} // end of method SimpleClass::MethodWithOutAndReturn

	.method public hidebysig 
		instance void MethodWithOutValue (
			[out] string& nonNullOutArg
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldnull
		IL_0003: stind.ref
		IL_0004: ldarg.1
		IL_0005: ldind.ref
		IL_0006: brtrue.s IL_0013

		IL_0008: ldstr "[NullGuard] Out parameter 'nonNullOutArg' is null."
		IL_000d: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0012: throw

		IL_0013: ret
	} // end of method SimpleClass::MethodWithOutValue

	.method public hidebysig 
		instance void MethodWithRef (
			object& returnNull
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: ldind.ref
		IL_0002: brtrue.s IL_0014

		IL_0004: ldstr "returnNull"
		IL_0009: ldstr "[NullGuard] returnNull is null."
		IL_000e: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0013: throw

		IL_0014: nop
		IL_0015: ret
	} // end of method SimpleClass::MethodWithRef

	.method public hidebysig 
		instance string MethodWithReturnValue (
			bool returnNull
		) cil managed 
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 32 (0x20)
		.maxstack 2
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: brtrue.s IL_000b

		IL_0004: ldstr ""
		IL_0009: br.s IL_000c

		IL_000b: ldnull

		IL_000c: stloc.0
		IL_000d: br.s IL_000f

		IL_000f: ldloc.0
		IL_0010: dup
		IL_0011: brtrue.s IL_001f

		IL_0013: pop
		IL_0014: ldstr "[NullGuard] Return value of method 'System.String SimpleClass::MethodWithReturnValue(System.Boolean)' is null."
		IL_0019: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_001e: throw

		IL_001f: ret
	} // end of method SimpleClass::MethodWithReturnValue

	.method public hidebysig 
		instance void MethodWithTwoOuts (
			[out] string& first,
			[out] string& second
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		.param [2]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 38 (0x26)
		.maxstack 2

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldnull
		IL_0003: stind.ref
		IL_0004: ldarg.2
		IL_0005: ldnull
		IL_0006: stind.ref
		IL_0007: ldarg.1
		IL_0008: ldind.ref
		IL_0009: brtrue.s IL_0016

		IL_000b: ldstr "[NullGuard] Out parameter 'first' is null."
		IL_0010: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0015: throw

		IL_0016: ldarg.2
		IL_0017: ldind.ref
		IL_0018: brtrue.s IL_0025

		IL_001a: ldstr "[NullGuard] Out parameter 'second' is null."
		IL_001f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0024: throw

		IL_0025: ret
	} // end of method SimpleClass::MethodWithTwoOuts

	.method public hidebysig 
		instance void MethodWithTwoRefs (
			string& first,
			string& second
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		.param [2]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 42 (0x2a)
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

		IL_0028: nop
		IL_0029: ret
	} // end of method SimpleClass::MethodWithTwoRefs

	.method public hidebysig 
		instance void OutValueChecksWithBranchToRetInstruction (
			[out] string& outParam
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 49 (0x31)
		.maxstack 2
		.locals init (
			[0] bool
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldnull
		IL_0003: stind.ref
		IL_0004: ldstr ""
		IL_0009: call instance int32 [mscorlib]System.String::get_Length()
		IL_000e: ldc.i4.s 42
		IL_0010: ceq
		IL_0012: stloc.0
		IL_0013: ldloc.0
		IL_0014: brfalse.s IL_0021

		IL_0016: ldstr "Not reachable"
		IL_001b: newobj instance void [mscorlib]System.Exception::.ctor(string)
		IL_0020: throw

		IL_0021: ldarg.1
		IL_0022: ldind.ref
		IL_0023: brtrue.s IL_0030

		IL_0025: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_002a: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_002f: throw

		IL_0030: ret
	} // end of method SimpleClass::OutValueChecksWithBranchToRetInstruction

	.method public hidebysig 
		instance void OutValueChecksWithRetInstructionAsSwitchCase (
			int32 i,
			[out] string& outParam
		) cil managed 
	{
		.param [2]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 78 (0x4e)
		.maxstack 2
		.locals init (
			[0] int32,
			[1] int32
		)

		IL_0000: nop
		IL_0001: ldarg.2
		IL_0002: ldnull
		IL_0003: stind.ref
		IL_0004: ldarg.1
		IL_0005: stloc.1
		IL_0006: ldloc.1
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: switch (IL_0020, IL_0022, IL_002f, IL_0031)

		IL_001e: br.s IL_003e

		IL_0020: br.s IL_003e

		IL_0022: ldstr "1"
		IL_0027: call void [mscorlib]System.Console::WriteLine(string)
		IL_002c: nop
		IL_002d: br.s IL_003e

		IL_002f: br.s IL_003e

		IL_0031: ldstr "3"
		IL_0036: call void [mscorlib]System.Console::WriteLine(string)
		IL_003b: nop
		IL_003c: br.s IL_003e

		IL_003e: ldarg.2
		IL_003f: ldind.ref
		IL_0040: brtrue.s IL_004d

		IL_0042: ldstr "[NullGuard] Out parameter 'outParam' is null."
		IL_0047: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_004c: throw

		IL_004d: ret
	} // end of method SimpleClass::OutValueChecksWithRetInstructionAsSwitchCase

	.method public hidebysig 
		instance void PublicWrapperOfPrivateMethod () cil managed 
	{
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 2

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldnull
		IL_0003: call instance void SimpleClass::SomePrivateMethod(string)
		IL_0008: nop
		IL_0009: ret
	} // end of method SimpleClass::PublicWrapperOfPrivateMethod

	.method public hidebysig 
		instance string ReturnValueChecksWithBranchToRetInstruction () cil managed 
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 53 (0x35)
		.maxstack 2
		.locals init (
			[0] string,
			[1] bool,
			[2] string
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: stloc.0
		IL_0003: ldstr ""
		IL_0008: call instance int32 [mscorlib]System.String::get_Length()
		IL_000d: ldc.i4.s 42
		IL_000f: ceq
		IL_0011: stloc.1
		IL_0012: ldloc.1
		IL_0013: brfalse.s IL_0020

		IL_0015: ldstr "Not reachable"
		IL_001a: newobj instance void [mscorlib]System.Exception::.ctor(string)
		IL_001f: throw

		IL_0020: ldloc.0
		IL_0021: stloc.2
		IL_0022: br.s IL_0024

		IL_0024: ldloc.2
		IL_0025: dup
		IL_0026: brtrue.s IL_0034

		IL_0028: pop
		IL_0029: ldstr "[NullGuard] Return value of method 'System.String SimpleClass::ReturnValueChecksWithBranchToRetInstruction()' is null."
		IL_002e: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0033: throw

		IL_0034: ret
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
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		.param [2]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 12
		// Code size: 28 (0x1c)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "nonNullArg"
		IL_0008: ldstr "[NullGuard] nonNullArg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: nop
		IL_0014: ldarg.1
		IL_0015: call void [mscorlib]System.Console::WriteLine(string)
		IL_001a: nop
		IL_001b: ret
	} // end of method SimpleClass::SomeMethod

	.method private hidebysig 
		instance void SomePrivateMethod (
			string x
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 1
		// Code size: 9 (0x9)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: call void [mscorlib]System.Console::WriteLine(string)
		IL_0007: nop
		IL_0008: ret
	} // end of method SimpleClass::SomePrivateMethod

	// Properties
	.property instance string GetterReturnValueChecksWithBranchToRetInstruction()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string SimpleClass::get_GetterReturnValueChecksWithBranchToRetInstruction()
	}
	.property instance valuetype [mscorlib]System.Nullable`1<int32> NonNullNullableProperty()
	{
		.get instance valuetype [mscorlib]System.Nullable`1<int32> SimpleClass::get_NonNullNullableProperty()
		.set instance void SimpleClass::set_NonNullNullableProperty(valuetype [mscorlib]System.Nullable`1<int32>)
	}
	.property instance string NonNullProperty()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string SimpleClass::get_NonNullProperty()
		.set instance void SimpleClass::set_NonNullProperty(string)
	}
	.property instance string NullProperty()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string SimpleClass::get_NullProperty()
		.set instance void SimpleClass::set_NullProperty(string)
	}
	.property instance string PropertyAllowsNullGetButDoesNotAllowNullSet()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string SimpleClass::get_PropertyAllowsNullGetButDoesNotAllowNullSet()
		.set instance void SimpleClass::set_PropertyAllowsNullGetButDoesNotAllowNullSet(string)
	}
	.property instance string PropertyAllowsNullSetButDoesNotAllowNullGet()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string SimpleClass::get_PropertyAllowsNullSetButDoesNotAllowNullGet()
		.set instance void SimpleClass::set_PropertyAllowsNullSetButDoesNotAllowNullGet(string)
	}

} // end of class SimpleClass
