﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Migrations.Versions
{
    [Migration(201810271445, "Baseline")]
    public class _201810271445_Baseline : Migration
    {
        //Servidor = LEANDRO\SQLEXPRESS
        //Data Source=LEANDRO\SQLEXPRESS;Initial Catalog=Trader;Integrated Security=True;Pooling=False

        private readonly string collationName = "";

        private readonly string TABELA_BDI = "TB_BDI";
        private readonly string CODIGO_BDI = "CODIGO_BDI";
        private readonly string TABELA_MERCADO = "TB_MERCADO";
        private readonly string CODIGO_MERCADO = "CODIGO_MERCADO";
        private readonly string TABELA_ESPECIFICACAO = "TB_ESPECIFICACAO";
        private readonly string CODIGO_ESPECIFICACAO = "CODIGO_ESPECIFICACAO";
        private readonly string TABELA_INDICADOR_CORRECAO = "TB_INDICADOR_CORRECAO";
        private readonly string CODIGO_INDICADOR_CORRECAO = "CODIGO_INDICADOR_CORRECAO";
        private readonly string TABELA_PAPEL = "TB_PAPEL";
        private readonly string TABELA_COTACAO = "TB_COTACAO";

        public override void Down()
        {
            Delete.Table(TABELA_PAPEL);
            Delete.Table(TABELA_BDI);
            Delete.Table(TABELA_MERCADO);
            Delete.Table(TABELA_ESPECIFICACAO);
            Delete.Table(TABELA_INDICADOR_CORRECAO);
            Delete.Table(TABELA_COTACAO);
        }

        public override void Up()
        {
            Criar_TABELA_BDI();

            Criar_TABELA_MERCADO();

            Criar_TABELA_ESPECIFICACAO();

            Criar_TABELA_INDICADOR_CORRECAO();

            Create.Table(TABELA_PAPEL)
                .WithColumn(CODIGO_BDI).AsAnsiString(2, collationName)
                .WithColumn("CODIGO_NEGOCIACAO").AsAnsiString(12, collationName)
                .WithColumn(CODIGO_MERCADO).AsInt32()
                .WithColumn("EMPRESA").AsAnsiString(12, collationName)
                .WithColumn(CODIGO_ESPECIFICACAO).AsAnsiString(10, collationName)
                .WithColumn("MOEDA").AsAnsiString(4, collationName)
                .WithColumn(CODIGO_INDICADOR_CORRECAO).AsInt32()
                .WithColumn("FATOR_COTACAO").AsInt32()
                .WithColumn("ISIN").AsAnsiString(12, collationName);

            //Create.ForeignKey($"FK01_{TABELA_PAPEL}")
            //    .FromTable(TABELA_PAPEL).ForeignColumn(CODIGO_BDI)
            //    .ToTable(TABELA_BDI).PrimaryColumn(CODIGO_BDI);

            //Create.ForeignKey($"FK02_{TABELA_PAPEL}")
            //    .FromTable(TABELA_PAPEL).ForeignColumn(CODIGO_MERCADO)
            //    .ToTable(TABELA_MERCADO).PrimaryColumn(CODIGO_MERCADO);

            //Create.ForeignKey($"FK03_{TABELA_PAPEL}")
            //    .FromTable(TABELA_PAPEL).ForeignColumn(CODIGO_ESPECIFICACAO)
            //    .ToTable(TABELA_ESPECIFICACAO).PrimaryColumn(CODIGO_ESPECIFICACAO);

            //Create.ForeignKey($"FK04_{TABELA_PAPEL}")
            //    .FromTable(TABELA_PAPEL).ForeignColumn(CODIGO_INDICADOR_CORRECAO)
            //    .ToTable(TABELA_INDICADOR_CORRECAO).PrimaryColumn(CODIGO_INDICADOR_CORRECAO);

            //Create.PrimaryKey("PK_TB_PAPEL")

            Create.Table(TABELA_COTACAO)
                .WithColumn("CODIGO_NEGOCIACAO").AsAnsiString(12, collationName)
                .WithColumn("DATA_NEGOCIACAO").AsDateTime()
                .WithColumn("VALOR_ABERTURA").AsDecimal(19, 2)
                .WithColumn("VALOR_MAXIMO").AsDecimal(19, 2)
                .WithColumn("VALOR_MINIMO").AsDecimal(19, 2)
                .WithColumn("VALOR_MEDIO").AsDecimal(19, 2)
                .WithColumn("VALOR_FECHAMENTO").AsDecimal(19, 2)
                .WithColumn("MELHOR_OFERTA_COMPRA").AsDecimal(19, 2)
                .WithColumn("MELHOR_OFERTA_VENDA").AsDecimal(19, 2)
                .WithColumn("QUANTIDADE_NEGOCIOS").AsInt32()
                .WithColumn("QUANTIDADE_TITULOS").AsInt64()
                .WithColumn("VOLUME_TOTAL_TITULOS").AsDecimal(19, 2);

            Create.PrimaryKey($"PK_{TABELA_COTACAO}")
                .OnTable(TABELA_COTACAO)
                .Columns("CODIGO_NEGOCIACAO", "DATA_NEGOCIACAO");
        }

        private void Criar_TABELA_BDI()
        {
            Create.Table(TABELA_BDI)
                .WithColumn(CODIGO_BDI).AsAnsiString(2, collationName)
                .PrimaryKey($"PK_{TABELA_BDI}")
                .WithColumn("DESCRICAO_BDI").AsAnsiString(100, collationName);

            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "02", DESCRICAO_BDI = "LOTE PADRAO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "05", DESCRICAO_BDI = "SANCIONADAS PELOS REGULAMENTOS BMFBOVESPA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "06", DESCRICAO_BDI = "CONCORDATARIAS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "07", DESCRICAO_BDI = "RECUPERACAO EXTRAJUDICIAL" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "08", DESCRICAO_BDI = "RECUPERAÇÃO JUDICIAL" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "09", DESCRICAO_BDI = "RAET - REGIME DE ADMINISTRACAO ESPECIAL TEMPORARIA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "10", DESCRICAO_BDI = "DIREITOS E RECIBOS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "11", DESCRICAO_BDI = "INTERVENCAO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "12", DESCRICAO_BDI = "FUNDOS IMOBILIARIOS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "14", DESCRICAO_BDI = "CERT.INVEST/TIT.DIV.PUBLICA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "18", DESCRICAO_BDI = "OBRIGACÕES" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "22", DESCRICAO_BDI = "BÔNUS (PRIVADOS)" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "26", DESCRICAO_BDI = "APOLICES/BÔNUS/TITULOS PUBLICOS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "32", DESCRICAO_BDI = "EXERCICIO DE OPCOES DE COMPRA DE INDICES" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "33", DESCRICAO_BDI = "EXERCICIO DE OPCOES DE VENDA DE INDICES" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "38", DESCRICAO_BDI = "EXERCICIO DE OPCOES DE COMPRA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "42", DESCRICAO_BDI = "EXERCICIO DE OPCOES DE VENDA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "46", DESCRICAO_BDI = "LEILAO DE NAO COTADOS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "48", DESCRICAO_BDI = "LEILAO DE PRIVATIZACAO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "49", DESCRICAO_BDI = "LEILAO DO FUNDO RECUPERACAO ECONOMICA ESPIRITO SANTO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "50", DESCRICAO_BDI = "LEILAO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "51", DESCRICAO_BDI = "LEILAO FINOR" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "52", DESCRICAO_BDI = "LEILAO FINAM" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "53", DESCRICAO_BDI = "LEILAO FISET" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "54", DESCRICAO_BDI = "LEILAO DE ACÕES EM MORA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "56", DESCRICAO_BDI = "VENDAS POR ALVARA JUDICIAL" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "58", DESCRICAO_BDI = "OUTROS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "60", DESCRICAO_BDI = "PERMUTA POR ACÕES" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "61", DESCRICAO_BDI = "META" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "62", DESCRICAO_BDI = "MERCADO A TERMO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "66", DESCRICAO_BDI = "DEBENTURES COM DATA DE VENCIMENTO ATE 3 ANOS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "68", DESCRICAO_BDI = "DEBENTURES COM DATA DE VENCIMENTO MAIOR QUE 3 ANOS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "70", DESCRICAO_BDI = "FUTURO COM RETENCAO DE GANHOS" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "71", DESCRICAO_BDI = "MERCADO DE FUTURO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "74", DESCRICAO_BDI = "OPCOES DE COMPRA DE INDICES" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "75", DESCRICAO_BDI = "OPCOES DE VENDA DE INDICES" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "78", DESCRICAO_BDI = "OPCOES DE COMPRA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "82", DESCRICAO_BDI = "OPCOES DE VENDA" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "83", DESCRICAO_BDI = "BOVESPAFIX" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "84", DESCRICAO_BDI = "SOMA FIX" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "90", DESCRICAO_BDI = "TERMO VISTA REGISTRADO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "96", DESCRICAO_BDI = "MERCADO FRACIONARIO" });
            Insert.IntoTable(TABELA_BDI).Row(new { CODIGO_BDI = "99", DESCRICAO_BDI = "TOTAL GERAL" });
        }

        private void Criar_TABELA_MERCADO()
        {
            Create.Table(TABELA_MERCADO)
                .WithColumn(CODIGO_MERCADO).AsInt32()
                .PrimaryKey($"PK_{TABELA_MERCADO}")
                .WithColumn("DESCRICAO_MERCADO").AsAnsiString(100, collationName);

            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "010", DESCRICAO_MERCADO = "VISTA" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "012", DESCRICAO_MERCADO = "EXERCÍCIO DE OPÇÕES DE COMPRA" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "013", DESCRICAO_MERCADO = "EXERCÍCIO DE OPÇÕES DE VENDA" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "017", DESCRICAO_MERCADO = "LEILÃO" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "020", DESCRICAO_MERCADO = "FRACIONÁRIO" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "030", DESCRICAO_MERCADO = "TERMO" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "050", DESCRICAO_MERCADO = "FUTURO COM RETENÇÃO DE GANHO" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "060", DESCRICAO_MERCADO = "FUTURO COM MOVIMENTAÇÃO CONTÍNUA" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "070", DESCRICAO_MERCADO = "OPÇÕES DE COMPRA" });
            Insert.IntoTable(TABELA_MERCADO).Row(new { CODIGO_MERCADO = "080", DESCRICAO_MERCADO = "OPÇÕES DE VENDA" });
        }

        private void Criar_TABELA_ESPECIFICACAO()
        {
            Create.Table(TABELA_ESPECIFICACAO)
                .WithColumn(CODIGO_ESPECIFICACAO).AsAnsiString(10, collationName)
                .PrimaryKey($"PK_{TABELA_ESPECIFICACAO}")
                .WithColumn("DESCRICAO_ESPECIFICACAO").AsAnsiString(100, collationName);

            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BDR", DESCRICAO_ESPECIFICACAO = "BDR" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES MISCELÂNEA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS B/A", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS ORD", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES ORDINÁRIAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/A", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/B", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/C", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/D", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/E", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/F", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/G", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS P/H", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "BNS PRE", DESCRICAO_ESPECIFICACAO = "BÔNUS DE SUBSCRIÇÃO EM ACÕES PREFERÊNCIA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "CDA", DESCRICAO_ESPECIFICACAO = "CERTIFICADO DE DEPÓSITO DE ACÕES ORDINÁRIAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "CI", DESCRICAO_ESPECIFICACAO = "FUNDO DE INVESTIMENTO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "CPA", DESCRICAO_ESPECIFICACAO = "CERTIF. DE POTENCIAL ADIC. DE CONSTRUÇÃO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO MISCELÂNEA (BÔNUS)" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR ORD", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES ORDINÁRIAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/A", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/B", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/C", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/D", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/E", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/F", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/G", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR P/H", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR PR", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR PRA", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR PRB", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR PRC", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "DIR PRE", DESCRICAO_ESPECIFICACAO = "DIREITOS DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "LFT", DESCRICAO_ESPECIFICACAO = "LETRA FINANCEIRA DO TESOURO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "M1 REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO DE MISCELÂNEAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "ON", DESCRICAO_ESPECIFICACAO = "ACÕES ORDINÁRIAS NOMINATIVAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "ON P", DESCRICAO_ESPECIFICACAO = "ACÕES ORDINÁRIAS NOMINATIVAS COM DIREITO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "ON REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES ORDINÁRIAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "OR", DESCRICAO_ESPECIFICACAO = "ACÕES ORDINÁRIAS NOMINATIVAS RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "OR P", DESCRICAO_ESPECIFICACAO = "ACÕES ORDINÁRIAS NOMINATIVAS RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PCD", DESCRICAO_ESPECIFICACAO = "POSIÇÃO CONSOLIDADA DA DIVIDA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PN", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PN P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS COM DIREITO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PN REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNA", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE A" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNA P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE A" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNA REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNB", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE B" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNB P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE B" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNB REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNC", DESCRICAO_ESPECIFICACAO = " ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE C" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNC P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE C" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNC REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PND", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE D" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PND P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE D" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PND REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNE", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE E" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNE P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE E" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNE REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNF", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE F" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNF P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE F" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNF REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNG", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE G" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNG P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE G" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNG REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNH", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE H" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNH P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE H" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNH REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNR", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNV", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAS NOMINATIVAS COM DIREITO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNV P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE V" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PNV REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES PREFERENCIAIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PR P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRA", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE A" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRA P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE A" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRA REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRB", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE B" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRB P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE B" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRB REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRC", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE C" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRC P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE C" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRC REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM ACÕES RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRD", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE D" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRD P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE D" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRE", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE E" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRE P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE E" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRF", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE F" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRF P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE F" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRG", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE G" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRG P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE G" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRH", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE H" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRH P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS CLASSE H" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRV", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS COM DIREITO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "PRV P", DESCRICAO_ESPECIFICACAO = "ACÕES PREFERÊNCIAIS NOMINATIVAS RESG. C/ DIREITO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "R", DESCRICAO_ESPECIFICACAO = "CESTA DE ACÕES NOMINATIVAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "REC", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO MISCELÂNEA" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "REC PR", DESCRICAO_ESPECIFICACAO = "RECIBO DE SUBSCRIÇÃO EM PREF RESGATÁVEIS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "RON", DESCRICAO_ESPECIFICACAO = "CESTA DE ACÕES ORDINÁRIAS NOMINATIVAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "TPR", DESCRICAO_ESPECIFICACAO = "TIT. PERPETUOS REMUN. VARIAV. ROYALTIES" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "UNT", DESCRICAO_ESPECIFICACAO = "CERTIFICADO DE DEPÓSITO DE ACÕES - MISCELÂNEAS" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "UP", DESCRICAO_ESPECIFICACAO = "PRECATÓRIO" });
            Insert.IntoTable(TABELA_ESPECIFICACAO).Row(new { CODIGO_ESPECIFICACAO = "WRT", DESCRICAO_ESPECIFICACAO = "WARRANTS DE DEBÊNTURES" });
        }

        private void Criar_TABELA_INDICADOR_CORRECAO()
        {
            Create.Table(TABELA_INDICADOR_CORRECAO)
                .WithColumn(CODIGO_INDICADOR_CORRECAO).AsInt32()
                .PrimaryKey($"PK_{TABELA_INDICADOR_CORRECAO}")
                .WithColumn("SIGLA_INDICADOR_CORRECAO").AsAnsiString(10, collationName)
                .WithColumn("DESCRICAO_INDICADOR_CORRECAO").AsAnsiString(100, collationName);

            Insert.IntoTable(TABELA_INDICADOR_CORRECAO).Row(new { CODIGO_INDICADOR_CORRECAO = 1, SIGLA_INDICADOR_CORRECAO = "US$", DESCRICAO_INDICADOR_CORRECAO = "CORREÇÃO PELA TAXA DO DÓLAR" });
            Insert.IntoTable(TABELA_INDICADOR_CORRECAO).Row(new { CODIGO_INDICADOR_CORRECAO = 2, SIGLA_INDICADOR_CORRECAO = "TJLP", DESCRICAO_INDICADOR_CORRECAO = "CORREÇÃO PELA TJLP" });
            Insert.IntoTable(TABELA_INDICADOR_CORRECAO).Row(new { CODIGO_INDICADOR_CORRECAO = 8, SIGLA_INDICADOR_CORRECAO = "IGPM", DESCRICAO_INDICADOR_CORRECAO = "CORREÇÃO PELO IGP-M - OPÇÕES PROTEGIDAS" });
            Insert.IntoTable(TABELA_INDICADOR_CORRECAO).Row(new { CODIGO_INDICADOR_CORRECAO = 9, SIGLA_INDICADOR_CORRECAO = "URV", DESCRICAO_INDICADOR_CORRECAO = "CORREÇÃO PELA URV" });
        }
    }
}
