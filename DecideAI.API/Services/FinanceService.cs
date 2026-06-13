using DecideAI.Core.Models;
using YahooFinanceApi;

namespace DecideAI.API.Services;

public class FinanceService
{
    public async Task<FinancialMetrics?> GetMetricsForTickerAsync(string ticker)
    {
        try
        {
            var securities = await Yahoo.Symbols(ticker).Fields(Field.Symbol, Field.RegularMarketPrice, Field.MarketCap, Field.TrailingPE, Field.FiftyTwoWeekHigh, Field.LongName).QueryAsync();
            var security = securities[ticker];

            if (security == null) return null;

            return new FinancialMetrics
            {
                Ticker = ticker,
                CompanyName = security.Fields.ContainsKey("LongName") ? security["LongName"] : ticker,
                RegularMarketPrice = security.Fields.ContainsKey("RegularMarketPrice") ? security.RegularMarketPrice : 0,
                MarketCap = security.Fields.ContainsKey("MarketCap") ? security.MarketCap : 0,
                TrailingPE = security.Fields.ContainsKey("TrailingPE") ? security.TrailingPE : 0,
                FiftyTwoWeekHigh = security.Fields.ContainsKey("FiftyTwoWeekHigh") ? security.FiftyTwoWeekHigh : 0
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching finance data for {ticker}: {ex.Message}");
            return null;
        }
    }
}
