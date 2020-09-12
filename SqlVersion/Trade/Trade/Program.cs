using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trader.Service;
using Trader.Service.Param;
using Trader.Service.Result;

namespace Trade
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Opções");
                Console.WriteLine("1 - Importar");
                Console.WriteLine("2 - Fundos Imobiliários");
                Console.WriteLine("3 - Ações");
                Console.WriteLine("9 - Sair");

                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1:
                            Importar();
                            break;
                        case 2:
                            PlanilhaFundosImobiliarios();
                            break;
                        case 3:
                            PlanilhaAcoes();
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

        public static double Divide(int a, int b)
        {
            double resultDivide = a / b;
            return Math.Round(resultDivide, 5);
        }

        private static void PlanilhaFundosImobiliarios()
        {
            var result = new StockService().CalculateRealStateFunds(new CalculateRealStateFundsParam());

            var file = "PlanilhaFundosImobiliarios.csv";
            GenerateSheet(result, file);
        }

        private static void GenerateSheet(DateTime initialDate, DateTime finalDate, List<Trader.Domain.Entities.StockAverage> result, string file)
        {
            var path = Path.Combine(Environment.CurrentDirectory, file);
            var formatDate = "dd/MM/yyyy";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.AutoFlush = true;
                sw.WriteLine($"{initialDate.ToString(formatDate)};{finalDate.ToString(formatDate)}");
                sw.WriteLine();
                sw.WriteLine("StockCode;Last;Average;%+;%-;AmountOfTradeInLastDay;InitialDate;FinalDate");
                foreach (var item in result)
                {
                    var above = item.PercentageAboveAverage > 0 ? item.PercentageAboveAverage.ToString("#0.0000") : string.Empty;
                    var below = item.PercentageBelowAverage > 0 ? item.PercentageBelowAverage.ToString("#0.0000") : string.Empty;

                    var line = $"{item.StockCode};" +
                        $"{item.LastValue.ToString("C")};" +
                        $"{item.AverageValue.ToString("C")};" +
                        $"{above};" +
                        $"{below};" +
                        $"{item.AmountOfTradeInLastDay.ToString()};" +
                        $"{item.InitialDate.ToString(formatDate)};" +
                        $"{item.FinalDate.ToString(formatDate)}";
                    sw.WriteLine(line);
                }
                sw.Close();
            }

            Console.WriteLine($"Arquivo {file} gerado com {result.Count} linhas");
        }

        private static void GenerateSheet(CalculateRealStateFundsResult result, string file)
        {
            var path = Path.Combine(Environment.CurrentDirectory, file);
            
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Code;CountInNegotiation;FirstNegotiationDate;LastNegotiationDate;MaxLastValue;MinLastValue;LastValue");
                foreach (var item in result.Statistics)
                {
                    var line = $"{item.Code};" +
                        $"{item.CountInNegotiation.ToString()};" +
                        $"{item.FirstNegotiationDate.ToString("dd/MM/yyyy")};" +
                        $"{item.LastNegotiationDate.ToString("dd/MM/yyyy")};" +
                        $"{item.MaxLastValue.ToString("C")};" +
                        $"{item.MinLastValue.ToString("C")};" +
                        $"{item.LastValue.ToString("C")}";
                    sw.WriteLine(line);
                }
                sw.Close();
            }

            Console.WriteLine($"Arquivo {file} gerado com {result.Statistics.Count} linhas");
        }

        private static void PlanilhaAcoes()
        {
            var initialDate = DateTime.Now.Date.AddYears(-1).Date;
            var finalDate = DateTime.Now.Date;

            var result = new StockService().CalculateAverage(new Trader.Service.Param.CalculateAverageParam
            {
                BdiCode = "02",
                MarketCode = 10,
                InitialDate = initialDate,
                FinalDate = finalDate
            });

            result = result.OrderBy(a => a.StockCode).ToList();

            var file = "PlanilhaAcoes.csv";
            GenerateSheet(initialDate, finalDate, result, file);
        }

        private static void Importar()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Arquivos");

            var files = Directory.GetFiles(path);

            if (files.Any())
            {
                Console.WriteLine("Arquivos importados");

                foreach (var file in files)
                {
                    var param = new Trader.Service.Param.ImportParam
                    {
                        BasePath = Path.GetDirectoryName(file),
                        FileName = Path.GetFileName(file)
                    };

                    new StockService().Import(param);

                    var destFile = file.Replace("Arquivos", "ArquivosProcessados");

                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }

                    File.Move(file, destFile);

                    Console.WriteLine(file);
                }
            }
        }

    }
}
