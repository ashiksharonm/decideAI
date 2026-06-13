using System.Text.Json.Serialization;

namespace DecideAI.Core.Models;

public class FinancialMetrics
{
    [JsonPropertyName("ticker")]
    public string Ticker { get; set; } = string.Empty;

    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; } = string.Empty;

    [JsonPropertyName("regularMarketPrice")]
    public double RegularMarketPrice { get; set; }

    [JsonPropertyName("marketCap")]
    public long MarketCap { get; set; }

    [JsonPropertyName("trailingPE")]
    public double TrailingPE { get; set; }

    [JsonPropertyName("fiftyTwoWeekHigh")]
    public double FiftyTwoWeekHigh { get; set; }
}
