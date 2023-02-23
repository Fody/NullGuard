.class public auto ansi beforefieldinit GenericClass`1<T>
	extends [mscorlib]System.Object
{
	// Nested Types
	.class nested private auto ansi sealed beforefieldinit '<GetThing2>d__12'<T>
		extends [mscorlib]System.ValueType
		implements [mscorlib]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T> '<>t__builder'
		.field private valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!T> '<>u__1'
		.field public class [mscorlib]System.Func`1<!T> getThing

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [mscorlib]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Header size: 12
			// Code size: 179 (0xb3)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] !T,
				[2] !T,
				[3] valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!T>,
				[4] class [mscorlib]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_0044

				IL_000a: ldarg.0
				IL_000b: ldfld class [mscorlib]System.Func`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::getThing
				IL_0010: call class [mscorlib]System.Threading.Tasks.Task`1<!!0> [mscorlib]System.Threading.Tasks.Task::Run<!T>(class [mscorlib]System.Func`1<!!0>)
				IL_0015: callvirt instance valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!0> class [mscorlib]System.Threading.Tasks.Task`1<!T>::GetAwaiter()
				IL_001a: stloc.3
				IL_001b: ldloca.s 3
				IL_001d: call instance bool valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!T>::get_IsCompleted()
				IL_0022: brtrue.s IL_0060

				IL_0024: ldarg.0
				IL_0025: ldc.i4.0
				IL_0026: dup
				IL_0027: stloc.0
				IL_0028: stfld int32 valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>1__state'
				IL_002d: ldarg.0
				IL_002e: ldloc.3
				IL_002f: stfld valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>u__1'
				IL_0034: ldarg.0
				IL_0035: ldflda valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>t__builder'
				IL_003a: ldloca.s 3
				IL_003c: ldarg.0
				IL_003d: call instance void valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::AwaitUnsafeOnCompleted<valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!T>, valuetype GenericClass`1/'<GetThing2>d__12'<!T>>(!!0&, !!1&)
				IL_0042: leave.s IL_00b2

				IL_0044: ldarg.0
				IL_0045: ldfld valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>u__1'
				IL_004a: stloc.3
				IL_004b: ldarg.0
				IL_004c: ldflda valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>u__1'
				IL_0051: initobj valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!T>
				IL_0057: ldarg.0
				IL_0058: ldc.i4.m1
				IL_0059: dup
				IL_005a: stloc.0
				IL_005b: stfld int32 valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>1__state'

				IL_0060: ldloca.s 3
				IL_0062: call instance !0 valuetype [mscorlib]System.Runtime.CompilerServices.TaskAwaiter`1<!T>::GetResult()
				IL_0067: stloc.2
				IL_0068: ldloc.2
				IL_0069: stloc.1
				IL_006a: leave.s IL_0085
			} // end .try
			catch [mscorlib]System.Exception
			{
				IL_006c: stloc.s 4
				IL_006e: ldarg.0
				IL_006f: ldc.i4.s -2
				IL_0071: stfld int32 valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>1__state'
				IL_0076: ldarg.0
				IL_0077: ldflda valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>t__builder'
				IL_007c: ldloc.s 4
				IL_007e: call instance void valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetException(class [mscorlib]System.Exception)
				IL_0083: leave.s IL_00b2
			} // end handler

			IL_0085: ldarg.0
			IL_0086: ldc.i4.s -2
			IL_0088: stfld int32 valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>1__state'
			IL_008d: ldarg.0
			IL_008e: ldflda valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>t__builder'
			IL_0093: ldloc.1
			IL_0094: dup
			IL_0095: box !T
			IL_009a: brtrue.s IL_00ad

			IL_009c: pop
			IL_009d: ldstr "[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<T> GenericClass`1::GetThing2(System.Func`1<T>)' is null."
			IL_00a2: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
			IL_00a7: call instance void valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetException(class [mscorlib]System.Exception)
			IL_00ac: ret

			IL_00ad: call instance void valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetResult(!0)

			IL_00b2: ret
		} // end of method '<GetThing2>d__12'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [mscorlib]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [mscorlib]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [mscorlib]System.Runtime.CompilerServices.IAsyncStateMachine)
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetStateMachine(class [mscorlib]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<GetThing2>d__12'::SetStateMachine

	} // end of class <GetThing2>d__12


	// Fields
	.field private !T '<CanBeNullProperty>k__BackingField'
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private !T '<NonNullProperty>k__BackingField'
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
	} // end of method GenericClass`1::.ctor

	.method public hidebysig 
		instance !!U GenericMethod<U> (
			!T t,
			!!U u
		) cil managed 
	{
		// Header size: 12
		// Code size: 78 (0x4e)
		.maxstack 2
		.locals init (
			[0] !!U
		)

		IL_0000: ldarg.1
		IL_0001: box !T
		IL_0006: brtrue.s IL_0018

		IL_0008: ldstr "t"
		IL_000d: ldstr "[NullGuard] t is null."
		IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0017: throw

		IL_0018: ldarg.2
		IL_0019: box !!U
		IL_001e: brtrue.s IL_0030

		IL_0020: ldstr "u"
		IL_0025: ldstr "[NullGuard] u is null."
		IL_002a: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_002f: throw

		IL_0030: ldloca.s 0
		IL_0032: initobj !!U
		IL_0038: ldloc.0
		IL_0039: dup
		IL_003a: box !!U
		IL_003f: brtrue.s IL_004d

		IL_0041: pop
		IL_0042: ldstr "[NullGuard] Return value of method 'U GenericClass`1::GenericMethod(T,U)' is null."
		IL_0047: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_004c: throw

		IL_004d: ret
	} // end of method GenericClass`1::GenericMethod

	.method public hidebysig 
		instance !!U GenericMethodReturnsParameter<U> (
			!T t,
			!!U u
		) cil managed 
	{
		// Header size: 12
		// Code size: 46 (0x2e)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: box !T
		IL_0006: brtrue.s IL_0018

		IL_0008: ldstr "t"
		IL_000d: ldstr "[NullGuard] t is null."
		IL_0012: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0017: throw

		IL_0018: ldarg.2
		IL_0019: dup
		IL_001a: box !!U
		IL_001f: brtrue.s IL_002d

		IL_0021: pop
		IL_0022: ldstr "[NullGuard] Return value of method 'U GenericClass`1::GenericMethodReturnsParameter(T,U)' is null."
		IL_0027: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_002c: throw

		IL_002d: ret
	} // end of method GenericClass`1::GenericMethodReturnsParameter

	.method public hidebysig specialname 
		instance !T get_CanBeNullProperty () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld !0 class GenericClass`1<!T>::'<CanBeNullProperty>k__BackingField'
		IL_0006: ret
	} // end of method GenericClass`1::get_CanBeNullProperty

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

	.method public hidebysig 
		instance class [mscorlib]System.Threading.Tasks.Task`1<!T> GetThing (
			class [mscorlib]System.Func`1<!T> getThing
		) cil managed 
	{
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "getThing"
		IL_0008: ldstr "[NullGuard] getThing is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldarg.1
		IL_0014: call class [mscorlib]System.Threading.Tasks.Task`1<!!0> [mscorlib]System.Threading.Tasks.Task::Run<!T>(class [mscorlib]System.Func`1<!!0>)
		IL_0019: dup
		IL_001a: brtrue.s IL_0028

		IL_001c: pop
		IL_001d: ldstr "[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<T> GenericClass`1::GetThing(System.Func`1<T>)' is null."
		IL_0022: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0027: throw

		IL_0028: ret
	} // end of method GenericClass`1::GetThing

	.method public hidebysig 
		instance class [mscorlib]System.Threading.Tasks.Task`1<!T> GetThing2 (
			class [mscorlib]System.Func`1<!T> getThing
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [mscorlib]System.Type) = (
			01 00 1f 47 65 6e 65 72 69 63 43 6c 61 73 73 60
			31 2b 3c 47 65 74 54 68 69 6e 67 32 3e 64 5f 5f
			31 32 00 00
		)
		// Header size: 12
		// Code size: 74 (0x4a)
		.maxstack 2
		.locals init (
			[0] valuetype GenericClass`1/'<GetThing2>d__12'<!T>
		)

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0013

		IL_0003: ldstr "getThing"
		IL_0008: ldstr "[NullGuard] getThing is null."
		IL_000d: newobj instance void [mscorlib]System.ArgumentNullException::.ctor(string, string)
		IL_0012: throw

		IL_0013: ldloca.s 0
		IL_0015: call valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::Create()
		IL_001a: stfld valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>t__builder'
		IL_001f: ldloca.s 0
		IL_0021: ldarg.1
		IL_0022: stfld class [mscorlib]System.Func`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::getThing
		IL_0027: ldloca.s 0
		IL_0029: ldc.i4.m1
		IL_002a: stfld int32 valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>1__state'
		IL_002f: ldloca.s 0
		IL_0031: ldflda valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>t__builder'
		IL_0036: ldloca.s 0
		IL_0038: call instance void valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::Start<valuetype GenericClass`1/'<GetThing2>d__12'<!T>>(!!0&)
		IL_003d: ldloca.s 0
		IL_003f: ldflda valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype GenericClass`1/'<GetThing2>d__12'<!T>::'<>t__builder'
		IL_0044: call instance class [mscorlib]System.Threading.Tasks.Task`1<!0> valuetype [mscorlib]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::get_Task()
		IL_0049: ret
	} // end of method GenericClass`1::GetThing2

	.method public hidebysig 
		instance !T NonNullMethod () cil managed 
	{
		// Header size: 12
		// Code size: 27 (0x1b)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: call instance !0 class GenericClass`1<!T>::get_CanBeNullProperty()
		IL_0006: dup
		IL_0007: box !T
		IL_000c: brtrue.s IL_001a

		IL_000e: pop
		IL_000f: ldstr "[NullGuard] Return value of method 'T GenericClass`1::NonNullMethod()' is null."
		IL_0014: newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
		IL_0019: throw

		IL_001a: ret
	} // end of method GenericClass`1::NonNullMethod

	.method public hidebysig specialname 
		instance void set_CanBeNullProperty (
			!T 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld !0 class GenericClass`1<!T>::'<CanBeNullProperty>k__BackingField'
		IL_0007: ret
	} // end of method GenericClass`1::set_CanBeNullProperty

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
	.property instance !T CanBeNullProperty()
	{
		.custom instance void CanBeNullAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance !0 GenericClass`1::get_CanBeNullProperty()
		.set instance void GenericClass`1::set_CanBeNullProperty(!0)
	}
	.property instance !T NonNullProperty()
	{
		.get instance !0 GenericClass`1::get_NonNullProperty()
		.set instance void GenericClass`1::set_NonNullProperty(!0)
	}

} // end of class GenericClass`1
