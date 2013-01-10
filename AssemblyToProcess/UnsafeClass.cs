
public class UnsafeClass
{
	unsafe public int* MethodWithAmp(int* instance)
	{
		return default(int*);
	}
	unsafe public int* NullProperty { get; set; }
}