.class private auto ansi beforefieldinit NonPublicWithNested
	extends [netstandard]System.Object
{
	// Nested Types
	.class nested public auto ansi beforefieldinit PublicNestedClass
		extends [netstandard]System.Object
	{
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Object::.ctor()
			IL_0006: nop
			IL_0007: ret
		} // end of method PublicNestedClass::.ctor

		.method public hidebysig 
			instance string MethodReturnsNull () cil managed 
		{
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
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
		} // end of method PublicNestedClass::MethodReturnsNull

	} // end of class PublicNestedClass


	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method NonPublicWithNested::.ctor

} // end of class NonPublicWithNested
