using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace socialbrothers_quotes_api.Util {
    public class NetworkManager<T> {
        private readonly HttpClient _client = new HttpClient();

        public NetworkManager() {
            _client.BaseAddress = new Uri("");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync(string path) {
            var result = (T) Convert.ChangeType(null, typeof(T));
            var response = await _client.GetAsync(path);

            if (!response.IsSuccessStatusCode) return result;
            result = await response.Content.ReadAsAsync<T>();
            return result;
        }

        public async Task<T> PostAsync(string path, HttpContent content) {
            var result = (T) Convert.ChangeType(null, typeof(T));
            var response = await _client.PostAsync(path, content);

            if (!response.IsSuccessStatusCode) return result;
            result = await response.Content.ReadAsAsync<T>();
            return result;
        }

        public async Task<bool> DeleteAsync(string path) {
            var response = await _client.DeleteAsync(path);
            return response.IsSuccessStatusCode;
        }
    }
}