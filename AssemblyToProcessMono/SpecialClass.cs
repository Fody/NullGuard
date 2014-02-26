using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SpecialClass
{
#pragma warning disable 1998

    public async Task<List<int>> Issue30Exception()
    {
        throw new Exception("");
    }

#pragma warning restore 1998
}