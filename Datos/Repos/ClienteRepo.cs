using Datos;
using Datos.Entidades;

namespace Datos.Repos
{
    public interface IClienteRepo
    {
        List<Clientes> GetAll();
        Clientes Get(int id);
        List<Clientes> Search(string nombre);
        void Insert(Clientes cliente);
        void Update(Clientes cliente);
        void Delete(Clientes cliente);
        
    }
    public class ClienteRepo : IClienteRepo
    {
        private readonly EfContext _ctx;

        public ClienteRepo(EfContext ctx)
        {
            _ctx = ctx;
        }

        public Clientes Get(int id)
        {
            var cliente = _ctx.Cliente.FirstOrDefault(f => f.Id == id);
            if(cliente == null)
            {
                throw new InvalidOperationException($"No encontre el cliente con id {id}");
            }
            return cliente;
        }

        public void Insert(Clientes cliente)
        {
            _ctx.Cliente.Add(cliente);
        }

        public void Update(Clientes cliente)
        {
            _ctx.Cliente.Update(cliente);
        }

        public void Delete(Clientes cliente)
        {
            _ctx.Cliente.Remove(cliente);
        }

        public List<Clientes> GetAll()
        {
            var clientes = _ctx.Cliente.ToList();
            if(clientes == null || clientes.Count == 0)
            {
                throw new InvalidOperationException("No encontre ningun cliente");
            }
            return clientes;
        }

        public List<Clientes> Search(string nombre)
        {
            var clientes = _ctx.Cliente.Where(f => f.Nombre == nombre).ToList();
            if (clientes == null)
            {
                throw new InvalidOperationException("No encontre ningun cliente");
            }
            return clientes;
        }
    }
}