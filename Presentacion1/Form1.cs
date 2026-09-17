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
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvArticulos.DataError += dgvArticulos_DataError;
            cargarDatos();
            dgvArticulos.Focus();
        }

        private void cargarDatos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar(false);
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.ReadOnly = true;
                dgvArticulos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvArticulos.StandardTab = true;    
                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los artículos: " + ex.Message);
            }
        }

        private void ocultarColumnas()
        {
            if (dgvArticulos.Columns["Id"] != null)
                dgvArticulos.Columns["Id"].Visible = false;

            if (dgvArticulos.Columns["Imagenes"] != null)
                dgvArticulos.Columns["Imagenes"].Visible = false;

            if (dgvArticulos.Columns["Precio"] != null)
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "C";
        }

        private void dgvArticulos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmArticuloAltas alta = new FrmArticuloAltas();
            alta.ShowDialog();
            cargarDatos(); // Recarga la grilla al cerrar la ventana de alta
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                FrmArticuloAltas modificar = new FrmArticuloAltas(seleccionado);
                modificar.ShowDialog();

                cargarDatos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un artículo de la grilla para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar el artículo " + seleccionado.Nombre + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(seleccionado.Id);
                    cargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un artículo de la grilla para eliminar.");
            }
        }

        
        private void btnMarcas_Click(object sender, EventArgs e)
        {
            frmMarcas ventana = new frmMarcas();
            ventana.ShowDialog();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategorias ventana = new frmCategorias();
            ventana.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                FrmVerArticulo ver = new FrmVerArticulo(seleccionado);
                ver.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un artículo de la grilla para ver sus detalles.");
            }
        }
    }
}