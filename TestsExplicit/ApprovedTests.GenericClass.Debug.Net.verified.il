﻿.class public auto ansi beforefieldinit GenericClass`1<T>
	extends [mscorlib]System.Object
{
	// Fields
	.field private !T '<NonNullProperty>k__BackingField'
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
		// Code size: 8 (0x8)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method GenericClass`1::.ctor

	.method public hidebysig specialname 
		instance !T get_NonNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld !0 class GenericClass`1<!T>::'<NonNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: box !T
		IL_000c: brtrue.s IL_001a

		IL_000e: pop
		IL_000f: ldstr "[NullGuard] Return value of property 'T GenericClass`1::NonNullProperty()' is null."
		IL_0014: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0019: throw

		IL_001a: ret
	} // end of method GenericClass`1::get_NonNullProperty

	.method public hidebysig specialname 
		instance void set_NonNullProperty (
			!T 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 32 (0x20)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: box !T
		IL_0006: brtrue.s IL_0018

		IL_0008: ldstr "value"
		IL_000d: ldstr "[NullGuard] Cannot set the value of property 'T GenericClass`1::NonNullProperty()' to null."
		IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0017: throw

		IL_0018: ldarg.0
		IL_0019: ldarg.1
		IL_001a: stfld !0 class GenericClass`1<!T>::'<NonNullProperty>k__BackingField'
		IL_001f: ret
	} // end of method GenericClass`1::set_NonNullProperty

	// Properties
	.property instance !T NonNullProperty()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance !0 GenericClass`1::get_NonNullProperty()
		.set instance void GenericClass`1::set_NonNullProperty(!0)
	}

} // end of class GenericClass`1
