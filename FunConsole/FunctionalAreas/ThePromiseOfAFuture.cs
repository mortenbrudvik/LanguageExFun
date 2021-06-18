using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FunConsole.CarDataModel;
using FunConsole.Extensions;
using LanguageExt;
using static System.Console;

using static FunConsole.Fun.TaskUtils;

namespace FunConsole.FunctionalAreas
{
    public static class ThePromiseOfAFuture
    {
        public static async Task Run()
        {
            // Using tasks with map
            var processItems = await Task.Run(() => Process.GetProcesses().ToSeq())
                .Map(processes =>
                    processes.Map(process => process.ProcessName)
                        .Map(name => new ProcessItem(name)));
            
            // Using apply to run tasks in parallel
            Func<Car, Car, Car> PickCheaper
                = (l, r) => l.Price < r.Price ? l : r;
            var mobileCarDealer = new MobileCarDealer();
            var autoCarDealer = new AutoCarDealer();
            
            Task<Car> BestPrice(int year)
                => Async(PickCheaper)
                    .Apply(mobileCarDealer.GetCheapest(year))
                    .Apply(autoCarDealer.GetCheapest(year));
            
            await Out.WriteLineAsync("Cheapest car: " + await BestPrice(2021));


        }
        
    }
}