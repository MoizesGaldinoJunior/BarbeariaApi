namespace BarbeariaApi.Models; // Ele avisa para o resto do programa que esse arquivo mora dentro da pasta 'Models'.

public class Agendamento //Criando uma classe chamada 'Agendamento' e qualquer um pode ver.
{
    // O "ID" é como se fosse o CPF ou o Código de Barras deste agendamento.
    // O banco de dados vai gerar um número diferente para cada agendamento (1, 2, 3...)
    public int Id { get; set; } 
    
    // "get" -> O sistema tem permissão para let a informação
    // "set" -> O sistema tem permissão para escrever e alterar a informação.
    
    // "string" significa que nesta propriedade nós vamos guardar TEXTO
    public string NomeCliente { get; set; } = string.Empty; // "string.Empty" -> Avisa para o programa que se ninguém digitar o nome pode manter a string vazia, par que o programa naão pare de funcionar.
    public string NomeBarbeiro { get; set; } = string.Empty;
    
    // 'DateTime' é um tipo especial do C# feito só para guardar Datas e Horas juntos!
    public DateTime DataHora { get; set; }
    
    public string Servico { get; set; } = string.Empty;
}