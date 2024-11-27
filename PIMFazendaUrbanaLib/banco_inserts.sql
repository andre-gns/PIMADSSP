use pimfazendaurbana;

## Funcionario:
begin;
INSERT INTO `funcionario` (`nome_funcionario`, `cpf_funcionario`, `sexo_funcionario`, `email_funcionario`, `cargo_funcionario`, `usuario_funcionario`, `senha_funcionario`, `ativo_funcionario`) VALUES
('Alice Silva', '123.456.789-00', 'F', 'alice.silva@unip.br', 'Funcionário', 'alice.s', 'A1!eB3@cD', true),
('Bruno Souza', '234.567.890-11', 'M', 'bruno.souza@unip.br', 'Funcionário', 'bruno.s', 'B2#rU4%uZ', true),
('Carla Dias', '345.678.901-22', 'F', 'carla.dias@unip.br', 'Funcionário', 'carla.d', 'C3$rL5&dI', true),
('Diego Pereira', '456.789.012-33', 'M', 'diego.pereira@unip.br', 'Gerente', 'diego.p', 'D4#eP6&pR', true),
('Eva Martins', '567.890.123-44', 'F', 'eva.martins@unip.br', 'Gerente', 'eva.m', 'E5@mA7&nS', true),
('Felipe Ramos', '678.901.234-55', 'M', 'felipe.ramos@unip.br', 'Funcionário', 'felipe.r', 'F6#lR8*pM', true),
('Giselle Rocha', '789.012.345-66', 'F', 'giselle.rocha@unip.br', 'Funcionário', 'giselle.r', 'G7&lR9@cA', true),
('Hugo Almeida', '890.123.456-77', 'M', 'hugo.almeida@unip.br', 'Funcionário', 'hugo.a', 'H8@uA1#lE', true),
('Isabela Fernandes', '901.234.567-88', 'F', 'isabela.fernandes@unip.br', 'Gerente', 'isabela.f', 'I9#bE2%mN', true),
('João Lima', '012.345.678-99', 'M', 'joao.lima@unip.br', 'Gerente', 'joao.l', 'J0!oL3&pA', true);

INSERT INTO `enderecofuncionario` (`logradouro_endfuncionario`, `numero_endfuncionario`, `complemento_endfuncionario`, `bairro_endfuncionario`, `cidade_endfuncionario`, `uf_endfuncionario`, `cep_endfuncionario`, `ativo_endfuncionario`, `id_funcionario`) VALUES
('Rua Sete de Setembro', '123', 'Apto 101', 'Centro', 'Ribeirão Preto', 'SP', '14010-000', true, 1),
('Rua Mariana Junqueira', '456', 'Casa', 'Vila Tibério', 'Ribeirão Preto', 'SP', '14020-000', true, 2),
('Rua Conde Afonso Celso', '789', 'Apto 202', 'Jardim Paulista', 'Ribeirão Preto', 'SP', '14090-000', true, 3),
('Rua Amador Bueno', '101', NULL, 'Sumarezinho', 'Ribeirão Preto', 'SP', '14055-000', true, 4),
('Rua General Osório', '102', 'Apto 303', 'Campos Elíseos', 'Ribeirão Preto', 'SP', '14080-000', true, 5),
('Rua Lafaiete', '103', NULL, 'Vila Seixas', 'Ribeirão Preto', 'SP', '14020-250', true, 6),
('Rua Chile', '104', 'Apto 404', 'Jardim Sumaré', 'Ribeirão Preto', 'SP', '14025-220', true, 7),
('Rua São Sebastião', '105', NULL, 'Jardim São Luiz', 'Ribeirão Preto', 'SP', '14020-420', true, 8),
('Rua Álvares Cabral', '106', 'Apto 505', 'Vila Virgínia', 'Ribeirão Preto', 'SP', '14030-000', true, 9),
('Rua João Penteado', '107', 'Casa', 'Jardim América', 'Ribeirão Preto', 'SP', '14020-110', true, 10);

