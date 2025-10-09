using Api.Esg.Fiap.Data.Repository;

using Api.Esg.Fiap.Models;

namespace Api.Esg.Fiap.Services;

public class ColetaService : IColetaService
{
    private readonly IColetaRepository _repository;

    public ColetaService(IColetaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<ColetaModel> ListarColetas() => _repository.GetAll();

    public ColetaModel ObterColetaPorId(int id) => _repository.GetById(id);

    public void CriarColeta(ColetaModel coleta) => _repository.Add(coleta);        

    public void AtualizarColeta(ColetaModel coleta) => _repository.Update(coleta);

    public void DeletarColeta(int id)
    {
        var coleta = _repository.GetById(id);
        if (coleta != null)
        {
            _repository.Delete(coleta);
        }
    }

}
