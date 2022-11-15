//Riprendiamo l’esercizio della biblioteca considerando però aclune varianti:
//non è necessario (ma consigliato) ragionare con gli oggetti.
//evitiamo categoricamente la questione dell’eredità tra oggetti
//potete implementare una singola tabella per tutti i documenti: ovviamente in questo caso ci dovrà essere una colonna che gestisce il tipo di documento
//Realizzate almeno le tabelle dei documenti e dei prestiti con le opportune relazioni;
//qui potete inserire solo un campo nome cliente nel prestito e ignorare la parte di registrazione richiesta
//Bonus: implementate anche la tabella utente e i controllo di registrazione
//(che significa che l’utente è dentro al db e quindi prima di fare il prestito deve essere trovato dal bibliotecario attraverso il sistema)
//Si vuole progettare un sistema per la gestione di una biblioteca. Gli utenti si possono registrare al sistema, fornendo:
//cognome,
//nome,
//email,
//password,
//recapito telefonico,
//Gli utenti registrati possono effettuare dei prestiti sui documenti che sono di vario tipo (libri, DVD). I documenti sono caratterizzati da:
//un codice identificativo di tipo stringa (ISBN per i libri, numero seriale per i DVD),
//titolo,
//anno,
//settore(storia, matematica, economia, …),
//stato(In Prestito, Disponibile),
//uno scaffale in cui è posizionato,
//un autore (Nome, Cognome).
//Per i libri si ha in aggiunta il numero di pagine, mentre per i dvd la durata.
//L’utente deve poter eseguire delle ricerche per codice o per titolo e, eventualmente,
//effettuare dei prestiti registrando il periodo (Dal/Al) del prestito e il documento.
//Deve essere possibile effettuare la ricerca dei prestiti dato nome e cognome di un utente.

using System.Data.SqlClient;

Biblioteca nuovaBiblioteca = new Biblioteca("Biblioteca1");

bool programmaInEsecuzione = true;

try
{
    DbConnection.CreateConnection().Open();

    Console.WriteLine($"Benvenuto/a nella {nuovaBiblioteca.Nome}");

    while (programmaInEsecuzione)
    {
        Console.WriteLine("Quale operazione desidera eseguire?");
        Console.WriteLine("[1]: Prestito");
        Console.WriteLine("[2]: Registrazione");
        Console.WriteLine("[3]: Controllo prestiti");
        Console.WriteLine("[4]: Termina programma");

        string sceltaPrincipaleUtente = Console.ReadLine();

        switch (sceltaPrincipaleUtente)
        {
            case "1":
                RichiestaPrestito();
                TerminaProgramma();
                break;

            case "2":
                RegistrazioneUtente();
                TerminaProgramma();
                break;

            case "3":
                ControlloPrestiti();
                TerminaProgramma();
                break;

            case "4":
                Console.WriteLine("Arrivederci!");
                DbConnection.CreateConnection().Close();
                programmaInEsecuzione = false;
                break;

            default:
                Console.WriteLine("opzione non disponibile");
                break;
        }
    }

}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    DbConnection.CreateConnection().Close();
}

