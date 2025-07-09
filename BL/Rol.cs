using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        private readonly JgonzalezProgramacionNcapasContext _context;

        public Rol(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
        public ML.Result GetAllEFSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                var query = _context.RolGetAll.FromSqlRaw("RolGetAll").ToList();
                result.Objects = new List<object>();
                if (query.Count > 0)
                {
                    foreach (var itemRol in query)
                    {
                        ML.Rol rol = new ML.Rol();
                        rol.IdRol = itemRol.IdRol;
                        rol.Nombre = itemRol.Rol;

                        result.Objects.Add(rol);
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
