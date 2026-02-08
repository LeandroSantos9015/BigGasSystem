-- =============================================
-- SCRIPT MESTRE - EXECUÇÃO COMPLETA
-- Execute todos os scripts de atualização em sequência
-- =============================================
-- IMPORTANTE: Revise cada script antes de executar!
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT '    INICIANDO ATUALIZAÇÃO DO BANCO        '
PRINT '=========================================='
PRINT ''
PRINT 'Este script executará 5 atualizações:'
PRINT '01 - Adicionar campos de fonte em Configuracoes'
PRINT '02 - Criar tabelas de segurança'
PRINT '03 - Inserir dados iniciais de segurança'
PRINT '04 - Atualizar procedure SalvarConfiguracoes'
PRINT '05 - Atualizar function ConsultarConfiguracoes'
PRINT ''
PRINT 'Pressione Ctrl+C para cancelar ou aguarde 5 segundos...'
PRINT ''
GO

WAITFOR DELAY '00:00:05'
GO

PRINT '=========================================='
PRINT 'COMEÇANDO EXECUÇÃO...'
PRINT '=========================================='
GO

-- =============================================
-- SCRIPT 01: Alteração da Tabela Configuracoes
-- =============================================
PRINT ''
PRINT '>>> EXECUTANDO SCRIPT 01 <<<'
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioNome')
BEGIN
    ALTER TABLE [dbo].[Configuracoes] ADD [FonteRelatorioNome] VARCHAR(100) NOT NULL DEFAULT 'Arial'
    PRINT '✓ Coluna FonteRelatorioNome adicionada'
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteRelatorioTamanho')
BEGIN
    ALTER TABLE [dbo].[Configuracoes] ADD [FonteRelatorioTamanho] INT NOT NULL DEFAULT 10
    PRINT '✓ Coluna FonteRelatorioTamanho adicionada'
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoNome')
BEGIN
    ALTER TABLE [dbo].[Configuracoes] ADD [FonteImpressaoNome] VARCHAR(100) NOT NULL DEFAULT 'Courier New'
    PRINT '✓ Coluna FonteImpressaoNome adicionada'
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Configuracoes]') AND name = 'FonteImpressaoTamanho')
BEGIN
    ALTER TABLE [dbo].[Configuracoes] ADD [FonteImpressaoTamanho] INT NOT NULL DEFAULT 8
    PRINT '✓ Coluna FonteImpressaoTamanho adicionada'
END
GO

PRINT '>>> SCRIPT 01 CONCLUÍDO <<<'
PRINT ''
GO

-- =============================================
-- SCRIPT 02: Criação de Tabelas de Segurança
-- =============================================
PRINT '>>> EXECUTANDO SCRIPT 02 <<<'
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Perfil]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Perfil](
        [Id] BIGINT IDENTITY(1,1) NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Descricao] VARCHAR(MAX) NULL,
        [Ativo] BIT NOT NULL DEFAULT 1,
        [DataCriacao] DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
    PRINT '✓ Tabela Perfil criada'
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
BEGIN
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
    PRINT '✓ Tabela Usuario criada'
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Menu](
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Nome] VARCHAR(50) NOT NULL,
        [Descricao] VARCHAR(100) NOT NULL,
        [Chave] VARCHAR(50) NOT NULL,
        [Ordem] INT NOT NULL,
        CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UK_Menu_Chave] UNIQUE ([Chave])
    )
    PRINT '✓ Tabela Menu criada'
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerfilMenu]') AND type in (N'U'))
BEGIN
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
    PRINT '✓ Tabela PerfilMenu criada'
END
GO

PRINT '>>> SCRIPT 02 CONCLUÍDO <<<'
PRINT ''
GO

-- =============================================
-- SCRIPT 03: Dados Iniciais
-- =============================================
PRINT '>>> EXECUTANDO SCRIPT 03 <<<'
GO

