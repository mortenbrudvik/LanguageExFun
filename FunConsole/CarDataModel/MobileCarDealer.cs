using System.Linq;
using System.Threading.Tasks;
using LanguageExt;

using static FunConsole.Fun.TaskUtils;

namespace FunConsole.CarDataModel
{
    public class MobileCarDealer : ICarDealer
    {
        private Seq<Car> _cars = new Seq<Car>(new[]
        {
            new Car(Brand.Fiat, Color.Blue, 2021, 5000),
            new Car(Brand.Volvo, Color.Blue, 2021, 4000),
            new Car(Brand.Toyota, Color.Red, 2021, 7000)
        }); 
        
        public Task<Seq<Car>> Brands(int year)
        {
            return Async(_cars.Where(car => car.Year == 2021));
        }

        public Task<Car> Cheapest(int year)
        {
            return Async(_cars.Where(car => car.Year == 2021)
                .OrderBy(car=>car.Price).First());
        }
    }
}