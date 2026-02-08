## RBAC — base

Tabelas:
- Users (Id, Login, PasswordHash, IsActive)
- Roles (Id, Name)
- Permissions (Id, Code, Description)
- UserRoles (UserId, RoleId)
- RolePermissions (RoleId, PermissionId)

Permission codes (exemplos):
- "PARCEIRO.VIEW"
- "PARCEIRO.EDIT"
- "VENDA.CREATE"
- "REPORT.SALES.PRINT"

Serviços:
- IAuthService: LoginAsync, Logout, CurrentUser
- ISessionContext: User, Roles, Permissions
- IAuthorizationService: Can(code), Demand(code)
