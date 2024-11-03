using Microsoft.ML;
using Sprint4.MLData;
using System.IO;

namespace Sprint4.Services
{
    public class SentimentAnalysisService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public SentimentAnalysisService()
        {
            _mlContext = new MLContext();
            var modelPath = Path.Combine("MLData", "sentiment_model.zip");
            _model = _mlContext.Model.Load(modelPath, out _);
        }

        public bool PredictSentiment(string text)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
            var prediction = predictionEngine.Predict(new SentimentData { Text = text });
            return prediction.Prediction;
        }
    }

}
