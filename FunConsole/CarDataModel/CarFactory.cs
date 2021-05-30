using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace FunConsole.CarDataModel
{
    internal class CarFactory
    {
        private readonly string _brand;
        private readonly List<Car> _cars = new();

        public CarFactory(string brand) => _brand = brand;
        public IReadOnlyCollection<Car> Cars => _cars;

        public void ProduceCars(int number, string withColor) =>
            _cars.AddRange(Enumerable.Repeat(_brand, number)
                .Select(brand => new Car(brand, withColor)));
        public Option<Car> BuyCar(string withColor) =>
            _cars.Find(x => x.Color == withColor) switch
            {
                { } car => _cars.Remove(car) ? car : Prelude.None,
                _ => Prelude.None
            };
    }
}