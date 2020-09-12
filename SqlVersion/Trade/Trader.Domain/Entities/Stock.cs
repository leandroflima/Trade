using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Domain.Entities
{
    public class Stock : IComparable<Stock>, IEquatable<Stock>
    { 
        public BdiType Bdi { get; set; }

        public string Code { get; set; }

        public Market Market { get; set; }

        public string Company { get; set; }

        public StockSpecification Specification { get; set; }

        public string Currency { get; set; }

        public CorrectionIndicator CorrectionIndicator { get; set; }

        public int CotationFactor { get; set; }

        public string IsinCode { get; set; }

        public Stock()
        {
        }

        public Stock(string line)
        {
            string codbdi = line.Substring(10, 2).Trim();
            string codigoNegociacao = line.Substring(12, 12).Trim();
            string tipoMercado = line.Substring(24, 3);
            string nomeEmpresa = line.Substring(27, 12);
            string especificacao = line.Substring(39, 10).Trim();
            string moeda = line.Substring(52, 4);
            int.TryParse(line.Substring(201, 1), out int indicadorCorrecao);
            int.TryParse(line.Substring(210, 7), out int fatorCotacao);
            string isin = line.Substring(230, 12);

            Bdi = new BdiType { Code = codbdi };
            Code = codigoNegociacao;
            Market = new Market { Code = tipoMercado };
            Company = nomeEmpresa;
            Specification = new StockSpecification { Code = especificacao };
            Currency = moeda;
            CorrectionIndicator = new CorrectionIndicator { Code = indicadorCorrecao };
            CotationFactor = fatorCotacao;
            IsinCode = isin;
        }

        public int CompareTo(Stock other)
        {
            return this.Code.CompareTo(other.Code);
        }

        public bool Equals(Stock other)
        {
            return this.Code.Equals(other.Code);
        }
    }
}
