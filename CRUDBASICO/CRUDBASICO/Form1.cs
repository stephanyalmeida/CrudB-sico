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

namespace CRUDBASICO
{
    public partial class frm_cadastro : Form
    {
        SqlConnection conexao;
        SqlCommand cmd;
        SqlDataAdapter adapter; 
        SqlDataReader reader;   

        string sql;
        public frm_cadastro()
        {
            InitializeComponent();
        }

        private void frm_cadastro_Load(object sender, EventArgs e)
        {

        }

        private void btn_sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            if (text_nome.Text != "" & text_numero.Text != "")
            {
                try
                {
                    conexao = new SqlConnection(@"Server=DESKTOP-3O6SOMD\SQLEXPRESS;Database=cadastro;User Id=sa;Password=qwe123;");
                    sql = "insert into dados (nome, numero) values (@nome, @numero)";
                    cmd = new SqlCommand(sql, conexao);
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = text_nome.Text;
                    cmd.Parameters.Add("@numero", SqlDbType.VarChar).Value = text_numero.Text;
                    conexao.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cadastro Efetuado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    conexao.Close();

                }
            }
            else
            {
                MessageBox.Show("Por favor digite todos os dados obrigatórios", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
                


        }

        private void btn_exibir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-3O6SOMD\SQLEXPRESS;Database=cadastro;User Id=sa;Password=qwe123;");
                sql = "select id as 'ID', nome as 'NOME', numero as 'TELEFONE' from dados order by id";
                DataSet ds = new DataSet();
                adapter = new SqlDataAdapter(sql, conexao);
                conexao.Open();
                adapter.Fill(ds);
                dataGridView1.DataSource=ds.Tables[0];
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conexao.Close();

            }
        }

        private void btn_consultar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-3O6SOMD\SQLEXPRESS;Database=cadastro;User Id=sa;Password=qwe123;");
                sql = "select * from dados where id = @id";
                cmd = new SqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@id", text_id.Text);
                                
                conexao.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    text_nome.Text = (string) reader["nome"];
                    text_numero.Text = Convert.ToString(reader["numero"]);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conexao.Close();

            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-3O6SOMD\SQLEXPRESS;Database=cadastro;User Id=sa;Password=qwe123;");
                sql = "update dados set nome = @nome, numero = @numero where id = @id";
                cmd = new SqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = text_id.Text;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = text_nome.Text;
                cmd.Parameters.Add("@numero", SqlDbType.VarChar).Value = text_numero.Text;
                conexao.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Dado Editado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conexao.Close();

            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-3O6SOMD\SQLEXPRESS;Database=cadastro;User Id=sa;Password=qwe123;");
                sql = "delete dados where id = @id";
                cmd = new SqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = text_id.Text;
                
                conexao.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Dado excluído com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conexao.Close();

            }
        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            text_id.Text = "";
            text_nome.Text = "";
            text_numero.Text = "";
        }
    }
}
