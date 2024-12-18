SELECT 
    u."ID_Usuario",
    u."Nome",
    u."Email",
    u."Plano_Usuario",
    u."Status"
FROM "Usuario" u
WHERE @Nome = '' OR u."Nome" ILIKE '%' || @NomePesquisa || '%'
ORDER BY u."ID_Usuario" Desc
LIMIT @TamanhoPagina OFFSET (@TamanhoPagina * (@Pagina - 1));