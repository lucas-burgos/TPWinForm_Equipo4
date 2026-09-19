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
            dgvArticulos.AccessibleName = "Lista de productos";
            dgvArticulos.DataError += dgvArticulos_DataError;
            txtBuscar.AccessibleDescription = "Busca por nombre o codigo";
            cargarDesplegables();
            cargarDatos();
            dgvArticulos.Focus();
        }

        private void cargarDesplegables()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                List<Marca> listaMarcas = marcaNegocio.Listar();
                listaMarcas.Insert(0, new Marca { Id = 0, Descripcion = "Todas" });
                cboFiltroMarca.DataSource = listaMarcas;
                cboFiltroMarca.ValueMember = "Id";
                cboFiltroMarca.DisplayMember = "Descripcion";

                List<Categoria> listaCategorias = categoriaNegocio.listar();
                listaCategorias.Insert(0, new Categoria { Id = 0, Descripcion = "Todas" });
                cboFiltroCategoria.DataSource = listaCategorias;
                cboFiltroCategoria.ValueMember = "Id";
                cboFiltroCategoria.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los filtros: " + ex.Message);
            }
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

        // --- LÓGICA DEL FILTRO COMBINADO ---

        private void aplicarFiltro()
        {
            if (listaArticulos == null) return;

            List<Articulo> filtrados = listaArticulos;

            string texto = txtBuscar.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(texto))
            {
                filtrados = filtrados.Where(x =>
                    (x.Nombre != null && x.Nombre.ToLower().Contains(texto)) ||
                    (x.Codigo != null && x.Codigo.ToLower().Contains(texto))
                ).ToList();
            }

            if (cboFiltroMarca.SelectedItem != null)
            {
                Marca marcaSel = (Marca)cboFiltroMarca.SelectedItem;
                if (marcaSel.Id != 0)
                {
                    filtrados = filtrados.Where(x => x.Marca != null && x.Marca.Id == marcaSel.Id).ToList();
                }
            }

            if (cboFiltroCategoria.SelectedItem != null)
            {
                Categoria categoriaSel = (Categoria)cboFiltroCategoria.SelectedItem;
                if (categoriaSel.Id != 0)
                {
                    filtrados = filtrados.Where(x => x.Categoria != null && x.Categoria.Id == categoriaSel.Id).ToList();
                }
            }

            dgvArticulos.DataSource = filtrados;
            ocultarColumnas();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            aplicarFiltro();
        }

        private void cboFiltroMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            aplicarFiltro();
        }

        private void cboFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            aplicarFiltro();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmArticuloAltas alta = new FrmArticuloAltas();
            alta.ShowDialog();
            cargarDatos();
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

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            frmMarcas ventana = new frmMarcas();
            ventana.ShowDialog();
            cargarDesplegables(); // Refrescamos por si agregaron marcas
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategorias ventana = new frmCategorias();
            ventana.ShowDialog();
            cargarDesplegables(); // Refrescamos por si agregaron categorías
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}