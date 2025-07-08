using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        private readonly JgonzalezProgramacionNcapasContext _context;

        public Usuario(JgonzalezProgramacionNcapasContext context)
        {
            _context = context;
        }
    }
}
