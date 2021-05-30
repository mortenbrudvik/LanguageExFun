using System;
using LanguageExt;

namespace FunConsole.Extensions
{
    public static class OptionExt
    {
        public static Option<T> OrElse<T>(this Option<T> left, Option<T> right)
            => left.Match( 
                () => right,
                _ => left);

        public static Option<T> OrElse<T>(this Option<T> left, Func<Option<T>> right)
            => left.Match(
                right, 
                _ => left);

        public static T GetOrElse<T>(this Option<T> opt, T defaultValue)
            => opt.Match(
                x => x,
                defaultValue);
        
        public static T GetOrElse<T>(this Option<T> opt, Func<T> fallback)
            => opt.Match(
                x => x,
                fallback());
    }
}