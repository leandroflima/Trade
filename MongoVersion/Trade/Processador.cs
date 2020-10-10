using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trade.Domain;

namespace Trade
{
    public class Processador
    {
        private readonly Queue<StockNegotiation> _stockNegotiationsQueue;
        private readonly IMongoCollection<StockNegotiation> _collection;
        private int _counter;

        public Processador(string connectionString)
        {
            _stockNegotiationsQueue = new Queue<StockNegotiation>();

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Trade");
            _collection = database.GetCollection<StockNegotiation>(nameof(StockNegotiation));

            _counter = 0;
        }

        public void Add(StockNegotiation stockNegotiation)
        {
            _stockNegotiationsQueue.Enqueue(stockNegotiation);
            
            if (_stockNegotiationsQueue.Count % 10000 == 0)
            {
                System.Console.WriteLine($"Fila:{_stockNegotiationsQueue.Count} Counter:{_counter}");
            }
        }

        public async Task Run()
        {
            do
            {
                if (_stockNegotiationsQueue.Count == 0)
                {
                    System.Console.WriteLine($"Fila zerada! Counter:{_counter}");
                    break;
                }

                await PersistNegotiation().ConfigureAwait(false);

            } while (true);
        }

        private async Task PersistNegotiation()
        {
            var bulkSize = 10000;

            var stocks = new List<StockNegotiation>();
            for (int i = 0; i < bulkSize; i++)
            {
                if (_stockNegotiationsQueue.TryDequeue(out StockNegotiation stock))
                {
                    stocks.Add(stock);
                }
            }

            await _collection.InsertManyAsync(stocks).ConfigureAwait(false);

            _counter += bulkSize;
            System.Console.WriteLine($"Fila:{_stockNegotiationsQueue.Count} Counter:{_counter}");
        }
    }
}
