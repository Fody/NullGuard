.class public auto ansi beforefieldinit ImplementsInterface
	extends [netstandard]System.Object
	implements [AssemblyWithAnnotations]AssemblyWithAnnotations.InterfaceWithAttributes
{
	// Fields
	.field private string '<NotNullProperty>k__BackingField'
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
	} // end of method ImplementsInterface::.ctor

	.method public final hidebysig specialname newslot virtual 
		instance string get_NotNullProperty () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld string ImplementsInterface::'<NotNullProperty>k__BackingField'
		IL_0006: dup
		IL_0007: brtrue.s IL_0015

		IL_0009: pop
		IL_000a: ldstr "[NullGuard] Return value of property 'System.String ImplementsInterface::NotNullProperty()' is null."
		IL_000f: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
		IL_0014: throw

		IL_0015: ret
	} // end of method ImplementsInterface::get_NotNullProperty

	.method public final hidebysig newslot virtual 
		instance void MethodWithNotNullParameter (
			string canBeNull,
			string arg
		) cil managed 
	{
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
	} // end of method ImplementsInterface::MethodWithNotNullParameter

	.method public final hidebysig newslot virtual 
		instance string MethodWithNotNullReturnValue (
			string arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 17 (0x11)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: dup
		IL_0002: brtrue.s IL_0010

		IL_0004: pop
		IL_0005: ldstr "[NullGuard] Return value of method 'System.String ImplementsInterface::MethodWithNotNullReturnValue(System.String)' is null."
		IL_000a: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
		IL_000f: throw

		IL_0010: ret
	} // end of method ImplementsInterface::MethodWithNotNullReturnValue

	.method public final hidebysig specialname newslot virtual 
		instance void set_NotNullProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "value"
		IL_0008: ldstr "[NullGuard] Cannot set the value of property 'System.String ImplementsInterface::NotNullProperty()' to null."
		IL_000d: newobj instance void [netstandard]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.0
		IL_0014: ldarg.1
		IL_0015: stfld string ImplementsInterface::'<NotNullProperty>k__BackingField'
		IL_001a: ret
	} // end of method ImplementsInterface::set_NotNullProperty

	// Properties
	.property instance string NotNullProperty()
	{
		.get instance string ImplementsInterface::get_NotNullProperty()
		.set instance void ImplementsInterface::set_NotNullProperty(string)
	}

} // end of class ImplementsInterface
