using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Negocio;

namespace Presentacion1
{
    public partial class frmCategorias : Form
    {
        CategoriaNegocio negocio = new CategoriaNegocio();

        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            dataGridView1.DataSource = negocio.listar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Categoria nueva = new Categoria();

            nueva.Descripcion = textBox1.Text;

            negocio.agregar(nueva);

            cargar();

            textBox1.Clear();

            MessageBox.Show("Categoría agregada correctamente.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            Categoria seleccionada = (Categoria)dataGridView1.CurrentRow.DataBoundItem;

            seleccionada.Descripcion = textBox1.Text;

            negocio.modificar(seleccionada);

            cargar();

            textBox1.Clear();

            MessageBox.Show("Categoría modificada correctamente.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            Categoria seleccionada = (Categoria)dataGridView1.CurrentRow.DataBoundItem;

            negocio.eliminar(seleccionada.Id);

            cargar();

            textBox1.Clear();

            MessageBox.Show("Categoría eliminada correctamente.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Categoria seleccionada =
                    (Categoria)dataGridView1.CurrentRow.DataBoundItem;

                if (seleccionada != null)
                    textBox1.Text = seleccionada.Descripcion;
            }
        }

       
    }
}