using System;
using System.Threading.Tasks;
using Flurl.Http;
using LanguageExt;
using Newtonsoft.Json.Linq;
using static System.Console;
using static LanguageExt.Prelude;

namespace FunConsole.FunctionalAreas
{
    public static class ExceptionHandling
    {
        public static async Task Run()
        {
            // Exception handling
            // The Try monad captures exceptions and uses them to cancel the computation.
            // Primarily useful for expression based processing of errors.
            // "https://api.chucknorris.io/jokes/random"
            Func<string, TryAsync<Uri>> createUri = url => TryAsync(Task.Run(() => new Uri(url)));
            Func<Uri, TryAsync<string>> getJsonFromUri = uri => TryAsync(async () => await uri.GetStringAsync());
            Func<string,string, TryAsync<string>> getJsonValue = (json, valueName) => TryAsync(Task.Run(() => "" + JObject.Parse(json)[valueName]));

            await createUri("https://api.chucknorris.io/jokes/random")
                .Bind(x => getJsonFromUri(x))
                .Bind(y => getJsonValue(y, "value"))
                .Match(
                    joke => WriteLine("Chuck Norris says: " + joke),
                    ex => WriteLine("Error: " + ex.Message));
        }
    }
}