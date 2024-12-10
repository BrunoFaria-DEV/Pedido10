using Npgsql;
using Pedido10.Domain;
using System.Data;
using System.Linq.Expressions;

namespace Pedido10.Data
{
    public static class Proxy
    {
        public async static Task<DataSet?> ReaderAsync(string path, List<NpgsqlParameter> listaParameter)
        {
            var dataSet = new DataSet();

            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();
                try
                {
                    string sqlScript = File.ReadAllText(path);
                    using (var command = new NpgsqlCommand(sqlScript, connection))
                    {
                        foreach (var parameter in listaParameter)
                        {
                            command.Parameters.Add(parameter);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }

                            while (reader.Read())
                            {
                                var row = dataTable.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[i] = reader[i];
                                }
                                dataTable.Rows.Add(row);
                            }

                            dataSet.Tables.Add(dataTable);
                        }
                    }
                    return dataSet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar o comando SQL: {ex.Message}");
                    return null;
                }
            }
        }

        public async static Task<bool> AnyQueryAsync(string path, List<NpgsqlParameter> listaParameter)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                throw new ArgumentException("O caminho do script SQL é inválido.", nameof(path));

            try
            {
                string sqlScript = await File.ReadAllTextAsync(path);

                using (var connection = new NpgsqlConnection(Utils.StringConnection))
                {
                    await connection.OpenAsync();

                    using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            using (var command = new NpgsqlCommand(sqlScript, connection, transaction))
                            {
                                foreach (var parameter in listaParameter)
                                {
                                    command.Parameters.Add(parameter);
                                }

                                var rowsAffected = await command.ExecuteNonQueryAsync();

                                await transaction.CommitAsync();

                                return rowsAffected > 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();

                            Console.WriteLine($"Erro ao executar o comando SQL, transação revertida: {ex.Message}");
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log do erro - fora da transação
                Console.WriteLine($"Erro geral no método ExecuteNonQueryAsync: {ex.Message}");
                throw;
            }
        }
    }
}
