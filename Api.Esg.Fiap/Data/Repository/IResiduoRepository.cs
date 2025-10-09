using Api.Esg.Fiap.Models;

namespace Api.Esg.Fiap.Data.Repository
{
    public interface IResiduoRepository
    {
        IEnumerable<ResiduoModel> GetAll();
        ResiduoModel GetById(int id);
        void Add(ResiduoModel residuo);
        void Update(ResiduoModel residuo);
        void Delete(ResiduoModel residuo);

    }
}
