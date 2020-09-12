using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.Domain.Entities;
using Trader.Domain.Filter;
using Trader.Infra.Models;
using Z.BulkOperations;
using Z.Dapper.Plus;

namespace Trader.Infra.Repositories
{
    public class StockRepository
    {
        private readonly string connectionString;

        public StockRepository()
        {
            connectionString = @"Data Source=LEANDRO\SQLEXPRESS;Initial Catalog=Trader;Integrated Security=True;Pooling=False";

            BulkMapper();
        }

        private void BulkMapper()
        {
            DapperPlusManager.MapperCache.Clear();

            DapperPlusManager.Entity<StockModel>()
                .Table("TB_PAPEL")
                .Key(a => a.Code, "CODIGO_NEGOCIACAO")
                .Map(a => a.BdiCode, "CODIGO_BDI")
                .Map(a => a.MarketCode, "CODIGO_MERCADO")
                .Map(a => a.Company, "EMPRESA")
                .Map(a => a.SpecificationCode, "CODIGO_ESPECIFICACAO")
                .Map(a => a.Currency, "MOEDA")
                .Map(a => a.CorrectionIndicatorCode, "CODIGO_INDICADOR_CORRECAO")
                .Map(a => a.CotationFactor, "FATOR_COTACAO")
                .Map(a => a.IsinCode, "ISIN");

            DapperPlusManager.Entity<StockNegotiationModel>()
                .Table("TB_COTACAO")
                .Key(a => a.Code, "CODIGO_NEGOCIACAO")
                .Key(a => a.Date, "DATA_NEGOCIACAO")
                .Map(a => a.AmountOfBonds, "QUANTIDADE_TITULOS")
                .Map(a => a.AmountOfTrade, "QUANTIDADE_NEGOCIOS")
                .Map(a => a.AverageValue, "VALOR_MEDIO")
                .Map(a => a.BestBuyValue, "MELHOR_OFERTA_COMPRA")
                .Map(a => a.BestSellValue, "MELHOR_OFERTA_VENDA")
                .Map(a => a.FirstValue, "VALOR_ABERTURA")
                .Map(a => a.LastValue, "VALOR_FECHAMENTO")
                .Map(a => a.MaxValue, "VALOR_MAXIMO")
                .Map(a => a.MinValue, "VALOR_MINIMO")
                .Map(a => a.TotalValueOfBonds, "VOLUME_TOTAL_TITULOS");
        }

        public void BulkMerge(List<Stock> stockList)
        {
            var stockListModel = stockList.ConvertAll(a => new StockModel(a));

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                connection.BulkMerge(stockListModel);

                connection.Close();
            }
        }