IF NOT EXISTS (SELECT * FROM [dbo].[Menu])
BEGIN
    INSERT INTO [dbo].[Menu] ([Nome], [Descricao], [Chave], [Ordem]) VALUES
    ('Clientes', 'Cadastro de Clientes', 'MENU_CLIENTES', 1),
    ('Mercadorias', 'Cadastro de Mercadorias', 'MENU_MERCADORIAS', 2),
    ('Entradas', 'Movimentação de Entradas', 'MENU_ENTRADAS', 3),
    ('Saídas', 'Movimentação de Saídas', 'MENU_SAIDAS', 4),
    ('Cancelamento', 'Cancelamento de Saídas', 'MENU_CANCELAMENTO', 5),
    ('Configurações', 'Configurações do Sistema', 'MENU_CONFIGURACOES', 6),
    ('Relatórios', 'Relatórios do Sistema', 'MENU_RELATORIOS', 7),
    ('Importar', 'Importação de Dados', 'MENU_IMPORTAR', 8),
    ('Perfis', 'Cadastro de Perfis', 'MENU_PERFIS', 9),
    ('Usuários', 'Cadastro de Usuários', 'MENU_USUARIOS', 10)
    PRINT '✓ 10 menus inseridos'
END

IF NOT EXISTS (SELECT * FROM [dbo].[Perfil] WHERE Nome = 'Administrador')
BEGIN
    INSERT INTO [dbo].[Perfil] ([Nome], [Descricao], [Ativo]) 
    VALUES ('Administrador', 'Perfil com acesso total ao sistema', 1)
    
    DECLARE @IdPerfilAdmin BIGINT = SCOPE_IDENTITY()
    
    INSERT INTO [dbo].[PerfilMenu] ([IdPerfil], [IdMenu], [Visualizar])
    SELECT @IdPerfilAdmin, [Id], 1 FROM [dbo].[Menu]
    
    INSERT INTO [dbo].[Usuario] ([Nome], [Login], [Senha], [IdPerfil], [Ativo]) 
    VALUES ('Administrador', 'administrador', '1234', @IdPerfilAdmin, 1)
    
    PRINT '✓ Perfil Administrador criado'
    PRINT '✓ Usuário administrador criado (login: administrador, senha: 1234)'
END
GO

PRINT '>>> SCRIPT 03 CONCLUÍDO <<<'
PRINT ''
GO

-- =============================================
-- SCRIPT 04: Procedures de Segurança
-- =============================================
PRINT '>>> EXECUTANDO SCRIPT 04 <<<'
GO

-- Remove procedures antigas
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'AutenticarUsuario' AND type = 'P')
    DROP PROCEDURE dbo.AutenticarUsuario
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ListarPermissoesPerfil' AND type = 'P')
    DROP PROCEDURE dbo.ListarPermissoesPerfil
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarPerfil' AND type = 'P')
    DROP PROCEDURE dbo.SalvarPerfil
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarPermissaoPerfil' AND type = 'P')
    DROP PROCEDURE dbo.SalvarPermissaoPerfil
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarUsuario' AND type = 'P')
    DROP PROCEDURE dbo.SalvarUsuario
GO

-- Cria AutenticarUsuario
CREATE PROCEDURE [dbo].[AutenticarUsuario]
    @Login VARCHAR(50), @Senha VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.[Id], u.[Nome], u.[Login], u.[IdPerfil], p.[Nome] AS NomePerfil, u.[Ativo]
    FROM [dbo].[Usuario] u
    INNER JOIN [dbo].[Perfil] p ON u.[IdPerfil] = p.[Id]
    WHERE u.[Login] = @Login AND u.[Senha] = @Senha AND u.[Ativo] = 1 AND p.[Ativo] = 1
    
    IF @@ROWCOUNT > 0
        UPDATE [dbo].[Usuario] SET [UltimoAcesso] = GETDATE() WHERE [Login] = @Login
END
GO

