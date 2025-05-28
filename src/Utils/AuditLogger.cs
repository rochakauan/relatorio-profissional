using System;

namespace RelatorioProfissional.Utils;

public static class AuditLogger
{
    private static readonly string CaminhoAuditoria = "auditoria.log";

    public static void RegistrarAcao(string acao)
    {
        var mensagem = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {acao}\n";
        File.AppendAllText(CaminhoAuditoria, mensagem);
    }

    public static void ExibirAuditoria()
    {
        if (!File.Exists(CaminhoAuditoria))
        {
            ConsoleUtils.Message("(!) Nenhum registro de auditoria encontrado ate o momento.",  ConsoleColor.Yellow);
            ConsoleUtils.Pause();
            Program.EnviarOpcoesDoMenu();
            return;
        }

        var logs = File.ReadAllLines(CaminhoAuditoria);
        ConsoleUtils.Message("==========> REGISTRO DE AUDITORIA <==========",  ConsoleColor.Yellow);
        Console.WriteLine(string.Join("\n", logs));
    }

    public static void LimparAuditoria()
    {
        if (!File.Exists(CaminhoAuditoria))
        {
            ConsoleUtils.Message("(!) Não há registro de auditoria para apagar.",  ConsoleColor.Yellow);
            ConsoleUtils.Pause();
            Program.EnviarOpcoesDoMenu();
            return;
        }
        
        var quantia = File.ReadAllLines(CaminhoAuditoria).Length;
        File.Delete(CaminhoAuditoria);
        ConsoleUtils.Message($"{quantia} REGISTRO(S) DE AUDITORIA APAGADO(S) COM SUCESSO!",   ConsoleColor.Green);
        ConsoleUtils.Pause();
        Program.EnviarOpcoesDoMenu();
    }
}