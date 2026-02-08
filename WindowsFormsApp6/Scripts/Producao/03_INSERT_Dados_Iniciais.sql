-- =============================================
-- SCRIPT 03: Dados Iniciais de Segurança
-- Insere menus do sistema e perfil/usuário administrador
-- =============================================
-- Execute este script TERCEIRO
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT 'INICIANDO SCRIPT 03: Dados Iniciais'
PRINT '=========================================='
GO

-- Inserir Menus do Sistema (apenas se não existir)
IF NOT EXISTS (SELECT * FROM [dbo].[Menu])
BEGIN
    PRINT 'Inserindo menus do sistema...'
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
    PRINT '10 menus inseridos com sucesso!'
END
ELSE
BEGIN
    PRINT 'Menus já existem, pulando inserção...'
END
GO

-- Inserir Perfil Administrador (apenas se não existir)
IF NOT EXISTS (SELECT * FROM [dbo].[Perfil] WHERE Nome = 'Administrador')
BEGIN
    PRINT 'Criando perfil Administrador...'
    
    INSERT INTO [dbo].[Perfil] ([Nome], [Descricao], [Ativo]) 
    VALUES ('Administrador', 'Perfil com acesso total ao sistema', 1)
    
    DECLARE @IdPerfilAdmin BIGINT = SCOPE_IDENTITY()
    PRINT 'Perfil Administrador criado com ID: ' + CAST(@IdPerfilAdmin AS VARCHAR)
    
    -- Dar todas as permissões ao Administrador
    PRINT 'Atribuindo todas as permissões ao Administrador...'
    INSERT INTO [dbo].[PerfilMenu] ([IdPerfil], [IdMenu], [Visualizar])
    SELECT @IdPerfilAdmin, [Id], 1 FROM [dbo].[Menu]
    PRINT 'Permissões atribuídas com sucesso!'
    
    -- Inserir Usuário Administrador (senha: 1234)
    PRINT 'Criando usuário administrador...'
    INSERT INTO [dbo].[Usuario] ([Nome], [Login], [Senha], [IdPerfil], [Ativo]) 
    VALUES ('Administrador', 'administrador', '1234', @IdPerfilAdmin, 1)
    PRINT 'Usuário administrador criado!'
    PRINT 'Login: administrador'
    PRINT 'Senha: 1234'
END
ELSE
BEGIN
    PRINT 'Perfil Administrador já existe, pulando...'
END
GO

PRINT '=========================================='
PRINT 'SCRIPT 03 CONCLUÍDO COM SUCESSO!'
PRINT '=========================================='
GO
