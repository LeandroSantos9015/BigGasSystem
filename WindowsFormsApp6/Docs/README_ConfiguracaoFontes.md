# Configuração de Fontes para Relatórios e Impressões

## 📋 Visão Geral

Sistema de configuração de fontes separadas para:
- **Relatórios (XtraReports)**: Visualização em tela, PDF e impressoras comuns
- **Impressões Matriciais**: Saídas em impressoras matriciais (melhor legibilidade)

---

## 🗄️ Estrutura do Banco de Dados

### Campos Adicionados na Tabela `Configuracoes`:

| Campo | Tipo | Padrão | Descrição |
|-------|------|--------|-----------|
| `FonteRelatorioNome` | VARCHAR(100) | 'Arial' | Nome da fonte para relatórios |
| `FonteRelatorioTamanho` | INT | 10 | Tamanho da fonte para relatórios |
| `FonteImpressaoNome` | VARCHAR(100) | 'Courier New' | Nome da fonte para impressões |
| `FonteImpressaoTamanho` | INT | 8 | Tamanho da fonte para impressões |

---

## 🚀 Instalação

### 1. Execute o Script SQL

Execute o arquivo `ScriptConfiguracaoFontes.sql` no seu banco de dados:

```sql
-- Este script adiciona as colunas de fonte à tabela Configuracoes
-- E atualiza a function ConsultarConfiguracoes
```

### 2. Compile o Projeto

O projeto já foi compilado com sucesso e está pronto para uso.

---

## 💻 Como Usar

### Na Tela de Configurações

1. Abra **Menu > Configurações**
2. Na seção **"Configurações de Fonte"**:
   - **Fonte Relatórios**: Selecione a fonte para XtraReports (ex: Arial, Verdana)
   - **Tamanho**: Defina o tamanho (6-72)
   - **Fonte Impressão**: Selecione a fonte para impressoras matriciais (ex: Courier New)
   - **Tamanho**: Defina o tamanho (6-24)
3. Clique em **Salvar**

### No Código - XtraReports

```csharp
using WindowsFormsApp6.Utilitarios;

// Obtém a fonte configurada para relatórios
Font fonteRelatorio = FonteHelper.ObterFonteRelatorio();

// Aplica em um controle do XtraReport
xrLabel1.Font = fonteRelatorio;

// Ou obtenha nome e tamanho separadamente
string nomeFonte = FonteHelper.ObterNomeFonteRelatorio();
int tamanhoFonte = FonteHelper.ObterTamanhoFonteRelatorio();
Font fonteCustomizada = new Font(nomeFonte, tamanhoFonte, FontStyle.Bold);
```

### No Código - Impressão Matricial

```csharp
using WindowsFormsApp6.Utilitarios;

// Obtém a fonte configurada para impressão
Font fonteImpressao = FonteHelper.ObterFonteImpressao();

// Usa em Graphics
e.Graphics.DrawString("Texto", fonteImpressao, Brushes.Black, x, y);

// Ou obtenha nome e tamanho separadamente
string nomeFonte = FonteHelper.ObterNomeFonteImpressao();
int tamanhoFonte = FonteHelper.ObterTamanhoFonteImpressao();
```

---

## 🎨 Fontes Recomendadas

### Para Relatórios (Visualização/PDF):
- **Arial** - Boa legibilidade em tela
- **Verdana** - Otimizada para tela
- **Tahoma** - Compacta e legível
- **Calibri** - Moderna e clean

**Tamanho recomendado**: 9-11 pontos

### Para Impressão Matricial:
- **Courier New** ⭐ Recomendada - Monoespaçada
- **Consolas** - Monoespaçada moderna
- **Lucida Console** - Boa legibilidade
- **Fixedsys** - Clássica para matricial

**Tamanho recomendado**: 7-9 pontos

---

## 📊 Exemplo Completo

