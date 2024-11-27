using ClosedXML.Excel;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using System.Text;
using PIMFazendaUrbanaAPI.DTOs;
using System.Text.Json;

namespace PIMFazendaUrbanaAPI.Services
{
    public class ExportacaoService : IExportacaoService
    {
        public byte[] Exportar(IEnumerable<object> dados, string formato)
        {
            if (dados == null || !dados.Any())
            {
                throw new ArgumentException("Nenhum dado fornecido.");
            }

            // Gera os dados mapeados de forma dinâmica
            var dadosMapeados = MapearDadosDinamicos(dados);

            switch (formato.ToLower())
            {
                case "xlsx":
                    return GerarExcel(dadosMapeados);
                case "csv":
                    return GerarCsv(dadosMapeados);
                default:
                    throw new ArgumentException("Formato não suportado.");
            }
        }

        // Método para mapear os dados dinamicamente a partir de JsonElement
        private List<Dictionary<string, object>> MapearDadosDinamicos(IEnumerable<object> dados)
        {
            var dadosMapeados = new List<Dictionary<string, object>>();

            foreach (var item in dados)
            {
                var linha = new Dictionary<string, object>();

                // Se o item é do tipo JsonElement
                if (item is JsonElement jsonElement)
                {
                    // Itera pelas propriedades do JsonElement e adiciona ao dicionário
                    foreach (var property in jsonElement.EnumerateObject())
                    {
                        // Ignora a chave "StatusAtivo" ou "Senha"
                        if (property.Name.Equals("StatusAtivo", StringComparison.OrdinalIgnoreCase) || property.Name.Equals("Senha", StringComparison.OrdinalIgnoreCase))
                            continue;

                        // Se a propriedade for um JsonObject (composto), desmembra em várias colunas
                        if (property.Value.ValueKind == JsonValueKind.Object)
                        {
                            var nestedProperties = MapearDadosDinamicos(new[] { property.Value }.Cast<object>()).First();

                            // Para cada propriedade dentro do objeto composto, adicione uma nova coluna
                            foreach (var nestedProperty in nestedProperties)
                            {
                                string newKey;

                                // Verifica se a chave é "Numero" e, nesse caso, inverte a ordem: "Número (Objeto)"
                                if (nestedProperty.Key.Equals("Numero", StringComparison.OrdinalIgnoreCase))
                                {
                                    newKey = $"{FormatarNomeObjeto(nestedProperty.Key)} ({FormatarNomeObjeto(property.Name)})";
                                }
                                else
                                {
                                    newKey = FormatarNomeObjeto(nestedProperty.Key);
                                }

                                // Aplica a formatação ao valor
                                linha[newKey] = FormatarValor(newKey, nestedProperty.Value);
                            }
                        }
                        else
                        {
                            // Adiciona a propriedade ao dicionário, aplicando a formatação
                            var formattedValue = FormatarValor(property.Name, property.Value.ToString());
                            linha[property.Name] = formattedValue;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"Tipo de objeto não tratado: {item.GetType().Name}");
                }

                // Formatar as chaves para capitalizar a primeira letra e remover chaves ignoradas
                var keys = linha.Keys.ToList();
                foreach (var key in keys)
                {
                    if (key.Equals("StatusAtivo", StringComparison.OrdinalIgnoreCase) || key.Equals("Senha", StringComparison.OrdinalIgnoreCase))
                    {
                        linha.Remove(key);
                        continue;
                    }

                    var capitalizedKey = key;

                    if (key != "CNPJ" || key != "CPF")
                    {
                        capitalizedKey = CapitalizarPrimeiraLetra(key);
                    }

                    if (capitalizedKey != key)
                    {
                        var value = linha[key];
                        linha.Remove(key);
                        linha.Add(capitalizedKey, value);
                    }

                    if (capitalizedKey == "Número (Telefone)")
                    {
                        // formata o valor, adicionando "-" no meio do número do telefone, sem chamar outro método
                        var stringValue = linha[capitalizedKey].ToString();
                        if (stringValue.Length == 8)
                        {
                            linha[capitalizedKey] = string.Format("{0}-{1}", stringValue.Substring(0, 4), stringValue.Substring(4));
                        }
                        else if (stringValue.Length == 9)
                        {
                            linha[capitalizedKey] = string.Format("{0}-{1}", stringValue.Substring(0, 5), stringValue.Substring(5));
                        }
                    }
                }

                dadosMapeados.Add(linha);
            }

            return dadosMapeados;
        }



        // Função para formatar os nomes das propriedades de maneira personalizada
        private string FormatarNomeObjeto(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            switch (texto.ToLower())
            {
                case "endereco":
                    return "Endereço";
                case "ddd":
                    return "DDD";
                case "cnpj":
                    return "CNPJ";
                case "cpf":
                    return "CPF";
                case "uf":
                    return "UF";
                case "cep":
                    return "CEP";
                case "numero":
                    return "Número";
                case "email":
                    return "E-mail";
                default:
                    return CapitalizarPrimeiraLetra(texto);
            }
        }

        // Função para capitalizar a primeira letra de uma string
        private string CapitalizarPrimeiraLetra(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            return char.ToUpper(texto[0]) + texto.Substring(1);
        }

        private string FormatarValor(string coluna, object valor)
        {
            if (valor == null) return string.Empty;

            string stringValue = valor.ToString();

            switch (coluna.ToLower())
            {
                case "cpf":
                    if (stringValue.Length == 11)
                        return string.Format("{0}.{1}.{2}-{3}", stringValue.Substring(0, 3), stringValue.Substring(3, 3), stringValue.Substring(6, 3), stringValue.Substring(9));
                    break;
                case "cnpj":
                    if (stringValue.Length == 14)
                        return string.Format("{0}.{1}.{2}/{3}-{4}", stringValue.Substring(0, 2), stringValue.Substring(2, 3), stringValue.Substring(5, 3), stringValue.Substring(8, 4), stringValue.Substring(12));
                    break;
                case "cep":
                    if (stringValue.Length == 8)
                        return string.Format("{0}-{1}", stringValue.Substring(0, 5), stringValue.Substring(5));
                    break;
            }

            return stringValue; // Retorna o valor sem formatação se não for tratado
        }


        // --------------------------------

        // Geração de arquivo Excel (XLSX)
        private byte[] GerarExcel(IEnumerable<Dictionary<string, object>> dados)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Exportação");

            // Obter as chaves (nomes das propriedades) do primeiro item
            var chaves = dados.First().Keys.ToList();

            // Adicionar e estilizar cabeçalhos
            for (int i = 0; i < chaves.Count; i++)
            {
                var headerCell = worksheet.Cell(1, i + 1);
                headerCell.Value = chaves[i];
                headerCell.Style.Font.Bold = true;
                headerCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#23992c");
                headerCell.Style.Font.FontColor = XLColor.White;
                headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // Adicionar dados
            int row = 2;
            foreach (var linha in dados)
            {
                for (int i = 0; i < chaves.Count; i++)
                {
                    var valor = linha[chaves[i]];

                    if (valor == null)
                    {
                        worksheet.Cell(row, i + 1).Value = string.Empty; // Para valores nulos
                    }
                    else if (valor is string stringValue)
                    {
                        worksheet.Cell(row, i + 1).Value = stringValue; // Tratamento de string
                    }
                    else if (valor is int intValue)
                    {
                        worksheet.Cell(row, i + 1).Value = intValue; // Tratamento de inteiro
                    }
                    else if (valor is decimal decimalValue)
                    {
                        worksheet.Cell(row, i + 1).Value = decimalValue; // Tratamento de decimal
                    }
                    else if (valor is double doubleValue)
                    {
                        worksheet.Cell(row, i + 1).Value = doubleValue; // Tratamento de double
                    }
                    else if (valor is bool boolValue)
                    {
                        worksheet.Cell(row, i + 1).Value = boolValue ? "Sim" : "Não"; // Tratamento de booleano
                    }
                    else if (valor is DateTime dateTimeValue)
                    {
                        worksheet.Cell(row, i + 1).Value = dateTimeValue.ToString("yyyy-MM-dd"); // Formato para Data
                    }
                    else
                    {
                        worksheet.Cell(row, i + 1).Value = valor.ToString(); // Para valores não especificados
                    }
                }
                row++;
            }

            // Ajustar largura das colunas
            worksheet.Columns().AdjustToContents();

            // Adicionar bordas
            var range = worksheet.Range(1, 1, row - 1, chaves.Count);
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Congelar o cabeçalho
            worksheet.SheetView.FreezeRows(1);

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }



        // Geração de arquivo CSV
        private byte[] GerarCsv(IEnumerable<Dictionary<string, object>> dados)
        {
            var sb = new StringBuilder();

            // Obter as chaves (nomes das propriedades) do primeiro item
            var chaves = dados.First().Keys.ToList();

            // Adicionar cabeçalhos
            sb.AppendLine(string.Join(",", chaves));

            // Adicionar dados
            foreach (var linha in dados)
            {
                sb.AppendLine(string.Join(",", linha.Values.Select(v => v.ToString())));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
