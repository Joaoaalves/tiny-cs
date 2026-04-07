namespace Joaoaalves.Tiny.Core.Tests.Mocks;

internal static class TinyResponseFixtures
{
    internal const string GetProductOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "produto": {
              "id": "123456789",
              "nome": "Camiseta Branca G",
              "codigo": "CAM-001",
              "unidade": "UN",
              "preco": "59.90",
              "preco_promocional": "0",
              "ncm": "6109.10.00",
              "origem": "0",
              "gtin": "7891234567890",
              "gtin_embalagem": "",
              "localizacao": "Prateleira A1",
              "peso_liquido": "0.200",
              "peso_bruto": "0.250",
              "estoque_minimo": "5",
              "estoque_maximo": "100",
              "id_fornecedor": "987",
              "codigo_fornecedor": "FORN-01",
              "codigo_pelo_fornecedor": "EXT-001",
              "unidade_por_caixa": "12",
              "preco_custo": "30.00",
              "preco_custo_medio": "31.50",
              "situacao": "A",
              "tipo": "P",
              "tipoVariacao": "",
              "marca": "MinhaMarca",
              "categoria": "Vestuário",
              "classe_produto": "S",
              "data_criacao": "01/03/2024 10:00:00"
            }
          }
        }
        """;

    internal const string SearchProductsOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "pagina": "1",
            "numero_paginas": "2",
            "produtos": [
              {
                "produto": {
                  "id": "111",
                  "nome": "Produto A",
                  "codigo": "SKU-A",
                  "preco": "10.00",
                  "preco_promocional": "0",
                  "preco_custo": "5.00",
                  "preco_custo_medio": "5.50",
                  "unidade": "UN",
                  "gtin": "",
                  "tipoVariacao": "",
                  "localizacao": "",
                  "situacao": "A",
                  "data_criacao": "15/01/2024"
                }
              },
              {
                "produto": {
                  "id": "222",
                  "nome": "Produto B",
                  "codigo": "SKU-B",
                  "preco": "20.00",
                  "preco_promocional": "18.00",
                  "preco_custo": "",
                  "preco_custo_medio": "",
                  "unidade": "CX",
                  "gtin": "1234567890123",
                  "tipoVariacao": "P",
                  "localizacao": "B2",
                  "situacao": "I",
                  "data_criacao": "20/02/2024"
                }
              }
            ]
          }
        }
        """;

    internal const string CreateProductOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "registros": [
              {
                "registro": {
                  "sequencia": "1",
                  "status": "OK",
                  "id": "999"
                }
              }
            ]
          }
        }
        """;

    internal const string CreateProductWithVariationsOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "registros": [
              {
                "registro": {
                  "sequencia": "1",
                  "status": "OK",
                  "id": "800",
                  "variacoes": [
                    { "variacao": { "id": "801" } },
                    { "variacao": { "id": "802" } }
                  ]
                }
              }
            ]
          }
        }
        """;

    internal const string ApiError = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "Erro",
            "codigo_erro": "1",
            "erros": [
              { "erro": "Token inválido." }
            ]
          }
        }
        """;

    internal const string GetOrderOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "pedido": {
              "id": "55500",
              "numero": "1001",
              "data_pedido": "05/04/2024",
              "data_prevista": "10/04/2024",
              "data_faturamento": "",
              "situacao": "Aberto",
              "valor_frete": "15.00",
              "valor_desconto": "5.00",
              "total_produtos": "100.00",
              "total_pedido": "110.00",
              "frete_por_conta": "D",
              "cliente": {
                "nome": "João da Silva",
                "tipo_pessoa": "F",
                "cpf_cnpj": "123.456.789-00",
                "cidade": "São Paulo",
                "uf": "SP"
              },
              "itens": [
                {
                  "item": {
                    "codigo": "SKU-A",
                    "descricao": "Produto A",
                    "unidade": "UN",
                    "quantidade": "2",
                    "valor_unitario": "50.00"
                  }
                }
              ]
            }
          }
        }
        """;

    internal const string SearchOrdersOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "pagina": "1",
            "numero_paginas": "1",
            "pedidos": [
              {
                "pedido": {
                  "id": "55500",
                  "numero": "1001",
                  "data_pedido": "05/04/2024",
                  "data_prevista": "10/04/2024",
                  "nome": "João da Silva",
                  "valor": "110.00",
                  "situacao": "Aberto"
                }
              }
            ]
          }
        }
        """;

    internal const string GetStockOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "produto": {
              "id": "123456789",
              "nome": "Camiseta Branca G",
              "codigo": "CAM-001",
              "unidade": "UN",
              "saldo": "42",
              "saldoReservado": "3",
              "depositos": [
                {
                  "deposito": {
                    "nome": "Depósito Central",
                    "desconsiderar": "N",
                    "saldo": "42",
                    "empresa": "Loja Principal"
                  }
                }
              ]
            }
          }
        }
        """;

    internal const string UpdateStockOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "registros": {
              "registro": {
                "sequencia": "1",
                "status": "OK",
                "id": "123456789",
                "saldoEstoque": "45",
                "saldoReservado": "3",
                "registroCriado": true
              }
            }
          }
        }
        """;

    internal const string ListStockUpdatesOk = """
        {
          "retorno": {
            "status_processamento": "3",
            "status": "OK",
            "pagina": "1",
            "numero_paginas": "1",
            "produtos": [
              {
                "produto": {
                  "id": "123456789",
                  "nome": "Camiseta Branca G",
                  "codigo": "CAM-001",
                  "unidade": "UN",
                  "tipo_variacao": "",
                  "localizacao": "A1",
                  "data_alteracao": "06/04/2024 14:30:00",
                  "saldo": "42",
                  "saldoReservado": "3",
                  "depositos": []
                }
              }
            ]
          }
        }
        """;
}
