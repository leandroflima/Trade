using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.Domain.Entities;

namespace Trader.Infra.Models
{
    public class StockModel
    {
        public string BdiCode { get; set; }

        public string BdiDescription { get; set; }

        public string Code { get; set; }

        public string MarketCode { get; set; }

        public string MarketDescription { get; set; }

        public string Company { get; set; }

        public string SpecificationCode { get; set; }

        public string SpecificationDescription { get; set; }

        public string Currency { get; set; }

        public int? CorrectionIndicatorCode { get; set; }

        public string CorrectionIndicatorSymbol { get; set; }

        public string CorrectionIndicatorDescription { get; set; }

        public int CotationFactor { get; set; }

        public string IsinCode { get; set; }

        public StockModel()
        {
        }

        public StockModel(Stock stock)
        {
            BdiCode = stock.Bdi.Code;
            Code = stock.Code;
            MarketCode = stock.Market.Code;
            Company = stock.Company;
            SpecificationCode = stock.Specification.Code;
            Currency = stock.Currency;
            CorrectionIndicatorCode = stock.CorrectionIndicator.Code;
            CotationFactor = stock.CotationFactor;
            IsinCode = stock.IsinCode;
        }

        public Stock ToEntity()
        {
            var stock = new Stock
            {
                Bdi = new BdiType
                {
                    Code = BdiCode,
                    Description = BdiDescription
                },
                Code = Code,
                Company = Company,
                CotationFactor = CotationFactor,
                Currency = Currency,
                IsinCode = IsinCode,
                Market = new Market
                {
                    Code = MarketCode,
                    Description = MarketDescription
                },
                Specification = new StockSpecification
                {
                    Code = SpecificationCode,
                    Description = SpecificationDescription
                }
            };

            if (CorrectionIndicatorCode.HasValue)
            {
                stock.CorrectionIndicator = new CorrectionIndicator
                {
                    Code = CorrectionIndicatorCode.Value,
                    Description = CorrectionIndicatorDescription,
                    Symbol = CorrectionIndicatorSymbol
                };
            }

            return stock;
        }
    }
}
