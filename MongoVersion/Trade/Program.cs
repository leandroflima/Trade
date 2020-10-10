using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using Trade.Domain;
using Trade.Repositories;
using Trade.Service;

namespace Trade
{
    class Program
    {
        private static readonly string MongoDbConnectionString = "mongodb://localhost:27017";
        private static Processador _processador;
        private static Importador _importador;
        private static StockRepository _stockRepository;
        private static StockService _stockService;

        static void Main()
        {
            _processador = new Processador(MongoDbConnectionString);
            _importador = new Importador(_processador);
            _stockRepository = new StockRepository(MongoDbConnectionString);
            _stockService = new StockService();

            do
            {
                Console.WriteLine("Opções");
                Console.WriteLine("1 - Importar");
                Console.WriteLine("2 - Filtrar por Data");
                Console.WriteLine("3 - Pesquisar Oscilações para cima");
                Console.WriteLine("9 - Sair");

                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    var initialDate = DateTime.MinValue;
                    var endDate = DateTime.MaxValue;
                    IEnumerable<StockNegotiation> stocks = null; 

                    switch (option)
                    {
                        case 1:
                            _importador.Run().Wait();
                            _processador.Run().Wait();
                            break;
                        case 2:
                            initialDate = ImportDate();
                            endDate = ImportDate();
                            stocks = _stockRepository.GetStockNegotiations(initialDate, endDate).Result;
                            _stockService.PrintAmountOfTradeReport(stocks);
                            break;
                        case 3:
                            initialDate = ImportDate();
                            endDate = ImportDate();
                            stocks = _stockRepository.GetStockNegotiations(initialDate, endDate).Result;
                            _stockService.SearchMostHighStocks(stocks);
                            break;
                    }
                }

                if (option == 9) break;

                Console.WriteLine();
                Console.WriteLine("digite qualquer tecla para voltar ao menu...");
                Console.ReadKey(true);

                Console.Clear();

            } while (true);
        }

        private static DateTime ImportDate()
        {
            do
            {
                Console.WriteLine("Digite uma data");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    return date;
                }

            } while (true);
        }
    }
}