void RichiestaPrestito()
{
    Console.WriteLine("Inserisca email e password:");

    Console.Write("Email: ");
    string emailUtente = Console.ReadLine();
    string passwordUtente;

    if (DbConnection.IsUserRegistered(emailUtente))
    {
        bool passwordRequest = true;
        while (passwordRequest)
        {
            Console.Write("Password: ");
            passwordUtente = Console.ReadLine();

            if (DbConnection.RegisteredUserCredentialsCheck(emailUtente, passwordUtente))
            {
                passwordRequest = false;
            }
            else
            {
                Console.WriteLine("Password errata.");
            }
        }

        bool loanRequest = true;
        while (loanRequest)
        {
            Console.WriteLine("Cosa desideri prendere in prestito?");
            Console.WriteLine("[1]: Libro");
            Console.WriteLine("[2]: Dvd");

            string sceltaPrestitoUtente = Console.ReadLine();

            switch (sceltaPrestitoUtente)
            {
                case "1":
                    Console.Write("Digita il codice ISBN o il titolo del libro che vorresti prendere in prestito: ");
                    string codiceLibro = Console.ReadLine();

                    bool esitoRicercaLibro = nuovaBiblioteca.ConrolloDatiDocumento(codiceLibro);

                    if (esitoRicercaLibro)
                    {
                        bool esitoPrestito = nuovaBiblioteca.RegistrazionePrestito(emailUtente, codiceLibro);

                        if (esitoPrestito)
                        {
                            Console.WriteLine("Ecco a lei il suo libro!");
                        }
                        else
                        {
                            Console.WriteLine("Sono spiacente ma il libro è terminato!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Mi dispiace ma questo libro non è presente nella nostra biblioteca...");
                    }

                    bool condizioneTermineOrdinazione = true;

                    while (condizioneTermineOrdinazione)
                    {
                        Console.WriteLine("Vuole effettuare un altro ordine?");
                        Console.WriteLine("[1]: Sì");
                        Console.WriteLine("[2]: No");

                        string terminaOrdinazione = Console.ReadLine();

                        switch (terminaOrdinazione)
                        {
                            case "1":
                                condizioneTermineOrdinazione = false;

                                break;

                            case "2":
                                Console.WriteLine("Grazie!");
                                condizioneTermineOrdinazione = false;
                                loanRequest = false;

                                break;

                            default:
                                Console.WriteLine("Opzione non disponibile");

                                break;
                        }
                    }

                    break;

                case "2":
                    Console.Write("Digiti il numero seriale o il titolo del dvd che vorrebbe prendere in prestito: ");
                    string dvdUtente = Console.ReadLine();

                    bool esitoRicercaDvd = nuovaBiblioteca.ConrolloDatiDocumento(dvdUtente);

                    if (esitoRicercaDvd)
                    {
                        bool esitoPrestito = nuovaBiblioteca.RegistrazionePrestito(emailUtente, dvdUtente);

                        if (esitoPrestito)
                        {
                            Console.WriteLine("Ecco a lei il suo dvd!");
                        }
                        else
                        {
                            Console.WriteLine("Sono spiacente ma il dvd è terminato!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Mi dispiace ma questo dvd non è presente nella nostra biblioteca...");
                    }

                    condizioneTermineOrdinazione = true;

                    while (condizioneTermineOrdinazione)
                    {
                        Console.WriteLine("Vuole effettuare un altro ordine?");
                        Console.WriteLine("[1]: Sì");
                        Console.WriteLine("[2]: No");

                        int terminaOrdinazione = Convert.ToInt16(Console.ReadLine());

                        switch (terminaOrdinazione)
                        {
                            case 1:
                                condizioneTermineOrdinazione = false;

                                break;

                            case 2:
                                Console.WriteLine("Grazie!");
                                condizioneTermineOrdinazione = false;
                                condizioneRichiestaOrdinazione = false;

                                break;

                            default:
                                Console.WriteLine("Opzione non disponibile");

                                break;
                        }
                    }

                    break;

                default:

                    Console.WriteLine("Mi dispiace non credo di aver capito...");
                    break;
            }
        }

    }
    else
    {
        Console.WriteLine("Nessun utente presente nel database con questa mail.");

        bool richiestaRegistrazione = true;
        while (richiestaRegistrazione)
        {
            Console.WriteLine("Vuoi registarti?.");
            Console.WriteLine("[1]: Sì");
            Console.WriteLine("[2]: No");

            string opzioneRegistrazione = Console.ReadLine();

            switch (opzioneRegistrazione)
            {
                case "1":
                    RegistrazioneUtente();
                    richiestaRegistrazione = false;
                    break;

                case "2":
                    richiestaRegistrazione = false;
                    break;

                default:
                    Console.WriteLine("Opzione non disponibile!");
                    break;
            }
        }

    }

    TerminaProgramma();
}

void RegistrazioneUtente()
{
    Console.WriteLine("Inserisca tutti i dati obbligatori:");

    Console.Write("Nome: ");
    string nomeUtente = Console.ReadLine();

    Console.Write("Cognome: ");
    string cognomeUtente = Console.ReadLine();

    Console.Write("Email: ");
    string emailUtente = Console.ReadLine();

    Console.Write("Password: ");
    string passwordUtente = Console.ReadLine();

    Console.WriteLine("Desidera inserire anche il numero di telefono?");
    Console.WriteLine("[1]: Sì");
    Console.WriteLine("[2]: No");
    string sceltaTelefonoUtente = Console.ReadLine();

    if (sceltaTelefonoUtente == "1")
    {
        Console.Write("Numero: ");
        string telefonoUtente = Console.ReadLine();

        Utente nuovoUtente = new Utente(nomeUtente, cognomeUtente, emailUtente, passwordUtente, telefonoUtente);

        bool esitoRegistrazione = nuovaBiblioteca.AggiungiNuovoUtente(nuovoUtente);

        if (esitoRegistrazione)
        {
            Console.WriteLine("Complimenti! La registrazione è avvenuta con successo!");
        }
        else
        {
            Console.WriteLine("Il tuo profilo è già presente nel nostro Database!");
        }

    }
    else if (sceltaTelefonoUtente == "2")
    {
        Utente nuovoUtente = new Utente(nomeUtente, cognomeUtente, emailUtente, passwordUtente);

        bool esitoRegistrazione = nuovaBiblioteca.AggiungiNuovoUtente(nuovoUtente);

        if (esitoRegistrazione)
        {
            Console.WriteLine("Complimenti! La registrazione è avvenuta con successo!");
        }
        else
        {
            Console.WriteLine("Il tuo profilo è già presente nel nostro Database");
        }
    }
    else
    {
        Console.WriteLine("Mi spiace ma la risposta non è corretta");
    }
}

void ControlloPrestiti()
{
    Console.WriteLine("Inserisca Nome e Cognome: ");

    Console.Write("Nome: ");
    string nomeUtentePrestito = Console.ReadLine();

    Console.Write("Cognome: ");
    string CognomeUtentePrestito = Console.ReadLine();

    List<Prestito> prestitiUtente = nuovaBiblioteca.ListaPrestiti(nomeUtentePrestito, CognomeUtentePrestito);

    if (prestitiUtente.Count() > 0)
    {
        nuovaBiblioteca.StampaPrestitiUtente(prestitiUtente);
    }
    else
    {
        Console.WriteLine("Non sono presenti prestiti a questo nome.");
        Console.WriteLine("Se non è registrato/a può farlo tornando al menù principale.");
    }
}

void TerminaProgramma()
{
    bool richiestaContinua = true;

    while (richiestaContinua)
    {
        Console.WriteLine("Ha bisogno di altro?");
        Console.WriteLine("[1]: Sì");
        Console.WriteLine("[2]: No");

        string opzioneContinua = Console.ReadLine();

        switch (opzioneContinua)
        {
            case "1":
                richiestaContinua = false;
                break;

            case "2":
                Console.WriteLine("Grazie e buona giornata!");
                DbConnection.CreateConnection().Close();
                richiestaContinua = false;
                programmaInEsecuzione = false;
                break;

            default:
                Console.WriteLine("Opzione non disponibile!");
                break;
        }
    }
}