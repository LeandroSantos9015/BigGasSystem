-- =============================================
-- Script para adicionar configurações de Fonte
-- =============================================

-- Verifica se as colunas de fonte já existem antes de adicionar
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioNome')
BEGIN
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteRelatorioNome] VARCHAR(100) NOT NULL DEFAULT 'Arial'
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioTamanho')
BEGIN
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteRelatorioTamanho] INT NOT NULL DEFAULT 10
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoNome')
BEGIN
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteImpressaoNome] VARCHAR(100) NOT NULL DEFAULT 'Courier New'
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoTamanho')
BEGIN
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteImpressaoTamanho] INT NOT NULL DEFAULT 8
END

GO

-- Atualiza a function ConsultarConfiguracoes para incluir as novas colunas
IF OBJECT_ID('dbo.ConsultarConfiguracoes', 'FN') IS NOT NULL
    DROP FUNCTION dbo.ConsultarConfiguracoes
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
