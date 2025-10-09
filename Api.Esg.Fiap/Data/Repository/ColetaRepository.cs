using Api.Esg.Fiap.Data.Contexts;
using Api.Esg.Fiap.Data.Repository;
using Api.Esg.Fiap.Models;


public class ColetaRepository : IColetaRepository
{
    private readonly DatabaseContext _context;

    public ColetaRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IEnumerable<ColetaModel> GetAll() => _context.Coletas.ToList();

    public ColetaModel GetById(int id) => _context.Coletas.Find(id);

    public void Add(ColetaModel coleta)
    {
        _context.Coletas.Add(coleta);
        _context.SaveChanges();
    }

    public void Update(ColetaModel coleta)
    {
        _context.Update(coleta);
        _context.SaveChanges();
    }

    public void Delete(ColetaModel coleta)
    {
        _context.Coletas.Remove(coleta);
        _context.SaveChanges();
    }
}