﻿.class public auto ansi beforefieldinit ImplementsInterfaceExplicit
	extends [netstandard]System.Object
	implements [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes
{
	// Fields
	.field private string '<AssemblyWithExternalAnnotations.InterfaceWithAttributes.NotNullProperty>k__BackingField'
	.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
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
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: ret
	} // end of method ImplementsInterfaceExplicit::.ctor

	.method private final hidebysig specialname newslot virtual 
		instance string AssemblyWithExternalAnnotations.InterfaceWithAttributes.get_NotNullProperty () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.override method instance string [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::get_NotNullProperty()
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string ImplementsInterfaceExplicit::'<AssemblyWithExternalAnnotations.InterfaceWithAttributes.NotNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.NotNullProperty()' is null."
		IL_000f: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.get_NotNullProperty

	.method private final hidebysig newslot virtual 
		instance void AssemblyWithExternalAnnotations.InterfaceWithAttributes.MethodWithNotNullParameter (
			string canBeNull,
			string arg
		) cil managed 
	{
		.override method instance void [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::MethodWithNotNullParameter(string, string)
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2

		IL_0000: ldarg.2
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "arg"
		IL_0008: ldstr "[NullGuard] arg is null."
		IL_000d: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ret
	} // end of method ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.MethodWithNotNullParameter

	.method private final hidebysig newslot virtual 
		instance string AssemblyWithExternalAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue (
			string arg
		) cil managed 
	{
		.override method instance string [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::MethodWithNotNullReturnValue(string)
		// Header size: 12
		// Code size: 17 (0x11)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: dup
		IL_0002: brtrue.s IL_0010

		IL_0004: pop
		IL_0005: ldstr "[NullGuard] Return value of method 'System.String ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue(System.String)' is null."
		IL_000a: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
		IL_000f: throw

		IL_0010: ret
	} // end of method ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue

	.method private final hidebysig specialname newslot virtual 
		instance void AssemblyWithExternalAnnotations.InterfaceWithAttributes.set_NotNullProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.override method instance void [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::set_NotNullProperty(string)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "value"
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.NotNullProperty()' to null."
		IL_000d: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string ImplementsInterfaceExplicit::'<AssemblyWithExternalAnnotations.InterfaceWithAttributes.NotNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.set_NotNullProperty

	.method public hidebysig specialname 
		instance string get_NotNullProperty () cil managed 
	{
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: callvirt instance string [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::get_NotNullProperty()
		IL_0006: ret
	} // end of method ImplementsInterfaceExplicit::get_NotNullProperty

	.method public hidebysig 
		instance void MethodWithNotNullParameter (
			string canBeNull,
			string arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 3

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: ldarg.2
		IL_0003: callvirt instance void [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::MethodWithNotNullParameter(string, string)
		IL_0008: ret
	} // end of method ImplementsInterfaceExplicit::MethodWithNotNullParameter

	.method public hidebysig 
		instance string MethodWithNotNullReturnValue (
			string arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: callvirt instance string [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::MethodWithNotNullReturnValue(string)
		IL_0007: ret
	} // end of method ImplementsInterfaceExplicit::MethodWithNotNullReturnValue

	.method public hidebysig specialname 
		instance void set_NotNullProperty (
			string 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: callvirt instance void [AssemblyWithExternalAnnotations]AssemblyWithExternalAnnotations.InterfaceWithAttributes::set_NotNullProperty(string)
		IL_0007: ret
	} // end of method ImplementsInterfaceExplicit::set_NotNullProperty

	// Properties
	.property instance string AssemblyWithExternalAnnotations.InterfaceWithAttributes.NotNullProperty()
	{
		.get instance string ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.get_NotNullProperty()
		.set instance void ImplementsInterfaceExplicit::AssemblyWithExternalAnnotations.InterfaceWithAttributes.set_NotNullProperty(string)
	}
	.property instance string NotNullProperty()
	{
		.get instance string ImplementsInterfaceExplicit::get_NotNullProperty()
		.set instance void ImplementsInterfaceExplicit::set_NotNullProperty(string)
	}

} // end of class ImplementsInterfaceExplicit
