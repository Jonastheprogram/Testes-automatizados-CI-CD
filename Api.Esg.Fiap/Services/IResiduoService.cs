using Api.Esg.Fiap.Models;

namespace Api.Esg.Fiap.Services
{
    public interface IResiduoService
    {
        IEnumerable<ResiduoModel> ListarResiduos();
        ResiduoModel ObterResiduoPorId(int id);
        void CriarResiduo(ResiduoModel residuo);
        void AtualizarResiduo(ResiduoModel residuo);
        void DeletarResiduo(int id);

    }
}
