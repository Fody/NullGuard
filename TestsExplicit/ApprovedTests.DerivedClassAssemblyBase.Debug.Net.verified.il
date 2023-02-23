.class public auto ansi beforefieldinit DerivedClass
	extends [AssemblyWithAnnotations]AssemblyWithAnnotations.BaseClassWithAttributes
{
	// Fields
	.field private string '<NotNullProperty>k__BackingField'
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
		IL_0001: call instance void [AssemblyWithAnnotations]AssemblyWithAnnotations.BaseClassWithAttributes::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method DerivedClass::.ctor

	.method public hidebysig specialname virtual 
		instance string get_NotNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string DerivedClass::'<NotNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String DerivedClass::NotNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method DerivedClass::get_NotNullProperty

	.method public hidebysig virtual 
		instance void MethodWithNotNullParameter (
			string canBeNull,
			string arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldarg.2
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "arg"
		IL_0008: ldstr "[NullGuard] arg is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: nop
		IL_0014: ret
	} // end of method DerivedClass::MethodWithNotNullParameter

	.method public hidebysig virtual 
		instance string MethodWithNotNullReturnValue (
			string arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: stloc.0
		IL_0003: br.s IL_0005

		IL_0005: ldloc.0
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of method 'System.String DerivedClass::MethodWithNotNullReturnValue(System.String)' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method DerivedClass::MethodWithNotNullReturnValue

	.method public hidebysig specialname virtual 
		instance void set_NotNullProperty (
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
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String DerivedClass::NotNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string DerivedClass::'<NotNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method DerivedClass::set_NotNullProperty

	// Properties
	.property instance string NotNullProperty()
	{
		.get instance string DerivedClass::get_NotNullProperty()
		.set instance void DerivedClass::set_NotNullProperty(string)
	}

} // end of class DerivedClass
