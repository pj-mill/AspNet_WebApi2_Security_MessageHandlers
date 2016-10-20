using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static readonly string baseAddress = "http://localhost:56805";
        private static readonly string apiKey = "56894sad874as54d78A4DFASD5F7SD4FSDF";

        static void Main(string[] args)
        {
            Run().Wait();
        }

        private static async Task Run()
        {
            // Simply prints a title to screen
            Action<string> PrintTitle = (title) =>
            {
                Console.WriteLine(new String('=', 60));
                Console.WriteLine(title);
                Console.WriteLine(new String('=', 60));
            };

            // Prints some test and waits for the user to press the enter key
            Action<string> WaitForEnterKey = (title) =>
            {
                Console.WriteLine(title);
                while (Console.ReadKey(intercept: true).Key != ConsoleKey.Enter) ; // Wait for enter key to be pressed
            };


            // Allow time for the web api to run
            WaitForEnterKey("Wait for Api to run and then press ENTER to start . . .");

            // ----------------- Configure Http Client
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // ----------------- Credentials Only (Both will fail)
            PrintTitle("Sending request with no credentials and no api key");
            await SendRequest(client, "api/test/testpoint", null);

            PrintTitle("Sending request with good credentials and no api key");
            await SendRequest(client, "api/test/testpoint", CreateBasicCredentials("SuperAdmin", "P@assword!"));


            // ----------------- Credentials and Api Key (The last one will pass)
            PrintTitle("Sending request with good credentials and bad api key");
            await SendRequest(client, "api/test/testpoint", CreateBasicCredentials("SuperAdmin", "P@assword!"), "asdasdasdasdasd");

            PrintTitle("Sending request with good credentials and good api key: TO AUTHORIZED POINT");
            await SendRequest(client, "api/test/testauthpoint", CreateBasicCredentials("SuperAdmin", "P@assword!"), apiKey);


            // ----------------- Terminate
            WaitForEnterKey("Press ENTER to exit . . .");

            client.Dispose();
        }

        /// <summary>
        /// Sends a request using credentials only (this will fail because this example requires that we send an api key also)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path"></param>
        /// <param name="authHeader"></param>
        /// <returns></returns>
        private static async Task SendRequest(HttpClient client, string path, AuthenticationHeaderValue authHeader)
        {
            var fullPath = client.BaseAddress + path;
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullPath))
            {
                request.Headers.Authorization = authHeader;     // Add Authorization Header

                Console.WriteLine(request.RequestUri);
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    Console.WriteLine($"{(int)response.StatusCode} {response.ReasonPhrase}");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine("\n");
                        return;
                    }

                    Console.WriteLine();

                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                }
                Console.WriteLine("\n");
            };
        }

        /// <summary>
        /// Sends a request using credentials and an api key
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path"></param>
        /// <param name="authHeader"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        private static async Task SendRequest(HttpClient client, string path, AuthenticationHeaderValue authHeader, string apiKey)
        {
            var fullPath = client.BaseAddress + path;
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullPath))
            {
                request.Headers.Authorization = authHeader;     // Add Authorization Header
                request.Headers.Add("x-api-key", apiKey);       // Add Api Key Header

                Console.WriteLine(request.RequestUri);
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    Console.WriteLine($"{(int)response.StatusCode} {response.ReasonPhrase}");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine("\n");
                        return;
                    }

                    Console.WriteLine();

                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                }
                Console.WriteLine("\n");
            };
        }

        /// <summary>
        /// Creates basic credentials for request authorization header
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static AuthenticationHeaderValue CreateBasicCredentials(string userName, string password)
        {
            var plainCredentials = userName + ":" + password;
            Encoding encoder = Encoding.GetEncoding("iso-8859-1");
            byte[] byteCredential = encoder.GetBytes(plainCredentials);
            string headerValue = Convert.ToBase64String(byteCredential);
            return new AuthenticationHeaderValue("Basic", headerValue);
        }
    }
}
