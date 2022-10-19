using System;
using Grpc.Net.Client;
using GrpcPersonDetailsClient;

namespace NetCoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var client = new PersonDetails.PersonDetailsClient(channel);
            var reply = client.GetPerson(new PersonRequest()
            {
                PersonId = "Bidyut"
            });
            Console.WriteLine("Reply from gRPC service: {0}, {1} from {2}", reply.Id, reply.Name, reply.Address.City);
            Console.ReadLine();
        }
    }
}
