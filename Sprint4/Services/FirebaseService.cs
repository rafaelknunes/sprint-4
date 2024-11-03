// Serviço responsável por fazer a comunicação com o Firebase

namespace Sprint4.Services
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    // Classe responsável por fazer a comunicação com o Firebase: Autenticação de usuário
    public class FirebaseService : IFirebaseService
    {
        private readonly HttpClient _httpClient;
        private const string FirebaseApiUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword";
        private readonly string _firebaseApiKey = "AIzaSyA-JlACVhOgMF0J0ygUZJOxATW29PXPvK0";

        public FirebaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateUserAsync(string email, string password)
        {
            var requestBody = new
            {
                email = email,
                password = password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{FirebaseApiUrl}?key={_firebaseApiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                return "Erro ao autenticar usuário.";
            }
        }
    }

}
