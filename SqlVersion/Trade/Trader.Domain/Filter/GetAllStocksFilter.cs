namespace Trader.Domain.Filter
{
    public class GetAllStocksFilter
    {
        public GetAllStocksFilter(int? marketCode, string bdiCode)
        {
            MarketCode = marketCode;
            BdiCode = bdiCode;
        }

        public GetAllStocksFilter(int? marketCode, string specificationCode, string bdiCode) : this(marketCode, specificationCode)
        {
            BdiCode = bdiCode;
        }

        public int? MarketCode { get; set; }

        public string SpecificationCode { get; set; }

        public string BdiCode { get; set; }
    }
}
