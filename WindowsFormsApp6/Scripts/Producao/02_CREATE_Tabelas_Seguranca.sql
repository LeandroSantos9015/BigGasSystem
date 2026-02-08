-- =============================================
-- SCRIPT 02: Criação de Tabelas de Segurança
-- Cria tabelas para controle de usuários e permissões
-- =============================================
-- Execute este script SEGUNDO
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT 'INICIANDO SCRIPT 02: Tabelas de Segurança'
PRINT '=========================================='
GO

-- Tabela de Perfis
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Perfil]') AND type in (N'U'))
BEGIN
    PRINT 'Criando tabela Perfil...'
    CREATE TABLE [dbo].[Perfil](
        [Id] BIGINT IDENTITY(1,1) NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Descricao] VARCHAR(MAX) NULL,
        [Ativo] BIT NOT NULL DEFAULT 1,
        [DataCriacao] DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
    PRINT 'Tabela Perfil criada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Tabela Perfil já existe, pulando...'
END
GO

-- Tabela de Usuários
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
BEGIN
    PRINT 'Criando tabela Usuario...'
    CREATE TABLE [dbo].[Usuario](
        [Id] BIGINT IDENTITY(1,1) NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Login] VARCHAR(50) NOT NULL,
        [Senha] VARCHAR(255) NOT NULL,
        [IdPerfil] BIGINT NOT NULL,
        [Ativo] BIT NOT NULL DEFAULT 1,
        [DataCriacao] DATETIME NOT NULL DEFAULT GETDATE(),
        [UltimoAcesso] DATETIME NULL,
        CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY([IdPerfil]) REFERENCES [dbo].[Perfil] ([Id]),
        CONSTRAINT [UK_Usuario_Login] UNIQUE ([Login])
    )
    PRINT 'Tabela Usuario criada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Tabela Usuario já existe, pulando...'
END
GO

-- Tabela de Menus
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
BEGIN
    PRINT 'Criando tabela Menu...'
    CREATE TABLE [dbo].[Menu](
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Nome] VARCHAR(50) NOT NULL,
        [Descricao] VARCHAR(100) NOT NULL,
        [Chave] VARCHAR(50) NOT NULL,
        [Ordem] INT NOT NULL,
        CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UK_Menu_Chave] UNIQUE ([Chave])
    )
    PRINT 'Tabela Menu criada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Tabela Menu já existe, pulando...'
END
GO

-- Tabela de Permissões (PerfilMenu)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerfilMenu]') AND type in (N'U'))
BEGIN
    PRINT 'Criando tabela PerfilMenu...'
    CREATE TABLE [dbo].[PerfilMenu](
        [Id] BIGINT IDENTITY(1,1) NOT NULL,
        [IdPerfil] BIGINT NOT NULL,
        [IdMenu] INT NOT NULL,
        [Visualizar] BIT NOT NULL DEFAULT 1,
        CONSTRAINT [PK_PerfilMenu] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_PerfilMenu_Perfil] FOREIGN KEY([IdPerfil]) REFERENCES [dbo].[Perfil] ([Id]),
        CONSTRAINT [FK_PerfilMenu_Menu] FOREIGN KEY([IdMenu]) REFERENCES [dbo].[Menu] ([Id]),
        CONSTRAINT [UK_PerfilMenu] UNIQUE ([IdPerfil], [IdMenu])
    )
    PRINT 'Tabela PerfilMenu criada com sucesso!'
END
ELSE
BEGIN
    PRINT 'Tabela PerfilMenu já existe, pulando...'
END
GO

PRINT '=========================================='
PRINT 'SCRIPT 02 CONCLUÍDO COM SUCESSO!'
PRINT '=========================================='
GO