INSERT INTO `telefonefuncionario` (`ddd_telfuncionario`, `numero_telfuncionario`, `ativo_telfuncionario`, `id_funcionario`) VALUES
('16', '987654321', true, 1),
('16', '976543210', true, 2),
('16', '965432109', true, 3),
('16', '954321098', true, 4),
('16', '943210987', true, 5),
('16', '932109876', true, 6),
('16', '921098765', true, 7),
('16', '910987654', true, 8),
('16', '909876543', true, 9),
('16', '898765432', true, 10);

## Cliente
INSERT INTO `cliente` (`nome_cliente`, `email_cliente`, `cnpj_cliente`, `ativo_cliente`) VALUES
('Mercado São João', 'contato@mercadosaojoao.com.br', '12.345.678/0001-00', true),
('Hortifruti Bom Preço', 'contato@hortifrutibompreco.com.br', '23.456.789/0001-11', true),
('Verduras & Cia', 'contato@verdurasecia.com.br', '34.567.890/0001-22', true),
('Armazém do Campo', 'contato@armazemdocampo.com.br', '45.678.901/0001-33', true),
('Quitanda do Interior', 'contato@quitandadointerior.com.br', '56.789.012/0001-44', true),
('Feira Livre Natural', 'contato@feiralivrenatural.com.br', '67.890.123/0001-55', true),
('Empório dos Sabores', 'contato@emporiodossabores.com.br', '78.901.234/0001-66', true),
('Cantinho Verde', 'contato@cantinhoverde.com.br', '89.012.345/0001-77', true),
('Supermercado Rural', 'contato@supermercadorural.com.br', '90.123.456/0001-88', true),
('Frutas Frescas', 'contato@frutasfrescas.com.br', '01.234.567/0001-99', true);

INSERT INTO `enderecocliente` (`logradouro_endcliente`, `numero_endcliente`, `complemento_endcliente`, `bairro_endcliente`, `cidade_endcliente`, `uf_endcliente`, `cep_endcliente`, `ativo_endcliente`, `id_cliente`) VALUES
('Rua Itacolomi', '1240', NULL, 'Centro', 'Ribeirão Preto', 'SP', '14010-050', true, 1),
('Avenida Independência', '200', NULL, 'Jardim América', 'Sertãozinho', 'SP', '14160-230', true, 2),
('Rua Bahia', '330', NULL, 'Vila Virgínia', 'Jaboticabal', 'SP', '14870-550', true, 3),
('Rua Camilo de Mattos', '440', NULL, 'Jardim Panorama', 'Bebedouro', 'SP', '14701-150', true, 4),
('Rua Afonso Pena', '550', 'Loja 2', 'Centro', 'Descalvado', 'SP', '13690-000', true, 5),
('Rua Marília', '660', 'Loja 1', 'Centro', 'Pirassununga', 'SP', '13640-000', true, 6),
('Rua Rio de Janeiro', '770', NULL, 'Jardim São Luís', 'São José do Rio Pardo', 'SP', '13720-000', true, 7),
('Rua Tupi', '880', NULL, 'Jardim São Paulo', 'Mococa', 'SP', '13736-300', true, 8),
('Rua Sergipe', '990', NULL, 'Centro', 'Batatais', 'SP', '14300-000', true, 9),
('Rua Tocantins', '1000', 'Loja 3', 'Centro', 'Serrana', 'SP', '14150-000', true, 10);

INSERT INTO `telefonecliente` (`ddd_telcliente`, `numero_telcliente`, `ativo_telcliente`, `id_cliente`) VALUES
('16', '912345678', true, 1),
('16', '923456789', true, 2),
('16', '934567890', true, 3),
('17', '945678901', true, 4),
('16', '956789012', true, 5),
('19', '967890123', true, 6),
('19', '978901234', true, 7),
('16', '989012345', true, 8),
('16', '990123456', true, 9),
('16', '901234567', true, 10);

