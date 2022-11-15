public class Prestito
{
    public string NomeUtente { get; private set; }
    public string CognomeUtente { get; private set; }
    public Documento DocumentoUtente { get; private set; }
    public string DataInizio { get; private set; }
    public string DataFine { get; private set; }

    //COSTRUTTORI
    public Prestito(string nomeUtente, string cognomeUtente, Documento documentoUtente, string dataInizio, string dataFine)
    {
        this.NomeUtente = nomeUtente;
        this.CognomeUtente = cognomeUtente;
        this.DocumentoUtente = documentoUtente;
        this.DataInizio = dataInizio;
        this.DataFine = dataFine;
    }
}