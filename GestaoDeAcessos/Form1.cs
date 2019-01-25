using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoDeAcessos
{
    public partial class Form1 : Form
    {

        string connectionString = @"Data Source=users.db";

        private Sistema pagnet = new Sistema("Pagnet");
        private Sistema debnet = new Sistema("Debnet");
        private Sistema esegVida = new Sistema("EsegVida");

        private Perfil[] perfisPagnet = new Perfil[2];
        private Perfil[] perfisDebnet = new Perfil[2];
        private Perfil[] perfisEsegVida = new Perfil[2];


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaDados();

            perfisPagnet[0] = new Perfil("Consulta", pagnet);
            perfisPagnet[1] = new Perfil("Admin", pagnet);
            perfisDebnet[0] = new Perfil("Financeiro", debnet);
            perfisDebnet[1] = new Perfil("Operações", debnet);
            perfisEsegVida[0] = new Perfil("Consulta", debnet);
            perfisEsegVida[1] = new Perfil("Agents", debnet);

            comboBoxSistema.Items.Add(pagnet.Nome);
            comboBoxSistema.Items.Add(debnet.Nome);
            comboBoxSistema.Items.Add(esegVida.Nome);
        }

        private void CarregaDados()
        {
            dataGridView1.DataSource = LeDados<SQLiteConnection, SQLiteDataAdapter>("Select * from Usuarios_Sistemas");
        }

        public DataTable LeDados<S, T>(string query) where S : IDbConnection, new()
                                           where T : IDbDataAdapter, IDisposable, new()
        {
            using (var conn = new S())
            {
                using (var da = new T())
                {
                    using (da.SelectCommand = conn.CreateCommand())
                    {
                        da.SelectCommand.CommandText = query;
                        da.SelectCommand.Connection.ConnectionString = connectionString;
                        DataSet ds = new DataSet(); //conn é aberto pelo dataadapter
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            usuario.Matricula = Convert.ToInt32(txtMatricula.Text);
            usuario.Nome = txtNome.Text;
            usuario.Login = txtLogin.Text;

            if (comboBoxSistema.Text == pagnet.Nome)
                usuario.Sistema = pagnet;
            if (comboBoxSistema.Text == debnet.Nome)
                usuario.Sistema = pagnet;
            if (comboBoxSistema.Text == esegVida.Nome)
                usuario.Sistema = pagnet;

            usuario.Perfil = comboBoxPerfil.Text;

            String[] novoUsuario = new String[6];
            novoUsuario[0] = usuario.Sistema.Nome;
            novoUsuario[1] = usuario.Nome;
            novoUsuario[2] = usuario.Login;
            novoUsuario[3] = usuario.Perfil;
            novoUsuario[4] = usuario.Status;
            novoUsuario[5] = Convert.ToString(usuario.Matricula);

            ListViewItem linhaUsuario = new ListViewItem(novoUsuario);

            UsersListView.Items.Add(linhaUsuario);
        }


        private void comboBoxSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxSistema.Text == pagnet.Nome)
            {
                comboBoxPerfil.Items.Clear();

                foreach (var perfil in perfisPagnet)
                {
                    comboBoxPerfil.Items.Add(perfil.Nome);
                }
            }

            if (comboBoxSistema.Text == debnet.Nome)
            {
                comboBoxPerfil.Items.Clear();

                foreach (var perfil in perfisDebnet)
                {
                    comboBoxPerfil.Items.Add(perfil.Nome);
                }
            }

            if (comboBoxSistema.Text == esegVida.Nome)
            {
                comboBoxPerfil.Items.Clear();

                foreach (var perfil in perfisEsegVida)
                {
                    comboBoxPerfil.Items.Add(perfil.Nome);
                }
            }
        }

        private void comboBoxPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
