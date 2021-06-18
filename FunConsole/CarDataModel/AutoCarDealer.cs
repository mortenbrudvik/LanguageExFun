using System.Linq;
using System.Threading.Tasks;
using FunConsole.Fun;
using LanguageExt;

namespace FunConsole.CarDataModel
{
    public class AutoCarDealer : ICarDealer
    {
        private Seq<Car> _cars = new Seq<Car>(new[]
        {
            new Car(Brand.Fiat, Color.Red, 2021, 2000),
            new Car(Brand.Ford, Color.Yellow, 2021, 9000),
            new Car(Brand.Toyota, Color.Blue, 2021, 6000)
        }); 
        
        public Task<Seq<Car>> Brands(int year)
        {
            return TaskUtils.Async(_cars.Where(car => car.Year == 2021));
        }

        public Task<Car> GetCheapest(int year)
        {
            return TaskUtils.Async(_cars.Where(car => car.Year == 2021)
                .OrderBy(car=>car.Price).First());
        }
    }
}