```csharp
using System;
using System.Drawing;
using System.Drawing.Printing;
using WindowsFormsApp6.Utilitarios;

public class ExemploImpressao
{
    private void ImprimirDocumento()
    {
        PrintDocument pd = new PrintDocument();
        pd.PrintPage += new PrintPageEventHandler(PrintPage);
        
        // Define impressora matricial
        pd.PrinterSettings.PrinterName = "Sua Impressora Matricial";
        
        pd.Print();
    }

    private void PrintPage(object sender, PrintPageEventArgs e)
    {
        // Obtém fonte configurada para impressão matricial
        Font fonteImpressao = FonteHelper.ObterFonteImpressao();
        
        float yPos = 0;
        int leftMargin = e.MarginBounds.Left;
        int topMargin = e.MarginBounds.Top;
        
        // Cabeçalho
        e.Graphics.DrawString("=== NOTA FISCAL ===", fonteImpressao, Brushes.Black, leftMargin, topMargin + yPos);
        yPos += fonteImpressao.GetHeight(e.Graphics);
        
        // Dados
        e.Graphics.DrawString("Cliente: João Silva", fonteImpressao, Brushes.Black, leftMargin, topMargin + yPos);
        yPos += fonteImpressao.GetHeight(e.Graphics);
        
        e.Graphics.DrawString("Total: R$ 150,00", fonteImpressao, Brushes.Black, leftMargin, topMargin + yPos);
    }
}
```

---

## 🔧 Arquivos Modificados/Criados

### Criados:
- `WindowsFormsApp6\Scripts\ScriptConfiguracaoFontes.sql`
- `WindowsFormsApp6\Utilitarios\FonteHelper.cs`
- `WindowsFormsApp6\Exemplos\ExemploUsoFontes.cs`

### Modificados:
- `WindowsFormsApp6\Modelos\ModelConfiguracao.cs`
- `WindowsFormsApp6\Interface\Utilitarios\IConfiguracao.cs`
- `WindowsFormsApp6\Menus\Utilitarios\FrmConfiguracoes.cs`
- `WindowsFormsApp6\Controles\Utilitarios\CtrlConfiguracao.cs`
- `WindowsFormsApp6\Scripts\Script.sql` (procedure SalvarConfiguracoes)

---

## 📝 Notas Importantes

1. **Cache de Fontes**: O `FonteHelper` mantém cache das configurações. Ao salvar novas configurações, o cache é automaticamente limpo.

2. **Fontes Inexistentes**: Se uma fonte configurada não existir no sistema, o helper retorna automaticamente a fonte padrão (Arial para relatórios, Courier New para impressões).

3. **Performance**: As fontes são carregadas uma vez e mantidas em cache, melhorando a performance.

4. **Valores Padrão**: 
   - Relatórios: Arial, 10pt
   - Impressão: Courier New, 8pt

---

## 🎯 Benefícios

✅ **Melhor legibilidade** em impressoras matriciais  
✅ **Configuração centralizada** de fontes  
✅ **Fácil manutenção** sem alterar código  
✅ **Suporte a diferentes tipos** de impressora  
✅ **Cache inteligente** para performance  

---

## 🆘 Solução de Problemas

### Problema: Fonte não aparece corretamente na impressora matricial

**Solução**: 
1. Verifique se a fonte selecionada está instalada no sistema
2. Para matriciais, prefira fontes monoespaçadas (Courier New, Consolas)
3. Reduza o tamanho da fonte (7-9 pontos)

### Problema: Texto muito grande/pequeno

**Solução**:
1. Ajuste o tamanho da fonte nas configurações
2. Para matriciais: teste com 7, 8 ou 9 pontos
3. Para relatórios: teste com 9, 10 ou 11 pontos

---

## 📞 Suporte

Para dúvidas ou problemas, consulte o arquivo de exemplos:
`WindowsFormsApp6\Exemplos\ExemploUsoFontes.cs`
