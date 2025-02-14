using System.Net.Http;
using System;

namespace OrsaAkademi.demo.WebApp.Helpers
{
    public class HttpclientOlusturucu
    {
        public HttpClient httpolusturucu(HttpClient httpClient)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, certChain, policyErrors) => true;
            httpClient = new HttpClient(handler);
            httpClient.BaseAddress = new Uri("https://localhost:44316/");
            return httpClient;
        }



    }
}
