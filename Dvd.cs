public class Dvd : Documento
{
    public int Durata { get; private set; }

    //COSTRUTTORI
    public Dvd(string titolo, string anno, string autore, string codice, int durata, bool disponibile) : base(titolo, anno, autore, codice, disponibile)
    {
        this.Durata = durata;
    }
    public Dvd(string titolo, string anno, string autore, string settore, int scaffale, string codice, int durata, bool disponibile) : base(titolo, anno, autore, codice, disponibile, settore, scaffale)
    {
        this.Durata = durata;
    }
}