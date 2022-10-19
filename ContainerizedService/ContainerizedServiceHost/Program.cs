using System;
using System.ServiceModel;
using ContainerizedService;
using System.ServiceModel.Description;
using System.Threading;

namespace ContainerizedServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var uriStr = @"http://localhost:8633/ContainerizedService/SampleService/";
                var uri = new Uri(uriStr);
                using (var host = new ServiceHost(typeof(SampleService), uri))
                {
                    var metadata = new ServiceMetadataBehavior();

                    metadata.HttpGetEnabled = true;
                    metadata.HttpsGetEnabled = true;

                    host.Description.Behaviors.Add(metadata);

                    var httpBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                    host.AddServiceEndpoint(typeof(ISampleService), httpBinding, string.Empty);

                    var mexHttpBinding = MetadataExchangeBindings.CreateMexHttpBinding();
                    host.AddServiceEndpoint(typeof(IMetadataExchange), mexHttpBinding, "mex");

                    host.Open();

                    Console.WriteLine("[{0}] hosted", uriStr);
                    
                    while(true)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                    Console.WriteLine(e.InnerException.StackTrace);
                }
            }            
        }
    }
}
