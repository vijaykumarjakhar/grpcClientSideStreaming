using Grpc.Net.Client;
using GrpcServerService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace grpcClientStreaming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new AverageNumber.AverageNumberClient(channel);
            var reply =  client.GetAverageNumber();
            Console.WriteLine("read nummber");
            foreach (int item in Enumerable.Range(20,30))
            {
                await reply.RequestStream.WriteAsync(new AverageNumberRequest() { Number=item});
            }
            await reply.RequestStream.CompleteAsync();
            var response = await reply.ResponseAsync;
            Console.WriteLine("Avarage: " + response.Result);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
