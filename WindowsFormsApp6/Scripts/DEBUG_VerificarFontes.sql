-- =============================================
-- SCRIPT DE TESTE - Verificar Fonte Salva
-- Execute este script para ver se as fontes foram salvas
-- =============================================

USE [venda]
GO

-- Mostra as configurações de fonte atuais
SELECT 
    FonteRelatorioNome,
    FonteRelatorioTamanho,
    FonteImpressaoNome,
    FonteImpressaoTamanho
FROM Configuracoes

-- Se aparecer NULL ou valores errados, as colunas podem não existir ainda
-- Execute o script de criação das colunas

-- Verifica se as colunas existem
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Configuracoes'
  AND COLUMN_NAME LIKE 'Fonte%'
ORDER BY COLUMN_NAME
