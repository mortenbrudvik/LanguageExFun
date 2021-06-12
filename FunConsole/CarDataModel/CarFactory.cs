using System.Collections.Generic;
using System.Linq;
using LanguageExt;

using static LanguageExt.Prelude;

namespace FunConsole.CarDataModel
{
    internal class CarFactory
    {
        private readonly Brand _brand;
        private readonly List<Car> _cars = new();

        public CarFactory(Brand brand) => _brand = brand;
        public IReadOnlyCollection<Car> Cars => _cars;

        public void ProduceCars(int number, Color withColor) =>
            _cars.AddRange(Enumerable.Repeat(_brand, number)
                .Select(brand => new Car(brand, withColor, 2021, 3000)));
        public Option<Car> BuyCar(Color withColor) =>
            _cars.Find(x => x.Color == withColor) switch
            {
                { } car => _cars.Remove(car) ? car : None,
                _ => None
            };
    }
}