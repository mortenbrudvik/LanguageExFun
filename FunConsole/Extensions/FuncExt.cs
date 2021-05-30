using System;

namespace FunConsole.Extensions
{
    public static class FuncExt
    {
        public static Func<R> Map<T, R>
            (this Func<T> f, Func<T, R> g)
            => () => g(f());

        public static Func<R> Bind<T, R>
            (this Func<T> f, Func<T, Func<R>> g)
            => () => g(f())();

    }
}