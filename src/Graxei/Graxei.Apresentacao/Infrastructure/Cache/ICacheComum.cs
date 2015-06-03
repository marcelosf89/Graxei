using Graxei.Apresentacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Apresentacao.Infrastructure.Cache
{
    public interface ICacheComum
    {
        IpRegiaoModel IpRegiaoModel { get; set; }
    }
}
