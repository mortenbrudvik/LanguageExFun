using System;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace FunConsole.Extensions
{
    public static class TaskExt
    {
        public static async Task<TR> Apply<T, TR>(this Task<Func<T, TR>> f, Task<T> arg)
            => (await f)(await arg);
        public static Task<Func<T2, TR>> Apply<T1, T2, TR>(this Task<Func<T1, T2, TR>> f, Task<T1> arg)
            => Apply(f.Map(curry), arg);
    }
}