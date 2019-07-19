using System.Threading;
using System.Threading.Tasks;

namespace ConsoleController
{
    public interface IConsoleController
    {
        Task StartAsync(CancellationToken token);
    }
}