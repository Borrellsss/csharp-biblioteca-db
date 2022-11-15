public class Libro : Documento
{
    public int NumeroPagine { get; private set; }

    //COSTRUTTORI
    public Libro(string titolo, string anno, string autore, string codice, int numeroPagine, bool disponibile) : base(titolo, anno, autore, codice, disponibile)
    {
        this.NumeroPagine = numeroPagine;
    }
    public Libro(string titolo, string anno, string autore, string settore, int scaffale, string codice, int numeroPagine, bool disponibile) : base(titolo, anno, autore, codice, disponibile settore, scaffale)
    {
        this.NumeroPagine = numeroPagine;
    }
}