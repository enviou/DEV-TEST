using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MuralDeRecados.Models;
using System.Linq.Expressions;

namespace MuralDeRecados.DAL
{
    public class MuralRepositorio : IMuralRepositorio, IDisposable
    {
        private UnitOfWork unitOfWork;

        public MuralRepositorio()
        {
            unitOfWork = new UnitOfWork();
        }

        public void AlterarMural(Mural mural)
        {
            unitOfWork.MuraisRep.Atualizar(mural);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void ExcluirMural(int muralId)
        {
            unitOfWork.MuraisRep.Excluir(muralId);
        }

        public void InserirMural(Mural mural)
        {
            unitOfWork.MuraisRep.Inserir(mural);
        }

        public IEnumerable<Mural> ObterMurais(Expression<Func<Mural, bool>> filter = null, 
                                              string includeProperties = "",
                                              Func<IQueryable<Mural>, IOrderedQueryable<Mural>> orderBy = null
            )
        {
            return unitOfWork.MuraisRep.Obter(filter, includeProperties, orderBy);
        }

        public Mural ObterMuralPorId(int muralId)
        {
            return unitOfWork.MuraisRep.ObterPorId(muralId);
        }

        public void Salvar()
        {
            unitOfWork.Salvar();
        }
    }
}