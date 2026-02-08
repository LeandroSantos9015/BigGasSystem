-- =============================================
-- SCRIPT DE VERIFICAÇÃO PRÉ-EXECUÇÃO
-- Execute este script ANTES dos scripts de atualização
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT '    VERIFICAÇÃO PRÉ-ATUALIZAÇÃO           '
PRINT '=========================================='
PRINT ''

-- Verifica versão do SQL Server
PRINT '1. Versão do SQL Server:'
SELECT @@VERSION
PRINT ''

-- Verifica se o banco existe
PRINT '2. Verificando banco de dados...'
IF DB_ID('venda') IS NOT NULL
    PRINT '✓ Banco [venda] encontrado'
ELSE
    PRINT '✗ ERRO: Banco [venda] NÃO encontrado!'
PRINT ''

-- Verifica tabela Configuracoes
PRINT '3. Verificando tabela Configuracoes...'
IF OBJECT_ID('dbo.Configuracoes', 'U') IS NOT NULL
BEGIN
    PRINT '✓ Tabela Configuracoes existe'
    
    -- Lista colunas atuais
    PRINT 'Colunas atuais:'
    SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Configuracoes'
    ORDER BY ORDINAL_POSITION
END
ELSE
    PRINT '✗ ERRO: Tabela Configuracoes NÃO encontrada!'
PRINT ''

-- Verifica se já tem tabelas de segurança
PRINT '4. Verificando se tabelas de segurança já existem...'
IF OBJECT_ID('dbo.Perfil', 'U') IS NOT NULL PRINT '⚠ Tabela Perfil JÁ EXISTE'
IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL PRINT '⚠ Tabela Usuario JÁ EXISTE'
IF OBJECT_ID('dbo.Menu', 'U') IS NOT NULL PRINT '⚠ Tabela Menu JÁ EXISTE'
IF OBJECT_ID('dbo.PerfilMenu', 'U') IS NOT NULL PRINT '⚠ Tabela PerfilMenu JÁ EXISTE'
PRINT ''

-- Verifica se colunas de fonte já existem
PRINT '5. Verificando colunas de fonte...'
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioNome')
    PRINT '⚠ Coluna FonteRelatorioNome JÁ EXISTE'
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioTamanho')
    PRINT '⚠ Coluna FonteRelatorioTamanho JÁ EXISTE'
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoNome')
    PRINT '⚠ Coluna FonteImpressaoNome JÁ EXISTE'
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoTamanho')
    PRINT '⚠ Coluna FonteImpressaoTamanho JÁ EXISTE'
PRINT ''

-- Verifica espaço em disco
PRINT '6. Verificando espaço em disco...'
EXEC sp_spaceused
PRINT ''

-- Conta registros em tabelas principais
PRINT '7. Contagem de registros:'
SELECT 'Cliente' AS Tabela, COUNT(*) AS Total FROM Cliente
UNION ALL
SELECT 'Mercadoria', COUNT(*) FROM Mercadoria
UNION ALL
SELECT 'Documento', COUNT(*) FROM Documento
UNION ALL
SELECT 'ItemDocumento', COUNT(*) FROM ItemDocumento
UNION ALL
SELECT 'Configuracoes', COUNT(*) FROM Configuracoes
PRINT ''

PRINT '=========================================='
PRINT '    VERIFICAÇÃO CONCLUÍDA                 '
PRINT '=========================================='
PRINT ''
PRINT 'Se tudo estiver OK, prossiga com a execução dos scripts.'
PRINT 'Caso contrário, corrija os problemas antes de continuar.'
PRINT ''
GO
