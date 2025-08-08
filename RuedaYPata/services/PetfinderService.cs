using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuedaYPata.Models;
using RuedaYPata.Models.Api;

namespace RuedaYPata.Services
{
    public class PetfinderService
    {
        private readonly HttpClient _httpClient;
        private readonly PetfinderSettings _settings;
        private readonly ILogger<PetfinderService> _logger;

        private string _accessToken;
        private DateTime _tokenExpiration;

        public PetfinderService(HttpClient httpClient, IOptions<PetfinderSettings> options, ILogger<PetfinderService> logger)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _logger = logger;
        }

        // Autentica y guarda token en memoria
        public async Task AuthenticateAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiration)
                return;

            _logger.LogInformation("Obteniendo token de Petfinder...");

            var formData = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _settings.ClientId },
                { "client_secret", _settings.ClientSecret }
            };

            using var content = new FormUrlEncodedContent(formData);
            var response = await _httpClient.PostAsync("https://api.petfinder.com/v2/oauth2/token", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<PetfinderTokenResponse>(json);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                throw new Exception("No se pudo obtener el token de autenticación de Petfinder");

            _accessToken = tokenResponse.AccessToken;
            _tokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 60);

            _logger.LogInformation($"Token obtenido, expira en {tokenResponse.ExpiresIn / 60} minutos");
        }

        // Obtiene lista de razas para un tipo (dog, cat, etc.)
        public async Task<List<string>> GetRazasAsync(string tipo)
        {
            await AuthenticateAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var url = $"https://api.petfinder.com/v2/types/{tipo}/breeds";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var breedsResponse = JsonSerializer.Deserialize<PetfinderBreedsResponse>(json);

            return breedsResponse?.Breeds?.Select(b => b.Name).ToList() ?? new List<string>();
        }
    }
}