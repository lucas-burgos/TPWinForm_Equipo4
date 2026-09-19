using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Modelo;
using Negocio;

namespace Presentacion1
{
    public partial class FrmVerArticulo : Form
    {
        private Articulo articulo;
        private int indiceImagen = 0;

        public FrmVerArticulo(Articulo articuloSeleccionado)
        {
            InitializeComponent();
            this.articulo = articuloSeleccionado;
        }

        private void FrmVerArticulo_Load(object sender, EventArgs e)
        {
            txtDetalles.Text = $"Código: {articulo.Codigo}\r\n\r\n" +
                               $"Nombre: {articulo.Nombre}\r\n\r\n" +
                               $"Descripción: {articulo.Descripcion}\r\n\r\n" +
                               $"Marca: {(articulo.Marca != null ? articulo.Marca.Descripcion : "-")}\r\n\r\n" +
                               $"Categoría: {(articulo.Categoria != null ? articulo.Categoria.Descripcion : "-")}\r\n\r\n" +
                               $"Precio: {articulo.Precio:C}";

            cargarImagen();

            tmrCarrusel.Interval = 3000;
            tmrCarrusel.Start();
        }

        private void cargarImagen()
        {
            if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
            {
                try
                {
                    pbxImagen.Load(articulo.Imagenes[indiceImagen].ImagenUrl);
                }
                catch (Exception)
                {
                    pbxImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
                }
            }
            else
            {
                pbxImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void automatico()
        {
            if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
            {
                indiceImagen++;
                if (indiceImagen >= articulo.Imagenes.Count)
                    indiceImagen = 0;

                cargarImagen();
            }
        }

        private void tmrCarrusel_Tick(object sender, EventArgs e)
        {
            automatico();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            automatico();
            Reiniciar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
            {
                indiceImagen--;
                if (indiceImagen < 0)
                    indiceImagen = articulo.Imagenes.Count - 1;

                cargarImagen();
                Reiniciar();
            }
        }

        private void Reiniciar()
        {
            tmrCarrusel.Stop();
            tmrCarrusel.Start();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}