
using Microsoft.Extensions.Configuration;
using Producto.Abstracciones.Interfaces.Servicios;
using System.Net.Http.Headers;
using System.Text.Json;


namespace Servicios
{
    public class TipoCambioServicio : ITipoDeCambioServicio
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClient;

        public TipoCambioServicio(IConfiguration configuration,
                                  IHttpClientFactory httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public Task<decimal> Obtener()
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> ObtenerTipoCambio()
        {
            var url = _configuration.GetSection("BancoCentralCR:UrlBase").Value;
            var token = _configuration.GetSection("BancoCentralCR:BearerToken").Value;

            var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var tipoCambio = document
                .RootElement
                .GetProperty("data")[0]
                .GetProperty("valor")
                .GetDecimal();

            return tipoCambio;
        }
    }
}
