# Scripts de Atualização para Produção

## 📋 Ordem de Execução

Execute os scripts **NA ORDEM** listada abaixo:

---

## ✅ Scripts Obrigatórios (em ordem):

### **00_EXECUTAR_TUDO.sql** ⭐ RECOMENDADO
- Executa TODOS os scripts de uma vez
- **Mais seguro e rápido**
- Aguarda 5 segundos antes de começar (tempo para cancelar se necessário)
- Mostra progresso detalhado

**OU execute individualmente:**

### **01_ALTER_Configuracoes_Fontes.sql**
- Adiciona 4 colunas na tabela `Configuracoes`:
  - `FonteRelatorioNome` (VARCHAR 100)
  - `FonteRelatorioTamanho` (INT)
  - `FonteImpressaoNome` (VARCHAR 100)
  - `FonteImpressaoTamanho` (INT)
- **Tempo estimado:** 1 segundo
- **Impacto:** Baixo - apenas adiciona colunas

### **02_CREATE_Tabelas_Seguranca.sql**
- Cria 4 novas tabelas:
  - `Perfil` - Perfis de acesso
  - `Usuario` - Usuários do sistema
  - `Menu` - Menus disponíveis
  - `PerfilMenu` - Permissões por perfil
- **Tempo estimado:** 2 segundos
- **Impacto:** Nenhum - são tabelas novas

### **03_INSERT_Dados_Iniciais.sql** (renomeado de 03_CREATE_Procedures_Seguranca.sql)
- Insere 10 menus do sistema
- Cria perfil "Administrador"
- Cria usuário "administrador" (senha: 1234)
- Atribui todas as permissões ao administrador
- **Tempo estimado:** 2 segundos
- **Impacto:** Nenhum - apenas insere dados

### **04_ALTER_Procedure_SalvarConfiguracoes.sql**
- Atualiza a procedure `SalvarConfiguracoes`
- Adiciona parâmetros de fonte
- **Tempo estimado:** 1 segundo
- **Impacto:** Médio - altera procedure existente

### **05_ALTER_Function_ConsultarConfiguracoes.sql**
- Atualiza a function `ConsultarConfiguracoes`
- Adiciona retorno dos campos de fonte
- **Tempo estimado:** 1 segundo
- **Impacto:** Médio - altera function existente

---

## ⚠️ IMPORTANTE - Antes de Executar:

1. **Faça BACKUP do banco de dados:**
   ```sql
   BACKUP DATABASE [venda] TO DISK = 'C:\Backup\venda_backup_antes_atualizacao.bak'
   ```

2. **Teste em ambiente de HOMOLOGAÇÃO primeiro**

3. **Execute em horário de baixo movimento**

4. **Tenha o script de ROLLBACK pronto** (caso precise reverter)

---

## 🔄 Script de Rollback

### **99_ROLLBACK.sql**
- Reverte TODAS as alterações
- **USE APENAS EM EMERGÊNCIA**
- **PERDE TODOS OS DADOS** de usuários/perfis criados
- Aguarda 10 segundos antes de executar

---

## 📊 Impacto Estimado:

| Script | Tempo | Impacto | Pode Falhar? |
|--------|-------|---------|--------------|
| 01 | 1s | Baixo | Não (idempotente) |
| 02 | 2s | Nenhum | Não (idempotente) |
| 03 | 2s | Nenhum | Não (idempotente) |
| 04 | 1s | Médio | Não (idempotente) |
| 05 | 1s | Médio | Não (idempotente) |
| **TOTAL** | **~7s** | **Baixo** | **Não** |

---

## 🎯 Recomendações:

### **Opção 1: Executar Tudo de Uma Vez** ⭐ RECOMENDADO
```sql
-- Execute apenas este arquivo:
00_EXECUTAR_TUDO.sql
```

**Vantagens:**
- ✅ Mais rápido
- ✅ Menos chance de erro
- ✅ Progresso detalhado
- ✅ Rollback automático em caso de erro (transaction)

### **Opção 2: Executar Individualmente**
```sql
-- Execute na ordem:
01_ALTER_Configuracoes_Fontes.sql
02_CREATE_Tabelas_Seguranca.sql
03_INSERT_Dados_Iniciais.sql
04_ALTER_Procedure_SalvarConfiguracoes.sql
05_ALTER_Function_ConsultarConfiguracoes.sql
```

**Vantagens:**
- ✅ Controle total de cada etapa
- ✅ Pode pausar entre scripts
- ✅ Fácil identificar problema

---

## 🔍 Verificação Pós-Execução:

Após executar, verifique se tudo foi criado corretamente:

```sql
-- Verifica colunas adicionadas
SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Configuracoes' 
  AND COLUMN_NAME LIKE 'Fonte%'

-- Verifica tabelas criadas
SELECT name FROM sys.tables 
WHERE name IN ('Perfil', 'Usuario', 'Menu', 'PerfilMenu')

-- Verifica usuário administrador
SELECT * FROM Usuario WHERE Login = 'administrador'

-- Verifica menus cadastrados
SELECT * FROM Menu ORDER BY Ordem
```

**Resultado esperado:**
- 4 colunas de fonte
- 4 tabelas de segurança
- 1 usuário administrador
- 10 menus cadastrados

---

## 🆘 Em Caso de Erro:

1. **Anote a mensagem de erro completa**
2. **Identifique em qual script falhou**
3. **Verifique se o objeto já existe:**
   ```sql
   SELECT * FROM sys.objects WHERE name = 'NomeDoObjeto'
   ```
4. **Se necessário, execute o ROLLBACK:**
   ```sql
   -- Use com cuidado!
   99_ROLLBACK.sql
   ```

---

## 📞 Suporte:

- **Logs detalhados**: Todos os scripts mostram mensagens de progresso
- **Idempotentes**: Podem ser executados múltiplas vezes
- **Seguros**: Verificam existência antes de criar/alterar
- **Com ROLLBACK**: Pode reverter se necessário

---

## ✨ Após Executar:

1. ✅ Reinicie a aplicação
2. ✅ Faça login com: `administrador` / `1234`
3. ✅ Teste as novas funcionalidades
4. ✅ Configure as fontes em "Configurações"
5. ✅ Crie novos perfis e usuários

---

**🎊 Boa sorte com a atualização em produção!** 🚀
