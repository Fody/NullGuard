.class public auto ansi beforefieldinit ClassWithExplicitInterface
	extends [mscorlib]System.Object
	implements class [mscorlib]System.IComparable`1<string>
{
	// Nested Types
	.class nested private auto ansi beforefieldinit ClassWithExplicitPrivateInterface
		extends [mscorlib]System.Object
		implements ClassWithExplicitInterface/IPrivate
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
		} // end of method ClassWithExplicitPrivateInterface::.ctor

		.method private final hidebysig newslot virtual 
			instance int32 ClassWithExplicitInterface.IPrivate.CompareTo (
				string other1
			) cil managed 
		{
			.override method instance int32 ClassWithExplicitInterface/IPrivate::CompareTo(string)
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.0
			IL_0001: ret
		} // end of method ClassWithExplicitPrivateInterface::ClassWithExplicitInterface.IPrivate.CompareTo

	} // end of class ClassWithExplicitPrivateInterface

	.class nested private auto ansi beforefieldinit ClassWithExplicitPublicInterface
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
		} // end of method ClassWithExplicitPublicInterface::.ctor

		.method private final hidebysig newslot virtual 
			instance int32 'System.IComparable<System.String>.CompareTo' (
				string other1
			) cil managed 
		{
			.override method instance int32 class [mscorlib]System.IComparable`1<string>::CompareTo(!0)
			// Header size: 12
			// Code size: 21 (0x15)
			.maxstack 2

			IL_0000: ldarg.1
			IL_0001: brtrue.s IL_0013

			IL_0003: ldstr "other1"
			IL_0008: ldstr "[NullGuard] other1 is null."
			IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
			IL_0012: throw

			IL_0013: ldc.i4.1
			IL_0014: ret
		} // end of method ClassWithExplicitPublicInterface::'System.IComparable<System.String>.CompareTo'

	} // end of class ClassWithExplicitPublicInterface

	.class interface nested private auto ansi abstract IPrivate
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
	} // end of method ClassWithExplicitInterface::.ctor

	.method public hidebysig 
		instance int32 CallInternalClassWithPrivateInterface (
			string other
		) cil managed 
	{
		// Header size: 12
		// Code size: 12 (0xc)
		.maxstack 2

		IL_0000: newobj instance void ClassWithExplicitInterface/ClassWithExplicitPrivateInterface::.ctor()
		IL_0005: ldarg.1
		IL_0006: callvirt instance int32 ClassWithExplicitInterface/IPrivate::CompareTo(string)
		IL_000b: ret
	} // end of method ClassWithExplicitInterface::CallInternalClassWithPrivateInterface

	.method public hidebysig 
		instance int32 CallInternalClassWithPublicInterface (
			string other
		) cil managed 
	{
		// Header size: 12
		// Code size: 12 (0xc)
		.maxstack 2

		IL_0000: newobj instance void ClassWithExplicitInterface/ClassWithExplicitPublicInterface::.ctor()
		IL_0005: ldarg.1
		IL_0006: callvirt instance int32 class [mscorlib]System.IComparable`1<string>::CompareTo(!0)
		IL_000b: ret
	} // end of method ClassWithExplicitInterface::CallInternalClassWithPublicInterface

	.method private final hidebysig newslot virtual 
		instance int32 'System.IComparable<System.String>.CompareTo' (
			string other
		) cil managed 
	{
		.override method instance int32 class [mscorlib]System.IComparable`1<string>::CompareTo(!0)
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
	} // end of method ClassWithExplicitInterface::'System.IComparable<System.String>.CompareTo'

} // end of class ClassWithExplicitInterface
