using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebService.Helpers;

namespace WebService.MessageHandlers
{
    public class CheckCredentialsMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            // Get request authorization header
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // Invalidate if no authorization header available
            if (authorization == null)
            {
                return await UnauthorizedResult();
            }

            var authParm = authorization.Parameter;

            // Invalidate if no authorization header parameter available
            if (string.IsNullOrWhiteSpace(authParm))
            {
                return await UnauthorizedResult();
            }

            // Get username and password
            Tuple<string, string> credentials = UsernameAndPasswordHelper.Extract(authParm);

            // Proper authentication would be required here in a real world scenario

            // Invalidate if authorization credentials are incorrect
            if (!credentials.Item1.Equals("SuperAdmin") && !credentials.Item2.Equals("P@assword!"))
            {
                return await UnauthorizedResult();
            }

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