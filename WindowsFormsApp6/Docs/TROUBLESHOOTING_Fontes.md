# 🔍 TROUBLESHOOTING - Fonte Não Aplicada

## 🎯 Problema: "Configurei Comic Sans mas não aparece no relatório"

### **Passo 1: Verifique se está salvando corretamente**

1. Execute o sistema
2. Menu > Configurações
3. Selecione "Comic Sans MS" (ou outra fonte)
4. Clique em **Salvar**
5. **Uma mensagem DEBUG aparecerá** mostrando o que está sendo salvo

**Esperado:**
```
Salvando configurações:

Fonte Relatório: Comic Sans MS (10pt)
Fonte Impressão: Courier New (8pt)
```

**Se aparecer valores diferentes, o ComboBox não está funcionando corretamente.**

---

### **Passo 2: Verifique se salvou no banco de dados**

Execute este SQL no Management Studio:

```sql
USE [venda]
GO

SELECT 
    FonteRelatorioNome,
    FonteRelatorioTamanho,
    FonteImpressaoNome,
    FonteImpressaoTamanho
FROM Configuracoes
```

**Resultado esperado:**
```
FonteRelatorioNome    FonteRelatorioTamanho    FonteImpressaoNome    FonteImpressaoTamanho
------------------    ---------------------    ------------------    ---------------------
Comic Sans MS         10                       Courier New           8
```

**Se aparecer NULL ou valores antigos:**
- ❌ Colunas não existem
- ❌ Procedure não está salvando
- ❌ Script de atualização não foi executado

**Solução:** Execute os scripts de atualização em `Scripts\Producao\`

---

### **Passo 3: Verifique se o cache foi limpo**

Após salvar:
1. **Feche COMPLETAMENTE** o sistema
2. Reabra o sistema
3. Gere uma impressão/relatório

**Por quê?** O cache é limpo ao salvar, mas se houver algum problema, fechar e reabrir garante que vai pegar do banco.

---

### **Passo 4: Verifique se a fonte existe no sistema**

Execute este teste no sistema:

1. Abra FrmConfiguracoes
2. Abra o ComboBox de fontes
3. Procure "Comic Sans MS"

**Se NÃO aparecer:**
- ❌ A fonte não está instalada no Windows
- ✅ Instale a fonte ou escolha outra

---

### **Passo 5: Teste com outra fonte**

Teste com fontes que CERTAMENTE existem:
- ✅ Arial
- ✅ Times New Roman
- ✅ Verdana
- ✅ Calibri

Se funcionar com essas fontes mas não com Comic Sans:
- Problema é a fonte específica
- Windows pode não ter Comic Sans instalada

---

### **Passo 6: Verifique os arquivos**

**Executou os scripts SQL?**
- [ ] `01_ALTER_Configuracoes_Fontes.sql`
- [ ] `04_ALTER_Procedure_SalvarConfiguracoes.sql`
- [ ] `05_ALTER_Function_ConsultarConfiguracoes.sql`

**OU**

- [ ] `00_EXECUTAR_TUDO.sql` (executa todos)

**Se NÃO executou:** As colunas não existem no banco!

---

## 🔧 Soluções Rápidas:

### **Solução 1: Executar Scripts SQL**
```sql
-- Execute na ordem:
Scripts\Producao\01_ALTER_Configuracoes_Fontes.sql
Scripts\Producao\04_ALTER_Procedure_SalvarConfiguracoes.sql
Scripts\Producao\05_ALTER_Function_ConsultarConfiguracoes.sql
```

### **Solução 2: Limpar Cache Manualmente**
```csharp
// Adicione temporariamente no Form_Load de qualquer tela:
FonteHelper.LimparCache();
MessageBox.Show("Cache limpo!");
```

### **Solução 3: Verificar Procedure**
```sql
-- Execute este teste:
DECLARE @ValorFrete DECIMAL(11,2) = 0
DECLARE @PortaImpressora VARCHAR(MAX) = 'LPT1'
DECLARE @MostrarExcluidos BIT = 0
DECLARE @PerguntarImpressora BIT = 0
DECLARE @FonteRelatorioNome VARCHAR(100) = 'Comic Sans MS'
DECLARE @FonteRelatorioTamanho INT = 10
DECLARE @FonteImpressaoNome VARCHAR(100) = 'Courier New'
DECLARE @FonteImpressaoTamanho INT = 8

EXEC SalvarConfiguracoes 
    @ValorFrete, 
    @PortaImpressora, 
    @MostrarExcluidos, 
    @PerguntarImpressora,
    @FonteRelatorioNome,
    @FonteRelatorioTamanho,
    @FonteImpressaoNome,
    @FonteImpressaoTamanho

-- Verifica se salvou:
SELECT * FROM Configuracoes
```

---

## 📊 Checklist de Verificação:

### **Banco de Dados:**
- [ ] Scripts SQL executados
- [ ] Colunas existem na tabela Configuracoes
- [ ] Procedure SalvarConfiguracoes atualizada
- [ ] Function ConsultarConfiguracoes atualizada

### **Aplicação:**
- [ ] System recompilado após mudanças
- [ ] Fonte selecionada no ComboBox
- [ ] Mensagem DEBUG apareceu ao salvar
- [ ] Cache limpo após salvar

### **Sistema Operacional:**
- [ ] Fonte instalada no Windows
- [ ] Fonte aparece no ComboBox do sistema

---

## 🎯 Teste Final:

Execute este teste completo:

1. **Feche TUDO** (sistema e Management Studio)
2. **Execute** o script de verificação:
   ```sql
   SELECT FonteRelatorioNome FROM Configuracoes
   ```
3. **Se retornar NULL:**
   - Execute `00_EXECUTAR_TUDO.sql`
4. **Se retornar valor antigo:**
   - Configure novamente
   - Salve
   - Verifique mensagem DEBUG
5. **Reabra sistema**
6. **Gere relatório**
7. **Verifique fonte**

---

## 💡 Dica Importante:

**Comic Sans MS** pode estar escrita de formas diferentes:
- "Comic Sans MS" ✅
- "Comic Sans" ❌
- "ComicSans" ❌

No ComboBox deve aparecer **exatamente** como está instalada no Windows.

---

## 🚨 Erro Comum:

**Se você configurou MAS fechou sem salvar:**
- ❌ Configuração perdida
- ✅ Configure novamente e clique em **SALVAR**

**Se você salvou MAS não fechou o relatório anterior:**
- ❌ Relatório pode ter cache
- ✅ Feche E reabra o sistema

---

## 📞 Ainda não funcionou?

Envie print screens de:
1. ✉️ Mensagem DEBUG ao salvar
2. ✉️ Resultado do SELECT no banco
3. ✉️ ComboBox mostrando a fonte selecionada
4. ✉️ Relatório impresso/visualizado

---

**🔍 Use o arquivo `DEBUG_VerificarFontes.sql` para diagnóstico!**
