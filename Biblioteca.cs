public class Biblioteca
{
    public string Nome { get; private set; }
    public List<Utente> UtentiRegistrati { get; private set; }
    public List<Libro> Libri { get; private set; }
    public List<Documento> Documenti { get; private set; }
    public List<Dvd> Dvds { get; private set; }
    public List<Prestito> Prestiti { get; private set; }

    //COSTRUTTORI
    public Biblioteca(string nome)
    {
        this.Nome = nome;

        this.UtentiRegistrati = new List<Utente>();
        this.Documenti = new List<Documento>();
        this.Prestiti = new List<Prestito>();

        Utente utenteTest = new Utente("Edoardo", "Borrello", "edoardo@mail.it", "12345678");
        this.AggiungiNuovoUtente(utenteTest);

        Libro libroTest1 = new Libro("Libro1", "2022", "autore1", "storia", 12, "122322133444", 33);
        Libro libroTest2 = new Libro("Libro2", "2022", "autore2", "storia", 12, "122322133443", 1047);
        Libro libroTest3 = new Libro("Libro3", "2022", "autore3", "storia", 12, "122322133449", 254);
        this.AggiungiNuovoDocumento(libroTest1);
        this.AggiungiNuovoDocumento(libroTest2);
        this.AggiungiNuovoDocumento(libroTest3);

        Dvd dvdTest1 = new Dvd("Dvd1", "2022", "autore1", "economia", 32, "h123456782", 68);
        Dvd dvdTest2 = new Dvd("Dvd2", "2022", "autore2", "economia", 32, "h123456733", 120);
        Dvd dvdTest3 = new Dvd("Dvd3", "2022", "autore3", "economia", 32, "h123456111", 140);
        this.AggiungiNuovoDocumento(dvdTest1);
        this.AggiungiNuovoDocumento(dvdTest2);
        this.AggiungiNuovoDocumento(dvdTest3);
    }

    //FUNZIONI
    public bool AggiungiNuovoUtente(Utente nuovoUtente)
    {
        if (this.UtentiRegistrati.Count() > 0)
        {
            foreach (Utente utenteCorrente in this.UtentiRegistrati)
            {
                if (utenteCorrente.Email != nuovoUtente.Email)
                {
                    this.UtentiRegistrati.Add(nuovoUtente);
                    return true;
                }

            }
        }
        else
        {
            this.UtentiRegistrati.Add(nuovoUtente);
            return true;
        }

        return false;
    }

    public bool ConrolloDatiUtente(string email, string password)
    {
        foreach (Utente utenteCorrente in UtentiRegistrati)
        {
            if (utenteCorrente.Email == email && utenteCorrente.Password == password)
            {
                return true;
            }
        }

        return false;
    }

    public void AggiungiNuovoDocumento(Documento nuovoDocumento)
    {
        this.Documenti.Add(nuovoDocumento);
    }

    public bool ConrolloDatiDocumento(string infoDocumento)
    {
        foreach (Documento documentoCorrente in Documenti)
        {
            if (documentoCorrente.Codice == infoDocumento || documentoCorrente.Titolo == infoDocumento)
            {
                return true;
            }
        }

        return false;
    }

    public bool RegistrazionePrestito(string email, string infoDocumento)
    {
        Utente utenteDaRegistrare = null;
        Documento documentoDaRegistare = null;
        string dataInizio = "12/11/2022";
        string dataFine = "15/11/2022";

        foreach (Utente utenteCorrente in this.UtentiRegistrati)
        {
            if (utenteCorrente.Email == email)
            {
                utenteDaRegistrare = utenteCorrente;
            }
        }

        foreach (Documento documentoCorrente in Documenti)
        {
            if (documentoCorrente.Codice == infoDocumento || documentoCorrente.Titolo == infoDocumento)
            {
                if (documentoCorrente.Disponibile)
                {
                    documentoDaRegistare = documentoCorrente;
                    Prestito nuovoPrestito = new Prestito(utenteDaRegistrare.Nome, utenteDaRegistrare.Cognome, documentoDaRegistare, dataInizio, dataFine);
                    this.Prestiti.Add(nuovoPrestito);
                    documentoCorrente.Disponibile = false;

                    return true;
                }
            }
        }

        return false;
    }

    public List<Prestito> ListaPrestiti(string nome, string cognome)
    {
        List<Prestito> prestitiUtente = new List<Prestito>();

        for (int i = 0; i < this.Prestiti.Count(); i++)
        {
            Prestito prestitoCorrente = this.Prestiti[i];

            if (prestitoCorrente.NomeUtente == nome && prestitoCorrente.CognomeUtente == cognome)
            {
                prestitiUtente.Add(prestitoCorrente);
            }
        }

        return prestitiUtente;
    }

    public void StampaPrestitiUtente(List<Prestito> prestitiUtente)
    {
        for (int i = 0; i < prestitiUtente.Count(); i++)
        {
            Prestito prestitoCorrente = prestitiUtente[i];

            if ((i == 0 && i == prestitiUtente.Count() - 1) || i == prestitiUtente.Count() - 1)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine($"PRESTITO {i + 1}");
                Console.WriteLine();
                Console.WriteLine($"Data inizio prestito: {prestitoCorrente.DataInizio}");
                Console.WriteLine($"Scadenza prestito: {prestitoCorrente.DataFine}");
                Console.WriteLine($"Titolo documento: {prestitoCorrente.DocumentoUtente.Titolo}");
                Console.WriteLine($"Codice documento: {prestitoCorrente.DocumentoUtente.Codice}");
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine($"PRESTITO {i + 1}");
                Console.WriteLine();
                Console.WriteLine($"Data inizio prestito: {prestitoCorrente.DataInizio}");
                Console.WriteLine($"Scadenza prestito: {prestitoCorrente.DataFine}");
                Console.WriteLine($"Titolo documento: {prestitoCorrente.DocumentoUtente.Titolo}");
                Console.WriteLine($"Codice documento: {prestitoCorrente.DocumentoUtente.Codice}");
                Console.WriteLine();
            }
        }
    }
}