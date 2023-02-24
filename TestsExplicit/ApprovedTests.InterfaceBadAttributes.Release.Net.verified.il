.class interface private auto ansi abstract InterfaceBadAttributes
{
	// Methods
	.method public hidebysig specialname newslot abstract virtual 
		instance string get_PropertyAllowsNullGetButDoesNotAllowNullSet () cil managed 
	{
	} // end of method InterfaceBadAttributes::get_PropertyAllowsNullGetButDoesNotAllowNullSet

	.method public hidebysig specialname newslot abstract virtual 
		instance string get_PropertyWithNoNullCheckOnSet () cil managed 
	{
	} // end of method InterfaceBadAttributes::get_PropertyWithNoNullCheckOnSet

	.method public hidebysig newslot abstract virtual 
		instance string MethodAllowsNullReturnValue () cil managed 
	{
	} // end of method InterfaceBadAttributes::MethodAllowsNullReturnValue

	.method public hidebysig newslot abstract virtual 
		instance void MethodWithNoNullCheckOnParam (
			string arg
		) cil managed 
	{
		.param [1]
			.custom instance void [JetBrains.Annotations]JetBrains.Annotations.CanBeNullAttribute::.ctor() = (
				01 00 00 00
			)
	} // end of method InterfaceBadAttributes::MethodWithNoNullCheckOnParam

	.method public hidebysig specialname newslot abstract virtual 
		instance void set_PropertyAllowsNullGetButDoesNotAllowNullSet (
			string 'value'
		) cil managed 
	{
	} // end of method InterfaceBadAttributes::set_PropertyAllowsNullGetButDoesNotAllowNullSet

	.method public hidebysig specialname newslot abstract virtual 
		instance void set_PropertyWithNoNullCheckOnSet (
			string 'value'
		) cil managed 
	{
	} // end of method InterfaceBadAttributes::set_PropertyWithNoNullCheckOnSet

	// Properties
	.property instance string PropertyAllowsNullGetButDoesNotAllowNullSet()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string InterfaceBadAttributes::get_PropertyAllowsNullGetButDoesNotAllowNullSet()
		.set instance void InterfaceBadAttributes::set_PropertyAllowsNullGetButDoesNotAllowNullSet(string)
	}
	.property instance string PropertyWithNoNullCheckOnSet()
	{
		.custom instance void [JetBrains.Annotations]JetBrains.Annotations.NotNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance string InterfaceBadAttributes::get_PropertyWithNoNullCheckOnSet()
		.set instance void InterfaceBadAttributes::set_PropertyWithNoNullCheckOnSet(string)
	}

} // end of class InterfaceBadAttributes
