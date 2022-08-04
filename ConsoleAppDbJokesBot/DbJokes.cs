using Npgsql;

namespace ConsoleAppDbJokesBot;

public class DbJokes
{
    private const string connectionString =
        "Host=194.67.105.79;Username=rayman208botuser;Password=12345;Database=rayman208botdb";

    private Random _random;

    private NpgsqlConnection _connection;

    public DbJokes()
    {
        _random = new Random();

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    private int GetMaxId()
    {
        string sqlRequest = "SELECT max(id) FROM jokes";
        NpgsqlCommand command = new NpgsqlCommand(sqlRequest, _connection);

        int maxId = int.Parse(command.ExecuteScalar().ToString());

        return maxId;
    }

    public string GetRandomJoke()
    {
        int maxId = GetMaxId();

        Object sqlResponse = null;

        do
        {
            int randomId = _random.Next(1, maxId + 1);
            string sqlRequest = $"SELECT joke_text FROM jokes WHERE id={randomId}";
            NpgsqlCommand command = new NpgsqlCommand(sqlRequest, _connection);
            sqlResponse = command.ExecuteScalar();
        } while (sqlResponse == null);

        string jokeText = sqlResponse.ToString();

        return jokeText;
    }
}