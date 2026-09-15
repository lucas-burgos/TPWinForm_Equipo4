using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(bool MostrarTodo)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
if (MostrarTodo)
                {
                    datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, " +
                                         "A.IdMarca, M.Descripcion AS Marca, " +
                                         "A.IdCategoria, C.Descripcion AS Categoria " +
                                         "FROM ARTICULOS A " +
                                         "LEFT JOIN MARCAS M ON M.Id = A.IdMarca " +
                                         "LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria");
                }
else
                {
                    datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, " +
     "A.IdMarca, M.Descripcion AS Marca, " +
     "A.IdCategoria, C.Descripcion AS Categoria " +
     "FROM ARTICULOS A " +
     "INNER JOIN MARCAS M ON M.Id = A.IdMarca " +
     "INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria");
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];

                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];

                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];

                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                datos.cerrarConexion();

                foreach (Articulo art in lista)
                {
                    art.Imagenes = listarImagenesPorArticulo(art.Id);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Imagen> listarImagenesPorArticulo(int idArticulo)
        {
            List<Imagen> listaImagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen img = new Imagen();
                    img.Id = (int)datos.Lector["Id"];
                    img.IdArticulo = (int)datos.Lector["IdArticulo"];
                    img.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    listaImagenes.Add(img);
                }

                return listaImagenes;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) OUTPUT INSERTED.Id VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio)");

                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@precio", nuevo.Precio);

                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    nuevo.Id = (int)datos.Lector["Id"];
                }
                datos.cerrarConexion(); // Cerramos la conexión principal antes de abrir otras

                if (nuevo.Imagenes != null)
                {
                    foreach (Imagen img in nuevo.Imagenes)
                    {
                        AccesoDatos datosImg = new AccesoDatos();
                        try
                        {
                            datosImg.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idArticulo, @imagenUrl)");
                            datosImg.setearParametro("@idArticulo", nuevo.Id);
                            datosImg.setearParametro("@imagenUrl", img.ImagenUrl);
                            datosImg.ejecutarAccion();
                        }
                        finally
                        {
                            datosImg.cerrarConexion();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // 1. Modificamos los datos principales del artículo
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, Precio = @precio WHERE Id = @id");
                datos.setearParametro("@codigo", articulo.Codigo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idMarca", articulo.Marca.Id);
                datos.setearParametro("@idCategoria", articulo.Categoria.Id);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.Id);
                datos.ejecutarAccion();
                datos.cerrarConexion(); // Cerramos antes de ejecutar la siguiente consulta

                // 2. Borramos las imágenes viejas de este artículo
                AccesoDatos datosBorrarImg = new AccesoDatos();
                try
                {
                    datosBorrarImg.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @idArticulo");
                    datosBorrarImg.setearParametro("@idArticulo", articulo.Id);
                    datosBorrarImg.ejecutarAccion();
                }
                finally
                {
                    datosBorrarImg.cerrarConexion();
                }

                // 3. Insertamos la lista de imágenes actualizadas
                if (articulo.Imagenes != null)
                {
                    foreach (Imagen img in articulo.Imagenes)
                    {
                        AccesoDatos datosImg = new AccesoDatos();
                        try
                        {
                            datosImg.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idArticulo, @imagenUrl)");
                            datosImg.setearParametro("@idArticulo", articulo.Id);
                            datosImg.setearParametro("@imagenUrl", img.ImagenUrl);
                            datosImg.ejecutarAccion();
                        }
                        finally
                        {
                            datosImg.cerrarConexion();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}