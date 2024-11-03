using Microsoft.ML;
using Microsoft.ML.Data;
using System.IO;

namespace Sprint4.MLData
{
     public class SentimentTrainer
    {
        private readonly MLContext _mlContext;
        private readonly string _modelPath;

        public SentimentTrainer()
        {
            _mlContext = new MLContext();
            _modelPath = Path.Combine("MLData", "sentiment_model.zip");
        }

        public void Train()
        {
            IDataView dataView = _mlContext.Data.LoadFromTextFile<SentimentData>("MLData/sentiment_data.csv", hasHeader: true, separatorChar: ',');

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            var model = pipeline.Fit(dataView);

            _mlContext.Model.Save(model, dataView.Schema, _modelPath);
        }
    }

}
