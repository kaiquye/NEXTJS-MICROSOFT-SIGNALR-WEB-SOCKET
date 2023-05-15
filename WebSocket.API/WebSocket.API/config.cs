public static class ConfigEnv
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new Exception("Error: .env file not found");

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(
                '=',
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;

            string name = parts[0];
            string value = parts[1];
            Environment.SetEnvironmentVariable(name, value);
        }
    }

    public static void Load()
    {
        var appRoot = Directory.GetCurrentDirectory();
        var dotEnv = Path.Combine(appRoot, ".env");

        Load(dotEnv);
    }
}
