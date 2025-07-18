using DL;
using ML;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BL
{
    public class Municipio
    {
        private readonly JgonzalezProgramacionNcapasContext _context;

        public Municipio(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
{
                    new SqlParameter("@IdEstado",IdEstado)
                };
                var query = _context.Municipios.FromSqlRaw("MunicipioGetByIdEstado @IdEstado", parametros).ToList();
                result.Objects = new List<object>();

                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        ML.Municipio municipio = new ML.Municipio();
                        municipio.IdMunicipio = item.IdMunicipio;
                        municipio.Nombre = item.Nombre ?? "";

                        result.Objects.Add(municipio);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron municipios para este estado.";
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
