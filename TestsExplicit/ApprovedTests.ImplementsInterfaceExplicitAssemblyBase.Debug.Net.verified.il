﻿.class public auto ansi beforefieldinit ImplementsInterfaceExplicit
	extends [mscorlib]System.Object
	implements [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes
{
	// Fields
	.field private string '<AssemblyWithAnnotations.InterfaceWithAttributes.NotNullProperty>k__BackingField'
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
	} // end of method ImplementsInterfaceExplicit::.ctor

	.method private final hidebysig specialname newslot virtual 
		instance string AssemblyWithAnnotations.InterfaceWithAttributes.get_NotNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.override method instance string [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::get_NotNullProperty()
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string ImplementsInterfaceExplicit::'<AssemblyWithAnnotations.InterfaceWithAttributes.NotNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.NotNullProperty()' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.get_NotNullProperty

	.method private final hidebysig newslot virtual 
		instance void AssemblyWithAnnotations.InterfaceWithAttributes.MethodWithNotNullParameter (
			string canBeNull,
			string arg
		) cil managed 
	{
		.override method instance void [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::MethodWithNotNullParameter(string, string)
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
	} // end of method ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.MethodWithNotNullParameter

	.method private final hidebysig newslot virtual 
		instance string AssemblyWithAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue (
			string arg
		) cil managed 
	{
		.override method instance string [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::MethodWithNotNullReturnValue(string)
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
		IL_000a: ldstr "[NullGuard] Return value of method 'System.String ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue(System.String)' is null."
		IL_000f: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue

	.method private final hidebysig specialname newslot virtual 
		instance void AssemblyWithAnnotations.InterfaceWithAttributes.set_NotNullProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.override method instance void [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::set_NotNullProperty(string)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "value"
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.NotNullProperty()' to null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string ImplementsInterfaceExplicit::'<AssemblyWithAnnotations.InterfaceWithAttributes.NotNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.set_NotNullProperty

	.method public hidebysig specialname 
		instance string get_NotNullProperty () cil managed 
	{
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: callvirt instance string [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::get_NotNullProperty()
		IL_0006: ret
	} // end of method ImplementsInterfaceExplicit::get_NotNullProperty

	.method public hidebysig 
		instance void MethodWithNotNullParameter (
			string canBeNull,
			string arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 3

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldarg.1
		IL_0003: ldarg.2
		IL_0004: callvirt instance void [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::MethodWithNotNullParameter(string, string)
		IL_0009: nop
		IL_000a: ret
	} // end of method ImplementsInterfaceExplicit::MethodWithNotNullParameter

	.method public hidebysig 
		instance string MethodWithNotNullReturnValue (
			string arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 13 (0xd)
		.maxstack 2
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldarg.1
		IL_0003: callvirt instance string [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::MethodWithNotNullReturnValue(string)
		IL_0008: stloc.0
		IL_0009: br.s IL_000b

		IL_000b: ldloc.0
		IL_000c: ret
	} // end of method ImplementsInterfaceExplicit::MethodWithNotNullReturnValue

	.method public hidebysig specialname 
		instance void set_NotNullProperty (
			string 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: callvirt instance void [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes::set_NotNullProperty(string)
		IL_0007: nop
		IL_0008: ret
	} // end of method ImplementsInterfaceExplicit::set_NotNullProperty

	// Properties
	.property instance string AssemblyWithAnnotations.InterfaceWithAttributes.NotNullProperty()
	{
		.get instance string ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.get_NotNullProperty()
		.set instance void ImplementsInterfaceExplicit::AssemblyWithAnnotations.InterfaceWithAttributes.set_NotNullProperty(string)
	}
	.property instance string NotNullProperty()
	{
		.get instance string ImplementsInterfaceExplicit::get_NotNullProperty()
		.set instance void ImplementsInterfaceExplicit::set_NotNullProperty(string)
	}

} // end of class ImplementsInterfaceExplicit
