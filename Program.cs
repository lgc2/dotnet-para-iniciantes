class Program
{
    static void Main()
    {
        ILogger logger = new FileLogger("myLog.txt");
        BankAccount account1 = new BankAccount("LG", 100, logger);
        BankAccount account2 = new BankAccount("Letícia", 150, logger);

        account1.Deposit(-100);
        Console.WriteLine($"O saldo do(a) {account1.Name} é R${account1.Balance}");

        account2.Deposit(100);
        Console.WriteLine($"O saldo do(a) {account2.Name} é R${account2.Balance}");
    }
}

class FileLogger : ILogger
{
    private readonly string filePath;

    public FileLogger(string filePath)
    {
        this.filePath = filePath;
    }

    public void Log(string message)
    {
        File.AppendAllText(filePath, $"{message}{Environment.NewLine}");
    }
}

class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"LOGGER: {message}");
    }
}

interface ILogger
{
    void Log(string message);
}

class BankAccount
{
    private string name;
    private decimal balance;
    private readonly ILogger logger;

    public string Name
    {
        get { return name; }
    }

    public decimal Balance
    {
        get { return balance; }
    }

    public BankAccount(string name, decimal balance, ILogger logger)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Nome inválido.", nameof(name));
        }
        if (balance < 0)
        {
            throw new Exception("Saldo não pode ser negativo.");
        }
        this.name = name;
        this.balance = balance;
        this.logger = logger;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            logger.Log($"Não é possível depositar {amount} na conta de {name}.");
            return;
        }
        balance += amount;
    }
}