using MuralDeRecados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MuralDeRecados.DAL
{
    interface IUsuarioRepositorio : IDisposable
    {
        IEnumerable<Usuario> ObterUsuarios(Expression<Func<Usuario, bool>> filter, string includeProperties,
                                           Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy);
        Usuario ObterLogin(string usuario, string senha);
        Usuario ObterUsuarioPorId(int muralId);
        void InserirUsuario(Usuario usuario);
        void Salvar();
    }
}