        public void Persist(List<Stock> stockList)
        {
            #region Command
            var command = @"
IF (NOT EXISTS(SELECT 1 FROM TB_PAPEL WHERE CODIGO_NEGOCIACAO = @Code))
BEGIN
	INSERT INTO TB_PAPEL
	(
		CODIGO_BDI,
		CODIGO_NEGOCIACAO,
		CODIGO_MERCADO,
		EMPRESA,
		CODIGO_ESPECIFICACAO,
		MOEDA,
		CODIGO_INDICADOR_CORRECAO,
		FATOR_COTACAO,
		ISIN
	)
	VALUES
	(
		@BdiCode,
		@Code,
		@MarketCode,
		@Company,
		@SpecificationCode,
		@Currency,
		@CorrectionIndicatorCode,
		@CotationFactor,
		@IsinCode
	)
END
";
            #endregion

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                var stockModelList = stockList.ConvertAll(a => { return new StockModel(a); });

                connection.Execute(command, stockModelList);

                connection.Close();
            }
        }

        public void BulkMerge(List<StockNegotiation> stockNegotiationList)
        {
            var stockNegotiationListModel = stockNegotiationList.ConvertAll(a => new StockNegotiationModel(a));

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                connection.BulkMerge(stockNegotiationListModel);

                connection.Close();
            }
        }

        public void Persist(List<StockNegotiation> stockNegotiationList)
        {
            #region Command
            var command = @"
IF (NOT EXISTS(SELECT 1 FROM TB_COTACAO WHERE CODIGO_NEGOCIACAO = @Code AND DATA_NEGOCIACAO = @Date))
BEGIN
	INSERT INTO TB_COTACAO
	(
		CODIGO_NEGOCIACAO,
		DATA_NEGOCIACAO,
		VALOR_ABERTURA,
		VALOR_MAXIMO,
		VALOR_MINIMO,
		VALOR_MEDIO,
		VALOR_FECHAMENTO,
		MELHOR_OFERTA_COMPRA,
		MELHOR_OFERTA_VENDA,
		QUANTIDADE_NEGOCIOS,
		QUANTIDADE_TITULOS,
		VOLUME_TOTAL_TITULOS
	)
	VALUES
	(
		@Code,
		@Date,
		@FirstValue,
		@MaxValue,
		@MinValue,
		@AverageValue,
		@LastValue,
		@BestBuyValue,
		@BestSellValue,
		@AmountOfTrade,
		@AmountOfBonds,
		@TotalValueOfBonds
	)
END
";
            #endregion

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                var stockNegotiationModelList = stockNegotiationList.ConvertAll(a => { return new StockNegotiationModel(a); });

                connection.Execute(command, stockNegotiationModelList);

                connection.Close();
            }
        }

        public List<Stock> GetAllStocks(GetAllStocksFilter filter)
        {
            #region Command
            var command = @"
SELECT 
	B.CODIGO_BDI 'BdiCode', 
	B.DESCRICAO_BDI 'BdiDescription',
	P.CODIGO_NEGOCIACAO 'Code',
	M.CODIGO_MERCADO 'MarketCode', 
	M.DESCRICAO_MERCADO 'MarketDescription', 
	P.EMPRESA 'Company',
	E.CODIGO_ESPECIFICACAO 'SpecificationCode', 
	E.DESCRICAO_ESPECIFICACAO 'SpecificationDescription',  
	P.MOEDA 'Currency',
	I.CODIGO_INDICADOR_CORRECAO 'CorrectionIndicatorCode',
	I.SIGLA_INDICADOR_CORRECAO 'CorrectionIndicatorSymbol',
	I.DESCRICAO_INDICADOR_CORRECAO 'CorrectionIndicatorDescription',
	P.FATOR_COTACAO 'CotationFactor',
	P.ISIN 'IsinCode'
FROM 
	TB_PAPEL P WITH(NOLOCK)
	LEFT JOIN TB_MERCADO M WITH(NOLOCK) ON P.CODIGO_MERCADO = M.CODIGO_MERCADO
	LEFT JOIN TB_BDI B WITH(NOLOCK) ON P.CODIGO_BDI = B.CODIGO_BDI
	LEFT JOIN TB_ESPECIFICACAO E WITH(NOLOCK) ON P.CODIGO_ESPECIFICACAO = E.CODIGO_ESPECIFICACAO
	LEFT JOIN TB_INDICADOR_CORRECAO I WITH(NOLOCK) ON P.CODIGO_INDICADOR_CORRECAO = I.CODIGO_INDICADOR_CORRECAO
WHERE 
	(@MarketCode IS NULL OR M.CODIGO_MERCADO = @MarketCode)
	AND (@SpecificationCode IS NULL OR E.CODIGO_ESPECIFICACAO = @SpecificationCode)
	AND (@BdiCode IS NULL OR B.CODIGO_BDI = @BdiCode)
ORDER BY 
	P.CODIGO_NEGOCIACAO";
            #endregion

            var result = new List<Stock>();

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                var resultModel = connection.Query<StockModel>(command, filter);

                foreach (var model in resultModel)
                {
                    result.Add(model.ToEntity());
                }

                connection.Close();
            }

            return result;
        }

        public List<StockNegotiation> GetStockNegotiation(GetStockNegotiationFilter filter)
        {
            #region Command
            var command = @"
SELECT 
    C.CODIGO_NEGOCIACAO 'Code',
    C.DATA_NEGOCIACAO 'Date',
    C.MELHOR_OFERTA_COMPRA 'BestBuyValue',
    C.MELHOR_OFERTA_VENDA 'BestSellValue',
    C.QUANTIDADE_NEGOCIOS 'AmountOfTrade',
    C.QUANTIDADE_TITULOS 'AmountOfBonds',
    C.VALOR_ABERTURA 'FirstValue',
    C.VALOR_FECHAMENTO 'LastValue',
    C.VALOR_MAXIMO 'MaxValue',
    C.VALOR_MEDIO 'AverageValue',
    C.VALOR_MINIMO 'MinValue',
    C.VOLUME_TOTAL_TITULOS 'TotalValueOfBonds'
FROM 
    TB_COTACAO C
WHERE 
    C.CODIGO_NEGOCIACAO = @StockCode
    AND (C.DATA_NEGOCIACAO BETWEEN @InitialDate AND @FinalDate)
";
            #endregion

            var result = new List<StockNegotiation>();

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                var resultModel = connection.Query<StockNegotiationModel>(command, filter);

                foreach (var model in resultModel)
                {
                    result.Add(model.ToEntity());
                }

                connection.Close();
            }

            return result;
        }
    }
}
