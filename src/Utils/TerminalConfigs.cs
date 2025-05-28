namespace RelatorioProfissional.Utils;

/// <summary>
/// Contains global configuration settings for <see cref="ConsoleUtils"/>.
/// These settings can be changed at runtime to affect terminal behavior.
/// </summary>
public static class TerminalConfig
{
    /// <summary>
    /// Senha para liberar o modo desenvolvedor oculto
    /// </summary>
    public const string Modo_Dev_Senha = "0000";
    /// <summary>
    /// Libera funcionalidades extras para desenvolvedores (e.x.: ler logs)
    /// </summary>
    public static bool ModoDesenvolvedorAtivo { get; private set; } = false;
        
    /// <summary>
    /// The default text color used for messages.
    /// </summary>
    public static ConsoleColor DefaultMessageColor { get; set; } = ConsoleColor.White;

    /// <summary>
    /// The default text color used for prompts.
    /// </summary>
    public static ConsoleColor DefaultPromptColor { get; set; } = ConsoleColor.DarkYellow;

    /// <summary>
    /// The default text color used for pause messages.
    /// </summary>
    public static ConsoleColor DefaultPauseColor { get; set; } = ConsoleColor.DarkGray;

    /// <summary>
    /// The default text color used for termination messages.
    /// </summary>
    public static ConsoleColor DefaultKillColor { get; set; } = ConsoleColor.DarkRed;

    /// <summary>
    /// Determines whether to clear the console before displaying any message.
    /// </summary>
    public static bool EnableClearBeforeMessages { get; set; } = false;

    /// <summary>
    /// The default delay in milliseconds before terminating the program.
    /// </summary>
    public static int DefaultKillDelay { get; set; } = 900;

    /// <summary>
    /// Skips program pauses when set to true. Useful for automated testing.
    /// </summary>
    public static bool SkipPause { get; set; } = false;

    /// <summary>
    /// Skips program termination when set to true. Useful for debugging.
    /// </summary>
    public static bool SkipKill { get; set; } = false;

    /// <summary>
    /// The symbol displayed in prompts before reading user input.
    /// </summary>
    public static string ShowPromptSymbol { get; set; } = TerminalDefaults.PromptSymbol;

    /// <summary>
    ///  Optional: method to reset all defaults back to original values
    /// </summary>
    public static void ResetDefaults()
    {
        DefaultMessageColor = ConsoleColor.White;
        DefaultPromptColor = ConsoleColor.DarkYellow;
        DefaultPauseColor = ConsoleColor.DarkGray;
        DefaultKillColor = ConsoleColor.DarkRed;
        DefaultKillDelay = 900;
        EnableClearBeforeMessages = false;
        SkipPause = false;
        SkipKill = false;
        ShowPromptSymbol = TerminalDefaults.PromptSymbol;
    }
    
    /// <summary>
    /// Ativa o modo desenvolvedor caso a senha informada no Console esteja correta
    /// </summary>
    public static void AtivarModoDesenvolvedor()
    {
        var senhaInput = ConsoleUtils.Ask("Informe a senha de desenvolvedor", ConsoleColor.DarkRed);
        if (senhaInput == Modo_Dev_Senha)
        {
            ModoDesenvolvedorAtivo = true;
            ConsoleUtils.Message("\nMODO DESENVOLVEDOR Ativado!", ConsoleColor.Magenta, true);
        }
        else { ConsoleUtils.Message("(!) Senha incorreta!",  ConsoleColor.Red); }
        ConsoleUtils.Pause();
        Program.EnviarOpcoesDoMenu();
    }
    
    /// <summary>
    /// Desativa o modo desenvolvedor no Console
    /// </summary>
    public static void DesativarModoDesenvolvedor()
    {
        ModoDesenvolvedorAtivo = false;
        ConsoleUtils.Message("\nVoce acaba de sair do modo desenvolvedor", ConsoleColor.Yellow, true);
        ConsoleUtils.Pause();
        Program.EnviarOpcoesDoMenu();
    }
}