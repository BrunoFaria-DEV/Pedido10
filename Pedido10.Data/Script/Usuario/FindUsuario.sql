SELECT
u."ID_Usuario",
u."Nome",
u."Email",
u."Senha",
u."Plano_Usuario",
u."Status"
FROM "Usuario" u
WHERE u."ID_Usuario" = @ID_Usuario 