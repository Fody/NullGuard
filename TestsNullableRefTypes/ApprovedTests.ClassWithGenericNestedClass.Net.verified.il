.class public auto ansi beforefieldinit ClassWithGenericNestedClass
	extends [mscorlib]System.Object
{
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 01 00 00
	)
	// Nested Types
	.class nested public auto ansi beforefieldinit NestedNotNull`1<T>
		extends [mscorlib]System.Object
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		// Fields
		.field private !T '<NotNullPropertyWithAllowNull>k__BackingField'
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.field private !T '<NotNullPropertyWithMaybeNull>k__BackingField'
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
		} // end of method NestedNotNull`1::.ctor

		.method public hidebysig specialname 
			instance !T get_NotNullPropertyWithAllowNull () cil managed 
		{
			.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 12
			// Code size: 27 (0x1b)
			.maxstack 2

			IL_0000: ldarg.0
			IL_0001: ldfld !0 class ClassWithGenericNestedClass/NestedNotNull`1<!T>::'<NotNullPropertyWithAllowNull>k__BackingField'
			IL_0006: dup
			IL_0007: box !T
			IL_000c: brtrue.s IL_001a

			IL_000e: pop
			IL_000f: ldstr "[NullGuard] Return value of property 'T ClassWithGenericNestedClass/NestedNotNull`1::NotNullPropertyWithAllowNull()' is null."
			IL_0014: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
			IL_0019: throw

			IL_001a: ret
		} // end of method NestedNotNull`1::get_NotNullPropertyWithAllowNull

		.method public hidebysig specialname 
			instance !T get_NotNullPropertyWithMaybeNull () cil managed 
		{
			.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			.param [0]
				.custom instance void System.Diagnostics.CodeAnalysis.MaybeNullAttribute::.ctor() = (
					01 00 00 00
				)
			// Header size: 12
			// Code size: 7 (0x7)
			.maxstack 1

			IL_0000: ldarg.0
			IL_0001: ldfld !0 class ClassWithGenericNestedClass/NestedNotNull`1<!T>::'<NotNullPropertyWithMaybeNull>k__BackingField'
			IL_0006: ret
		} // end of method NestedNotNull`1::get_NotNullPropertyWithMaybeNull

		.method public hidebysig 
			instance bool MaybeNullOutValueWhenFalse (
				[out] !T& maybeNullWhenFalseArg
			) cil managed 
		{
			.param [1]
				.custom instance void System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute::.ctor(bool) = (
					01 00 00 00 00
				)
			// Header size: 12
			// Code size: 9 (0x9)
			.maxstack 1

			IL_0000: ldarg.1
			IL_0001: initobj !T
			IL_0007: ldc.i4.0
			IL_0008: ret
		} // end of method NestedNotNull`1::MaybeNullOutValueWhenFalse

		.method public hidebysig specialname 
			instance void set_NotNullPropertyWithAllowNull (
				!T 'value'
			) cil managed 
		{
			.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			.param [1]
				.custom instance void System.Diagnostics.CodeAnalysis.AllowNullAttribute::.ctor() = (
					01 00 00 00
				)
			// Header size: 12
			// Code size: 8 (0x8)
			.maxstack 2

			IL_0000: ldarg.0
			IL_0001: ldarg.1
			IL_0002: stfld !0 class ClassWithGenericNestedClass/NestedNotNull`1<!T>::'<NotNullPropertyWithAllowNull>k__BackingField'
			IL_0007: ret
		} // end of method NestedNotNull`1::set_NotNullPropertyWithAllowNull

		.method public hidebysig specialname 
			instance void set_NotNullPropertyWithMaybeNull (
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
			IL_000d: ldstr "[NullGuard] Cannot set the value of property 'T ClassWithGenericNestedClass/NestedNotNull`1::NotNullPropertyWithMaybeNull()' to null."
			IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
			IL_0017: throw

			IL_0018: ldarg.0
			IL_0019: ldarg.1
			IL_001a: stfld !0 class ClassWithGenericNestedClass/NestedNotNull`1<!T>::'<NotNullPropertyWithMaybeNull>k__BackingField'
			IL_001f: ret
		} // end of method NestedNotNull`1::set_NotNullPropertyWithMaybeNull

		// Properties
		.property instance !T NotNullPropertyWithAllowNull()
		{
			.get instance !0 ClassWithGenericNestedClass/NestedNotNull`1::get_NotNullPropertyWithAllowNull()
			.set instance void ClassWithGenericNestedClass/NestedNotNull`1::set_NotNullPropertyWithAllowNull(!0)
		}
		.property instance !T NotNullPropertyWithMaybeNull()
		{
			.get instance !0 ClassWithGenericNestedClass/NestedNotNull`1::get_NotNullPropertyWithMaybeNull()
			.set instance void ClassWithGenericNestedClass/NestedNotNull`1::set_NotNullPropertyWithMaybeNull(!0)
		}

	} // end of class NestedNotNull`1

	.class nested public auto ansi beforefieldinit NestedUnconstrained`1<T>
		extends [mscorlib]System.Object
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		.param type T
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Fields
		.field private !T '<PossiblyNullPropertyWithDisallowNull>k__BackingField'
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.field private !T '<PossiblyNullPropertyWithNotNull>k__BackingField'
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
		} // end of method NestedUnconstrained`1::.ctor

		.method public hidebysig 
			instance void DisallowedNullAndNotNullRefValue (
				!T& nonNullArg
			) cil managed 
		{
			.param [1]
				.custom instance void System.Diagnostics.CodeAnalysis.DisallowNullAttribute::.ctor() = (
					01 00 00 00
				)
				.custom instance void System.Diagnostics.CodeAnalysis.NotNullAttribute::.ctor() = (
					01 00 00 00
				)
			// Header size: 12
			// Code size: 61 (0x3d)
			.maxstack 2

			IL_0000: ldarg.1
			IL_0001: ldobj !T
			IL_0006: box !T
			IL_000b: brtrue.s IL_001d

			IL_000d: ldstr "nonNullArg"
			IL_0012: ldstr "[NullGuard] nonNullArg is null."
			IL_0017: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
			IL_001c: throw

			IL_001d: ldarg.1
			IL_001e: initobj !T
			IL_0024: ldarg.1
			IL_0025: ldobj !T
			IL_002a: box !T
			IL_002f: brtrue.s IL_003c

			IL_0031: ldstr "[NullGuard] Out parameter 'nonNullArg' is null."
			IL_0036: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
			IL_003b: throw

			IL_003c: ret
		} // end of method NestedUnconstrained`1::DisallowedNullAndNotNullRefValue

		.method public hidebysig specialname 
			instance !T get_PossiblyNullPropertyWithDisallowNull () cil managed 
		{
			.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 12
			// Code size: 7 (0x7)
			.maxstack 1

			IL_0000: ldarg.0
			IL_0001: ldfld !0 class ClassWithGenericNestedClass/NestedUnconstrained`1<!T>::'<PossiblyNullPropertyWithDisallowNull>k__BackingField'
			IL_0006: ret
		} // end of method NestedUnconstrained`1::get_PossiblyNullPropertyWithDisallowNull

		.method public hidebysig specialname 
			instance !T get_PossiblyNullPropertyWithNotNull () cil managed 
		{
			.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			.param [0]
				.custom instance void System.Diagnostics.CodeAnalysis.NotNullAttribute::.ctor() = (
					01 00 00 00
				)
			// Header size: 12
			// Code size: 27 (0x1b)
			.maxstack 2

			IL_0000: ldarg.0
			IL_0001: ldfld !0 class ClassWithGenericNestedClass/NestedUnconstrained`1<!T>::'<PossiblyNullPropertyWithNotNull>k__BackingField'
			IL_0006: dup
			IL_0007: box !T
			IL_000c: brtrue.s IL_001a

			IL_000e: pop
			IL_000f: ldstr "[NullGuard] Return value of property 'T ClassWithGenericNestedClass/NestedUnconstrained`1::PossiblyNullPropertyWithNotNull()' is null."
			IL_0014: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
			IL_0019: throw

			IL_001a: ret
		} // end of method NestedUnconstrained`1::get_PossiblyNullPropertyWithNotNull

		.method public hidebysig specialname 
			instance void set_PossiblyNullPropertyWithDisallowNull (
				!T 'value'
			) cil managed 
		{
			.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			.param [1]
				.custom instance void System.Diagnostics.CodeAnalysis.DisallowNullAttribute::.ctor() = (
					01 00 00 00
				)
			// Header size: 12
			// Code size: 32 (0x20)
			.maxstack 2

			IL_0000: ldarg.1
			IL_0001: box !T
			IL_0006: brtrue.s IL_0018

			IL_0008: ldstr "value"
			IL_000d: ldstr "[NullGuard] Cannot set the value of property 'T ClassWithGenericNestedClass/NestedUnconstrained`1::PossiblyNullPropertyWithDisallowNull()' to null."
			IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
			IL_0017: throw

			IL_0018: ldarg.0
			IL_0019: ldarg.1
			IL_001a: stfld !0 class ClassWithGenericNestedClass/NestedUnconstrained`1<!T>::'<PossiblyNullPropertyWithDisallowNull>k__BackingField'
			IL_001f: ret
		} // end of method NestedUnconstrained`1::set_PossiblyNullPropertyWithDisallowNull

		.method public hidebysig specialname 
			instance void set_PossiblyNullPropertyWithNotNull (
				!T 'value'
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
			IL_0002: stfld !0 class ClassWithGenericNestedClass/NestedUnconstrained`1<!T>::'<PossiblyNullPropertyWithNotNull>k__BackingField'
			IL_0007: ret
		} // end of method NestedUnconstrained`1::set_PossiblyNullPropertyWithNotNull

		// Properties
		.property instance !T PossiblyNullPropertyWithDisallowNull()
		{
			.get instance !0 ClassWithGenericNestedClass/NestedUnconstrained`1::get_PossiblyNullPropertyWithDisallowNull()
			.set instance void ClassWithGenericNestedClass/NestedUnconstrained`1::set_PossiblyNullPropertyWithDisallowNull(!0)
		}
		.property instance !T PossiblyNullPropertyWithNotNull()
		{
			.get instance !0 ClassWithGenericNestedClass/NestedUnconstrained`1::get_PossiblyNullPropertyWithNotNull()
			.set instance void ClassWithGenericNestedClass/NestedUnconstrained`1::set_PossiblyNullPropertyWithNotNull(!0)
		}

	} // end of class NestedUnconstrained`1


	// Fields
	.field private string '<Value>k__BackingField'
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
	} // end of method ClassWithGenericNestedClass::.ctor

	.method public hidebysig specialname 
		instance string get_Value () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string ClassWithGenericNestedClass::'<Value>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String ClassWithGenericNestedClass::Value()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ClassWithGenericNestedClass::get_Value

	.method public hidebysig specialname 
		instance void set_Value (
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
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String ClassWithGenericNestedClass::Value()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string ClassWithGenericNestedClass::'<Value>k__BackingField'
		IL_001a: ret
	} // end of method ClassWithGenericNestedClass::set_Value

	// Properties
	.property instance string Value()
	{
		.get instance string ClassWithGenericNestedClass::get_Value()
		.set instance void ClassWithGenericNestedClass::set_Value(string)
	}

} // end of class ClassWithGenericNestedClass
