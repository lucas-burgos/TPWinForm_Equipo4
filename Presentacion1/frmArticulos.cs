using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Modelo;
using Negocio;

namespace Presentacion1
{
    public partial class frmArticulos : Form
    {
        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargarLista();
        }

        private void cargarLista()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                lstArticulos.DataSource = negocio.listar();
                lstArticulos.DisplayMember = "Nombre"; 
                lstArticulos.ValueMember = "Id"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstArticulos.SelectedItem != null)
            {
                Articulo seleccionado = (Articulo)lstArticulos.SelectedItem;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstArticulos.SelectedItem != null)
            {
                Articulo seleccionado = (Articulo)lstArticulos.SelectedItem;
                DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar el artículo " + seleccionado.Nombre + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(seleccionado.Id);
                    cargarLista();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}