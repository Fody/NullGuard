.class public auto ansi beforefieldinit GenericClassWithValueTypeConstraints`1<valuetype .ctor ([mscorlib]System.ValueType) T>
	extends [mscorlib]System.Object
{
	// Fields
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
	} // end of method GenericClassWithValueTypeConstraints`1::.ctor

	.method public hidebysig 
		instance !!U GenericMethod<valuetype .ctor ([mscorlib]System.ValueType) U> (
			!T t,
			!!U u
		) cil managed 
	{
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 1
		.locals init (
			[0] !!U
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj !!U
		IL_0008: ldloc.0
		IL_0009: ret
	} // end of method GenericClassWithValueTypeConstraints`1::GenericMethod

	.method public hidebysig specialname 
		instance !T get_NonNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld !0 class GenericClassWithValueTypeConstraints`1<!T>::'<NonNullProperty>k__BackingField'
		IL_0006: ret
	} // end of method GenericClassWithValueTypeConstraints`1::get_NonNullProperty

	.method public hidebysig 
		instance !T NonNullMethod () cil managed 
	{
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 1
		.locals init (
			[0] !T
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj !T
		IL_0008: ldloc.0
		IL_0009: ret
	} // end of method GenericClassWithValueTypeConstraints`1::NonNullMethod

	.method public hidebysig specialname 
		instance void set_NonNullProperty (
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
		IL_0002: stfld !0 class GenericClassWithValueTypeConstraints`1<!T>::'<NonNullProperty>k__BackingField'
		IL_0007: ret
	} // end of method GenericClassWithValueTypeConstraints`1::set_NonNullProperty

	// Properties
	.property instance !T NonNullProperty()
	{
		.get instance !0 GenericClassWithValueTypeConstraints`1::get_NonNullProperty()
		.set instance void GenericClassWithValueTypeConstraints`1::set_NonNullProperty(!0)
	}

} // end of class GenericClassWithValueTypeConstraints`1
