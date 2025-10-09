using Api.Esg.Fiap.Data.Repository;
using Api.Esg.Fiap.Models;


namespace Api.Esg.Fiap.Services;

public class ResiduoService : IResiduoService
{

        private readonly IResiduoRepository _repository;

        public ResiduoService(IResiduoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ResiduoModel> ListarResiduos() => _repository.GetAll();

        public ResiduoModel ObterResiduoPorId(int id) => _repository.GetById(id);

        public void CriarResiduo(ResiduoModel residuo) => _repository.Add(residuo);

        public void AtualizarResiduo(ResiduoModel residuo) => _repository.Update(residuo);

        public void DeletarResiduo(int id)
        {
            var residuo = _repository.GetById(id);
            if (residuo != null)
            {
                _repository.Delete(residuo);
            }
        }

    }

