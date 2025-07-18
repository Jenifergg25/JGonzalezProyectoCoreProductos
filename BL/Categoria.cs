using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        private readonly JgonzalezProgramacionNcapasContext _context;
        public Categoria(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                var query = _context.Categoria.FromSqlRaw("CategoriaGetAll").ToList();
                result.Objects = new List<object>();
                if (query.Count > 0)
                {
                    foreach ( var itemCategoria in query)
                    {
                        ML.Categoria categoria = new ML.Categoria();
                        categoria.IdCategoria = itemCategoria.IdCategoria;
                        categoria.Nombre = itemCategoria.Nombre;

                        result.Objects.Add(categoria);  
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
    }
}
