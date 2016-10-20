using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebService.MessageHandlers
{
    public class CheckApiKeyMessageHandler : DelegatingHandler
    {
        private readonly string apiKey = "56894sad874as54d78A4DFASD5F7SD4FSDF";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            bool keyFound = false;
            var requestHeaders = request.Headers;

            IEnumerable<string> authTokenContainer = new List<string>();
            if (!requestHeaders.TryGetValues("x-api-key", out authTokenContainer))
            {
                return await ForbiddinResult();
            }

            // Check key
            foreach (var key in authTokenContainer)
            {
                if (apiKey.Equals(key))
                {
                    keyFound = true;
                    break;
                }
            }

            // No correct key found
            if (!keyFound)
            {
                return await ForbiddinResult();
            }

            // Ok, move on down the pipeline
            return await base.SendAsync(request, ct);
        }

        private Task<HttpResponseMessage> ForbiddinResult()
        {
            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent("You are forbidden from accessing resources"),
                ReasonPhrase = "Invalid Api Key"
            };

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(res);
            return tcs.Task;
        }
    }
}