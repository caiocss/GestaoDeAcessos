using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace GestaoDeAcessos.Classes
{
    public class ConnectDatabase
    {
        //Atributo que guarda a conexão com o banco de dados.
        private string _connectionString = @"Data Source=users.db";

        //Le os dados do banco de dados
        private DataTable LeDados<S, T>(string query) where S : IDbConnection, new()
                                           where T : IDbDataAdapter, IDisposable, new()
        {
            using (var conn = new S())
            {
                using (var da = new T())
                {
                    using (da.SelectCommand = conn.CreateCommand())
                    {
                        da.SelectCommand.CommandText = query;
                        da.SelectCommand.Connection.ConnectionString = _connectionString;
                        DataSet ds = new DataSet(); //conn é aberto pelo dataadapter
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
        }

        //Método que carrega os dados do banco no dataGrid do formulário (Select no banco)
        public void CarregaDados(DataGridView dataGrid) //Colocar como parametro o dataGrid onde aparecerá os dados
        {
            dataGrid.DataSource = LeDados<SQLiteConnection, SQLiteDataAdapter>("Select * from Usuarios_Sistemas");
        }

        //Método que adiciona novos usúários na tabela
        public void AdicionaUsuario(Usuario usuario)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                int resultado = -1;
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "INSERT INTO Usuarios_Sistemas(matricula, login, nome, sistema, perfil, status)" +
                        "VALUES (@matricula,@login,@nome,@sistema,@perfil,@status)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@matricula", usuario.Matricula);
                    cmd.Parameters.AddWithValue("@login", usuario.Login);
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@sistema", usuario.Sistema.Nome);
                    cmd.Parameters.AddWithValue("@perfil", usuario.Perfil);
                    cmd.Parameters.AddWithValue("@status", 1);
                    try
                    {
                        resultado = cmd.ExecuteNonQuery();                        
                    }
                    catch (SQLiteException ex)
                    {                        
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }            
        }
        
        public void alterarUsuario()
        {

        }

        public void removerUsuario()
        {

        }

    }
}
