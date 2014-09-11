using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petra.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ManterCaseAttribute : System.Attribute
    {
        public ManterCaseAttribute()
        {
        }
        /*
        public string NomeEntidade { get; set; }
        public string Objeto { get; set; }
        public string Campo { get; set; }
        // "Dardani.EDU.BO.NH.SalaDAO"
        public string ClasseDao { get; set; }
        //"Dardani.EDU.BO.dll";
        public string FileName { get; set; }
         */ 
    }
}