## Fornecedor
INSERT INTO `fornecedor` (`nome_fornecedor`, `email_fornecedor`, `cnpj_fornecedor`, `ativo_fornecedor`) VALUES
('AgroFertilizantes S.A.', 'contato@agrofertilizantes.com.br', '12.345.678/0001-00', true),
('Sementes de Qualidade Ltda.', 'contato@sementesdequalidade.com.br', '23.456.789/0001-11', true),
('Adubo Forte Indústria e Comércio', 'contato@aduboforte.com.br', '34.567.890/0001-22', true),
('Irrigação Eficiente Ltda.', 'contato@irrigacaoeficiente.com.br', '45.678.901/0001-33', true),
('Ferramentas Agrícolas & Cia.', 'contato@ferramentasagricolas.com.br', '56.789.012/0001-44', true),
('Plásticos para Estufas Ltda.', 'contato@plasticosestufas.com.br', '67.890.123/0001-55', true),
('Pesticidas do Interior', 'contato@pesticidasdointerior.com.br', '78.901.234/0001-66', true),
('Embalagens Verdes S.A.', 'contato@embalagensverdes.com.br', '89.012.345/0001-77', true),
('Equipamentos para Agricultura Ltda.', 'contato@equipamentosagricultura.com.br', '90.123.456/0001-88', true),
('Insumos Naturais do Brasil', 'contato@insumosnaturais.com.br', '01.234.567/0001-99', true);

INSERT INTO `enderecofornecedor` (`logradouro_endfornecedor`, `numero_endfornecedor`, `complemento_endfornecedor`, `bairro_endfornecedor`, `cidade_endfornecedor`, `uf_endfornecedor`, `cep_endfornecedor`, `ativo_endfornecedor`, `id_fornecedor`) VALUES
('Rua Sebastião Sampaio', '1100', NULL, 'Distrito Industrial 2', 'Ribeirão Preto', 'SP', '14000-000', true, 1),
('Rua Fioravante Tordin', '220', 'Galpão 2', 'Distrito Industrial', 'Sertãozinho', 'SP', '14177-500', true, 2),
('Rua José Fregonese', '330', NULL, 'Parque Industrial 2', 'Jaboticabal', 'SP', '14870-000', true, 3),
('Rua Olívio Franceschini', '440', NULL, 'Distrito Industrial', 'Bebedouro', 'SP', '14708-700', true, 4),
('Rua Ernesto Navarro', '550', 'Loja 1', 'Parque Industrial', 'Araraquara', 'SP', '14801-395', true, 5),
('Avenida 21 de Março', '660', NULL, 'Distrito Industrial 1', 'Pirassununga', 'SP', '13640-000', true, 6),
('Rua das Pimenteiras', '770', 'Sala 3', 'Distrito Industrial 1', 'São José do Rio Pardo', 'SP', '13720-000', true, 7),
('Rua dos Coqueiros', '880', NULL, 'Parque Industrial', 'Mococa', 'SP', '13736-000', true, 8),
('Rua das Mangueiras', '990', 'Galpão 3', 'Distrito Industrial', 'Batatais', 'SP', '14300-000', true, 9),
('Rua 13 de Maio', '1000', NULL, 'Distrito Industrial', 'Serrana', 'SP', '14150-000', true, 10);

INSERT INTO `telefonefornecedor` (`ddd_telfornecedor`, `numero_telfornecedor`, `ativo_telfornecedor`, `id_fornecedor`) VALUES
('16', '912345678', true, 1),
('16', '923456789', true, 2),
('16', '934567890', true, 3),
('17', '945678901', true, 4),
('16', '956789012', true, 5),
('19', '967890123', true, 6),
('19', '978901234', true, 7),
('16', '989012345', true, 8),
('16', '990123456', true, 9),
('16', '901234567', true, 10);

