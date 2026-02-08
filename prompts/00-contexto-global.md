Você está em um projeto C# WinForms com Dapper e SQL Server.

Regras obrigatórias:
1) View: só UI (sem regra, sem SQL/Dapper, sem permissões, sem QuestPDF).
2) Controller: fluxo + validações + permissões (IAuthorizationService).
3) Behaviors: regras condicionais por modo/tipo.
4) Repos (Dapper): SQL parametrizado, sem regra.
5) RBAC: IAuthService/ISessionContext/IAuthorizationService.
6) QuestPDF: ReportModel + ReportDocument + DataProvider, coordenado pelo Controller.
7) Existente: refatorar incrementalmente.
8) Do zero: perguntar em blocos curtos e implementar por etapas compiláveis.
