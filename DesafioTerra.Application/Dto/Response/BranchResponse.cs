using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Dto.Response
{
    public class BranchResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<BranchList> Branchs { get; set; }
    }

    public class BranchList
    {
        public string Name { get; set; }
    }
}
