-- =============================================
-- SCRIPT 04: Procedures e Functions de Segurança
-- Cria/Atualiza procedures e functions do sistema de login
-- =============================================
-- Execute este script QUARTO
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT 'INICIANDO SCRIPT 04: Procedures e Functions'
PRINT '=========================================='
GO

-- =============================================
-- Procedure: AutenticarUsuario
-- =============================================
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'AutenticarUsuario' AND type = 'P')
BEGIN
    PRINT 'Removendo procedure AutenticarUsuario antiga...'
    DROP PROCEDURE dbo.AutenticarUsuario
END
GO

PRINT 'Criando procedure AutenticarUsuario...'
GO

CREATE PROCEDURE [dbo].[AutenticarUsuario]
    @Login VARCHAR(50),
    @Senha VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.[Id],
        u.[Nome],
        u.[Login],
        u.[IdPerfil],
        p.[Nome] AS NomePerfil,
        u.[Ativo]
    FROM [dbo].[Usuario] u
    INNER JOIN [dbo].[Perfil] p ON u.[IdPerfil] = p.[Id]
    WHERE u.[Login] = @Login 
      AND u.[Senha] = @Senha
      AND u.[Ativo] = 1
      AND p.[Ativo] = 1
    
    IF @@ROWCOUNT > 0
    BEGIN
        UPDATE [dbo].[Usuario] 
        SET [UltimoAcesso] = GETDATE() 
        WHERE [Login] = @Login
    END
END
GO

PRINT 'Procedure AutenticarUsuario criada com sucesso!'
GO

-- =============================================
-- Procedure: ListarPermissoesPerfil
-- =============================================
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ListarPermissoesPerfil' AND type = 'P')
BEGIN
    PRINT 'Removendo procedure ListarPermissoesPerfil antiga...'
    DROP PROCEDURE dbo.ListarPermissoesPerfil
END
GO

PRINT 'Criando procedure ListarPermissoesPerfil...'
GO

CREATE PROCEDURE [dbo].[ListarPermissoesPerfil]
    @IdPerfil BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        m.[Id],
        m.[Nome],
        m.[Descricao],
        m.[Chave],
        m.[Ordem],
        ISNULL(pm.[Visualizar], 0) AS Visualizar,
        ISNULL(pm.[Id], 0) AS IdPerfilMenu
    FROM [dbo].[Menu] m
    LEFT JOIN [dbo].[PerfilMenu] pm ON m.[Id] = pm.[IdMenu] AND pm.[IdPerfil] = @IdPerfil
    ORDER BY m.[Ordem]
END
GO

PRINT 'Procedure ListarPermissoesPerfil criada com sucesso!'
GO

-- =============================================
-- Procedure: SalvarPerfil
-- =============================================
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarPerfil' AND type = 'P')
BEGIN
    PRINT 'Removendo procedure SalvarPerfil antiga...'
    DROP PROCEDURE dbo.SalvarPerfil
END
GO

PRINT 'Criando procedure SalvarPerfil...'
GO

CREATE PROCEDURE [dbo].[SalvarPerfil]
    @Id BIGINT,
    @Nome VARCHAR(100),
    @Descricao VARCHAR(MAX),
    @Ativo BIT,
    @Return BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF(@Id IS NULL OR @Id = 0)
    BEGIN
        INSERT INTO [dbo].[Perfil] ([Nome], [Descricao], [Ativo])
        VALUES (@Nome, @Descricao, @Ativo)
        
        SET @Return = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[Perfil]
        SET [Nome] = @Nome,
            [Descricao] = @Descricao,
            [Ativo] = @Ativo
        WHERE [Id] = @Id
        
        SET @Return = @Id
    END
    
    RETURN @Return
END
GO

PRINT 'Procedure SalvarPerfil criada com sucesso!'
GO

-- =============================================
-- Procedure: SalvarPermissaoPerfil
-- =============================================
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarPermissaoPerfil' AND type = 'P')
BEGIN
    PRINT 'Removendo procedure SalvarPermissaoPerfil antiga...'
    DROP PROCEDURE dbo.SalvarPermissaoPerfil
