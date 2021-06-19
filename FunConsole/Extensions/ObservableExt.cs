using System;

using static System.Console;

namespace FunConsole.Extensions
{
    public static class ObservableExt
    {
        public static IDisposable Trace<T>
            (this IObservable<T> source, string name)
            => source.Subscribe(
                onNext: t => WriteLine($"{name} -> {t}"),
                onError: ex => WriteLine($"{name} ERROR: {ex.Message}"),
                onCompleted: () => WriteLine($"{name} END"));

        
    }
}