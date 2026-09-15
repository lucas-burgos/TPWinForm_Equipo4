using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Modelo;

namespace Presentacion1
{
    public partial class frmMarcas : Form
    {
        MarcaNegocio negocio = new MarcaNegocio();

        public frmMarcas()
        {
            InitializeComponent();
        }
        private void frmMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            Marca seleccionada = (Marca)dataGridView1.CurrentRow.DataBoundItem;
            seleccionada.Descripcion = textBox1.Text;
            negocio.modificar(seleccionada);
            cargar();
            textBox1.Clear();
            MessageBox.Show("Marca modificada correctamente.");
        }
        private void cargar()
        {
            dataGridView1.DataSource = negocio.Listar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Marca seleccionada = (Marca)dataGridView1.CurrentRow.DataBoundItem;
                if (seleccionada != null)
                    textBox1.Text = seleccionada.Descripcion;
            }
            
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Marca nueva = new Marca();
            nueva.Descripcion = textBox1.Text;
            negocio.agregar(nueva);
            cargar();
            textBox1.Clear();
            MessageBox.Show("Marca agregada correctamente.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            Marca seleccionada = (Marca)dataGridView1.CurrentRow.DataBoundItem;
            negocio.eliminar(seleccionada.Id);
            cargar();
            textBox1.Clear();
            MessageBox.Show("Marca Eliminada correctamente.");
        }

        
    }
}