END
GO

PRINT 'Criando procedure SalvarPermissaoPerfil...'
GO

CREATE PROCEDURE [dbo].[SalvarPermissaoPerfil]
    @IdPerfil BIGINT,
    @IdMenu INT,
    @Visualizar BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS(SELECT 1 FROM [dbo].[PerfilMenu] WHERE [IdPerfil] = @IdPerfil AND [IdMenu] = @IdMenu)
    BEGIN
        UPDATE [dbo].[PerfilMenu]
        SET [Visualizar] = @Visualizar
        WHERE [IdPerfil] = @IdPerfil AND [IdMenu] = @IdMenu
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[PerfilMenu] ([IdPerfil], [IdMenu], [Visualizar])
        VALUES (@IdPerfil, @IdMenu, @Visualizar)
    END
END
GO

PRINT 'Procedure SalvarPermissaoPerfil criada com sucesso!'
GO

-- =============================================
-- Procedure: SalvarUsuario
-- =============================================
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarUsuario' AND type = 'P')
BEGIN
    PRINT 'Removendo procedure SalvarUsuario antiga...'
    DROP PROCEDURE dbo.SalvarUsuario
END
GO

PRINT 'Criando procedure SalvarUsuario...'
GO

CREATE PROCEDURE [dbo].[SalvarUsuario]
    @Id BIGINT,
    @Nome VARCHAR(100),
    @Login VARCHAR(50),
    @Senha VARCHAR(255),
    @IdPerfil BIGINT,
    @Ativo BIT,
    @Return BIGINT OUTPUT
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
        BEGIN
            UPDATE [dbo].[Usuario]
            SET [Nome] = @Nome,
                [Login] = @Login,
                [Senha] = @Senha,
                [IdPerfil] = @IdPerfil,
                [Ativo] = @Ativo
            WHERE [Id] = @Id
        END
        ELSE
        BEGIN
            UPDATE [dbo].[Usuario]
            SET [Nome] = @Nome,
                [Login] = @Login,
                [IdPerfil] = @IdPerfil,
                [Ativo] = @Ativo
            WHERE [Id] = @Id
        END
        
        SET @Return = @Id
    END
    
    RETURN @Return
END
GO

PRINT 'Procedure SalvarUsuario criada com sucesso!'
GO

-- =============================================
-- Function: ConsultarPerfis
-- =============================================
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ConsultarPerfis' AND type = 'FN')
BEGIN
    PRINT 'Removendo function ConsultarPerfis antiga...'
    DROP FUNCTION dbo.ConsultarPerfis
END
GO

PRINT 'Criando function ConsultarPerfis...'
GO

CREATE FUNCTION [dbo].[ConsultarPerfis]()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        [Id],
        [Nome],
        [Descricao],
        [Ativo],
        [DataCriacao]
    FROM [dbo].[Perfil]
)
GO

PRINT 'Function ConsultarPerfis criada com sucesso!'
GO

-- =============================================
-- Function: ConsultarUsuarios
-- =============================================
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'ConsultarUsuarios' AND type = 'FN')
BEGIN
    PRINT 'Removendo function ConsultarUsuarios antiga...'
    DROP FUNCTION dbo.ConsultarUsuarios
END
GO

PRINT 'Criando function ConsultarUsuarios...'
GO

CREATE FUNCTION [dbo].[ConsultarUsuarios]()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        u.[Id],
        u.[Nome],
        u.[Login],
        u.[IdPerfil],
        p.[Nome] AS NomePerfil,
        u.[Ativo],
        u.[DataCriacao],
        u.[UltimoAcesso]
    FROM [dbo].[Usuario] u
    INNER JOIN [dbo].[Perfil] p ON u.[IdPerfil] = p.[Id]
)
GO

PRINT 'Function ConsultarUsuarios criada com sucesso!'
GO

PRINT '=========================================='
PRINT 'SCRIPT 03 CONCLUÍDO COM SUCESSO!'
PRINT '=========================================='
GO
