using LanguageExt;
using static LanguageExt.Prelude;
namespace FunConsole.Utils
{
    public static class Double
    {
        public static Option<double> Parse(string number) =>
            Try(() => double.Parse(number)).ToOption();
    }
}