INSERT INTO `estoqueinsumo` (`nome_insumo`, `categoria_insumo`, `qtd_insumo`, `unidqtd_insumo`, `ativo_insumo`) VALUES
('Nitrato de amônio', 'Fertilizantes', 0, 'kg', true),
('Fosfato diamônico', 'Fertilizantes', 0, 'kg', true),
('Sulfato de potássio', 'Fertilizantes', 0, 'kg', true),
('Calcário dolomítico', 'Fertilizantes', 0, 'kg', true),
('Uréia', 'Fertilizantes', 0, 'kg', true),
('Superfosfato simples', 'Fertilizantes', 0, 'kg', true),
('Cloreto de potássio', 'Fertilizantes', 0, 'kg', true),
('Fertilizante líquido NPK 10-10-10', 'Fertilizantes', 0, 'l', true),
('Fertilizante líquido NPK 20-5-10', 'Fertilizantes', 0, 'l', true),
('Fertilizante líquido NPK 15-30-15', 'Fertilizantes', 0, 'l', true),
('Fertilizante líquido NPK 12-0-12', 'Fertilizantes', 0, 'l', true),
('Fertilizante granulado NPK 20-10-10', 'Fertilizantes', 0, 'kg', true),
('Fertilizante granulado NPK 15-15-15', 'Fertilizantes', 0, 'kg', true),
('Fertilizante granulado NPK 10-20-10', 'Fertilizantes', 0, 'kg', true),
('Fertilizante granulado NPK 10-10-20', 'Fertilizantes', 0, 'kg', true),
('Abacaxi Pérola', 'Sementes', 0, 'kg', true),
('Abóbora Japonesa', 'Sementes', 0, 'kg', true),
('Abobrinha Menina Brasileira', 'Sementes', 0, 'kg', true),
('Acelga Verde de Verão', 'Sementes', 0, 'kg', true),
('Agrião de Água', 'Sementes', 0, 'kg', true),
('Alface Crespa', 'Sementes', 0, 'kg', true),
('Alface Americana', 'Sementes', 0, 'kg', true),
('Algodão BRS 368', 'Sementes', 0, 'kg', true),
('Alho Roxo', 'Sementes', 0, 'kg', true),
('Alho-poró Porto Rico', 'Sementes', 0, 'kg', true),
('Banana Prata', 'Sementes', 0, 'kg', true),
('Batata-doce Beauregard', 'Sementes', 0, 'kg', true),
('Beterraba Detroit Dark Red', 'Sementes', 0, 'kg', true),
('Beterraba Early Wonder', 'Sementes', 0, 'kg', true),
('Berinjela Roxa', 'Sementes', 0, 'kg', true),
('Brócolis Calabrês', 'Sementes', 0, 'kg', true),
('Caju Anão Precoce', 'Sementes', 0, 'kg', true),
('Cebola Baia Periforme', 'Sementes', 0, 'kg', true),
('Cebolinha Verde Todo o Ano', 'Sementes', 0, 'kg', true),
('Cenoura Brasília', 'Sementes', 0, 'kg', true),
('Cenoura Nantes', 'Sementes', 0, 'kg', true),
('Chicória Catalonha', 'Sementes', 0, 'kg', true),
('Coentro Português', 'Sementes', 0, 'kg', true),
('Couve Manteiga', 'Sementes', 0, 'kg', true),
('Couve-de-bruxelas Menina', 'Sementes', 0, 'kg', true),
('Couve-flor de Inverno', 'Sementes', 0, 'kg', true),
('Cupuaçuzeiro', 'Sementes', 0, 'kg', true),
('Erva-doce de Mesa', 'Sementes', 0, 'kg', true),
('Ervilha Douce Provence', 'Sementes', 0, 'kg', true),
('Ervilha Early Frosty', 'Sementes', 0, 'kg', true);
commit;
