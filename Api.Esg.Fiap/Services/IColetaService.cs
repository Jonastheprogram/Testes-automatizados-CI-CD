using Api.Esg.Fiap.Models;

namespace Api.Esg.Fiap.Services;
    public interface IColetaService
    {
        IEnumerable<ColetaModel> ListarColetas();
        ColetaModel ObterColetaPorId(int id);
        void CriarColeta(ColetaModel coleta);
        void AtualizarColeta(ColetaModel coleta);
        void DeletarColeta(int id);
    }

