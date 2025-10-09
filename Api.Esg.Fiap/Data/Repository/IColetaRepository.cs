using Api.Esg.Fiap.Models;

namespace Api.Esg.Fiap.Data.Repository
{
    public interface IColetaRepository
    {
        IEnumerable<ColetaModel> GetAll();
        ColetaModel GetById(int id);
        void Add(ColetaModel cliente);
        void Update(ColetaModel cliente);
        void Delete(ColetaModel cliente);
    }
}
