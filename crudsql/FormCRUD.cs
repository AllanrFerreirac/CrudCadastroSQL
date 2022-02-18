using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUDSQL2022
{
    public partial class FormCRUD : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\crudsql\\DbEvento.mdf;Integrated Security=True");
        public FormCRUD()
        {
            InitializeComponent();
        }

        public void CarregaDgv() 
        {
            String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\crudsql\\DbEvento.mdf;Integrated Security=True";
            String query = "SELECT * FROM Evento";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable evento = new DataTable();
            da.Fill(evento);
            dgvEvento.DataSource = evento;
            con.Close();
        }

        private void FormCRUD_Load(object sender, EventArgs e)
        {
            CarregaDgv();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Inserir", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
            cmd.Parameters.AddWithValue("@data_nascimento", SqlDbType.NChar).Value = txtData_nascimento.Text.Trim();
            cmd.Parameters.AddWithValue("@cidade", SqlDbType.NChar).Value = txtCidade.Text.Trim();
            cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
            cmd.ExecuteNonQuery();
            CarregaDgv();
            MessageBox.Show("Pessoa cadastrada com sucesso!", "Cadastro de Pessoas");
            txtNome.Text = "";
            txtData_nascimento.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";
            con.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Atualizar", con);
                cmd.Parameters.AddWithValue("@id", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.Parameters.AddWithValue("@data_nascimento", SqlDbType.NChar).Value = txtData_nascimento.Text.Trim();
                cmd.Parameters.AddWithValue("@cidade", SqlDbType.NChar).Value = txtCidade.Text.Trim();
                cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
                cmd.ExecuteNonQuery();
                CarregaDgv();
                MessageBox.Show("Pessoa atualizada com sucesso!", "Atualização de Pessoas");
                txtNome.Text = "";
                txtData_nascimento.Text = "";
                txtCidade.Text = "";
                txtCelular.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Excluir", con);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                CarregaDgv();
                MessageBox.Show("Pessoa excluída com sucesso!", "Exclusão de Pessoas");
                txtNome.Text = "";
                txtData_nascimento.Text = "";
                txtCidade.Text = "";
                txtCelular.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Localizar", con);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNome.Text = dr["nome"].ToString();
                    txtData_nascimento.Text = dr["data_nascimento"].ToString();
                    txtCidade.Text = dr["cidade"].ToString();
                    txtCelular.Text = dr["celular"].ToString();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!");
                }
            }
            finally
            {

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtData_nascimento.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";
        }

        private void dgvEvento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvEvento.Rows[e.RowIndex];
                txtNome.Text = row.Cells[1].Value.ToString();
                txtData_nascimento.Text = row.Cells[2].Value.ToString();
                txtCidade.Text = row.Cells[3].Value.ToString();
                txtCelular.Text = row.Cells[4].Value.ToString();

            }
        }
    }
}
