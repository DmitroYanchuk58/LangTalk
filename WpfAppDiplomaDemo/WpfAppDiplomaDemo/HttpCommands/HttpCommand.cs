using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppDiplomaDemo.HttpCommands
{
    public static class HttpCommand
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<HttpResponseMessage> SendGetHttpRequest(string command, string token)
        {
            var url = "https://localhost:7067" + command;

            client.DefaultRequestHeaders.Authorization = null;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim());
            }
            var result = await client.SendAsync(request).ConfigureAwait(false);
            return result;
        }

        public static async Task<HttpResponseMessage> SendPostHttpRequest(string command, HttpContent? content = null)
        {
            var url = "https://localhost:7067" + command;

            client.DefaultRequestHeaders.Authorization = null;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = content;

            var result = await client.SendAsync(request);
            return result;
        }

    }
}
