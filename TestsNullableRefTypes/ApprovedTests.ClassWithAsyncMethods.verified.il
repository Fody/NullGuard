.class public auto ansi beforefieldinit ClassWithAsyncMethods
	extends [netstandard]System.Object
{
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 01 00 00
	)
	// Nested Types
	.class nested private auto ansi sealed beforefieldinit '<GetMaybeNullAsync>d__7`1'<T>
		extends [netstandard]System.ValueType
		implements .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		[netstandard]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T> '<>t__builder'
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Header size: 12
			// Code size: 61 (0x3d)
			.maxstack 2
			.locals init (
				[0] int32,
				[1] !T,
				[2] class [netstandard]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!T>::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloca.s 1
				IL_0009: initobj !T
				IL_000f: leave.s IL_0028
			} // end .try
			catch [netstandard]System.Exception
			{
				IL_0011: stloc.2
				IL_0012: ldarg.0
				IL_0013: ldc.i4.s -2
				IL_0015: stfld int32 valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!T>::'<>1__state'
				IL_001a: ldarg.0
				IL_001b: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!T>::'<>t__builder'
				IL_0020: ldloc.2
				IL_0021: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetException(class [netstandard]System.Exception)
				IL_0026: leave.s IL_003c
			} // end handler

			IL_0028: ldarg.0
			IL_0029: ldc.i4.s -2
			IL_002b: stfld int32 valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!T>::'<>1__state'
			IL_0030: ldarg.0
			IL_0031: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!T>::'<>t__builder'
			IL_0036: ldloc.1
			IL_0037: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetResult(!0)

			IL_003c: ret
		} // end of method '<GetMaybeNullAsync>d__7`1'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [netstandard]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			.param [1]
				.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
					01 00 00 00 00
				)
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!T>::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<GetMaybeNullAsync>d__7`1'::SetStateMachine

	} // end of class <GetMaybeNullAsync>d__7`1

	.class nested private auto ansi sealed beforefieldinit '<GetNonNullAsync>d__4'
		extends [netstandard]System.ValueType
		implements .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		[netstandard]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> '<>t__builder'
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Header size: 12
			// Code size: 75 (0x4b)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] string,
				[2] class [netstandard]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldnull
				IL_0008: stloc.1
				IL_0009: leave.s IL_0022
			} // end .try
			catch [netstandard]System.Exception
			{
				IL_000b: stloc.2
				IL_000c: ldarg.0
				IL_000d: ldc.i4.s -2
				IL_000f: stfld int32 ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>1__state'
				IL_0014: ldarg.0
				IL_0015: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>t__builder'
				IL_001a: ldloc.2
				IL_001b: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::SetException(class [netstandard]System.Exception)
				IL_0020: leave.s IL_004a
			} // end handler

			IL_0022: ldarg.0
			IL_0023: ldc.i4.s -2
			IL_0025: stfld int32 ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>1__state'
			IL_002a: ldarg.0
			IL_002b: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>t__builder'
			IL_0030: ldloc.1
			IL_0031: dup
			IL_0032: brtrue.s IL_0045

			IL_0034: pop
			IL_0035: ldstr "[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> ClassWithAsyncMethods::GetNonNullAsync()' is null."
			IL_003a: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
			IL_003f: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::SetException(class [netstandard]System.Exception)
			IL_0044: ret

			IL_0045: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::SetResult(!0)

			IL_004a: ret
		} // end of method '<GetNonNullAsync>d__4'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [netstandard]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			.param [1]
				.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
					01 00 00 00 00
				)
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<GetNonNullAsync>d__4'::SetStateMachine

	} // end of class <GetNonNullAsync>d__4

	.class nested private auto ansi sealed beforefieldinit '<GetNonNullAsyncWithDelay>d__6`1'<T>
		extends [netstandard]System.ValueType
		implements .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		[netstandard]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T> '<>t__builder'
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		.field private valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Header size: 12
			// Code size: 177 (0xb1)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] !T,
				[2] valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter,
				[3] class [netstandard]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_003f

				IL_000a: ldc.i4.1
				IL_000b: call class [netstandard]System.Threading.Tasks.Task [netstandard]System.Threading.Tasks.Task::Delay(int32)
				IL_0010: callvirt instance valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter [netstandard]System.Threading.Tasks.Task::GetAwaiter()
				IL_0015: stloc.2
				IL_0016: ldloca.s 2
				IL_0018: call instance bool [netstandard]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
				IL_001d: brtrue.s IL_005b

				IL_001f: ldarg.0
				IL_0020: ldc.i4.0
				IL_0021: dup
				IL_0022: stloc.0
				IL_0023: stfld int32 valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>1__state'
				IL_0028: ldarg.0
				IL_0029: ldloc.2
				IL_002a: stfld valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>u__1'
				IL_002f: ldarg.0
				IL_0030: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>t__builder'
				IL_0035: ldloca.s 2
				IL_0037: ldarg.0
				IL_0038: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::AwaitUnsafeOnCompleted<valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter, valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>>(!!0&, !!1&)
				IL_003d: leave.s IL_00b0

				IL_003f: ldarg.0
				IL_0040: ldfld valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>u__1'
				IL_0045: stloc.2
				IL_0046: ldarg.0
				IL_0047: ldflda valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>u__1'
				IL_004c: initobj [netstandard]System.Runtime.CompilerServices.TaskAwaiter
				IL_0052: ldarg.0
				IL_0053: ldc.i4.m1
				IL_0054: dup
				IL_0055: stloc.0
				IL_0056: stfld int32 valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>1__state'

				IL_005b: ldloca.s 2
				IL_005d: call instance void [netstandard]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
				IL_0062: ldloca.s 1
				IL_0064: initobj !T
				IL_006a: leave.s IL_0083
			} // end .try
			catch [netstandard]System.Exception
			{
				IL_006c: stloc.3
				IL_006d: ldarg.0
				IL_006e: ldc.i4.s -2
				IL_0070: stfld int32 valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>1__state'
				IL_0075: ldarg.0
				IL_0076: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>t__builder'
				IL_007b: ldloc.3
				IL_007c: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetException(class [netstandard]System.Exception)
				IL_0081: leave.s IL_00b0
			} // end handler

			IL_0083: ldarg.0
			IL_0084: ldc.i4.s -2
			IL_0086: stfld int32 valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>1__state'
			IL_008b: ldarg.0
			IL_008c: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>t__builder'
			IL_0091: ldloc.1
			IL_0092: dup
			IL_0093: box !T
			IL_0098: brtrue.s IL_00ab

			IL_009a: pop
			IL_009b: ldstr "[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<T> ClassWithAsyncMethods::GetNonNullAsyncWithDelay()' is null."
			IL_00a0: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
			IL_00a5: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetException(class [netstandard]System.Exception)
			IL_00aa: ret

			IL_00ab: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetResult(!0)

			IL_00b0: ret
		} // end of method '<GetNonNullAsyncWithDelay>d__6`1'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [netstandard]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			.param [1]
				.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
					01 00 00 00 00
				)
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!T>::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<GetNonNullAsyncWithDelay>d__6`1'::SetStateMachine

	} // end of class <GetNonNullAsyncWithDelay>d__6`1

	.class nested private auto ansi sealed beforefieldinit '<GetNullAsync>d__5'
		extends [netstandard]System.ValueType
		implements .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		[netstandard]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> '<>t__builder'
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Header size: 12
			// Code size: 55 (0x37)
			.maxstack 2
			.locals init (
				[0] int32,
				[1] string,
				[2] class [netstandard]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldnull
				IL_0008: stloc.1
				IL_0009: leave.s IL_0022
			} // end .try
			catch [netstandard]System.Exception
			{
				IL_000b: stloc.2
				IL_000c: ldarg.0
				IL_000d: ldc.i4.s -2
				IL_000f: stfld int32 ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>1__state'
				IL_0014: ldarg.0
				IL_0015: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>t__builder'
				IL_001a: ldloc.2
				IL_001b: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::SetException(class [netstandard]System.Exception)
				IL_0020: leave.s IL_0036
			} // end handler

			IL_0022: ldarg.0
			IL_0023: ldc.i4.s -2
			IL_0025: stfld int32 ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>1__state'
			IL_002a: ldarg.0
			IL_002b: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>t__builder'
			IL_0030: ldloc.1
			IL_0031: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::SetResult(!0)

			IL_0036: ret
		} // end of method '<GetNullAsync>d__5'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [netstandard]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			.param [1]
				.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
					01 00 00 00 00
				)
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<GetNullAsync>d__5'::SetStateMachine

	} // end of class <GetNullAsync>d__5

	.class nested private auto ansi sealed beforefieldinit '<GetNullAsyncWithDelay>d__8`1'<class T>
		extends [netstandard]System.ValueType
		implements .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		[netstandard]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T> '<>t__builder'
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		.field private valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Header size: 12
			// Code size: 152 (0x98)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] !T,
				[2] valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter,
				[3] class [netstandard]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_003f

				IL_000a: ldc.i4.1
				IL_000b: call class [netstandard]System.Threading.Tasks.Task [netstandard]System.Threading.Tasks.Task::Delay(int32)
				IL_0010: callvirt instance valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter [netstandard]System.Threading.Tasks.Task::GetAwaiter()
				IL_0015: stloc.2
				IL_0016: ldloca.s 2
				IL_0018: call instance bool [netstandard]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
				IL_001d: brtrue.s IL_005b

				IL_001f: ldarg.0
				IL_0020: ldc.i4.0
				IL_0021: dup
				IL_0022: stloc.0
				IL_0023: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>1__state'
				IL_0028: ldarg.0
				IL_0029: ldloc.2
				IL_002a: stfld valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>u__1'
				IL_002f: ldarg.0
				IL_0030: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>t__builder'
				IL_0035: ldloca.s 2
				IL_0037: ldarg.0
				IL_0038: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::AwaitUnsafeOnCompleted<valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter, valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>>(!!0&, !!1&)
				IL_003d: leave.s IL_0097

				IL_003f: ldarg.0
				IL_0040: ldfld valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>u__1'
				IL_0045: stloc.2
				IL_0046: ldarg.0
				IL_0047: ldflda valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>u__1'
				IL_004c: initobj [netstandard]System.Runtime.CompilerServices.TaskAwaiter
				IL_0052: ldarg.0
				IL_0053: ldc.i4.m1
				IL_0054: dup
				IL_0055: stloc.0
				IL_0056: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>1__state'

				IL_005b: ldloca.s 2
				IL_005d: call instance void [netstandard]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
				IL_0062: ldloca.s 1
				IL_0064: initobj !T
				IL_006a: leave.s IL_0083
			} // end .try
			catch [netstandard]System.Exception
			{
				IL_006c: stloc.3
				IL_006d: ldarg.0
				IL_006e: ldc.i4.s -2
				IL_0070: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>1__state'
				IL_0075: ldarg.0
				IL_0076: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>t__builder'
				IL_007b: ldloc.3
				IL_007c: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetException(class [netstandard]System.Exception)
				IL_0081: leave.s IL_0097
			} // end handler

			IL_0083: ldarg.0
			IL_0084: ldc.i4.s -2
			IL_0086: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>1__state'
			IL_008b: ldarg.0
			IL_008c: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>t__builder'
			IL_0091: ldloc.1
			IL_0092: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetResult(!0)

			IL_0097: ret
		} // end of method '<GetNullAsyncWithDelay>d__8`1'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [netstandard]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			.param [1]
				.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
					01 00 00 00 00
				)
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!T>::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<GetNullAsyncWithDelay>d__8`1'::SetStateMachine

	} // end of class <GetNullAsyncWithDelay>d__8`1

	.class nested private auto ansi sealed beforefieldinit '<GetNullAsyncWithDelay2>d__9`1'<class T>
		extends [netstandard]System.ValueType
		implements .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		[netstandard]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T> '<>t__builder'
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		.field private valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Header size: 12
			// Code size: 152 (0x98)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] !T,
				[2] valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter,
				[3] class [netstandard]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_003f

				IL_000a: ldc.i4.1
				IL_000b: call class [netstandard]System.Threading.Tasks.Task [netstandard]System.Threading.Tasks.Task::Delay(int32)
				IL_0010: callvirt instance valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter [netstandard]System.Threading.Tasks.Task::GetAwaiter()
				IL_0015: stloc.2
				IL_0016: ldloca.s 2
				IL_0018: call instance bool [netstandard]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
				IL_001d: brtrue.s IL_005b

				IL_001f: ldarg.0
				IL_0020: ldc.i4.0
				IL_0021: dup
				IL_0022: stloc.0
				IL_0023: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>1__state'
				IL_0028: ldarg.0
				IL_0029: ldloc.2
				IL_002a: stfld valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>u__1'
				IL_002f: ldarg.0
				IL_0030: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>t__builder'
				IL_0035: ldloca.s 2
				IL_0037: ldarg.0
				IL_0038: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::AwaitUnsafeOnCompleted<valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter, valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>>(!!0&, !!1&)
				IL_003d: leave.s IL_0097

				IL_003f: ldarg.0
				IL_0040: ldfld valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>u__1'
				IL_0045: stloc.2
				IL_0046: ldarg.0
				IL_0047: ldflda valuetype [netstandard]System.Runtime.CompilerServices.TaskAwaiter valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>u__1'
				IL_004c: initobj [netstandard]System.Runtime.CompilerServices.TaskAwaiter
				IL_0052: ldarg.0
				IL_0053: ldc.i4.m1
				IL_0054: dup
				IL_0055: stloc.0
				IL_0056: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>1__state'

				IL_005b: ldloca.s 2
				IL_005d: call instance void [netstandard]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
				IL_0062: ldloca.s 1
				IL_0064: initobj !T
				IL_006a: leave.s IL_0083
			} // end .try
			catch [netstandard]System.Exception
			{
				IL_006c: stloc.3
				IL_006d: ldarg.0
				IL_006e: ldc.i4.s -2
				IL_0070: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>1__state'
				IL_0075: ldarg.0
				IL_0076: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>t__builder'
				IL_007b: ldloc.3
				IL_007c: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetException(class [netstandard]System.Exception)
				IL_0081: leave.s IL_0097
			} // end handler

			IL_0083: ldarg.0
			IL_0084: ldc.i4.s -2
			IL_0086: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>1__state'
			IL_008b: ldarg.0
			IL_008c: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>t__builder'
			IL_0091: ldloc.1
			IL_0092: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetResult(!0)

			IL_0097: ret
		} // end of method '<GetNullAsyncWithDelay2>d__9`1'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [netstandard]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			.param [1]
				.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
					01 00 00 00 00
				)
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!T>::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!T>::SetStateMachine(class [netstandard]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<GetNullAsyncWithDelay2>d__9`1'::SetStateMachine

	} // end of class <GetNullAsyncWithDelay2>d__9`1


	// Fields
	.field private string '<InstanceProperty>k__BackingField'
	.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 02 00 00
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
	} // end of method ClassWithAsyncMethods::.ctor

	.method public hidebysig specialname 
		instance string get_InstanceProperty () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1

		IL_0000: ldarg.0
		IL_0001: ldfld string ClassWithAsyncMethods::'<InstanceProperty>k__BackingField'
		IL_0006: ret
	} // end of method ClassWithAsyncMethods::get_InstanceProperty

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<!!T> GetMaybeNullAsync<T> () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [netstandard]System.Type) = (
			01 00 2f 43 6c 61 73 73 57 69 74 68 41 73 79 6e
			63 4d 65 74 68 6f 64 73 2b 3c 47 65 74 4d 61 79
			62 65 4e 75 6c 6c 41 73 79 6e 63 3e 64 5f 5f 37
			60 31 00 00
		)
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!!T>
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Create()
		IL_0007: stfld valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!!T>::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!!T>::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!!T>::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Start<valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!!T>>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetMaybeNullAsync>d__7`1'<!!T>::'<>t__builder'
		IL_0029: call instance class [netstandard]System.Threading.Tasks.Task`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::get_Task()
		IL_002e: ret
	} // end of method ClassWithAsyncMethods::GetMaybeNullAsync

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<string> GetNonNullAsync () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [netstandard]System.Type) = (
			01 00 2b 43 6c 61 73 73 57 69 74 68 41 73 79 6e
			63 4d 65 74 68 6f 64 73 2b 3c 47 65 74 4e 6f 6e
			4e 75 6c 6c 41 73 79 6e 63 3e 64 5f 5f 34 00 00
		)
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype ClassWithAsyncMethods/'<GetNonNullAsync>d__4'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::Create()
		IL_0007: stfld valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::Start<valuetype ClassWithAsyncMethods/'<GetNonNullAsync>d__4'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNonNullAsync>d__4'::'<>t__builder'
		IL_0029: call instance class [netstandard]System.Threading.Tasks.Task`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::get_Task()
		IL_002e: ret
	} // end of method ClassWithAsyncMethods::GetNonNullAsync

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<!!T> GetNonNullAsyncWithDelay<T> () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [netstandard]System.Type) = (
			01 00 36 43 6c 61 73 73 57 69 74 68 41 73 79 6e
			63 4d 65 74 68 6f 64 73 2b 3c 47 65 74 4e 6f 6e
			4e 75 6c 6c 41 73 79 6e 63 57 69 74 68 44 65 6c
			61 79 3e 64 5f 5f 36 60 31 00 00
		)
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!!T>
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Create()
		IL_0007: stfld valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!!T>::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!!T>::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!!T>::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Start<valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!!T>>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNonNullAsyncWithDelay>d__6`1'<!!T>::'<>t__builder'
		IL_0029: call instance class [netstandard]System.Threading.Tasks.Task`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::get_Task()
		IL_002e: ret
	} // end of method ClassWithAsyncMethods::GetNonNullAsyncWithDelay

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<string> GetNonNullTask () cil managed 
	{
		// Header size: 12
		// Code size: 17 (0x11)
		.maxstack 2

		IL_0000: ldnull
		IL_0001: dup
		IL_0002: brtrue.s IL_0010

		IL_0004: pop
		IL_0005: ldstr "[NullGuard] Return value of method 'System.Threading.Tasks.Task`1<System.String> ClassWithAsyncMethods::GetNonNullTask()' is null."
		IL_000a: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
		IL_000f: throw

		IL_0010: ret
	} // end of method ClassWithAsyncMethods::GetNonNullTask

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<string> GetNullAsync () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [netstandard]System.Type) = (
			01 00 28 43 6c 61 73 73 57 69 74 68 41 73 79 6e
			63 4d 65 74 68 6f 64 73 2b 3c 47 65 74 4e 75 6c
			6c 41 73 79 6e 63 3e 64 5f 5f 35 00 00
		)
		.param [0]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
				01 00 02 00 00 00 01 02 00 00
			)
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype ClassWithAsyncMethods/'<GetNullAsync>d__5'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::Create()
		IL_0007: stfld valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::Start<valuetype ClassWithAsyncMethods/'<GetNullAsync>d__5'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string> ClassWithAsyncMethods/'<GetNullAsync>d__5'::'<>t__builder'
		IL_0029: call instance class [netstandard]System.Threading.Tasks.Task`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<string>::get_Task()
		IL_002e: ret
	} // end of method ClassWithAsyncMethods::GetNullAsync

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<!!T> GetNullAsyncWithDelay<class T> () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [netstandard]System.Type) = (
			01 00 33 43 6c 61 73 73 57 69 74 68 41 73 79 6e
			63 4d 65 74 68 6f 64 73 2b 3c 47 65 74 4e 75 6c
			6c 41 73 79 6e 63 57 69 74 68 44 65 6c 61 79 3e
			64 5f 5f 38 60 31 00 00
		)
		.param [0]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
				01 00 02 00 00 00 01 02 00 00
			)
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!!T>
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Create()
		IL_0007: stfld valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!!T>::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!!T>::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!!T>::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Start<valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!!T>>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay>d__8`1'<!!T>::'<>t__builder'
		IL_0029: call instance class [netstandard]System.Threading.Tasks.Task`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::get_Task()
		IL_002e: ret
	} // end of method ClassWithAsyncMethods::GetNullAsyncWithDelay

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<!!T> GetNullAsyncWithDelay2<class T> () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [netstandard]System.Type) = (
			01 00 34 43 6c 61 73 73 57 69 74 68 41 73 79 6e
			63 4d 65 74 68 6f 64 73 2b 3c 47 65 74 4e 75 6c
			6c 41 73 79 6e 63 57 69 74 68 44 65 6c 61 79 32
			3e 64 5f 5f 39 60 31 00 00
		)
		.param type T
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 02 00 00
			)
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!!T>
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Create()
		IL_0007: stfld valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!!T>::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!!T>::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!!T>::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::Start<valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!!T>>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype ClassWithAsyncMethods/'<GetNullAsyncWithDelay2>d__9`1'<!!T>::'<>t__builder'
		IL_0029: call instance class [netstandard]System.Threading.Tasks.Task`1<!0> valuetype [netstandard]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!!T>::get_Task()
		IL_002e: ret
	} // end of method ClassWithAsyncMethods::GetNullAsyncWithDelay2

	.method public hidebysig static 
		class [netstandard]System.Threading.Tasks.Task`1<string> GetNullTask () cil managed 
	{
		.param [0]
			.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = (
				01 00 02 00 00 00 02 01 00 00
			)
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 1

		IL_0000: ldnull
		IL_0001: ret
	} // end of method ClassWithAsyncMethods::GetNullTask

	.method public hidebysig specialname 
		instance void set_InstanceProperty (
			string 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld string ClassWithAsyncMethods::'<InstanceProperty>k__BackingField'
		IL_0007: ret
	} // end of method ClassWithAsyncMethods::set_InstanceProperty

	// Properties
	.property instance string InstanceProperty()
	{
		.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 02 00 00
		)
		.get instance string ClassWithAsyncMethods::get_InstanceProperty()
		.set instance void ClassWithAsyncMethods::set_InstanceProperty(string)
	}

} // end of class ClassWithAsyncMethods