-- Cria ListarPermissoesPerfil
CREATE PROCEDURE [dbo].[ListarPermissoesPerfil]
    @IdPerfil BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT m.[Id], m.[Nome], m.[Descricao], m.[Chave], m.[Ordem],
           ISNULL(pm.[Visualizar], 0) AS Visualizar, ISNULL(pm.[Id], 0) AS IdPerfilMenu
    FROM [dbo].[Menu] m
    LEFT JOIN [dbo].[PerfilMenu] pm ON m.[Id] = pm.[IdMenu] AND pm.[IdPerfil] = @IdPerfil
    ORDER BY m.[Ordem]
END
GO

-- Cria SalvarPerfil
CREATE PROCEDURE [dbo].[SalvarPerfil]
    @Id BIGINT, @Nome VARCHAR(100), @Descricao VARCHAR(MAX), @Ativo BIT, @Return BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    IF(@Id IS NULL OR @Id = 0)
    BEGIN
        INSERT INTO [dbo].[Perfil] ([Nome], [Descricao], [Ativo]) VALUES (@Nome, @Descricao, @Ativo)
        SET @Return = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[Perfil] SET [Nome] = @Nome, [Descricao] = @Descricao, [Ativo] = @Ativo WHERE [Id] = @Id
        SET @Return = @Id
    END
    RETURN @Return
END
GO

-- Cria SalvarPermissaoPerfil
CREATE PROCEDURE [dbo].[SalvarPermissaoPerfil]
    @IdPerfil BIGINT, @IdMenu INT, @Visualizar BIT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM [dbo].[PerfilMenu] WHERE [IdPerfil] = @IdPerfil AND [IdMenu] = @IdMenu)
        UPDATE [dbo].[PerfilMenu] SET [Visualizar] = @Visualizar WHERE [IdPerfil] = @IdPerfil AND [IdMenu] = @IdMenu
    ELSE
        INSERT INTO [dbo].[PerfilMenu] ([IdPerfil], [IdMenu], [Visualizar]) VALUES (@IdPerfil, @IdMenu, @Visualizar)
END
GO

-- Cria SalvarUsuario
CREATE PROCEDURE [dbo].[SalvarUsuario]
    @Id BIGINT, @Nome VARCHAR(100), @Login VARCHAR(50), @Senha VARCHAR(255), 
    @IdPerfil BIGINT, @Ativo BIT, @Return BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    IF(@Id IS NULL OR @Id = 0)
    BEGIN
        INSERT INTO [dbo].[Usuario] ([Nome], [Login], [Senha], [IdPerfil], [Ativo])
        VALUES (@Nome, @Login, @Senha, @IdPerfil, @Ativo)
        SET @Return = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        IF(LEN(@Senha) > 0)
            UPDATE [dbo].[Usuario] SET [Nome] = @Nome, [Login] = @Login, [Senha] = @Senha, 
                   [IdPerfil] = @IdPerfil, [Ativo] = @Ativo WHERE [Id] = @Id
        ELSE
            UPDATE [dbo].[Usuario] SET [Nome] = @Nome, [Login] = @Login, 
                   [IdPerfil] = @IdPerfil, [Ativo] = @Ativo WHERE [Id] = @Id
        SET @Return = @Id
    END
    RETURN @Return
END
GO

PRINT '✓ Procedures de segurança criadas'
GO

-- Remove functions antigas
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ConsultarPerfis' AND type = 'FN')
    DROP FUNCTION dbo.ConsultarPerfis
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ConsultarUsuarios' AND type = 'FN')
    DROP FUNCTION dbo.ConsultarUsuarios
GO

-- Cria ConsultarPerfis
CREATE FUNCTION [dbo].[ConsultarPerfis]()
RETURNS TABLE AS RETURN
(
    SELECT [Id], [Nome], [Descricao], [Ativo], [DataCriacao] FROM [dbo].[Perfil]
)
GO

