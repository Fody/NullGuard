﻿.class private auto ansi beforefieldinit NonPublicWithNested
	extends [mscorlib]System.Object
{
	// Nested Types
	.class nested public auto ansi beforefieldinit PublicNestedClass
		extends [mscorlib]System.Object
	{
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [mscorlib]System.Object::.ctor()
			IL_0006: ret
		} // end of method PublicNestedClass::.ctor

		.method public hidebysig 
			instance string MethodReturnsNull () cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldnull
			IL_0001: ret
		} // end of method PublicNestedClass::MethodReturnsNull

	} // end of class PublicNestedClass


	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
	} // end of method NonPublicWithNested::.ctor

} // end of class NonPublicWithNested
