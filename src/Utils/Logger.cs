using System;

namespace RelatorioProfissional.Utils;

public static class Logger
{
    
    private static readonly string CaminhoLog = "erros.log";
    
    public static void LogarErro(Exception ex)
    {
        var mensagem = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {ex.GetType().Name} | {ex.Message}\n";
        File.AppendAllText(CaminhoLog, mensagem);
    }

    public static void LogarMensagem(string mensagem)
    {
        var log =  $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {mensagem}\n";
        File.AppendAllText(CaminhoLog, log);
    }

    public static void ExibirLogs()
    {
        if (!File.Exists(CaminhoLog))
        {
            ConsoleUtils.Message("Nenhum Log foi foi encontrado no sistema ate o momento.", ConsoleColor.Yellow);
            return;
        }
        var logs = File.ReadAllLines(CaminhoLog);
        ConsoleUtils.Message("==========> LOG DE ERROS <==========\n- modo desenvolvedor ativado", ConsoleColor.Yellow);
        Console.WriteLine(string.Join("\n", logs));
    }

    public static void LimparLogs()
    {
        if (!File.Exists(CaminhoLog))
        {
            ConsoleUtils.Message("Nenhum Log foi foi encontrado para apagar.", ConsoleColor.Yellow);
            return;
        }
        var logsQuantia = File.ReadAllLines(CaminhoLog).Length;
        File.Delete(CaminhoLog);
        ConsoleUtils.Message($"{logsQuantia} LOG(S) APAGADOS COM SUCESSO!",  ConsoleColor.Green);
    }
}