namespace DesafioProjetoHospedagem.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
            Hospedes = new List<Pessoa>();
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            if (Suite == null)
            {
                throw new Exception("Nenhuma suite foi adicionada a reserva, informe a suite antes de adicionar os hóspedes");
            }
            int quantidadeHospedes = ObterQuantidadeHospedes() + hospedes.Count;
            if (quantidadeHospedes > Suite.Capacidade)
            {
                throw ObterQuantidadeHospedes() > 0 ?
                new Exception($"Não é possível adicionar mais {hospedes.Count} hóspede(s) na reserva. A capacidade da Suite é {Suite.Capacidade} e já possuí {ObterQuantidadeHospedes()} hóspede(s)") :
                new Exception($"A quantidade de hóspedes ({quantidadeHospedes}) é maior que a capacidade disponível na suite ({Suite.Capacidade}), não é possível efetuar a reserva");
            }

            Hospedes.AddRange(hospedes);
        }

        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            return Hospedes.Count;
        }

        public decimal CalcularValorDiaria()
        {
            decimal valor = DiasReservados * Suite.ValorDiaria;
            if (DiasReservados >= 10)
            {
                valor -= valor * 0.10m;
            }
            return valor;
        }
    }
}