-- =============================================
-- Script de Login e Controle de Acesso
-- =============================================

-- Tabela de Perfis
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
END
GO

-- Tabela de Usuários
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
END
GO

-- Tabela de Menus
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
END
GO

-- Tabela de Permissões
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
END
GO

-- Inserir Menus do Sistema
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
END
GO

-- Inserir Perfil Administrador
IF NOT EXISTS (SELECT * FROM [dbo].[Perfil] WHERE Nome = 'Administrador')
BEGIN
    INSERT INTO [dbo].[Perfil] ([Nome], [Descricao], [Ativo]) 
    VALUES ('Administrador', 'Perfil com acesso total ao sistema', 1)
    
    DECLARE @IdPerfilAdmin BIGINT = SCOPE_IDENTITY()
    
    -- Dar todas as permissões ao Administrador
    INSERT INTO [dbo].[PerfilMenu] ([IdPerfil], [IdMenu], [Visualizar])
    SELECT @IdPerfilAdmin, [Id], 1 FROM [dbo].[Menu]
    
    -- Inserir Usuário Administrador (senha: 1234)
    INSERT INTO [dbo].[Usuario] ([Nome], [Login], [Senha], [IdPerfil], [Ativo]) 
    VALUES ('Administrador', 'administrador', '1234', @IdPerfilAdmin, 1)
END
GO

-- Procedures
IF OBJECT_ID('dbo.AutenticarUsuario', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AutenticarUsuario
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

IF OBJECT_ID('dbo.ListarPermissoesPerfil', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ListarPermissoesPerfil
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

IF OBJECT_ID('dbo.SalvarPerfil', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SalvarPerfil
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

IF OBJECT_ID('dbo.SalvarPermissaoPerfil', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SalvarPermissaoPerfil
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

IF OBJECT_ID('dbo.SalvarUsuario', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SalvarUsuario
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

IF OBJECT_ID('dbo.ConsultarPerfis', 'FN') IS NOT NULL
    DROP FUNCTION dbo.ConsultarPerfis
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

IF OBJECT_ID('dbo.ConsultarUsuarios', 'FN') IS NOT NULL
    DROP FUNCTION dbo.ConsultarUsuarios
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
