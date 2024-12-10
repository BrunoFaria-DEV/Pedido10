UPDATE "Usuario"
SET "Nome"=@Nome,
	"Email"=@Email,
	"Senha"=@Senha,
	"Plano_Usuario"=@Plano_Usuario
	"Status"=@Status,
WHERE "ID_Usuario" = @ID_Usuario