-- =============================================
-- SCRIPT DE ROLLBACK (REVERSÃO)
-- Use este script APENAS se precisar desfazer as alterações
-- =============================================
-- ATENÇÃO: Este script REMOVE dados e tabelas!
-- USE COM CUIDADO!
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT '    INICIANDO ROLLBACK DAS ALTERAÇÕES     '
PRINT '=========================================='
PRINT ''
PRINT 'ATENÇÃO: Este script vai REMOVER:'
PRINT '- Tabelas de segurança (Perfil, Usuario, Menu, PerfilMenu)'
PRINT '- Procedures de segurança'
PRINT '- Functions de segurança'
PRINT '- Campos de fonte da tabela Configuracoes'
PRINT ''
PRINT 'TODOS OS DADOS DE USUÁRIOS E PERFIS SERÃO PERDIDOS!'
PRINT ''
PRINT 'Pressione Ctrl+C para cancelar ou aguarde 10 segundos...'
PRINT ''
GO

WAITFOR DELAY '00:00:10'
GO

PRINT 'COMEÇANDO ROLLBACK...'
GO

-- Remove Procedures
PRINT 'Removendo procedures...'
IF OBJECT_ID('dbo.AutenticarUsuario', 'P') IS NOT NULL DROP PROCEDURE dbo.AutenticarUsuario
IF OBJECT_ID('dbo.ListarPermissoesPerfil', 'P') IS NOT NULL DROP PROCEDURE dbo.ListarPermissoesPerfil
IF OBJECT_ID('dbo.SalvarPerfil', 'P') IS NOT NULL DROP PROCEDURE dbo.SalvarPerfil
IF OBJECT_ID('dbo.SalvarPermissaoPerfil', 'P') IS NOT NULL DROP PROCEDURE dbo.SalvarPermissaoPerfil
IF OBJECT_ID('dbo.SalvarUsuario', 'P') IS NOT NULL DROP PROCEDURE dbo.SalvarUsuario
GO

-- Remove Functions
PRINT 'Removendo functions...'
IF OBJECT_ID('dbo.ConsultarPerfis', 'FN') IS NOT NULL DROP FUNCTION dbo.ConsultarPerfis
IF OBJECT_ID('dbo.ConsultarUsuarios', 'FN') IS NOT NULL DROP FUNCTION dbo.ConsultarUsuarios
GO

-- Remove Tabelas (ordem é importante por causa das FKs)
PRINT 'Removendo tabelas...'
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerfilMenu]') AND type in (N'U'))
    DROP TABLE [dbo].[PerfilMenu]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
    DROP TABLE [dbo].[Usuario]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
    DROP TABLE [dbo].[Menu]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Perfil]') AND type in (N'U'))
    DROP TABLE [dbo].[Perfil]
GO

-- Remove Colunas de Fonte (com cuidado)
PRINT 'Removendo colunas de fonte...'

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoTamanho')
    ALTER TABLE [dbo].[Configuracoes] DROP COLUMN [FonteImpressaoTamanho]

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoNome')
    ALTER TABLE [dbo].[Configuracoes] DROP COLUMN [FonteImpressaoNome]

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioTamanho')
    ALTER TABLE [dbo].[Configuracoes] DROP COLUMN [FonteRelatorioTamanho]

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioNome')
    ALTER TABLE [dbo].[Configuracoes] DROP COLUMN [FonteRelatorioNome]
GO

PRINT ''
PRINT '=========================================='
PRINT '    ROLLBACK CONCLUÍDO!                   '
PRINT '=========================================='
PRINT 'Todas as alterações foram revertidas.'
PRINT '=========================================='
GO
