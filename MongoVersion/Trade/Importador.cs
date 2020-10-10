using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Trade.Repositories;

namespace Trade
{
    public class Importador
    {
        private readonly Processador _processador;
        private readonly BovespaRepository _bovespaRepository;

        public Importador(Processador processador)
        {
            _bovespaRepository = new BovespaRepository();
            _processador = processador;
        }

        public async Task Run()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Arquivos");

            var files = Directory.GetFiles(path);

            if (files.Any())
            {
                Console.WriteLine("Arquivos importados");

                foreach (var file in files)
                {
                    var basePath = Path.GetDirectoryName(file);
                    var fileName = Path.GetFileName(file);

                    await _bovespaRepository.ImportFileHistory(basePath, fileName, _processador).ConfigureAwait(false);

                    var destFile = file.Replace("Arquivos", "ArquivosProcessados");

                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }

                    File.Move(file, destFile);

                    Console.WriteLine(file);
                }
            }

            return;
        }

    }
}
