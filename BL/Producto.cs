using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        private readonly JgonzalezProgramacionNcapasContext _context;
        public Producto(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetAll(ML.Producto productoParametro)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                var parametros = new[]
                {
                    new SqlParameter("@IdSubcategoria", productoParametro.SubCategoria.IdSubCategoria),
                    new SqlParameter("@IdCategoria", productoParametro.SubCategoria.Categoria.IdCategoria)
                };
                var query = _context.ProductoViews.FromSqlRaw("ProductoGetAll @IdSubcategoria, @IdCategoria", parametros).ToList();
                if (query.Count > 0)
                {
                    foreach (var itemProducto in query)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.Categoria = new ML.Categoria();

                        producto.IdProducto = itemProducto.IdProducto;
                        producto.Nombre = itemProducto.Producto;
                        producto.Descripcion = itemProducto.Descripcion;
                        producto.Precio = itemProducto.Precio;
                        producto.Imagen = itemProducto.Imagen;
                        producto.SubCategoria.IdSubCategoria = itemProducto.IdSubCategoria ?? 0;
                        producto.SubCategoria.Nombre = itemProducto.Subcategoria;
                        producto.SubCategoria.Categoria.IdCategoria = itemProducto.IdCategoria ?? 0;
                        producto.SubCategoria.Categoria.Nombre = itemProducto.Categoria;

                        result.Objects.Add(producto);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
{
                    new SqlParameter("@IdProducto",IdProducto)
                };
                var query = _context.ProductoViews.FromSqlRaw("ProductoGetById @IdProducto", parametros).AsEnumerable().FirstOrDefault();
                if (query != null)
                {
                    ML.Producto producto = new ML.Producto();
                    producto.SubCategoria = new ML.SubCategoria();
                    producto.SubCategoria.Categoria = new ML.Categoria();

                    producto.IdProducto = query.IdProducto;
                    producto.Nombre = query.Producto;
                    producto.Descripcion = query.Descripcion;
                    producto.Precio = query.Precio;
                    producto.Imagen = query.Imagen;
                    producto.SubCategoria.IdSubCategoria = query.IdSubCategoria ?? 0;
                    producto.SubCategoria.Nombre = query.Subcategoria;
                    producto.SubCategoria.Categoria.IdCategoria = query.IdCategoria ?? 0;
                    producto.SubCategoria.Categoria.Nombre = query.Categoria;

                    result.Object = producto;
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
                {
                    new SqlParameter("@Nombre", producto.Nombre ?? ""),
                    new SqlParameter("@Precio", SqlDbType.Decimal) { Value = producto.Precio },
                    new SqlParameter("@Descripcion", producto.Descripcion ?? ""),
                    new SqlParameter("@Imagen", SqlDbType.VarBinary)
                    {
                        Value = (object?)producto.Imagen ?? DBNull.Value
                    },
                    new SqlParameter("@IdSubCategoria", producto.SubCategoria.IdSubCategoria)
                };
                var query = _context.Database.ExecuteSqlRaw("ProductoAdd @Nombre, @Descripcion, @Precio, @Imagen, @IdSubCategoria", parametros);
                if (query > 0)
                {
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;  
        }
        public ML.Result Delete(int idProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.Database.ExecuteSqlRaw("ProductoDelete @IdProducto", new SqlParameter("@IdProducto", idProducto));
                if (query > 0)
                {
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
