using System.Threading.Tasks;
using LanguageExt;

namespace FunConsole.CarDataModel
{
    interface ICarDealer
    {
        Task<Seq<Car>> Brands(int year);
        Task<Car> GetCheapest(int year);
    }
}