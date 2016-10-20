using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebService.Authenticators;

namespace WebService.MessageHandlers
{
    public class CheckCredentialsMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var authenticator = new BasicAuthenticator();

            var principal = await authenticator.AuthenticateAsync(request, ct);

            if (principal == null)
            {
                return await UnauthorizedResult();
            }

            // Set principal
            request.GetRequestContext().Principal = principal;

            // Ok, move on to the next handler
            return await base.SendAsync(request, ct);
        }

        private Task<HttpResponseMessage> UnauthorizedResult()
        {
            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("You are not authorized"),
                ReasonPhrase = "Invalid Authorization Credentials"
            };

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(res);
            return tcs.Task;
        }
    }
}