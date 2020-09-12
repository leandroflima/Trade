using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.Domain.Entities;

namespace Trader.Infra.Repositories
{
    public class BovespaRepository
    {
        public List<StockNegotiation> ImportFileHistory(string basePath, string fileName)
        {
            var result = new List<StockNegotiation>();

            var path = Path.Combine(basePath, fileName);

            using (StreamReader leitor = new StreamReader(path, Encoding.Default))
            {
                while (!leitor.EndOfStream)
                {
                    var linha = leitor.ReadLine();

                    if (linha.Substring(0, 2) == "00" || linha.Substring(0, 2) == "99") continue;

                    var stock = new Stock(linha);

                    var stockNegotiation = new StockNegotiation(linha, stock);

                    result.Add(stockNegotiation);
                }
            }

            return result;
        }

    }
}
