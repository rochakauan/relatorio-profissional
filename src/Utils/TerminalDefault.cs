namespace RelatorioProfissional.Utils
{
    /// <summary>
    /// Contains default messages used by <see cref="ConsoleUtils"/>.
    /// You can customize these values before running your program.
    /// </summary>
    public static class TerminalDefaults
    {
        /// <summary>
        /// The default message shown when pausing the program.
        /// </summary>
        public static string DefaultPauseMessage { get; set; } = "Pressione qualquer tecla para continuar...";

        /// <summary>
        /// The default message shown before terminating the program.
        /// </summary>
        public static string DefaultKillMessage { get; set; } = "Encerrando o programa...";

        /// <summary>
        /// The message shown when the user provides invalid input.
        /// </summary>
        public static string InvalidInputMessage { get; set; } = "Argumento invalido, tente novamente.";
        /// <summary>
        ///  The symbol displayed in prompts before reading user input.
        /// </summary>
        public static string PromptSymbol { get; set; } = "> ";

        /// <summary>
        ///  Optional: method to reset all defaults back to original values
        /// </summary>
        public static void ResetDefaults()
        {
            DefaultPauseMessage = "Press any key to continue...";
            DefaultKillMessage = "Closing the program...";
            InvalidInputMessage = "Invalid input, try again.";
            PromptSymbol = "> ";
        }
    }
}
