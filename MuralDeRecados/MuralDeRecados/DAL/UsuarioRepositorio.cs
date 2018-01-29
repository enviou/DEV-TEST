using System;
using System.Collections.Generic;
using System.Linq;
using MuralDeRecados.Models;
using System.Linq.Expressions;
using MuralDeRecados.Utils;

namespace MuralDeRecados.DAL
{
    public class UsuarioRepositorio : IUsuarioRepositorio, IDisposable
    {
        private UnitOfWork unitOfWork;

        public UsuarioRepositorio()
        {
            unitOfWork = new UnitOfWork();
        }

        public IEnumerable<Usuario> ObterUsuarios(Expression<Func<Usuario, bool>> filter = null, string includeProperties = "",
                                                   Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy = null)
        {
            return unitOfWork.UsuariosRep.Obter(filter, includeProperties, orderBy);
        }

        public Usuario ObterLogin(string usuario, string senha)
        {
            return unitOfWork.UsuariosRep.Obter(filter: u => (u.Email.Equals(usuario) || u.LoginRedeSocial.Equals(usuario)) && u.Senha.Equals(senha)).FirstOrDefault();
        }

        public Usuario ObterUsuarioPorId(int usuarioId)
        {
            return unitOfWork.UsuariosRep.ObterPorId(usuarioId);
        }

        public void InserirUsuario(Usuario usuario)
        {
            usuario.Senha = CriptografiaService.Encrypt(usuario.Senha);
            unitOfWork.UsuariosRep.Inserir(usuario);
            unitOfWork.Salvar();
        }

        public void Salvar()
        {
            unitOfWork.Salvar();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}