using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Trade.Domain;

namespace Trade.Repositories
{
    public class BovespaRepository
    {
        public async Task ImportFileHistory(string basePath, string fileName, Processador processador)
        {
            var path = Path.Combine(basePath, fileName);

            using (StreamReader leitor = new StreamReader(path, Encoding.Default))
            {
                while (!leitor.EndOfStream)
                {
                    var linha = await leitor.ReadLineAsync().ConfigureAwait(false);

                    if (linha.Substring(0, 2) == "00" || linha.Substring(0, 2) == "99") continue;

                    var stock = new Stock(linha);

                    var stockNegotiation = new StockNegotiation(linha, stock);

                    processador.Add(stockNegotiation);
                }
            }
        }

    }
}
