
using Libro_Progetto;
using Libro_Progetto.Classi;
using Swashbuckle.AspNetCore; //ho installato il pacchetto swashbuckle 
using Swashbuckle.AspNetCore.SwaggerUI; //questo pacchetto servirà per integrare un'interfaccia grafica
using System.ComponentModel.Design;
using System.Xml.Linq;





var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Libro_Progetto",
        Version = "v1"
    }); //questo blocco di codice crea un file che swashbuckle userà per visualizzare le funzioni api in localhost
});


var app = builder.Build();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Libro_Progetto v1");
    c.RoutePrefix = "swagger"; //  imposta la URL come /swagger quindi l'url corretto per visualizzare l'app in swash è: https://localhost:7116/swagger
});


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();




var elenco_libri = new List<Libro>
{
    new Libro
    {
        Id = 1,
        Titolo = "Il profumo delle parole",
        Anno = 2005,
        Autore = new Autore { nome = "Elena Ferrante" }
    },
    new Libro
    {
        Id = 2,
        Titolo = "La matematica del cuore",
        Anno = 2012,
        Autore = new Autore { nome = "Marco Malvaldi" }
    },
    new Libro
    {
        Id = 3,
        Titolo = "Oceano di silenzi",
        Anno = 1999,
        Autore = new Autore { nome = "Margaret Mazzantini" }
    },
    new Libro
    {
        Id = 4,
        Titolo = "Il codice dell’anima",
        Anno = 1997,
        Autore = new Autore { nome = "James Hillman" }
    }
};

//GET mostra elenco libro
app.MapGet("/api/elencoLibri", () =>
{
    return Results.Ok(elenco_libri);
});


//POST aggiungi libro
app.MapPost("api/elencoLibri", (Libro lib) =>
{
    lib.Id = elenco_libri.Count + 1;
    elenco_libri.Add(lib);

    return Results.Created("/api/elencoLibri" + lib.Titolo, null);

});

// GET visualizza tutti i libri di un autore
app.MapGet("api/autori/{nome}/Libri", (string nome) =>
{
    // Il controllo 'libro.Autore != null' previene errori se un libro non ha un autore associato.
    var libriAutore = elenco_libri
        .Where(libro => libro.Autore != null && libro.Autore.nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
        .ToList();
    if (!libriAutore.Any())
    {
        return Results.NotFound($"Nessun libro trovato per l'autore: {nome}");
    }

    
    return Results.Ok(libriAutore);
});


//UPDATE aggiorna libro
app.MapPut("/elencoLibri/{Id}", (int Id, Libro libroAggiornato) =>
{
    var libroEsistente = elenco_libri.Find(libro => libro.Id == Id);
    if (libroEsistente is null)
        return Results.NotFound($"Libro non trovato.");

    // Aggiorna i campi, in questa fase, se il libro esiste, i dati vengono trasferiti nel nuovo libro
    libroEsistente.Titolo = libroAggiornato.Titolo;
    libroEsistente.Anno = libroAggiornato.Anno;
    libroEsistente.Autore = libroAggiornato.Autore;

    return Results.Ok($"Libro con ID {Id} aggiornato con successo.");
});



//DELETE elimina un libro
app.MapDelete("/elencoLibri/{Id}", (int Id) =>
{
    Libro? lib = elenco_libri.Find(libro => libro.Id == Id);
    if (lib is null)
        return Results.NotFound("Libro non trovato!");
    elenco_libri.Remove(lib);
    return Results.Ok("Libro eliminato con successo!");
});


app.Run();
