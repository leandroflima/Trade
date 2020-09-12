--select * from TB_BDI
select distinct A.DATA_NEGOCIACAO, COUNT(A.CODIGO_NEGOCIACAO) 
from TRADER..TB_COTACAO A with(nolock) 
inner join TRADER..TB_PAPEL p with(nolock) on A.CODIGO_NEGOCIACAO = p.CODIGO_NEGOCIACAO
where p.CODIGO_BDI = '12'
group by A.DATA_NEGOCIACAO order by A.DATA_NEGOCIACAO DESC
