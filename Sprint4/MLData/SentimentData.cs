﻿using Microsoft.ML.Data;

namespace Sprint4.MLData
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string Text { get; set; }

        [LoadColumn(1)]
        public bool Label { get; set; }
    }
}
