using BarbeariaApi.Models;
using Xunit;

namespace BarbeariaApi.Tests;

public class AgendamentoTests
{
    [Fact]
    //Avisa ao Rider: "Isto é um teste, rode-o!

    public void DeveCriarAgendamentoComOsDadosCorretos()
    {
        // 1. ARRANGE (Preparar o cenário)
        var nomeEsperado = "Moizes Galdino";
        var servicoEsperado = "Corte e Barba";

        // 2. ACT (Agir - Vamos tentar criar a ficha na memória)
        var agendamento = new Agendamento
        {
            NomeCliente = nomeEsperado,
            Servico = servicoEsperado
        };
        
        // 3. ASSERT (Verificar - O robô confere se o C# guardou as coisas no lugar certo)
        Assert.Equal(nomeEsperado, agendamento.NomeCliente);
        Assert.Equal(servicoEsperado, agendamento.Servico);
    }
}