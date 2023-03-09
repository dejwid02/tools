using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MovieParser.DAL;

namespace ParserRunner
{
    public class Main
    {
        private readonly IMoviesRepository _repository;

        public Main(IMoviesRepository repository)
        {
            _repository = repository;
        }
        [FunctionName("Main")]
        public void Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _repository.GetAllMovies()
        }
    }
}
