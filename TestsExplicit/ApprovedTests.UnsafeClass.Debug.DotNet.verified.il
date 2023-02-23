.class public auto ansi beforefieldinit UnsafeClass
	extends [netstandard]System.Object
{
	// Fields
	.field private int32* '<NullProperty>k__BackingField'
	.custom instance void [netstandard]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [netstandard]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
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
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method UnsafeClass::.ctor

	.method public hidebysig specialname 
		instance int32* get_NullProperty () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld int32* UnsafeClass::'<NullProperty>k__BackingField'
		IL_0006: ret
	} // end of method UnsafeClass::get_NullProperty

	.method public hidebysig 
		instance int32* MethodWithAmp (
			int32* 'instance'
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
		// Code size: 8 (0x8)
		.maxstack 1
		.locals init (
			[0] int32*
		)

		IL_0000: nop
		IL_0001: ldc.i4.0
		IL_0002: conv.u
		IL_0003: stloc.0
		IL_0004: br.s IL_0006

		IL_0006: ldloc.0
		IL_0007: ret
	} // end of method UnsafeClass::MethodWithAmp

	.method public hidebysig specialname 
		instance void set_NullProperty (
			int32* 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld int32* UnsafeClass::'<NullProperty>k__BackingField'
		IL_0007: ret
	} // end of method UnsafeClass::set_NullProperty

	// Properties
	.property instance int32* NullProperty()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance int32* UnsafeClass::get_NullProperty()
		.set instance void UnsafeClass::set_NullProperty(int32*)
	}

} // end of class UnsafeClass
