﻿.class public auto ansi beforefieldinit ClassWithImplicitInterface
	extends [mscorlib]System.Object
	implements class [mscorlib]System.IComparable`1<string>
{
	// Nested Types
	.class nested private auto ansi beforefieldinit ClassWithImplicitPrivateInterface
		extends [mscorlib]System.Object
		implements ClassWithImplicitInterface/IPrivate
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
		} // end of method ClassWithImplicitPrivateInterface::.ctor

		.method public final hidebysig newslot virtual 
			instance int32 CompareTo (
				string other1
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.0
			IL_0001: ret
		} // end of method ClassWithImplicitPrivateInterface::CompareTo

	} // end of class ClassWithImplicitPrivateInterface

	.class nested private auto ansi beforefieldinit ClassWithImplicitPublicInterface
		extends [mscorlib]System.Object
		implements class [mscorlib]System.IComparable`1<string>
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
		} // end of method ClassWithImplicitPublicInterface::.ctor

		.method public final hidebysig newslot virtual 
			instance int32 CompareTo (
				string other1
			) cil managed 
		{
			// Header size: 12
			// Code size: 21 (0x15)
			.maxstack 2

			IL_0000: ldarg.1
			IL_0001: brtrue.s IL_0013

			IL_0003: ldstr "other1"
			IL_0008: ldstr "[NullGuard] other1 is null."
			IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
			IL_0012: throw

			IL_0013: ldc.i4.0
			IL_0014: ret
		} // end of method ClassWithImplicitPublicInterface::CompareTo

	} // end of class ClassWithImplicitPublicInterface

	.class interface nested private auto ansi abstract beforefieldinit IPrivate
	{
		// Methods
		.method public hidebysig newslot abstract virtual 
			instance int32 CompareTo (
				string other
			) cil managed 
		{
		} // end of method IPrivate::CompareTo

	} // end of class IPrivate


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
	} // end of method ClassWithImplicitInterface::.ctor

	.method public hidebysig 
		instance int32 CallInternalClassWithPrivateInterface (
			string other
		) cil managed 
	{
		// Header size: 12
		// Code size: 12 (0xc)
		.maxstack 2

		IL_0000: newobj instance void ClassWithImplicitInterface/ClassWithImplicitPrivateInterface::.ctor()
		IL_0005: ldarg.1
		IL_0006: call instance int32 ClassWithImplicitInterface/ClassWithImplicitPrivateInterface::CompareTo(string)
		IL_000b: ret
	} // end of method ClassWithImplicitInterface::CallInternalClassWithPrivateInterface

	.method public hidebysig 
		instance int32 CallInternalClassWithPublicInterface (
			string other
		) cil managed 
	{
		// Header size: 12
		// Code size: 12 (0xc)
		.maxstack 2

		IL_0000: newobj instance void ClassWithImplicitInterface/ClassWithImplicitPublicInterface::.ctor()
		IL_0005: ldarg.1
		IL_0006: call instance int32 ClassWithImplicitInterface/ClassWithImplicitPublicInterface::CompareTo(string)
		IL_000b: ret
	} // end of method ClassWithImplicitInterface::CallInternalClassWithPublicInterface

	.method public final hidebysig newslot virtual 
		instance int32 CompareTo (
			string other
		) cil managed 
	{
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "other"
		IL_0008: ldstr "[NullGuard] other is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldc.i4.0
		IL_0014: ret
	} // end of method ClassWithImplicitInterface::CompareTo

} // end of class ClassWithImplicitInterface
