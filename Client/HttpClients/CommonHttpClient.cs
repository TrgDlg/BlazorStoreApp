using System.Net.Http.Json;
using System.Text.Json;
using StoreBlazor.Client.Exceptions;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using UnauthorizedException = StoreBlazor.Client.Exceptions.UnauthorizedException;

namespace StoreBlazor.Client.HttpClients
{
    public class CommonHttpClient
    {
        private readonly HttpClient _httpClient;

        public CommonHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public event Action? Logout;

        private void EnsureSuccessStatusCode(string uri, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NetworkAuthenticationRequired:
                case System.Net.HttpStatusCode.NotFound:
                    throw new NotFoundException();
                case System.Net.HttpStatusCode.Unauthorized:
                    Logout?.Invoke();
                    throw new UnauthorizedException();
            }
        }

        public async Task<T> Get<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            EnsureSuccessStatusCode(uri, response);

            var responseMessage = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (typeof(T) == typeof(string)) return (T)(object)responseMessage;

            return string.IsNullOrEmpty(responseMessage) ? default : JsonSerializer.Deserialize<T>(responseMessage, options);
        }

        /// <summary>
        /// Post object as content and query result
        /// </summary>
        /// <param name="uri">Target uri</param>
        /// <param name="content">Object will be serialized to JSON and used as content</param>
        /// <returns>Posting result</returns>
        public async Task<T> Post<T>(string uri, object content)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, content);
            EnsureSuccessStatusCode(uri, response);

            var responseMessage = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = JsonSerializer.Deserialize<T>(responseMessage, options);

            return result;
        }

        /// <summary>
        /// Post object as content
        /// </summary>
        /// <param name="uri">Target uri</param>
        /// <param name="content">Object will be serialized to JSON and used as content</param>
        /// <param name="throwIfUnauthorized">Throw Unauthorized if server returned 401 or 403</param>
        public async Task Post(string uri, object content, bool throwIfUnauthorized = false)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, content);
            EnsureSuccessStatusCode(uri, response);
        }

        /// <summary>
        /// Post form as content
        /// </summary>
        /// <param name="uri">Target uri</param>
        /// <param name="content">Form content</param>
        /// <param name="throwIfUnauthorized">Throw Unauthorized if server returned 401 or 403</param>
        public async Task Post(string uri, MultipartFormDataContent content, bool throwIfUnauthorized = false)
        {
            var response = await _httpClient.PostAsync(uri, content);
            EnsureSuccessStatusCode(uri, response);
        }

        /// <summary>
        /// Post without content
        /// </summary>
        /// <param name="uri">Target uri</param>
        /// <param name="throwIfUnauthorized">Throw Unauthorized if server returned 401 or 403</param>
        public async Task Post(string uri, bool throwIfUnauthorized = false)
        {
            var response = await _httpClient.PostAsync(uri, null);
            EnsureSuccessStatusCode(uri, response);
        }

        /// <summary>
        /// Put object as content
        /// </summary>
        /// <param name="uri">Target uri</param>
        /// <param name="content">Object will be serialized to JSON and used as content</param>
        /// <param name="throwIfUnauthorized">Throw Unauthorized if server returned 401 or 403</param>
        public async Task Put(string uri, object content, bool throwIfUnauthorized = false)
        {
            var response = await _httpClient.PutAsJsonAsync(uri, content);
            EnsureSuccessStatusCode(uri, response);
        }

        public async IAsyncEnumerable<T> GetStream<T>(string uri, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            request.SetBrowserResponseStreamingEnabled(true);

            using var response =
                await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

            await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<T>(
                               responseStream,
                               new JsonSerializerOptions
                               {
                                   PropertyNameCaseInsensitive = true,
                                   DefaultBufferSize = 128
                               }, cancellationToken))
            {
                yield return item;
            }
        }
    }
}