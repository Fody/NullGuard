.class public auto ansi beforefieldinit Indexers
	extends [netstandard]System.Object
{
	// Nested Types
	.class nested public auto ansi beforefieldinit AllowedNulls
		extends [netstandard]System.Object
	{
		.custom instance void [netstandard]System.Reflection.DefaultMemberAttribute::.ctor(string) = (
			01 00 04 49 74 65 6d 00 00
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
		} // end of method AllowedNulls::.ctor

		.method public hidebysig specialname 
			instance string get_Item (
				string allowNull,
				valuetype [netstandard]System.Nullable`1<int32> nullableInt,
				[opt] string optional
			) cil managed 
		{
			.param [1]
				.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
					01 00 00 00
				)
			.param [3] = nullref
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
		} // end of method AllowedNulls::get_Item

		.method public hidebysig specialname 
			instance void set_Item (
				string allowNull,
				valuetype [netstandard]System.Nullable`1<int32> nullableInt,
				[opt] string optional,
				string 'value'
			) cil managed 
		{
			.param [1]
				.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
					01 00 00 00
				)
			.param [3] = nullref
			// Header size: 12
			// Code size: 2 (0x2)
			.maxstack 0

			IL_0000: nop
			IL_0001: ret
		} // end of method AllowedNulls::set_Item

		// Properties
		.property instance string Item(
			string allowNull,
			valuetype [netstandard]System.Nullable`1<int32> nullableInt,
			[opt] string optional
		)
		{
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
				01 00 00 00
			)
			.get instance string Indexers/AllowedNulls::get_Item(string, valuetype [netstandard]System.Nullable`1<int32>, string)
			.set instance void Indexers/AllowedNulls::set_Item(string, valuetype [netstandard]System.Nullable`1<int32>, string, string)
		}

	} // end of class AllowedNulls

	.class nested public auto ansi beforefieldinit NonNullable
		extends [netstandard]System.Object
	{
		.custom instance void [netstandard]System.Reflection.DefaultMemberAttribute::.ctor(string) = (
			01 00 04 49 74 65 6d 00 00
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
		} // end of method NonNullable::.ctor

		.method public hidebysig specialname 
			instance string get_Item (
				string nonNullParam1,
				string nonNullParam2
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
			// Code size: 59 (0x3b)
			.maxstack 2

			IL_0000: ldarg.1
			IL_0001: brtrue.s IL_0013

			IL_0003: ldstr "nonNullParam1"
			IL_0008: ldstr "[NullGuard] nonNullParam1 is null."
			IL_000d: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
			IL_0012: throw

			IL_0013: ldarg.2
			IL_0014: brtrue.s IL_0026

			IL_0016: ldstr "nonNullParam2"
			IL_001b: ldstr "[NullGuard] nonNullParam2 is null."
			IL_0020: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
			IL_0025: throw

			IL_0026: ldstr "return value of NonNullable"
			IL_002b: dup
			IL_002c: brtrue.s IL_003a

			IL_002e: pop
			IL_002f: ldstr "[NullGuard] Return value of property 'System.String Indexers/NonNullable::Item(System.String,System.String)' is null."
			IL_0034: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
			IL_0039: throw

			IL_003a: ret
		} // end of method NonNullable::get_Item

		.method public hidebysig specialname 
			instance void set_Item (
				string nonNullParam1,
				string nonNullParam2,
				string 'value'
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
			// Code size: 59 (0x3b)
			.maxstack 2

			IL_0000: ldarg.3
			IL_0001: brtrue.s IL_0013

			IL_0003: ldstr "value"
			IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String Indexers/NonNullable::Item(System.String,System.String)' to null."
			IL_000d: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
			IL_0012: throw

			IL_0013: ldarg.1
			IL_0014: brtrue.s IL_0026

			IL_0016: ldstr "nonNullParam1"
			IL_001b: ldstr "[NullGuard] nonNullParam1 is null."
			IL_0020: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
			IL_0025: throw

			IL_0026: ldarg.2
			IL_0027: brtrue.s IL_0039

			IL_0029: ldstr "nonNullParam2"
			IL_002e: ldstr "[NullGuard] nonNullParam2 is null."
			IL_0033: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
			IL_0038: throw

			IL_0039: nop
			IL_003a: ret
		} // end of method NonNullable::set_Item

		// Properties
		.property instance string Item(
			string nonNullParam1,
			string nonNullParam2
		)
		{
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
			.get instance string Indexers/NonNullable::get_Item(string, string)
			.set instance void Indexers/NonNullable::set_Item(string, string, string)
		}

	} // end of class NonNullable

	.class nested public auto ansi beforefieldinit PassThroughGetterReturnValue
		extends [netstandard]System.Object
	{
		.custom instance void [netstandard]System.Reflection.DefaultMemberAttribute::.ctor(string) = (
			01 00 04 49 74 65 6d 00 00
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
		} // end of method PassThroughGetterReturnValue::.ctor

		.method public hidebysig specialname 
			instance string get_Item (
				string returnValue
			) cil managed 
		{
			.param [1]
				.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
					01 00 00 00
				)
			// Header size: 12
			// Code size: 17 (0x11)
			.maxstack 2

			IL_0000: ldarg.1
			IL_0001: dup
			IL_0002: brtrue.s IL_0010

			IL_0004: pop
			IL_0005: ldstr "[NullGuard] Return value of property 'System.String Indexers/PassThroughGetterReturnValue::Item(System.String)' is null."
			IL_000a: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
			IL_000f: throw

			IL_0010: ret
		} // end of method PassThroughGetterReturnValue::get_Item

		// Properties
		.property instance string Item(
			string returnValue
		)
		{
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
			.get instance string Indexers/PassThroughGetterReturnValue::get_Item(string)
		}

	} // end of class PassThroughGetterReturnValue


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
	} // end of method Indexers::.ctor

} // end of class Indexers
