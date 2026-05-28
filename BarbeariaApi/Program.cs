using BarbeariaApi.Data;
using BarbeariaApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -- LIGANDO O BANCO DE DADOS ---
// Entregando o gerente de arquivos para o mestre de obras

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// LIGANDO O SWAGGER 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita o servidor a entrgar telas de Front-end (HTML/CSS/JS)
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("/agendamentos", async (Agendamento novoAgendamento, AppDbContext banco, ILogger<Program> logger) =>
{
    
    // O atendente anota no carderno ANTES de fazer a ação
    logger.LogInformation("Recebendo novo agendamento para o cliente: {Nome}", novoAgendamento.NomeCliente);
    
    banco.Agendamentos.Add(novoAgendamento);

    await banco.SaveChangesAsync();
    
    // O atendente anota no caderno que deu tudo certo
    logger.LogInformation("Sucesso! Agendamento criado com ID: {id}", novoAgendamento.Id);

    return Results.Created($"/agendamentos/{novoAgendamento.Id}", novoAgendamento);
});

app.MapGet("/agendamentos", async (AppDbContext banco) =>
{
    var listaDeAgendamentos = await banco.Agendamentos.ToListAsync();

    return Results.Ok(listaDeAgendamentos);
});

//CANCELAMENTO (ROTA DELETE)
app.MapDelete("/agendamentos/{id}", async (int id, AppDbContext banco) =>
{
    // 1. O atendente vai na gaveta e procura (findasync) a ficha exata com o número do ID.
    var agendamento = await banco.Agendamentos.FindAsync(id);

    // 2. Regra de negócio: Se o atendente procurou e a mão voltou vazia (null)...
    if (agendamento == null)
    {
        //Devolvemos um erro 404 (Not found / Não encontrado)
        return Results.NotFound("Agendamento não encontrado na barbearia.");
    }

    // 3. Se a ficha existe, nós removemos ela da gaveta
    banco.Agendamentos.Remove(agendamento);

    // 4. Fechamos a gaveta e salvamos a alteração no disco (O Botão de Salvar!)
    await banco.SaveChangesAsync();
    
    // 5. Avisamos que deu tudo certo (200 - ok)
    return Results.Ok("Agendamento cancelado com sucesso!");
});

app.Run();
