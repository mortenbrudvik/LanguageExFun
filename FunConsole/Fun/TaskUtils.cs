using System.Threading.Tasks;

namespace FunConsole.Fun
{
    public static class TaskUtils
    {
        public static Task<T> Async<T>(T t)
            => Task.FromResult(t);
    }
}