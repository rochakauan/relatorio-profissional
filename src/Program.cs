using RelatorioProfissional.Chaveiro;
using RelatorioProfissional.Utils;

namespace RelatorioProfissional;

class Program
{
    
    public static void Main(string[] args)
    {
        EnviarOpcoesDoMenu();
    }
    public static void EnviarOpcoesDoMenu()
    {
        ConsoleUtils.Message("Bem vindo ao Relatorio Profissional", ConsoleColor.DarkCyan, true);
        ConsoleUtils.Message("Selecione o que deseja executar:\n| 1 - Ler relatorio\n| 2 - Adicionar servico\n| 3 - Remover um serviço\n| 0 - Sair", ConsoleColor.Cyan);
     
        if(TerminalConfig.ModoDesenvolvedorAtivo)
            ConsoleUtils.Message("| 9 - Exibir logs | 10 - Limpar logs", ConsoleColor.DarkMagenta);
        
        var option = ConsoleUtils.Ask("Digite sua resposta", ConsoleColor.DarkCyan);
        ManipularMenu(option);
    }

    private static void ManipularMenu(string option)
    {
        switch (option)
        {
            case "1":
                Console.Clear();
                ConsoleUtils.Message("Relatorio Profissional", ConsoleColor.Yellow);
                ChecarSeExistemServicos();
                var servicos = GerenciadorDeServicos.Carregar();
                foreach (var servico in servicos)
                    ConsoleUtils.Message(servico.ToString(), ConsoleColor.DarkYellow);
                ConsoleUtils.Pause();
                EnviarOpcoesDoMenu();
                break;
            case "2":
                AdicionarServicoPorConsole();
                break;
            case "3":
                ChecarSeExistemServicos();
                RemoverServicoPorConsole(GerenciadorDeServicos.Carregar());
                break;
            case "9":
                if(TerminalConfig.ModoDesenvolvedorAtivo) Logger.ExibirLogs();
                else ConsoleUtils.Message("Opçao invalida!",  ConsoleColor.Red, true);
                ConsoleUtils.Pause();
                EnviarOpcoesDoMenu();
                break;
            case "10": if(TerminalConfig.ModoDesenvolvedorAtivo) Logger.LimparLogs();
                else ConsoleUtils.Message("Opçao invalida!",   ConsoleColor.Red, true);
                ConsoleUtils.Pause();
                EnviarOpcoesDoMenu();
                break;
            case "dev":
                TerminalConfig.AtivarModoDesenvolvedor();
                break;
            case "0": ConsoleUtils.KillTerminal(1000, "Encerrando o programa...", clearConsoleBefore: true); break;
            default: EnviarOpcoesDoMenu(); break;
        }
    }

    private static void AdicionarServicoPorConsole()
    {
        var nomeServico = ConsoleUtils.Ask("Informe o nome do servico: ", clearConsoleBefore: true);
        var valorUnitario = decimal.Parse(ConsoleUtils.Ask("Agora, informe o valor unitario (e.x.: 25,50"));
        var custoUnitario = decimal.Parse(ConsoleUtils.Ask("Qual o preço do material? (Se nao houve material gasto, digite 0)"));
        var quantidade = int.Parse(ConsoleUtils.Ask("Quase la, quantas unidades do serviço?"));
        ConsoleUtils.Message("Selecione o metodo de pagamento usado: ",  ConsoleColor.Green, true);
        ConsoleUtils.Message("1 | Pix\n2 | Cartao\n3 | Dinheiro em especie", ConsoleColor.DarkGreen);
        var meioPagamentoInput = ConsoleUtils.Ask("", ConsoleColor.Green);

        var meioPagamento = meioPagamentoInput switch
        {
            "1" => EMeioDePagamento.Pix,
            "2" => EMeioDePagamento.Cartao,
            "3" => EMeioDePagamento.Dinheiro,
            _ => EMeioDePagamento.Pix // padrao
        };
        
        var novoServico = new Services(nomeServico, valorUnitario, custoUnitario, quantidade, meioPagamento);
        var lista = GerenciadorDeServicos.Carregar();
        GerenciadorDeServicos.AdicionarServico(lista, novoServico);
        
        ConsoleUtils.Message("SERVICO ADICIONADO COM SUCESSO!", ConsoleColor.Green);
        ConsoleUtils.Pause();
        EnviarOpcoesDoMenu();
    }

    public static void RemoverServicoPorConsole(List<Chaveiro.Services> lista)
    {
        Console.Clear();
        ChecarSeExistemServicos();
        
        foreach (var servico in lista)
        {
            ConsoleUtils.Message(
                $"\nServiço: {servico.Servico} | Meio de pagamento: {servico.Pagamento} | Data que foi cadastrado: {servico.DataCadastro}",
                ConsoleColor.DarkYellow);
            ConsoleUtils.Message($"- ID do serviço: {servico.Id}", ConsoleColor.Red);
        }

        var idInput = ConsoleUtils.Ask("--------\nInforme o ID do serviço que deseja remover", ConsoleColor.Yellow);
        if(int.TryParse(idInput, out var idRemover))
         RemoverServico(lista, idRemover);
        else
        {
            ConsoleUtils.Message("(!) O ID inserido nao é valido!", ConsoleColor.Red);
            ConsoleUtils.Pause();
            EnviarOpcoesDoMenu();
        }
    }
    private static void RemoverServico(List<Chaveiro.Services> services, int id)
    {
        try
        {
            var servico = services.FirstOrDefault(s => s.Id == id);
            if (servico == null)
                throw new Erros.ServicoNaoEncontradoExc(id);

            services.Remove(servico);
            GerenciadorDeServicos.Salvar(services);
            ConsoleUtils.Message("SERVIÇO REMOVIDO COM SUCESSO!", ConsoleColor.DarkGreen);
            ConsoleUtils.Message(
                $"Dados do serviço removido: | ID {servico.Id} | Serviço {servico.Servico} | Data que foi cadastrado {servico.DataCadastro}",
                ConsoleColor.Yellow);
        }
        catch (Erros.ServicoNaoEncontradoExc ex)
        {
            ConsoleUtils.Message(ex.Message, ConsoleColor.Red);
        }
        catch (Exception ex)
        {
            ConsoleUtils.Message("(!) Ocorreu um erro desconhecido ao tentar remover o serviço.", ConsoleColor.Red);
            ConsoleUtils.Message($"Relatorio do erro para desenvolvedores: {ex.Message}", ConsoleColor.Red);
            ConsoleUtils.Message("Se o erro persistir, denuncie o relatorio acima ao desenvolvedor do programa",
                ConsoleColor.Red);
            ConsoleUtils.Message("Repositorio do GitHub para issues: (TODO)"); //TODO
        }
        finally
        {
            ConsoleUtils.Pause();
            EnviarOpcoesDoMenu();
        }
    }

    private static void ChecarSeExistemServicos()
    {
        var servicos = GerenciadorDeServicos.Carregar();
        if (servicos.Count == 0)
        {
            ConsoleUtils.Message("| Voce ainda nao fez nenhum servico.", ConsoleColor.DarkRed, true);
            ConsoleUtils.Pause("(!) Esqueceu de adicionar algum serviço? Pressione qualquer tecla para voltar ao menu ao menu...", ConsoleColor.Yellow);
            EnviarOpcoesDoMenu();
        }
    }
}