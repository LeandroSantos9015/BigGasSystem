-- =============================================
-- SCRIPT DE CORREÇÃO - SalvarConfiguracoes
-- Use este script se já executou o 00_EXECUTAR_TUDO.sql
-- e a procedure está com erro no INSERT
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT 'CORRIGINDO PROCEDURE SalvarConfiguracoes'
PRINT '=========================================='
GO

-- Remove procedure com erro
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarConfiguracoes' AND type = 'P')
BEGIN
    PRINT 'Removendo procedure SalvarConfiguracoes...'
    DROP PROCEDURE dbo.SalvarConfiguracoes
END
GO

PRINT 'Criando procedure SalvarConfiguracoes corrigida...'
GO

CREATE PROCEDURE [dbo].[SalvarConfiguracoes]
@ValorFrete DECIMAL(11,2),
@PortaImpressora VARCHAR(MAX),
@MostrarExcluidos BIT,
@PerguntarImpressora BIT,
@FonteRelatorioNome VARCHAR(100),
@FonteRelatorioTamanho INT,
@FonteImpressaoNome VARCHAR(100),
@FonteImpressaoTamanho INT

AS BEGIN

	IF((SELECT COUNT(*) FROM Configuracoes) >= 1 )
	BEGIN
		UPDATE [dbo].[Configuracoes]
			SET  ValorFrete = @ValorFrete
				,PortaImpressora = @PortaImpressora
				,MostrarExcluidos = @MostrarExcluidos
				,PerguntarImpressora = @PerguntarImpressora
				,FonteRelatorioNome = @FonteRelatorioNome
				,FonteRelatorioTamanho = @FonteRelatorioTamanho
				,FonteImpressaoNome = @FonteImpressaoNome
				,FonteImpressaoTamanho = @FonteImpressaoTamanho
	 END
	 ELSE
	 BEGIN
		-- CORRIGIDO: Agora especifica as colunas no INSERT
		INSERT INTO Configuracoes (ValorFrete, PortaImpressora, MostrarExcluidos, PerguntarImpressora, 
								   FonteRelatorioNome, FonteRelatorioTamanho, FonteImpressaoNome, FonteImpressaoTamanho)
		VALUES (@ValorFrete, @PortaImpressora, @MostrarExcluidos, @PerguntarImpressora, 
			    @FonteRelatorioNome, @FonteRelatorioTamanho, @FonteImpressaoNome, @FonteImpressaoTamanho)
	 END
END
GO

PRINT '=========================================='
PRINT 'CORREÇÃO CONCLUÍDA COM SUCESSO!'
PRINT '=========================================='
PRINT 'Agora você pode salvar configurações de fonte normalmente.'
GO
