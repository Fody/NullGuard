﻿.class public auto ansi beforefieldinit GenericClassWithReferenceTypeConstraints`1<class T>
	extends [mscorlib]System.Object
{
	// Fields
	.field private !T '<CanBeNullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private !T '<NonNullProperty>k__BackingField'
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
	} // end of method GenericClassWithReferenceTypeConstraints`1::.ctor

	.method public hidebysig 
		instance !!U GenericMethod<([mscorlib]System.Collections.IList) U> (
			!T t,
			!!U u
		) cil managed 
	{
		// Header size: 12
		// Code size: 73 (0x49)
		.maxstack 2
		.locals init (
			[0] !!U
		)

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "t"
		IL_0008: ldstr "[NullGuard] t is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.2
		IL_0014: box !!U
		IL_0019: brtrue.s IL_002b

		IL_001b: ldstr "u"
		IL_0020: ldstr "[NullGuard] u is null."
		IL_0025: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_002a: throw

		IL_002b: ldloca.s 0
		IL_002d: initobj !!U
		IL_0033: ldloc.0
		IL_0034: dup
		IL_0035: box !!U
		IL_003a: brtrue.s IL_0048

		IL_003c: pop
		IL_003d: ldstr "[NullGuard] Return value of method 'U GenericClassWithReferenceTypeConstraints`1::GenericMethod(T,U)' is null."
		IL_0042: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0047: throw

		IL_0048: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::GenericMethod

	.method public hidebysig 
		instance !!U GenericMethodReturnsParameter<([mscorlib]System.Collections.IList) U> (
			!T t,
			!!U u
		) cil managed 
	{
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "t"
		IL_0008: ldstr "[NullGuard] t is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.2
		IL_0014: dup
		IL_0015: box !!U
		IL_001a: brtrue.s IL_0028

		IL_001c: pop
		IL_001d: ldstr "[NullGuard] Return value of method 'U GenericClassWithReferenceTypeConstraints`1::GenericMethodReturnsParameter(T,U)' is null."
		IL_0022: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0027: throw

		IL_0028: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::GenericMethodReturnsParameter

	.method public hidebysig 
		instance void GenericMethodVoid<([mscorlib]System.Collections.IList) U> (
			!T t,
			!!U u
		) cil managed 
	{
		// Header size: 12
		// Code size: 44 (0x2c)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "t"
		IL_0008: ldstr "[NullGuard] t is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.2
		IL_0014: box !!U
		IL_0019: brtrue.s IL_002b

		IL_001b: ldstr "u"
		IL_0020: ldstr "[NullGuard] u is null."
		IL_0025: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_002a: throw

		IL_002b: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::GenericMethodVoid

	.method public hidebysig specialname 
		instance !T get_CanBeNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld !0 class GenericClassWithReferenceTypeConstraints`1<!T>::'<CanBeNullProperty>k__BackingField'
		IL_0006: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::get_CanBeNullProperty

	.method public hidebysig specialname 
		instance !T get_NonNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld !0 class GenericClassWithReferenceTypeConstraints`1<!T>::'<NonNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'T GenericClassWithReferenceTypeConstraints`1::NonNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::get_NonNullProperty

	.method public hidebysig 
		instance !T NonNullMethod () cil managed 
	{
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: call instance !0 class GenericClassWithReferenceTypeConstraints`1<!T>::get_CanBeNullProperty()
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of method 'T GenericClassWithReferenceTypeConstraints`1::NonNullMethod()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::NonNullMethod

	.method public hidebysig specialname 
		instance void set_CanBeNullProperty (
			!T 'value'
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
		IL_0002: stfld !0 class GenericClassWithReferenceTypeConstraints`1<!T>::'<CanBeNullProperty>k__BackingField'
		IL_0007: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::set_CanBeNullProperty

	.method public hidebysig specialname 
		instance void set_NonNullProperty (
			!T 'value'
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
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'T GenericClassWithReferenceTypeConstraints`1::NonNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld !0 class GenericClassWithReferenceTypeConstraints`1<!T>::'<NonNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method GenericClassWithReferenceTypeConstraints`1::set_NonNullProperty

	// Properties
	.property instance !T CanBeNullProperty()
	{
		.custom instance void CanBeNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance !0 GenericClassWithReferenceTypeConstraints`1::get_CanBeNullProperty()
		.set instance void GenericClassWithReferenceTypeConstraints`1::set_CanBeNullProperty(!0)
	}
	.property instance !T NonNullProperty()
	{
		.get instance !0 GenericClassWithReferenceTypeConstraints`1::get_NonNullProperty()
		.set instance void GenericClassWithReferenceTypeConstraints`1::set_NonNullProperty(!0)
	}

} // end of class GenericClassWithReferenceTypeConstraints`1
