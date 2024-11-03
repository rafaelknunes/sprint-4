using Microsoft.AspNetCore.Mvc;
using Sprint4.Services;

namespace Sprint4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IFirebaseService _firebaseService;
        private readonly SentimentAnalysisService _sentimentAnalysisService;

        public AuthController(IFirebaseService firebaseService, SentimentAnalysisService sentimentAnalysisService)
        {
            _firebaseService = firebaseService;
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        /// <summary>
        /// Autentica o usuário usando email e senha.
        /// </summary>
        /// <remarks>
        /// Para testar a autenticação, use o email "teste@teste.com" e a senha "123456".
        /// </remarks>
        /// <param name="email">O email do usuário. Exemplo: "teste@teste.com"</param>
        /// <param name="password">A senha do usuário. Exemplo: "123456"</param>
        /// <returns>Um token de autenticação se as credenciais estiverem corretas.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _firebaseService.AuthenticateUserAsync(email, password);
            return Ok(result);
        }

        /// <summary>
        /// Analisa o sentimento de um texto fornecido.
        /// </summary>
        /// <remarks>
        /// Envie um texto para análise de sentimento. O serviço retornará "Positive" para textos com sentimento positivo e "Negative" para textos com sentimento negativo.
        /// </remarks>
        /// <param name="text">O texto a ser analisado.</param>
        /// <returns>Uma mensagem indicando se o sentimento é positivo ou negativo.</returns>
        [HttpPost("analyze-sentiment")]
        public IActionResult AnalyzeSentiment([FromBody] string text)
        {
            var result = _sentimentAnalysisService.PredictSentiment(text);
            return Ok(result ? "Positive" : "Negative");
        }
    }
}
