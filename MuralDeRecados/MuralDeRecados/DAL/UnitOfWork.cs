using MuralDeRecados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuralDeRecados.DAL
{
    public class UnitOfWork : IDisposable
    {
        private MuralDeRecadosContext ctx = new MuralDeRecadosContext();
        private RepositorioGenerico<Mural> muraisRep;
        private RepositorioGenerico<Usuario> usuariosRep;

        public RepositorioGenerico<Mural> MuraisRep
        {
            get
            {
                if (muraisRep == null)
                {
                    muraisRep = new RepositorioGenerico<Mural>(ctx);
                }
                return muraisRep;
            }
        }

        public RepositorioGenerico<Usuario> UsuariosRep
        {
            get
            {
                if (usuariosRep == null)
                {
                    usuariosRep = new RepositorioGenerico<Usuario>(ctx);
                }
                return usuariosRep;
            }
        }

        public void Salvar()
        {
            ctx.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}