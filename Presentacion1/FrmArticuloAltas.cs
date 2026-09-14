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
    public partial class FrmArticuloAltas : Form
    {
        private List<Imagen> listaImagenesLocal;
        private Articulo articulo = null;

        public FrmArticuloAltas()
        {
            InitializeComponent();
            listaImagenesLocal = new List<Imagen>();
        }
        public FrmArticuloAltas(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            listaImagenesLocal = new List<Imagen>();
        }
        private void FrmArticuloAltas_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                cboMarca.DataSource = marcaNegocio.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();

                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;

                    if (articulo.Imagenes != null)
                    {
                        listaImagenesLocal = articulo.Imagenes;
                        lstImagenes.DataSource = null;
                        lstImagenes.DataSource = listaImagenesLocal;
                        lstImagenes.DisplayMember = "ImagenUrl";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUrlImagen.Text))
            {
                Imagen nueva = new Imagen();
                nueva.ImagenUrl = txtUrlImagen.Text;
                listaImagenesLocal.Add(nueva);

                lstImagenes.DataSource = null;
                lstImagenes.DataSource = listaImagenesLocal;
                lstImagenes.DisplayMember = "ImagenUrl";

                txtUrlImagen.Clear(); // Limpiamos la caja de texto
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Imagenes = listaImagenesLocal; // Le pasamos las imágenes (nuevas o editadas)

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el artículo: " + ex.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}