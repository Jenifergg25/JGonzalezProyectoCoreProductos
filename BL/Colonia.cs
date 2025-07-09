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
    public class Colonia
    {
        private readonly JgonzalezProgramacionNcapasContext _context;

        public Colonia(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetByIdMunicipio(int IdMUnicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                var parametros = new[]
{
                    new SqlParameter("@IdMunicipio",IdMUnicipio)
                };
                var query = _context.ColoniaGetByIdIdMunicipio.FromSqlRaw("ColoniaGetByIdIdMunicipio @IdMunicipio", parametros).ToList();
                result.Objects = new List<object>();

                if (query != null)
                {
                    foreach (var itemColonia in query)
                    {
                        ML.Colonia colonia = new ML.Colonia();
                        if (colonia.IdColonia == null)
                        {
                            colonia.IdColonia = 0;
                        }
                        else
                        {
                            colonia.IdColonia = itemColonia.IdColonia;
                        }
                        colonia.Nombre = itemColonia.Colonia ?? "";
                        colonia.CodigoPostal = itemColonia.CodigoPostal ?? "";

                        result.Objects.Add(colonia);
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
