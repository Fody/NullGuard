﻿.class public auto ansi abstract beforefieldinit ClassWithBadAttributes
	extends [netstandard]System.Object
{
	// Methods
	.method family hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method ClassWithBadAttributes::.ctor

	.method public hidebysig specialname newslot abstract virtual 
		instance string get_PropertyAllowsNullGetButDoesNotAllowNullSet () cil managed 
	{
	} // end of method ClassWithBadAttributes::get_PropertyAllowsNullGetButDoesNotAllowNullSet

	.method public hidebysig specialname newslot abstract virtual 
		instance string get_PropertyWithNoNullCheckOnSet () cil managed 
	{
	} // end of method ClassWithBadAttributes::get_PropertyWithNoNullCheckOnSet

	.method public hidebysig newslot abstract virtual 
		instance string MethodAllowsNullReturnValue () cil managed 
	{
	} // end of method ClassWithBadAttributes::MethodAllowsNullReturnValue

	.method public hidebysig newslot abstract virtual 
		instance void MethodWithNoNullCheckOnParam (
			string arg
		) cil managed 
	{
	} // end of method ClassWithBadAttributes::MethodWithNoNullCheckOnParam

	.method public hidebysig specialname newslot abstract virtual 
		instance void set_PropertyAllowsNullGetButDoesNotAllowNullSet (
			string 'value'
		) cil managed 
	{
	} // end of method ClassWithBadAttributes::set_PropertyAllowsNullGetButDoesNotAllowNullSet

	.method public hidebysig specialname newslot abstract virtual 
		instance void set_PropertyWithNoNullCheckOnSet (
			string 'value'
		) cil managed 
	{
	} // end of method ClassWithBadAttributes::set_PropertyWithNoNullCheckOnSet

	// Properties
	.property instance string PropertyAllowsNullGetButDoesNotAllowNullSet()
	{
		.get instance string ClassWithBadAttributes::get_PropertyAllowsNullGetButDoesNotAllowNullSet()
		.set instance void ClassWithBadAttributes::set_PropertyAllowsNullGetButDoesNotAllowNullSet(string)
	}
	.property instance string PropertyWithNoNullCheckOnSet()
	{
		.get instance string ClassWithBadAttributes::get_PropertyWithNoNullCheckOnSet()
		.set instance void ClassWithBadAttributes::set_PropertyWithNoNullCheckOnSet(string)
	}

} // end of class ClassWithBadAttributes
