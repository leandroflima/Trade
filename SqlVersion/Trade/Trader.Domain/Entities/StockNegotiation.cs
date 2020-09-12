using System;

namespace Trader.Domain.Entities
{
    public class StockNegotiation
    {
        public Stock Stock { get; set; }

        public DateTime Date { get; set; }

        public decimal FirstValue { get; set; }

        public decimal MaxValue { get; set; }

        public decimal MinValue { get; set; }

        public decimal AverageValue { get; set; }

        public decimal LastValue { get; set; }

        public decimal BestBuyValue { get; set; }

        public decimal BestSellValue { get; set; }

        public int AmountOfTrade { get; set; }

        public long AmountOfBonds { get; set; }

        public decimal TotalValueOfBonds { get; set; }

        public StockNegotiation()
        {
        }

        public StockNegotiation(string line, Stock stock)
        {
            DateTime data = DateTime.MinValue;
            string dataAlfa = $"{line.Substring(8, 2)}/{line.Substring(6, 2)}/{line.Substring(2, 4)}";
            DateTime.TryParse(dataAlfa, out data);

            if (decimal.TryParse(line.Substring(56, 13), out decimal abertura))
            {
                abertura = (abertura / 100);
            }

            if (decimal.TryParse(line.Substring(69, 13), out decimal maximo))
            {
                maximo = (maximo / 100);
            }

            if (decimal.TryParse(line.Substring(82, 13), out decimal minimo))
            {
                minimo = (minimo / 100);
            }

            if (decimal.TryParse(line.Substring(95, 13), out decimal medio))
            {
                medio = (medio / 100);
            }

            if (decimal.TryParse(line.Substring(108, 13), out decimal ultimo))
            {
                ultimo = (ultimo / 100);
            }

            if (decimal.TryParse(line.Substring(121, 11), out decimal melhorOfertaCompra))
            {
                melhorOfertaCompra = (melhorOfertaCompra / 100);
            }

            if (decimal.TryParse(line.Substring(134, 11), out decimal melhorOfertaVenda))
            {
                melhorOfertaVenda = (melhorOfertaVenda / 100);
            }

            int.TryParse(line.Substring(147, 5), out int qtdeNegocios);

            long.TryParse(line.Substring(152, 18), out long qtdeTitulos);

            Stock = stock;
            Date = data;
            FirstValue = abertura;
            MaxValue = maximo;
            MinValue = minimo;
            AverageValue = medio;
            LastValue = ultimo;
            BestBuyValue = melhorOfertaCompra;
            BestSellValue = melhorOfertaVenda;
            AmountOfTrade = qtdeNegocios;
            AmountOfBonds = qtdeTitulos;
        }
    }
}
