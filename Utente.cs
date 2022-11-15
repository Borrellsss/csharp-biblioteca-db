public class Utente
{
    public string Nome { get; private set; }
    public string Cognome { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Telefono { get; private set; }
    public List<string> Prestiti { get; private set; }

    //COSTRUTTORI
    public Utente(string nome, string cognome, string email, string password)
    {
        this.Nome = nome;
        this.Cognome = cognome;
        this.Email = email;
        this.Password = password;
        this.Telefono = null;
    }

    public Utente(string nome, string cognome, string email, string password, string telefono)
    {
        this.Nome = nome;
        this.Cognome = cognome;
        this.Email = email;
        this.Password = password;
        this.Telefono = telefono;
    }
}