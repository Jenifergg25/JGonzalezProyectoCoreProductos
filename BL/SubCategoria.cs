using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        private readonly JgonzalezProgramacionNcapasContext _context;
        public SubCategoria(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetByIdCategoria(int idCategoria)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
{
                    new SqlParameter("@IdCategoria",idCategoria)
                };
                var query = _context.SubCategoria.FromSqlRaw("SubCategoriaGetById @IdCategoria", parametros).ToList();
                result.Objects = new List<object>();

                if (query != null && query.Count > 0)
                {
                    foreach (var itemSubCategoria in query)
                    {
                        ML.SubCategoria subCategoria = new ML.SubCategoria();
                        subCategoria.IdSubCategoria = itemSubCategoria.IdSubCategoria;
                        subCategoria.Nombre = itemSubCategoria.Nombre;

                        result.Objects.Add(subCategoria);
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
