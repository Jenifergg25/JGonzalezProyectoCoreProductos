using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        private readonly JgonzalezProgramacionNcapasContext _context;
        public Estado(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }

        public ML.Result GetAllEFSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                var query = _context.EstadoGetAll.FromSqlRaw("EstadoGetAll").ToList();
                result.Objects = new List<object>();
                if (query.Count > 0)
                {
                    foreach (var itemEstado in query)
                    {
                        ML.Estado estado = new ML.Estado();
                        estado.IdEstado = itemEstado.IdEstado;
                        estado.Nombre = itemEstado.Estado;

                        result.Objects.Add(estado);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "Sin resultados";
                    result.Correct = false;
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
