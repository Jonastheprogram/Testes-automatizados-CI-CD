namespace Api.Esg.Fiap.Models
{
    public class ColetaModel
    {
       public int ColetaId { get; set; }
        public string PontoColeta { get; set; }
        public double CapacidadeMax { get; set; }
        public double QtdAtual { get; set; }

    }
}
