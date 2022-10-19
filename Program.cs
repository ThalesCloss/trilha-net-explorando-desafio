using System.Globalization;
using System.Text;
using DesafioProjetoHospedagem.Models;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Console.OutputEncoding = Encoding.UTF8;

Reserva BaseTeste(int capacidade, decimal valorDiaria, int diasReservados)
{
    Suite suite = new Suite(tipoSuite: "Premium", capacidade, valorDiaria);

    Reserva reserva = new Reserva(diasReservados);

    reserva.CadastrarSuite(suite);

    return reserva;
}
List<Pessoa> CriarHospedes(int quantidadeHospedes, string prefixo = "")
{
    return new List<Pessoa>(Enumerable.Range(1, quantidadeHospedes).Select((value) => new Pessoa($"Hóspede {prefixo} {value}")));
}

void ExecutarTestePadrao(int capacidade, decimal valorDiaria, int diasReservados, int quantidadeHospedes)
{
    try
    {
        var reserva = BaseTeste(capacidade, valorDiaria, diasReservados);
        var hospedes = CriarHospedes(quantidadeHospedes);
        reserva.CadastrarHospedes(hospedes);
        Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
        Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria():C}");
    }
    catch (Exception error)
    {
        Console.WriteLine($"Erro ao adicionar hóspedes: {error.Message}");
    }
}

void ExecutarTesteAdicionandoMaisHospedes(int capacidade, decimal valorDiaria, int diasReservados, List<int> quantidadeHospedesPorVez)
{
    try
    {
        var reserva = BaseTeste(capacidade, valorDiaria, diasReservados);
        foreach (var (quantidadeHospedes, prefixo) in quantidadeHospedesPorVez.Select((value, index) => (value, $"add-{index}")))
        {
            reserva.CadastrarHospedes(CriarHospedes(quantidadeHospedes, prefixo));
        }

        Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
        Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria():C}");
    }
    catch (Exception error)
    {
        Console.WriteLine($"Erro ao adicionar hóspedes: {error.Message}");
    }
}
Console.WriteLine("Teste padrão:\n");
ExecutarTestePadrao(capacidade: 2, valorDiaria: 30, diasReservados: 5, quantidadeHospedes: 2);
ExecutarTestePadrao(capacidade: 2, valorDiaria: 30, diasReservados: 10, quantidadeHospedes: 2);
ExecutarTestePadrao(capacidade: 2, valorDiaria: 30, diasReservados: 20, quantidadeHospedes: 1);
ExecutarTestePadrao(capacidade: 1, valorDiaria: 30, diasReservados: 10, quantidadeHospedes: 2);
ExecutarTestePadrao(capacidade: 10, valorDiaria: 3000, diasReservados: 10, quantidadeHospedes: 20);
Console.WriteLine('\n');

Console.WriteLine("Teste adicionando mais hóspedes em uma reserva existente:\n");
ExecutarTesteAdicionandoMaisHospedes(capacidade: 4, valorDiaria: 30, diasReservados: 5, new List<int> { 2, 2 });
ExecutarTesteAdicionandoMaisHospedes(capacidade: 2, valorDiaria: 30, diasReservados: 5, new List<int> { 2, 2 });
ExecutarTesteAdicionandoMaisHospedes(capacidade: 2, valorDiaria: 30, diasReservados: 5, new List<int> { 1, 2 });
ExecutarTesteAdicionandoMaisHospedes(capacidade: 2, valorDiaria: 30, diasReservados: 5, new List<int> { 1, 1, 3 });