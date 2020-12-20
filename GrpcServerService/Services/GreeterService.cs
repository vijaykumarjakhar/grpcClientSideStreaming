using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServerService
{
    public class GreeterService : AverageNumber.AverageNumberBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }
        public async override Task<AverageNumberResponse> GetAverageNumber(IAsyncStreamReader<AverageNumberRequest> requestStream, ServerCallContext context)
        {
            List<int> holdNumber = new List<int>();
          
            while (await requestStream.MoveNext())
            {
                
                Console.WriteLine("Number found {0}", requestStream.Current.Number);
                holdNumber.Add(requestStream.Current.Number);
            }
            Console.WriteLine(holdNumber.Average().ToString());
            return  new AverageNumberResponse() { Result = holdNumber.Average().ToString()};
        }

    }
}
