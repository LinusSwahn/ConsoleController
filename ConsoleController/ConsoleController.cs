using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConsoleController.Parsing;
using Microsoft.Extensions.Logging;

namespace ConsoleController
{
    public class ConsoleController : IConsoleController
    {
        private ICommandParser _parser;
        private ILogger<ConsoleController> _logger;

        public ConsoleController(ICommandParser parser, ILogger<ConsoleController> logger)
        {
            _parser = parser;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken token)
        {   
            return Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        var command = Console.ReadLine();
                        var response = _parser.ParseCommand(command);
                        Console.WriteLine(response);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, e.Message);
                    }
                }
            }, token);
        }
    }
}