.class public auto ansi beforefieldinit SpecialClass
	extends [mscorlib]System.Object
{
	// Nested Types
	.class nested private auto ansi sealed beforefieldinit '<CountTo>d__0'
		extends [mscorlib]System.Object
		implements class [mscorlib]System.Collections.Generic.IEnumerable`1<int32>,
		           class [mscorlib]System.Collections.Generic.IEnumerator`1<int32>,
		           [mscorlib]System.Collections.IEnumerable,
		           [mscorlib]System.Collections.IEnumerator,
		           [mscorlib]System.IDisposable
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field private int32 '<>1__state'
		.field private int32 '<>2__current'
		.field public int32 '<>3__end'
		.field private int32 '<>l__initialThreadId'
		.field private int32 '<i>5__2'
		.field private int32 end

		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				int32 '<>1__state'
			) cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 1
			// Code size: 25 (0x19)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [mscorlib]System.Object::.ctor()
			IL_0006: ldarg.0
			IL_0007: ldarg.1
			IL_0008: stfld int32 SpecialClass/'<CountTo>d__0'::'<>1__state'
			IL_000d: ldarg.0
			IL_000e: call int32 [mscorlib]System.Environment::get_CurrentManagedThreadId()
			IL_0013: stfld int32 SpecialClass/'<CountTo>d__0'::'<>l__initialThreadId'
			IL_0018: ret
		} // end of method '<CountTo>d__0'::.ctor

		.method private final hidebysig newslot virtual 
			instance bool MoveNext () cil managed 
		{
			.override method instance bool [mscorlib]System.Collections.IEnumerator::MoveNext()
			// Header size: 12
			// Code size: 92 (0x5c)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] int32
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 SpecialClass/'<CountTo>d__0'::'<>1__state'
			IL_0006: stloc.0
			IL_0007: ldloc.0
			IL_0008: brfalse.s IL_0010

			IL_000a: ldloc.0
			IL_000b: ldc.i4.1
			IL_000c: beq.s IL_0035

			IL_000e: ldc.i4.0
			IL_000f: ret

			IL_0010: ldarg.0
			IL_0011: ldc.i4.m1
			IL_0012: stfld int32 SpecialClass/'<CountTo>d__0'::'<>1__state'
			IL_0017: ldarg.0
			IL_0018: ldc.i4.0
			IL_0019: stfld int32 SpecialClass/'<CountTo>d__0'::'<i>5__2'
			IL_001e: br.s IL_004c

			IL_0020: ldarg.0
			IL_0021: ldarg.0
			IL_0022: ldfld int32 SpecialClass/'<CountTo>d__0'::'<i>5__2'
			IL_0027: stfld int32 SpecialClass/'<CountTo>d__0'::'<>2__current'
			IL_002c: ldarg.0
			IL_002d: ldc.i4.1
			IL_002e: stfld int32 SpecialClass/'<CountTo>d__0'::'<>1__state'
			IL_0033: ldc.i4.1
			IL_0034: ret

			IL_0035: ldarg.0
			IL_0036: ldc.i4.m1
			IL_0037: stfld int32 SpecialClass/'<CountTo>d__0'::'<>1__state'
			IL_003c: ldarg.0
			IL_003d: ldfld int32 SpecialClass/'<CountTo>d__0'::'<i>5__2'
			IL_0042: stloc.1
			IL_0043: ldarg.0
			IL_0044: ldloc.1
			IL_0045: ldc.i4.1
			IL_0046: add
			IL_0047: stfld int32 SpecialClass/'<CountTo>d__0'::'<i>5__2'

			IL_004c: ldarg.0
			IL_004d: ldfld int32 SpecialClass/'<CountTo>d__0'::'<i>5__2'
			IL_0052: ldarg.0
			IL_0053: ldfld int32 SpecialClass/'<CountTo>d__0'::end
			IL_0058: blt.s IL_0020

			IL_005a: ldc.i4.0
			IL_005b: ret
		} // end of method '<CountTo>d__0'::MoveNext

		.method private final hidebysig newslot virtual 
			instance class [mscorlib]System.Collections.Generic.IEnumerator`1<int32> 'System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator' () cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance class [mscorlib]System.Collections.Generic.IEnumerator`1<!0> class [mscorlib]System.Collections.Generic.IEnumerable`1<int32>::GetEnumerator()
			// Header size: 12
			// Code size: 55 (0x37)
			.maxstack 2
			.locals init (
				[0] class SpecialClass/'<CountTo>d__0'
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 SpecialClass/'<CountTo>d__0'::'<>1__state'
			IL_0006: ldc.i4.s -2
			IL_0008: bne.un.s IL_0022

			IL_000a: ldarg.0
			IL_000b: ldfld int32 SpecialClass/'<CountTo>d__0'::'<>l__initialThreadId'
			IL_0010: call int32 [mscorlib]System.Environment::get_CurrentManagedThreadId()
			IL_0015: bne.un.s IL_0022

			IL_0017: ldarg.0
			IL_0018: ldc.i4.0
			IL_0019: stfld int32 SpecialClass/'<CountTo>d__0'::'<>1__state'
			IL_001e: ldarg.0
			IL_001f: stloc.0
			IL_0020: br.s IL_0029

			IL_0022: ldc.i4.0
			IL_0023: newobj instance void SpecialClass/'<CountTo>d__0'::.ctor(int32)
			IL_0028: stloc.0

			IL_0029: ldloc.0
			IL_002a: ldarg.0
			IL_002b: ldfld int32 SpecialClass/'<CountTo>d__0'::'<>3__end'
			IL_0030: stfld int32 SpecialClass/'<CountTo>d__0'::end
			IL_0035: ldloc.0
			IL_0036: ret
		} // end of method '<CountTo>d__0'::'System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator'

		.method private final hidebysig specialname newslot virtual 
			instance int32 'System.Collections.Generic.IEnumerator<System.Int32>.get_Current' () cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance !0 class [mscorlib]System.Collections.Generic.IEnumerator`1<int32>::get_Current()
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld int32 SpecialClass/'<CountTo>d__0'::'<>2__current'
			IL_0006: ret
		} // end of method '<CountTo>d__0'::'System.Collections.Generic.IEnumerator<System.Int32>.get_Current'

		.method private final hidebysig newslot virtual 
			instance class [mscorlib]System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance class [mscorlib]System.Collections.IEnumerator [mscorlib]System.Collections.IEnumerable::GetEnumerator()
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance class [mscorlib]System.Collections.Generic.IEnumerator`1<int32> SpecialClass/'<CountTo>d__0'::'System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator'()
			IL_0006: ret
		} // end of method '<CountTo>d__0'::System.Collections.IEnumerable.GetEnumerator

		.method private final hidebysig specialname newslot virtual 
			instance object System.Collections.IEnumerator.get_Current () cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance object [mscorlib]System.Collections.IEnumerator::get_Current()
			// Header size: 1
			// Code size: 12 (0xc)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld int32 SpecialClass/'<CountTo>d__0'::'<>2__current'
			IL_0006: box [mscorlib]System.Int32
			IL_000b: ret
		} // end of method '<CountTo>d__0'::System.Collections.IEnumerator.get_Current

		.method private final hidebysig newslot virtual 
			instance void System.Collections.IEnumerator.Reset () cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [mscorlib]System.Collections.IEnumerator::Reset()
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: newobj instance void [mscorlib]System.NotSupportedException::.ctor()
			IL_0005: throw
		} // end of method '<CountTo>d__0'::System.Collections.IEnumerator.Reset

		.method private final hidebysig newslot virtual 
			instance void System.IDisposable.Dispose () cil managed 
		{
			.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [mscorlib]System.IDisposable::Dispose()
			// Header size: 1
			// Code size: 1 (0x1)
			.maxstack 8

			IL_0000: ret
		} // end of method '<CountTo>d__0'::System.IDisposable.Dispose

		// Properties
		.property instance int32 'System.Collections.Generic.IEnumerator<System.Int32>.Current'()
		{
			.get instance int32 SpecialClass/'<CountTo>d__0'::'System.Collections.Generic.IEnumerator<System.Int32>.get_Current'()
		}
		.property instance object System.Collections.IEnumerator.Current()
		{
			.get instance object SpecialClass/'<CountTo>d__0'::System.Collections.IEnumerator.get_Current()
		}

	} // end of class <CountTo>d__0


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
	} // end of method SpecialClass::.ctor

	.method public hidebysig 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<int32> CountTo (
			int32 end
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.IteratorStateMachineAttribute::.ctor(class [mscorlib]System.Type) = (
			01 00 1a 53 70 65 63 69 61 6c 43 6c 61 73 73 2b
			3c 43 6f 75 6e 74 54 6f 3e 64 5f 5f 30 00 00
		)
		// Header size: 12
		// Code size: 15 (0xf)
		.maxstack 3

		IL_0000: ldc.i4.s -2
		IL_0002: newobj instance void SpecialClass/'<CountTo>d__0'::.ctor(int32)
		IL_0007: dup
		IL_0008: ldarg.1
		IL_0009: stfld int32 SpecialClass/'<CountTo>d__0'::'<>3__end'
		IL_000e: ret
	} // end of method SpecialClass::CountTo

} // end of class SpecialClass
