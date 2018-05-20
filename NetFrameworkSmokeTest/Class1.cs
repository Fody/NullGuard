namespace NetFrameworkSmokeTest
{
    using NullGuard;

    class Class1
    {
        public Class1(string something)
        {

        }

        public string Test([AllowNull] string canbenull)
        {
            return default(string);
        }

        public string Test2(string notnull)
        {
            return default(string);
        }
    }
}
