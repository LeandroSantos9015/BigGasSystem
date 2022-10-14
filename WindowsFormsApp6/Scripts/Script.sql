/****** Object:  StoredProcedure [dbo].[CancelarNotaDeSaida]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CancelarNotaDeSaida]
@Id BIGINT


AS BEGIN
	UPDATE [dbo].[Documento]
		SET 
			[Status]   = 4,
			[Operacao] = 1
			  
		WHERE Id = @id

	UPDATE ItemDocumento 
		SET 
			[Status]   = 4,
			[Operacao] = 1
			  
		WHERE Id = @id

END




GO
/****** Object:  StoredProcedure [dbo].[SalvarCliente]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalvarCliente]

	@Id BIGINT,
	@Nome VARCHAR(MAX),
	@Cpf VARCHAR(MAX),
	@Endereco VARCHAR(MAX),
	@Numero VARCHAR(MAX),
	@Bairro VARCHAR(MAX),
	@Complemento VARCHAR(MAX),
	@Telefone VARCHAR(MAX),
	@Cidade BIGINT,
	@Fornecedor BIT,
	@Obs VARCHAR(MAX)
	


AS BEGIN

DECLARE @Return BIGINT;

	SET NOCOUNT ON;

	IF(@Id IS NULL OR @Id = 0)
	BEGIN
		INSERT INTO [dbo].[Cliente]
           ([Nome]
           ,[Cpf]
           ,[Endereco]
           ,[Numero]
           ,[Bairro]
           ,[Complemento]
           ,[Telefone]
           ,[Cidade]
           ,[Obs]
		   ,[Fornecedor])
		VALUES
           (@Nome
           ,@Cpf
           ,@Endereco
           ,@Numero
           ,@Bairro
           ,@Complemento
           ,@Telefone
           ,@Cidade
           ,@Obs
		   ,@Fornecedor)

		  RETURN SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Cliente]
		   SET [Nome] = @Nome
			  ,[Cpf] = @Cpf
			  ,[Endereco] = @Endereco
			  ,[Numero] = @Numero
			  ,[Bairro] = @Bairro
			  ,[Complemento] = @Complemento
			  ,[Telefone] = @Telefone
			  ,[Cidade] = @Cidade
			  ,[Obs] = @Obs
			  ,[Fornecedor] = @Fornecedor
		 WHERE Id = @Id
	END

	RETURN @Id

	END

	

GO
/****** Object:  StoredProcedure [dbo].[SalvarConfiguracoes]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalvarConfiguracoes]
@ValorFrete DECIMAL(11,2),
@PortaImpressora VARCHAR(MAX)

AS BEGIN

	IF((SELECT COUNT(ValorFrete) FROM Configuracoes) = 1 )
	BEGIN

		UPDATE [dbo].[Configuracoes]
			SET  ValorFrete = @ValorFrete
				,PortaImpressora = @PortaImpressora
	 END
	 ELSE
		INSERT Configuracoes
		SELECT @ValorFrete, @PortaImpressora
END





GO
/****** Object:  StoredProcedure [dbo].[SalvarItemMovimentacao]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalvarItemMovimentacao]
	@Id BIGINT,
	@IdDocumento BIGINT,
	@IdMercadoria BIGINT, 
	@Descricao VARCHAR(MAX),
	@Quantidade DECIMAL(11,2),
	@PrecoCusto DECIMAL(11,2),
	@PrecoVenda DECIMAL(11,2),
	@ValorTotal DECIMAL(11,2),
	@Operacao TINYINT,
	@Status TINYINT

AS BEGIN

	IF(@Id IS NULL OR @Id = 0)
	BEGIN	
		INSERT INTO [dbo].[ItemDocumento]
				   ([IdDocumento],[Descricao],[Quantidade],[PrecoCusto],[PrecoVenda],[ValorTotal],[Operacao], [IdMercadoria], [Status])
			 VALUES
				   (@IdDocumento,@Descricao,@Quantidade,@PrecoCusto,@PrecoVenda,@ValorTotal,@Operacao, @IdMercadoria, @Status)
	END
	ELSE
	BEGIN
	
		UPDATE [dbo].[ItemDocumento]
		   SET [IdDocumento] = @IdDocumento
			  ,[Descricao]   = @Descricao   
			  ,[Quantidade]  = @Quantidade
			  ,[PrecoCusto]  = @PrecoCusto
			  ,[PrecoVenda]  = @PrecoVenda
			  ,[ValorTotal]  = @ValorTotal
			  ,[Operacao]    = @Operacao
			  ,IdMercadoria  = @IdMercadoria
			  ,Status= @Status
		 WHERE Id = @Id

	END
END



GO
/****** Object:  StoredProcedure [dbo].[SalvarMercadoria]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalvarMercadoria]

	@Id BIGINT,
	@Descricao VARCHAR(255),
	@Quantidade DECIMAL(11,2),
	@PrecoCusto	DECIMAL(11,2),
	@PrecoVenda	DECIMAL(11,2)

AS BEGIN

SET NOCOUNT ON;

	IF(@Id IS NULL OR @Id = 0)
	BEGIN
		INSERT INTO [dbo].[Mercadoria]
				   ([Nome]
				   ,[Quantidade]
				   ,[PrecoCusto]
				   ,[PrecoVenda])
			 VALUES
				   (@Descricao
				   ,@Quantidade
				   ,@PrecoCusto
				   ,@PrecoVenda)

		RETURN SCOPE_IDENTITY()
	END

	ELSE
	BEGIN
		UPDATE [dbo].[Mercadoria]
		   SET [Nome]       = @Descricao
			  --,[Quantidade] = @Quantidade
			  ,[PrecoCusto] = @PrecoCusto
			  ,[PrecoVenda] = @PrecoVenda
		 WHERE Id = @Id
		 	 
	END

	RETURN @Id

	END


GO
/****** Object:  StoredProcedure [dbo].[SalvarMovimentacao]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SalvarMovimentacao]
@Id BIGINT,
@Descricao VARCHAR(MAX),
@IdParceiro BIGINT,
@Data DATETIME,
@Status TINYINT,
@Operacao TINYINT,
@DescAcres DECIMAL(11,2),
@ValorTotal DECIMAL(11,2),
@ValorLiquidoTotal DECIMAL(11,2),
@NumeroNota VARCHAR(MAX)

AS BEGIN
	IF(@Id IS NULL OR @Id = 0)
	BEGIN
		INSERT INTO [dbo].[Documento]
				   ([Descricao],[IdParceiro],[Data],[Status],[Operacao],[DescAcres],[ValorTotal],[NumeroNota])
			 VALUES
				   (@Descricao, @IdParceiro, @Data, @Status, @Operacao, @DescAcres, @ValorTotal, @NumeroNota)

		  RETURN SCOPE_IDENTITY()
	END
	ELSE
	BEGIN

		UPDATE [dbo].[Documento]
		   SET [Descricao]  = @Descricao
			  ,[IdParceiro] = @IdParceiro
			  ,[Data] = 	  @Data
			  ,[Status] =	  @Status
			  ,[Operacao] =	  @Operacao
			  ,[DescAcres] =  @DescAcres
			  ,[ValorTotal] = @ValorTotal
			  ,[NumeroNota] = @NumeroNota
		 WHERE Id = @id

	END
	RETURN @Id
END




GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](max) NOT NULL,
	[Cpf] [varchar](max) NOT NULL,
	[Endereco] [varchar](max) NOT NULL,
	[Numero] [varchar](max) NOT NULL,
	[Bairro] [varchar](max) NOT NULL,
	[Complemento] [varchar](max) NULL,
	[Telefone] [varchar](max) NULL,
	[Cidade] [bigint] NOT NULL,
	[Obs] [varchar](max) NULL,
	[Fornecedor] [bit] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Configuracoes]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Configuracoes](
	[ValorFrete] [decimal](11, 2) NOT NULL,
	[PortaImpressora] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Documento]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documento](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](max) NOT NULL,
	[IdParceiro] [bigint] NOT NULL,
	[Data] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Operacao] [tinyint] NOT NULL,
	[DescAcres] [decimal](11, 2) NOT NULL,
	[ValorTotal] [decimal](11, 2) NOT NULL,
	[NumeroNota] [varchar](max) NOT NULL,
	[ValorLiquidoTotal]  AS ([ValorTotal]+[DescAcres]),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemDocumento]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemDocumento](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdDocumento] [bigint] NOT NULL,
	[Descricao] [varchar](max) NOT NULL,
	[Quantidade] [decimal](11, 2) NOT NULL,
	[PrecoCusto] [decimal](11, 2) NOT NULL,
	[PrecoVenda] [decimal](11, 2) NOT NULL,
	[ValorTotal] [decimal](11, 2) NOT NULL,
	[Operacao] [tinyint] NOT NULL,
	[IdMercadoria] [bigint] NOT NULL,
	[Status] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Mercadoria]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mercadoria](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Quantidade] [decimal](11, 2) NOT NULL,
	[PrecoCusto] [decimal](11, 2) NOT NULL,
	[PrecoVenda] [decimal](11, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Mercadoria] UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[ConsultaNotas]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConsultaNotas](@Operacao TINYINT, @Status TINYINT)
RETURNS TABLE AS RETURN
SELECT [Id]
      ,[Descricao]
      ,[IdParceiro]
      ,[Data]
      ,[Status]
      ,[Operacao]
      ,[DescAcres]
      ,[ValorTotal]
      ,[NumeroNota]
	  ,[ValorLiquidoTotal]
  FROM [dbo].[Documento]
  WHERE (@Status = 1 AND Status = 1
	 OR (@Status = 2 AND Status= 2)
	 OR (@Status = 4 AND Status= 4)
	 OR (@Status = 7)) AND Operacao = @Operacao




GO
/****** Object:  UserDefinedFunction [dbo].[ConsultaNotasPorPeriodo]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConsultaNotasPorPeriodo]
(
	@Operacao TINYINT,
	@Status TINYINT, 
	@inicio VARCHAR(MAX), 
	@fim VARCHAR(MAX)
)
RETURNS TABLE AS RETURN
SELECT
	doc.Id,
	cli.Nome,
	doc.Data,
	doc.ValorLiquidoTotal

  FROM [dbo].[Documento] doc
  INNER JOIN Cliente cli
  ON cli.id = doc.IdParceiro
  WHERE
	Data BETWEEN @inicio AND @fim
	AND (@Status = 1 AND Status = 1
	 OR (@Status = 2 AND Status= 2)
	 OR (@Status = 4 AND Status= 4)
	 OR (@Status = 7)) AND Operacao = @Operacao




GO
/****** Object:  UserDefinedFunction [dbo].[ConsultarCliente]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConsultarCliente] ()
RETURNS TABLE AS RETURN
SELECT [Id]
      ,[Nome]
      ,[Cpf]
      ,[Endereco]
      ,[Numero]
      ,[Bairro]
      ,[Complemento]
      ,[Telefone]
      ,[Cidade]
      ,[Obs]
	  ,[Fornecedor]
  FROM [dbo].[Cliente]


/*
@Ativo TINYINT
@Ativo = 1 AND Ativo = 1
			AND ((@ApenasUnitarios = 1 AND mer.Unidade = 2) OR @ApenasUnitarios = 0)
			OR (@Ativo = 2 AND Ativo = 0)
			OR (@Ativo = 4)*/
