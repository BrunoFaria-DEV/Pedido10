-- criação do banco e usuário
CREATE DATABASE "Pedido10";
CREATE USER "Pedido10" WITH PASSWORD 'Pedido10';
GRANT ALL PRIVILEGES ON DATABASE "Pedido10" TO "Pedido10";
FLUSH PRIVILEGES;

-- criação das tabelas e triggers
CREATE TABLE "Usuario" (
  "ID_Usuario" SERIAL PRIMARY KEY,
  "Email" VARCHAR(120),
  "Nome" VARCHAR(120),
  "Senha" VARCHAR(100),
  "Plano_Usuario" CHAR(1),
  "Status" CHAR(1),
  "Tipo_Usuario" VARCHAR(14)
);

CREATE TABLE "Forma_PGTO" (
  "ID_Forma_PGTO" SERIAL PRIMARY KEY,
  "Desc_Forma_PGTO" VARCHAR(15)
);

CREATE TABLE "Cidade" (
  "ID_Cidade" SERIAL PRIMARY KEY,
  "Nome_Cidade" VARCHAR(120),
  "Codigo_IBGE" INTEGER,
  "UF" CHAR(2)
);

CREATE TABLE "Cliente" (
  "ID_Cliente" SERIAL PRIMARY KEY,
  "Nome" VARCHAR(100),
  "Tipo_Pessoa" BOOLEAN,
  "CPF" CHAR(11),
  "CNPJ" CHAR(14),
  "Email" VARCHAR(50),
  "Telefone" VARCHAR(15),
  "Nascimento" DATE,
  "Endereco" VARCHAR(150),
  "Localizador" VARCHAR(255),
  "ID_Cidade" INTEGER,
  FOREIGN KEY ("ID_Cidade") REFERENCES "Cidade" ("ID_Cidade")
);

CREATE TABLE "Produto" (
  "ID_Produto" SERIAL PRIMARY KEY,
  "Nome_Produto" VARCHAR(50),
  "Descricao" VARCHAR(150),
  "Custo_Producao" NUMERIC(7,2),
  "Margem_Lucro" NUMERIC(7,2),
  "Preco" NUMERIC(7, 2),
  "QTDE_Estoque" INTEGER
);

CREATE TABLE "Pedido" (
  "ID_Pedido" SERIAL PRIMARY KEY,
  "Observacao" VARCHAR(150),
  "VLR_Total_Pedido" NUMERIC(9,2),
  "Status_Entrega_Pedido" CHAR(1),
  "DT_Pedido" DATE,
  "DT_Entrega" DATE,
  "Hora_Entrega" TIMESTAMP,
  "Pago" CHAR(1),
  "ID_Usuario" INTEGER,
  "ID_Cliente" INTEGER,
  FOREIGN KEY ("ID_Usuario") REFERENCES "Usuario" ("ID_Usuario"),
  FOREIGN KEY ("ID_Cliente") REFERENCES "Cliente" ("ID_Cliente")
);

CREATE TABLE "Pedido_Produto" (
  "ID_Pedido_Produto" SERIAL PRIMARY KEY,
  "QTDE_Produto" INTEGER,
  "VLR_Unitario_Produto" NUMERIC(7,2),
  "VLR_Total_Produto" NUMERIC(7,2),
  "ID_Pedido" INTEGER,
  "ID_Produto" INTEGER,
  FOREIGN KEY ("ID_Pedido") REFERENCES "Pedido" ("ID_Pedido"),
  FOREIGN KEY ("ID_Produto") REFERENCES "Produto" ("ID_Produto")
);

CREATE TABLE "Parcela" (
  "ID_Parcela" SERIAL PRIMARY KEY,
  "Status_Parcela" CHAR(1),
  "Data_Pagamento" DATE,
  "DT_Vencimento" DATE,
  "Valor_Parcela" NUMERIC(7,2),
  "Numero_Parcela" INTEGER,
  "Valor_Pago_Parcela" NUMERIC(7,2),
  "ID_Pedido" INTEGER,
  "ID_Forma_PGTO" INTEGER,
  FOREIGN KEY ("ID_Pedido") REFERENCES "Pedido" ("ID_Pedido"),
  FOREIGN KEY ("ID_Forma_PGTO") REFERENCES "Forma_PGTO" ("ID_Forma_PGTO")
);

