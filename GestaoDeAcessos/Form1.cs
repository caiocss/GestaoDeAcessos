using GestaoDeAcessos.Classes;
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

        //string connectionString = @"Data Source=users.db";

        //Instancia e cria 3 sistemas para testar o formulário;
        private Sistema pagnet = new Sistema("Pagnet");
        private Sistema debnet = new Sistema("Debnet");
        private Sistema esegVida = new Sistema("EsegVida");

        //Cria arrays de perfis para utilizar no teste do formulário
        private Perfil[] perfisPagnet = new Perfil[2];
        private Perfil[] perfisDebnet = new Perfil[2];
        private Perfil[] perfisEsegVida = new Perfil[2];

        //Instancia a classe de conexão com bando de dados
        ConnectDatabase database = new ConnectDatabase();


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Carrega os dados do banco de dados no DataGrid do formulário
            database.CarregaDados(dataGridView1);

            //Cria os perfis dos sistemas e adiciona no comboBox de perfil no sistema
            perfisPagnet[0] = new Perfil("Consulta", pagnet);
            perfisPagnet[1] = new Perfil("Admin", pagnet);
            perfisDebnet[0] = new Perfil("Financeiro", debnet);
            perfisDebnet[1] = new Perfil("Operações", debnet);
            perfisEsegVida[0] = new Perfil("Consulta", debnet);
            perfisEsegVida[1] = new Perfil("Agents", debnet);

            //Adiciona os sistemas na comboBox de Sistemas
            comboBoxSistema.Items.Add(pagnet.Nome);
            comboBoxSistema.Items.Add(debnet.Nome);
            comboBoxSistema.Items.Add(esegVida.Nome);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            //Cria novo usuário e pega os dados dos campos do formulário;
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

            //Adiona o usuário no banco de dados
            try
            {
                database.AdicionaUsuario(usuario);
                database.CarregaDados(dataGridView1);
                MessageBox.Show("Usuário cadastrado!");

                //Limpa os campos do formulário após adicionar o usuário com sucesso.
                txtMatricula.Text = "";
                txtNome.Text = "";
                txtLogin.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário");
                MessageBox.Show(ex.Message);
            }
        }
        
        //comboBox Sistema, quando escolhe um sistema nesse comboBox
        //Automaticamete atualiza os perfis no comboBox de perfil.
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtBuscar.Text == "")
                MessageBox.Show("Campo de busca vazio");
            else
            {
                database.CarregaDadosPesquisa(dataGridView1, txtBuscar.Text);
                txtBuscar.Text = "";
            }
        }

        private void btnLimparBusca_Click(object sender, EventArgs e)
        {
            database.CarregaDados(dataGridView1);
            txtBuscar.Text = "";
        }
    }
}
