   1. Parte teorica.
   2. Descrizione del progetto sviluppato.
   3. Documentazione degli endpoint con esempi di chiamata (Postman o curl).
   4. Diagramma UML delle entità.


1. Parte teorica
# cos'è .NET
 .NET è una piattaforma di sviluppo  ideata e sviluppata da Microsoft che mette a disposizione varie funzionalità come il supporto per più linguaggi di programmazione (coprendo tutti i paradigmi di programmazione: object oriented, procedurali, funzionali, imperativi, user oriented, ecc.), modelli di programmazione asincroni e simultanei, interoperabilità, consentendo l'esecuzione su più piattaforme ed in presenza di scenari applicativi variegati. 

# cos'è una rest api 
un'API è un meccanismo che permette a un'applicazione o a un servizio di accedere a una risorsa all'interno di un'altra applicazione, servizio o database. L'applicazione o il servizio che accede alle risorse è il client e l'applicazione o il servizio che contiene la risorsa è il server. In particolare il Prof.Pace come metodologia ci ha insegnato quella della minimal api, ossia una normale api ottimizzata nei processi, quindi leggera quindi più veloce nell'esecuzione e tempi di sviluppo.

2. Descrizione del progetto sviluppato
Ho scelto di creare un mini progetto, basato su 2 entità collegate fra loro(Libro-Autore), in sostanza è un progetto molto semplice, le classi sono collegate fra loro per mezzo di un riferimento all'autore nella classe Libro (public Autore? Autore { get; set; }) e da come si evince dalla sinstassi che ho utilizzato un libro deve avere necessariamente un autore per mezzo del "?" (nullable). Ho utilizzato l'ia per fornirmi i MOC dei libri e ho anche chiesto ausilio a quest'ultima per suggerirmi idee per gli endpoint(che sono comunque molto semplici), cosi una volta eseguiti gli insert sono passato a strutturare gli endpoint per soddisfare il requisito minimo delle operazioni CRUD passando poi per la ricerca filtrata. In soldoni questa minimal api consente di inserire dei libri e fare tutte le CRUD, nonchè ricerche personalizzate per l'autore specifico.

# nota su swashbuckle:
Il Prof. ci ha richiesto di uscire dagli schemi della classica lezione e documentarci di nostro conto per quanto riguarda la libreria swashbuckle. Swashbuckle è una libreria .NET che può impiegata nel programma installando dei pacchetti nugget, uno relativo all'ui(che permette di interagire con un'interfaccia grafica) e uno relativo al salvataggio e documentazione degli endpoint. In sostanza è un tool utile per evitare di andare a simulare gli endpoint altrove come su postman.

3. Documentazione degli endpoint con esempi di chiamata(postman)
Gli endpoint sono in totale 5, 2 get, 1 put, 1 delete, 1 post.

## GET ELENCO LIBRI
il primo get è relativo alla visualizzazione dell'elenco dei libri ed ha una sintassi molto semplice, si iniliazza una variabile chiamata elenco_libri sotto forma di lista e la si chiama ove necessario, in questo caso per restituire l'elenco.

un esempio di chiamata in postman è questo:

# GET - BODY - RAW - JSON
# https://localhost:7222/api/elencoLibri

## GET VISUALIZZA TUTTI I LIBRI DI UN AUTORE
In questo caso la sintassi del codice è più complessa, si tratta di una ricerca per filtro per autore che restituisce la lista dei libri ad esso inerenti. si parte con un metodo chiamato .Where() che serve a filtrare gli oggetti in base a una specifica condizione(in questo caso quella dell'autore) naturalmente per non incombere in errori ho usato l'ignore case e il .ToList() per convertire il risultato filtrato in lista.
esempio di chiamata:
# GET - BODY - RAW - JSON
# https://localhost:7222api/autori/Federico Pavone/Libri


## POST AGGIUNGI LIBRO
nei parametri del metodo viene passato l'oggetto lib che servirà a istanziare un nuovo libro nella lista e con lib.Id = elenco_libri.Count + 1; ne si assegnerà anche un Id.

esempio di chiamata: 

# POST - RAW - BODY - JSON
# https://localhost:7222/api/elencoLibri

Nel body:

{
   "titolo" : "Fede",

   "anno" : 2025,

   "autore" : "Federico Pavone"
}


## UPDATE AGGIORNA LIBRO
Per aggiornare un libro ho usato ovviamente il metodo put, la sintassi inizia chiamando il metodo .Find() specificando fra parentesi l'id del libro che si vuole andare a modificare cosi una volta trovato l'id viene assegnato alla variabile libroEsitente, successivamente i dati di libro esistente al momento della send vengono salvati nella variabile libro aggiornato.

esempio di chiamata: 

# PUT - RAW - BODY - JSON
# https://localhost:7222/elencoLibri/Id (OVVIAMENTE L'ID SARA' NUMERICO)

{
   "titolo" : "Fede",

   "anno" : 2020,

   "autore" : "Federico Ciccione"
}


## DELETE ELIMINA UN LIBRO
cosi come il PUT si comincia cercando l'id corrispondente alla ricerca, con il metodo .Find(), tramite .Remove() elimino lib. Traimite una struttura condizionale classica, l'if, si gestisce anche l'errore qualora non vi fossero corrispondenze.

esemepio di chiamata:

# DELETE - RAW - BODY - JSON
# https://localhost:7222/elencoLibri/Id (OVVIAMENTE L'ID SARA' NUMERICO)

risultato:

return response OK (200)

"Libro eliminato con successo!"




LINK AL MIO REPOSITORY!

## https://github.com/FedePE-04/Progetto_esame_minimalApi.git
