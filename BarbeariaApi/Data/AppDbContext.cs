using BarbeariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarbeariaApi.Data;

public class AppDbContext : DbContext
{
    //O construtor é como a "porta de entrada" desse gerente.
    //Ele precisa receber as opções de onde o banco de dados está salvo.
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    //Aqui estamos dizendo: crie uma tabela de agendamentos no banco, usando a class "Agendamento" que criamos antes.
    
    public DbSet<Agendamento> Agendamentos { get; set; }
}