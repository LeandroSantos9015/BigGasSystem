-- =============================================
-- SCRIPT 01: Alteração da Tabela Configuracoes
-- Adiciona campos de configuração de fontes
-- =============================================
-- Execute este script PRIMEIRO
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT 'INICIANDO SCRIPT 01: Configurações de Fonte'
PRINT '=========================================='
GO

-- Adiciona coluna FonteRelatorioNome
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioNome')
BEGIN
    PRINT 'Adicionando coluna FonteRelatorioNome...'
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteRelatorioNome] VARCHAR(100) NOT NULL DEFAULT 'Arial'
    PRINT 'Coluna FonteRelatorioNome adicionada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Coluna FonteRelatorioNome já existe, pulando...'
END
GO

-- Adiciona coluna FonteRelatorioTamanho
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioTamanho')
BEGIN
    PRINT 'Adicionando coluna FonteRelatorioTamanho...'
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteRelatorioTamanho] INT NOT NULL DEFAULT 10
    PRINT 'Coluna FonteRelatorioTamanho adicionada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Coluna FonteRelatorioTamanho já existe, pulando...'
END
GO

-- Adiciona coluna FonteImpressaoNome
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoNome')
BEGIN
    PRINT 'Adicionando coluna FonteImpressaoNome...'
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteImpressaoNome] VARCHAR(100) NOT NULL DEFAULT 'Courier New'
    PRINT 'Coluna FonteImpressaoNome adicionada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Coluna FonteImpressaoNome já existe, pulando...'
END
GO

-- Adiciona coluna FonteImpressaoTamanho
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoTamanho')
BEGIN
    PRINT 'Adicionando coluna FonteImpressaoTamanho...'
    ALTER TABLE [dbo].[Configuracoes]
    ADD [FonteImpressaoTamanho] INT NOT NULL DEFAULT 8
    PRINT 'Coluna FonteImpressaoTamanho adicionada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Coluna FonteImpressaoTamanho já existe, pulando...'
END
GO

PRINT '=========================================='
PRINT 'SCRIPT 01 CONCLUÍDO COM SUCESSO!'
PRINT '=========================================='
GO