GO
/****** Object:  UserDefinedFunction [dbo].[ConsultarConfiguracoes]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConsultarConfiguracoes]()
RETURNS TABLE AS RETURN
SELECT [ValorFrete]
      ,[PortaImpressora]
  FROM [dbo].[Configuracoes]



GO
/****** Object:  UserDefinedFunction [dbo].[ConsultarMercadoria]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConsultarMercadoria] ()
RETURNS TABLE AS RETURN
SELECT [Id]
      ,Nome as Descricao
      ,[Quantidade]
      ,[PrecoCusto]
      ,[PrecoVenda]
  FROM [dbo].[Mercadoria]




GO
/****** Object:  UserDefinedFunction [dbo].[ListaMercadoriasNotas]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ListaMercadoriasNotas](@idDocumento BIGINT, @Operacao TINYINT, @Status TINYINT)
RETURNS TABLE AS RETURN
SELECT [Id]
      ,[IdDocumento]
      ,[Descricao]
      ,[Quantidade]
      ,[PrecoCusto]
      ,[PrecoVenda]
      ,[ValorTotal]
      ,[Operacao]
      ,[IdMercadoria]
      ,[Status]
  FROM [dbo].[ItemDocumento]
 WHERE (@Status = 1 AND Status = 1
	 OR (@Status = 0 AND Status= 0)
	 OR (@Status = 7)) AND IdDocumento = @idDocumento


GO
/****** Object:  UserDefinedFunction [dbo].[ListarNotasPorCliente]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ListarNotasPorCliente] (@IdCliente BIGINT)
RETURNS TABLE AS RETURN
SELECT 
doc.Id,
doc.NumeroNota Nota,
CONVERT(DATE, doc.Data) Data,
doc.Operacao

FROM Mercadoria merc
INNER JOIN ItemDocumento item
ON item.IdMercadoria  = merc.Id
INNER JOIN Documento doc
ON doc.Id = item.IdDocumento
WHERE doc.Status = 2
AND item.IdMercadoria = @IdCliente
GROUP BY doc.Id, doc.NumeroNota,
doc.Data, doc.Operacao

GO
/****** Object:  UserDefinedFunction [dbo].[ListarNotasPorMercadoria]    Script Date: 14/10/2022 00:38:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ListarNotasPorMercadoria] (@IdMercadoria BIGINT)
RETURNS TABLE AS RETURN
SELECT 
doc.Id,
doc.NumeroNota Nota,
CONVERT(DATE, doc.Data) Data

FROM Mercadoria merc
INNER JOIN ItemDocumento item
ON item.IdMercadoria  = merc.Id
INNER JOIN Documento doc
ON doc.Id = item.IdDocumento
WHERE doc.Status = 2 AND doc.Operacao = 1
AND item.IdMercadoria = @IdMercadoria
GROUP BY doc.Id, doc.NumeroNota, doc.Data

GO
ALTER TABLE [dbo].[Documento]  WITH CHECK ADD  CONSTRAINT [FK_Parceiro] FOREIGN KEY([IdParceiro])
REFERENCES [dbo].[Cliente] ([Id])
GO
ALTER TABLE [dbo].[Documento] CHECK CONSTRAINT [FK_Parceiro]
GO
ALTER TABLE [dbo].[ItemDocumento]  WITH CHECK ADD  CONSTRAINT [FK_Documento] FOREIGN KEY([IdDocumento])
REFERENCES [dbo].[Documento] ([Id])
GO
ALTER TABLE [dbo].[ItemDocumento] CHECK CONSTRAINT [FK_Documento]
GO
ALTER TABLE [dbo].[ItemDocumento]  WITH CHECK ADD  CONSTRAINT [FK_MercadoriaEntrada] FOREIGN KEY([IdMercadoria])
REFERENCES [dbo].[Mercadoria] ([Id])
GO
ALTER TABLE [dbo].[ItemDocumento] CHECK CONSTRAINT [FK_MercadoriaEntrada]
GO
