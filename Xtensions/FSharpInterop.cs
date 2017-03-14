using System;
using System.Collections.Generic;
using Microsoft.FSharp.Collections;

public static class FSharpInterop
{
    public static FSharpList<T> ToFSharpList<T>(this IEnumerable<T> lst)
    {
        return ListModule.OfSeq<T>(lst);
    }

    public static IEnumerable<T> ToEnumerable<T>(this FSharpList<T> fList)
    {
        return SeqModule.OfList<T>(fList);
    }
}