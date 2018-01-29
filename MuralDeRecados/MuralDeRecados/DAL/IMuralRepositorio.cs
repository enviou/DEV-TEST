using MuralDeRecados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MuralDeRecados.DAL
{
    interface IMuralRepositorio : IDisposable
    {
        IEnumerable<Mural> ObterMurais(Expression<Func<Mural, bool>> filter, string includeProperties, 
                                       Func<IQueryable<Mural>, IOrderedQueryable<Mural>> orderBy);
        Mural ObterMuralPorId(int muralId);
        void InserirMural(Mural mural);
        void ExcluirMural(int muralId);
        void AlterarMural(Mural mural);
        void Salvar();
    }
}
