-- =============================================
-- SCRIPT 05: Atualização da Function ConsultarConfiguracoes
-- Adiciona retorno dos campos de fonte
-- =============================================
-- Execute este script SEXTO
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT 'INICIANDO SCRIPT 05: Atualizar ConsultarConfiguracoes'
PRINT '=========================================='
GO

-- Remove function antiga
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ConsultarConfiguracoes' AND type = 'FN')
BEGIN
    PRINT 'Removendo function ConsultarConfiguracoes antiga...'
    DROP FUNCTION dbo.ConsultarConfiguracoes
END
GO

PRINT 'Criando function ConsultarConfiguracoes atualizada...'
GO

CREATE FUNCTION [dbo].[ConsultarConfiguracoes]()
RETURNS TABLE AS RETURN
SELECT [ValorFrete]
      ,[PortaImpressora]
	  ,[MostrarExcluidos]
	  ,[PerguntarImpressora]
	  ,[FonteRelatorioNome]
	  ,[FonteRelatorioTamanho]
	  ,[FonteImpressaoNome]
	  ,[FonteImpressaoTamanho]
  FROM [dbo].[Configuracoes]
GO

PRINT 'Function ConsultarConfiguracoes atualizada com sucesso!'
GO

PRINT '=========================================='
PRINT 'SCRIPT 05 CONCLUÍDO COM SUCESSO!'
PRINT '=========================================='
GO