-- Trigger da Tabela Cliente
CREATE OR REPLACE FUNCTION tipo_cliente_cpf_cnpj()
RETURNS TRIGGER AS $$
BEGIN
  IF NEW."Tipo_Pessoa" = TRUE THEN
    NEW."CNPJ" := NULL;
  ELSE
    NEW."CPF" := NULL;
  END IF;
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_cliente_cpf_cnpj
BEFORE INSERT OR UPDATE ON "Cliente"
FOR EACH ROW
EXECUTE FUNCTION tipo_cliente_cpf_cnpj();

-- Trigger da Tabela Pedido
CREATE OR REPLACE FUNCTION atualizar_hora_entrega()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW."Status_Entrega_Pedido" = 'E' THEN
        NEW."Hora_Entrega" := NOW();
    ELSE
        NEW."Hora_Entrega" := NULL;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_atualizar_hora_entrega
BEFORE INSERT OR UPDATE ON "Pedido"
FOR EACH ROW
EXECUTE FUNCTION atualizar_hora_entrega();

-- Povoamento
INSERT INTO "Forma_PGTO" ("Desc_Forma_PGTO") VALUES
('Dinheiro'),
('Pix'),
('Debito'),
('Credito');

INSERT INTO "Cidade" ("Nome_Cidade", "UF", "Codigo_IBGE") VALUES
('Rio Branco', 'AC', 1200401),
('Maceió', 'AL', 2704302),
('Macapá', 'AP', 1600303),
('Manaus', 'AM', 1302603),
('Salvador', 'BA', 2927408),
('Fortaleza', 'CE', 2304400),
('Brasília', 'DF', 5300108),
('Vitória', 'ES', 3205309),
('Goiânia', 'GO', 5208707),
('São Luís', 'MA', 2111300),
('Cuiabá', 'MT', 5103403),
('Campo Grande', 'MS', 5002704),
('Belo Horizonte', 'MG', 3106200),
('Belém', 'PA', 1501402),
('João Pessoa', 'PB', 2507507),
('Curitiba', 'PR', 4106902),
('Recife', 'PE', 2611606),
('Teresina', 'PI', 2211001),
('Rio de Janeiro', 'RJ', 3304557),
('Natal', 'RN', 2408102),
('Porto Alegre', 'RS', 4314902),
('Porto Velho', 'RO', 1100205),
('Boa Vista', 'RR', 1400100),
('Florianópolis', 'SC', 4205407),
('São Paulo', 'SP', 3550308),
('Aracaju', 'SE', 2800308),
('Palmas', 'TO', 1721000);


INSERT INTO "Cidade" ("Nome_Cidade", "UF", "Codigo_IBGE") VALUES
('Senador Guiomard', 'AC', 1200500),  -- Próxima de Rio Branco
('Rio Largo', 'AL', 2707701),        -- Próxima de Maceió
('Santana', 'AP', 1600600),          -- Próxima de Macapá
('Iranduba', 'AM', 1301852),         -- Próxima de Manaus
('Lauro de Freitas', 'BA', 2919207), -- Próxima de Salvador
('Eusébio', 'CE', 2304285),          -- Próxima de Fortaleza
('Águas Claras', 'DF', 5300108),     -- Próxima de Brasília
('Vila Velha', 'ES', 3205200),       -- Próxima de Vitória
('Aparecida de Goiânia', 'GO', 5201405), -- Próxima de Goiânia
('Raposa', 'MA', 2109452),           -- Próxima de São Luís
('Várzea Grande', 'MT', 5108402),    -- Próxima de Cuiabá
('Terenos', 'MS', 5008006),          -- Próxima de Campo Grande
('Sabará', 'MG', 3156700),           -- Próxima de Belo Horizonte
('Ananindeua', 'PA', 1500800),       -- Próxima de Belém
('Cabedelo', 'PB', 2503209),         -- Próxima de João Pessoa
('São José dos Pinhais', 'PR', 4125506), -- Próxima de Curitiba
('Jaboatão dos Guararapes', 'PE', 2607901), -- Próxima de Recife
('Altos', 'PI', 2200402),            -- Próxima de Teresina
('Niterói', 'RJ', 3303302),          -- Próxima de Rio de Janeiro
('Parnamirim', 'RN', 2403251),       -- Próxima de Natal
('Canoas', 'RS', 4304606),           -- Próxima de Porto Alegre
('Candeias do Jamari', 'RO', 1100802), -- Próxima de Porto Velho
('Cantá', 'RR', 1400175),            -- Próxima de Boa Vista
('São José', 'SC', 4216602),         -- Próxima de Florianópolis
('Guarulhos', 'SP', 3518800),        -- Próxima de São Paulo
('Nossa Senhora do Socorro', 'SE', 2804805), -- Próxima de Aracaju
('Paraíso do Tocantins', 'TO', 1716109); -- Próxima de Palmas
