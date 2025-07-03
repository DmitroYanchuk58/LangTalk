using System.Net.Http.Headers;

namespace LangTalkMobileApp.HttpCommands
{
    public static class HttpCommand
    {
        private static readonly HttpClient client2 = new HttpClient();

        public static async Task<HttpResponseMessage> SendGetHttpRequest(string command, string token)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            HttpClient client = new HttpClient(handler);

            var url = "http://10.0.2.2:5254" + command;

            client.DefaultRequestHeaders.Authorization = null;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim());
            }
            var result = await client.SendAsync(request);
            return result;
        }

        public static async Task<HttpResponseMessage> SendPostHttpRequest(string command, HttpContent? content = null)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            HttpClient client = new HttpClient(handler);

            var url = "http://10.0.2.2:5254" + command;

            client.DefaultRequestHeaders.Authorization = null;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = content;

            var result = await client.SendAsync(request);
            return result;
        }

    }
}
