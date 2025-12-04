
using System.Text.Json;

do
{
    var json = File.ReadAllText("./config.json");

    var config = JsonSerializer.Deserialize<Config>(json);

    string path = config.path;
    int interval = 60000 * config.time_minutes;

    if (Directory.Exists(path))
    {

        DateTime currentDate = DateTime.Now;

        Console.WriteLine($"Horário de verificação: {currentDate}\n");

        string[] files = Directory.GetFiles(path);

        Console.WriteLine("### Lista de arquivos ### \n");

        int countFiles = 0;
        int countFilesDeleted = 0;

        foreach (string file in files)
        {
            countFiles++;

            var info = new FileInfo(file);

            Console.WriteLine($"#{countFiles}");
            Console.WriteLine($"\tCaminho: {file}");
            Console.WriteLine($"\tNome: {info.Name}");
            Console.WriteLine($"\tCriado: {info.CreationTime}");
            Console.WriteLine($"\tEditado: {info.LastWriteTime}");

            TimeSpan createdSince = currentDate - info.CreationTime;
            Console.WriteLine($"\tCriado há: {createdSince.Days} dias {createdSince.Hours} horas {createdSince.Minutes} minutos {createdSince.Seconds} segundos");

            if(createdSince.Days > 30)
            {
                Console.WriteLine($"\n{info.Name} excluído!");

                // File.Delete(file);

                countFilesDeleted++;
            }


                Console.WriteLine();
        }
        
        Console.WriteLine($"Total de arquivos encontrados: {countFiles}");
        Console.WriteLine($"Total de arquivos excluídos: {countFilesDeleted}");

    }
    else
    {
        Console.WriteLine("Caminho ou pasta inexistente!");
    }

    var convertInterval = interval/60000;
    Console.WriteLine($"Próxima verificação em {convertInterval} minuto{(convertInterval > 1 ? "s":"")}");
    Thread.Sleep(interval);  
      
} while (true);

public class Config
{
    public string path { get; set; }
    public int time_minutes { get; set; }
}