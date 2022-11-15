using System.Data.SqlClient;
using System.Security.Cryptography;

public static class DbConnection
{
    public static string connectionString = "Data Source=localhost;Initial Catalog = db-biblioteca; Integrated Security = True";

    public static SqlConnection CreateConnection()
    {
        SqlConnection connection = new SqlConnection(connectionString);

        return connection;
    }

    public static bool IsUserRegistered(string email)
    {
        try
        {
            string query = $"SELECT * FROM users WHERE email = @Email";
            SqlCommand cmd = new SqlCommand(query, CreateConnection());
            cmd.Parameters.Add(new SqlParameter("@Email", email));
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return true;
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Qualcosa è andato storto");
        }

        return false;
    }
    public static bool RegisteredUserCredentialsCheck(string email, string password)
    {
        try
        {
            string query = $"SELECT * FROM users WHERE email = @Email AND password = @Password";

            SqlCommand cmd = new SqlCommand(query, CreateConnection());

            cmd.Parameters.Add(new SqlParameter("@Email", email));
            cmd.Parameters.Add(new SqlParameter("@Password", password));


            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Qualcosa è andato storto.");
        }

        return false;
    }

    public static Libro ReadBook(string code)
    {
        try
        {
            string query = $"SELECT * FROM books WHERE isbn = @Code";

            SqlCommand cmd = new SqlCommand(query, CreateConnection());

            cmd.Parameters.Add(new SqlParameter("@Code", code));

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Libro userBook = null;

                while(reader.Read())
                {
                    string titolo = reader.GetString(1);
                    string anno = reader.GetString(2);
                    string autore = reader.GetString(3);
                    int numeroPagine = reader.GetInt32(4);
                    string codice = reader.GetString(5);
                    string settore = reader.GetString(6);
                    int scaffale = reader.GetInt16(7);
                    bool disponibile = reader.GetBoolean(8);

                    userBook = new Libro(titolo, anno, autore, settore, scaffale, codice, numeroPagine, disponibile);
                }

                return userBook;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Qualcosa è andato storto.");
        }

        return null;
    }

    public static void InsertBook(Libro book)
    {
        try
        {
            string query = $"SELECT * FROM books WHERE isbn = @Code";

            SqlCommand cmd = CreateConnection().CreateCommand();
            SqlTransaction transaction = CreateConnection().BeginTransaction("insertBook");

            cmd.Parameters.Add(new SqlParameter("@Code", code));

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Libro userBook = null;

                while (reader.Read())
                {
                    string titolo = reader.GetString(1);
                    string anno = reader.GetString(2);
                    string autore = reader.GetString(3);
                    int numeroPagine = reader.GetInt32(4);
                    string codice = reader.GetString(5);
                    string settore = reader.GetString(6);
                    int scaffale = reader.GetInt16(7);
                    bool disponibile = reader.GetBoolean(8);

                    userBook = new Libro(titolo, anno, autore, settore, scaffale, codice, numeroPagine, disponibile);
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Qualcosa è andato storto.");
        }

    }

    public static Dvd ReadDvd(string code)
    {
        try
        {
            string query = $"SELECT * FROM dvds WHERE serial_code = @Code";

            SqlCommand cmd = new SqlCommand(query, CreateConnection());

            cmd.Parameters.Add(new SqlParameter("@Code", code));

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Dvd userDvd = null;

                while (reader.Read())
                {
                    string titolo = reader.GetString(1);
                    string anno = reader.GetString(2);
                    string autore = reader.GetString(3);
                    int durata = reader.GetInt32(4);
                    string codice = reader.GetString(5);
                    string settore = reader.GetString(6);
                    int scaffale = reader.GetInt16(7);
                    bool disponibile = reader.GetBoolean(8);

                    userDvd = new Dvd(titolo, anno, autore, settore, scaffale, codice, durata, disponibile);
                }

                return userDvd;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Qualcosa è andato storto.");
        }

        return null;
    }
}

