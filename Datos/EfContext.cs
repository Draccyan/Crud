using System.Data;
using Datos.Entidades;
using Datos.Exceptions;
using Datos.Repos;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    //public interface IContext
    //{
    //    IDbConnection GetDbConnection();
    //    IClienteRepo ClienteRepo { get; }
    //    int SaveChanges();
    //}
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {

        }
        public EfContext(string connectionString) : base(GetOptions(connectionString))
        { }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
        }
        #region DbSet

        public DbSet<Clientes> Cliente { get; set; }

        #endregion

        #region Repos
        ////public IClienteRepo ClienteRepo => new ClienteRepo(this);
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>().HasKey(k => k.Id);
        }

        public override int SaveChanges()
        {
            try
            {
                var recordModified = base.SaveChanges();
                //
                return recordModified;
            }
            catch (Exception ex)
            {
                //Tratamos de obtener mas informacion sobre la expection
                var e = ExceptionHelper.Handle(ex);
                //Siempre hacemos el throw
                throw e;
            }

        }
    }
}

