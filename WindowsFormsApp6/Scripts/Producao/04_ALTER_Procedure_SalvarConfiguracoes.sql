-- =============================================
-- SCRIPT 04: Atualização da Procedure SalvarConfiguracoes
-- Adiciona parâmetros de fonte
-- =============================================
-- Execute este script QUINTO
-- =============================================

USE [venda]
GO

PRINT '=========================================='
PRINT 'INICIANDO SCRIPT 04: Atualizar SalvarConfiguracoes'
PRINT '=========================================='
GO

-- Remove procedure antiga
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'SalvarConfiguracoes' AND type = 'P')
BEGIN
    PRINT 'Removendo procedure SalvarConfiguracoes antiga...'
    DROP PROCEDURE dbo.SalvarConfiguracoes
END
GO

PRINT 'Criando procedure SalvarConfiguracoes atualizada...'
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
		PRINT 'Atualizando configurações existentes...'
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
		PRINT 'Inserindo configurações iniciais...'
		INSERT INTO Configuracoes (ValorFrete, PortaImpressora, MostrarExcluidos, PerguntarImpressora, 
								   FonteRelatorioNome, FonteRelatorioTamanho, FonteImpressaoNome, FonteImpressaoTamanho)
		VALUES (@ValorFrete, @PortaImpressora, @MostrarExcluidos, @PerguntarImpressora, 
			    @FonteRelatorioNome, @FonteRelatorioTamanho, @FonteImpressaoNome, @FonteImpressaoTamanho)
	 END
END
GO

PRINT 'Procedure SalvarConfiguracoes atualizada com sucesso!'
GO

PRINT '=========================================='
PRINT 'SCRIPT 04 CONCLUÍDO COM SUCESSO!'
PRINT '=========================================='
GO