-- Cria ConsultarUsuarios
CREATE FUNCTION [dbo].[ConsultarUsuarios]()
RETURNS TABLE AS RETURN
(
    SELECT u.[Id], u.[Nome], u.[Login], u.[IdPerfil], p.[Nome] AS NomePerfil,
           u.[Ativo], u.[DataCriacao], u.[UltimoAcesso]
    FROM [dbo].[Usuario] u
    INNER JOIN [dbo].[Perfil] p ON u.[IdPerfil] = p.[Id]
)
GO

PRINT '✓ Functions de segurança criadas'
GO

PRINT '>>> SCRIPT 04 CONCLUÍDO <<<'
PRINT ''
GO

-- =============================================
-- SCRIPT 05: Atualizar SalvarConfiguracoes
-- =============================================
PRINT '>>> EXECUTANDO SCRIPT 05 <<<'
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarConfiguracoes' AND type = 'P')
    DROP PROCEDURE dbo.SalvarConfiguracoes
GO

CREATE PROCEDURE [dbo].[SalvarConfiguracoes]
@ValorFrete DECIMAL(11,2), @PortaImpressora VARCHAR(MAX), @MostrarExcluidos BIT, @PerguntarImpressora BIT,
@FonteRelatorioNome VARCHAR(100), @FonteRelatorioTamanho INT, 
@FonteImpressaoNome VARCHAR(100), @FonteImpressaoTamanho INT
AS BEGIN
	IF((SELECT COUNT(*) FROM Configuracoes) >= 1 )
		UPDATE [dbo].[Configuracoes]
			SET ValorFrete = @ValorFrete, PortaImpressora = @PortaImpressora,
				MostrarExcluidos = @MostrarExcluidos, PerguntarImpressora = @PerguntarImpressora,
				FonteRelatorioNome = @FonteRelatorioNome, FonteRelatorioTamanho = @FonteRelatorioTamanho,
				FonteImpressaoNome = @FonteImpressaoNome, FonteImpressaoTamanho = @FonteImpressaoTamanho
	ELSE
		INSERT INTO Configuracoes (ValorFrete, PortaImpressora, MostrarExcluidos, PerguntarImpressora,
								   FonteRelatorioNome, FonteRelatorioTamanho, FonteImpressaoNome, FonteImpressaoTamanho)
		VALUES (@ValorFrete, @PortaImpressora, @MostrarExcluidos, @PerguntarImpressora,
			    @FonteRelatorioNome, @FonteRelatorioTamanho, @FonteImpressaoNome, @FonteImpressaoTamanho)
END
GO

PRINT '✓ Procedure SalvarConfiguracoes atualizada'
GO

-- Atualizar ConsultarConfiguracoes
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ConsultarConfiguracoes' AND type = 'FN')
    DROP FUNCTION dbo.ConsultarConfiguracoes
GO

CREATE FUNCTION [dbo].[ConsultarConfiguracoes]()
RETURNS TABLE AS RETURN
SELECT [ValorFrete], [PortaImpressora], [MostrarExcluidos], [PerguntarImpressora],
       [FonteRelatorioNome], [FonteRelatorioTamanho], [FonteImpressaoNome], [FonteImpressaoTamanho]
FROM [dbo].[Configuracoes]
GO

PRINT '✓ Function ConsultarConfiguracoes atualizada'
GO

PRINT '>>> SCRIPT 05 CONCLUÍDO <<<'
PRINT ''
GO

PRINT '=========================================='
PRINT '    ATUALIZAÇÃO CONCLUÍDA COM SUCESSO!    '
PRINT '=========================================='
PRINT ''
PRINT 'Resumo das alterações:'
PRINT '✓ Tabela Configuracoes atualizada (4 campos de fonte)'
PRINT '✓ 4 Tabelas de segurança criadas'
PRINT '✓ 10 Menus cadastrados'
PRINT '✓ Perfil Administrador criado'
PRINT '✓ Usuário administrador criado'
PRINT '✓ 5 Procedures criadas'
PRINT '✓ 2 Functions criadas'
PRINT ''
PRINT 'Login padrão: administrador / 1234'
PRINT '=========================================='
GO
