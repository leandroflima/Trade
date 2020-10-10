using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trade.Domain;

namespace Trade.Repositories
{
    public class StockRepository
    {
        private readonly IMongoCollection<StockNegotiation> _collection;

        public StockRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Trade");
            _collection = database.GetCollection<StockNegotiation>(nameof(StockNegotiation));
        }

        public async Task<IEnumerable<StockNegotiation>> GetStockNegotiations(DateTime initialDate, DateTime endDate)
        {
            var stocks = await _collection
                .FindAsync(stock =>
                    stock.Stock.Bdi.Code == "02" &&     //LOTE PADRAO
                    stock.Stock.Market.Code == "010" && //LOTE PADRAO
                    stock.Date >= initialDate.Date && 
                    stock.Date <= endDate.Date)
                .ConfigureAwait(false);

            return stocks.ToList();
        }

    }
}
