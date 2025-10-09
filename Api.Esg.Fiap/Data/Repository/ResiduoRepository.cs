
using Api.Esg.Fiap.Models;
using Api.Esg.Fiap.Data.Contexts;
namespace Api.Esg.Fiap.Data.Repository
{
    public class ResiduoRepository : IResiduoRepository
    {

        private readonly DatabaseContext _context;

        public ResiduoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ResiduoModel> GetAll() => _context.Residuos.ToList();

        public ResiduoModel GetById(int id) => _context.Residuos.Find(id);

        public void Add(ResiduoModel residuo)
        {
            _context.Residuos.Add(residuo);
            _context.SaveChanges();
        }

        public void Update(ResiduoModel residuo)
        {
            _context.Residuos.Update(residuo);
            _context.SaveChanges();
        }

        public void Delete(ResiduoModel residuo)
        {
            _context.Residuos.Remove(residuo);
            _context.SaveChanges();
        }
    }
}
