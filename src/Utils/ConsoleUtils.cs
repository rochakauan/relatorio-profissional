namespace RelatorioProfissional.Utils
{
    /// <summary>
    /// Provides utility methods for styled console output, input prompts, pauses and program termination.
    /// </summary>
    public static class ConsoleUtils
    {
        /// <summary>
        /// Displays a styled message in the console.
        /// </summary>
        /// <param name="message">The text to display.</param>
        /// <param name="color">The color of the text. If null, uses the default message color from <see cref="TerminalConfig"/>.</param>
        /// <param name="clearConsoleBefore">If true, clears the console before displaying the message.</param>
        public static void Message(
            string message,
            ConsoleColor? color = null,
            bool? clearConsoleBefore = null)
        {
            if (clearConsoleBefore ?? TerminalConfig.EnableClearBeforeMessages)
                Console.Clear();
            
            Console.ForegroundColor = color ?? TerminalConfig.DefaultMessageColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a question prompt and reads user input from the console.
        /// </summary>
        /// <param name="questionMessage">The prompt message to display.</param>
        /// <param name="color">The color of the prompt message. If null, uses the default prompt color from <see cref="TerminalConfig"/>.</param>
        /// <param name="clearConsoleBefore">If true, clears the console before displaying the prompt.</param>
        /// <param name="cursorLeft">Optional left cursor position before displaying the prompt.</param>
        /// <param name="cursorTop">Optional top cursor position before displaying the prompt.</param>
        /// <param name="allowEmpty">If true, allows empty input; otherwise, re-prompts until valid input is provided.</param>
        /// <returns>The user's input as a string.</returns>
        public static string Ask(
            string questionMessage,
            ConsoleColor? color = null,
            bool? clearConsoleBefore = null,
            int cursorLeft = 0,
            int cursorTop = 0,
            bool allowEmpty = true) {
            
            if (clearConsoleBefore ?? TerminalConfig.EnableClearBeforeMessages)
                Console.Clear();
            
            Message(questionMessage, color ?? TerminalConfig.DefaultPromptColor);

            if (cursorLeft != 0 || cursorTop != 0)
                Console.SetCursorPosition(cursorLeft, cursorTop);

            //var inputTry = 0;
            string input;
            do
            {
                Console.Write(TerminalConfig.ShowPromptSymbol);
                input = Console.ReadLine() ?? string.Empty;

                if (!allowEmpty && string.IsNullOrWhiteSpace(input))
                {
                    Pause(TerminalDefaults.InvalidInputMessage, ConsoleColor.Red);
                    
                    /*
                    inputTry++;

                    if (inputTry == 4)
                    {
                        Message(TerminalDefaults.InvalidInputMessage, ConsoleColor.Red, true);
                        Program.EnviarOpcoesDoMenu();
                        inputTry = 0;
                    }
                    */
                }

            } while (!allowEmpty && string.IsNullOrWhiteSpace(input));

            return input;
        }

        /// <summary>
        /// Pauses program execution until the user presses a key.
        /// </summary>
        /// <param name="message">The message displayed before pausing. If null, uses the default pause message.</param>
        /// <param name="color">The color of the pause message. If null, uses the default pause color from <see cref="TerminalConfig"/>.</param>
        /// <param name="clearConsoleBefore">If true, clears the console before displaying the message.</param>
        public static void Pause(
            string? message = null,
            ConsoleColor? color = null,
            bool? clearConsoleBefore = null)
        {
            if (TerminalConfig.SkipPause)
                return;

            if (clearConsoleBefore ?? TerminalConfig.EnableClearBeforeMessages)
                Console.Clear();

            Message(message ?? TerminalDefaults.DefaultPauseMessage, color ?? TerminalConfig.DefaultPauseColor);
            Console.ReadKey(true);
        }

        /// <summary>
        /// Displays a closing message and terminates the application after a delay.
        /// </summary>
        /// <param name="threadInterval">The delay in milliseconds before closing. If null, uses the default kill delay from <see cref="TerminalConfig"/>.</param>
        /// <param name="message">The message displayed before closing. If null, uses the default kill message.</param>
        /// <param name="color">The color of the closing message. If null, uses the default kill color from <see cref="TerminalConfig"/>.</param>
        /// <param name="clearConsoleBefore">If true, clears the console before displaying the message.</param>
        public static void KillTerminal(
            int? threadInterval = null,
            string? message = null,
            ConsoleColor? color = null,
            bool? clearConsoleBefore = null)
        {
            if (TerminalConfig.SkipKill)
                return;

            if (clearConsoleBefore ?? TerminalConfig.EnableClearBeforeMessages)
                Console.Clear();

            Message(message ?? TerminalDefaults.DefaultKillMessage, color ?? TerminalConfig.DefaultKillColor);
            Thread.Sleep(threadInterval ?? TerminalConfig.DefaultKillDelay);
            Console.Clear();
            Environment.Exit(0);
        }
    }
}
