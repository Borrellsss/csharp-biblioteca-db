public class Documento
{
    public string Titolo { get; protected set; }
    public string Anno { get; protected set; }
    public string Autore { get; protected set; }
    public string Codice { get; protected set; }
    public string Settore { get; protected set; }
    public int Scaffale { get; protected set; }
    public bool Disponibile { get; set; }

    //COSTRUTTORI
    public Documento(string titolo, string anno, string autore, string codice, bool disponibile)
    {
        this.Titolo = titolo;
        this.Anno = anno;
        this.Autore = autore;
        this.Codice = codice;
        this.Settore = null;
        this.Scaffale = 0;
        this.Disponibile = disponibile;
    }
    public Documento(string titolo, string anno, string autore, string codice, bool disponibile, string settore, int scaffale)
    {
        this.Titolo = titolo;
        this.Anno = anno;
        this.Autore = autore;
        this.Codice = codice;
        this.Settore = settore;
        this.Scaffale = scaffale;
        this.Disponibile = disponibile;
    }
}