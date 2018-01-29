using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MuralDeRecados.DAL
{
    public class RepositorioGenerico<TEntity> where TEntity : class
    {
        internal MuralDeRecadosContext ctx;
        internal DbSet<TEntity> dbSet;

        public RepositorioGenerico(MuralDeRecadosContext ctx)
        {
            this.ctx = ctx;
            this.dbSet = ctx.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> ObterComSqlPuro(string query, params object[] parametros)
        {
            return dbSet.SqlQuery(query, parametros).ToList();
        }

        public virtual IEnumerable<TEntity> Obter(Expression<Func<TEntity, bool>> filter = null, 
                                                  string includeProperties = "",
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual TEntity ObterPorId(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Inserir(TEntity entidade)
        {
            dbSet.Add(entidade);
        }

        public virtual void Excluir(object id)
        {
            TEntity remover = dbSet.Find(id);
            if (ctx.Entry(remover).State == EntityState.Detached)
            {
                dbSet.Attach(remover);
            }
            dbSet.Remove(remover);
        }

        public virtual void Atualizar(TEntity entidade)
        {
            dbSet.Attach(entidade);
            ctx.Entry(entidade).State = EntityState.Modified;
        }
    }
}