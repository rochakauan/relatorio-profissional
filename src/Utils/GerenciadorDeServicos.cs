using System.Globalization;
using System.Text.Json;
using RelatorioProfissional.Chaveiro;

namespace RelatorioProfissional.Utils;

public static class GerenciadorDeServicos
{
    private static readonly string CaminhoJson = "servicos-e-vendas.json";
    
    public static List<Services> Carregar()
    {
        if(!File.Exists(CaminhoJson))
            return new List<Services>();
        var json = File.ReadAllText(CaminhoJson);
        var servicos = JsonSerializer.Deserialize<List<Services>>(json) ?? new List<Services>();

        if (servicos.Count > 0)
        {
            var maiorId = servicos.Max(s => s.Id);
            Services.AtualizarUltimoId(maiorId);
        }
        return servicos;
    }

    public static void Salvar(List<Services> lista)
    {
        var options = new JsonSerializerOptions {  WriteIndented = true };
        var json = JsonSerializer.Serialize(lista, options);
        File.WriteAllText(CaminhoJson, json);
        
        if(lista.Count == 0)
            Services.AtualizarUltimoId(0);
    }

    public static void AdicionarServico(List<Services> lista, Services servico)
    {
        lista.Add(servico);
        Salvar(lista);
    }
    
    
    public static void FecharSemana()
    {
        var servicos = Carregar();
        if (servicos.Count == 0)
        {
            ConsoleUtils.Message("(!) Nenhum serviço foi cadastrado", ConsoleColor.Red, clearConsoleBefore: true);
            ConsoleUtils.Pause();
            return;
        }

        var dataLimite = DateTime.Now.AddDays(-7);
        
        var servicosDaSemana = servicos
            .Where(s => s.DataCadastro >= dataLimite)
            .ToList();

        if (servicosDaSemana.Count == 0)
        {
            ConsoleUtils.Message("(!) Voce nao fez nenhum serviço nos ultimos 7 dias!", ConsoleColor.Yellow, clearConsoleBefore: true);
            ConsoleUtils.Pause();
            return;
        }

        var totalComissao = servicosDaSemana.Sum(s => s.ComissaoFuncionario);
        
        ConsoleUtils.Message("Relatorio semanal:", ConsoleColor.Yellow, clearConsoleBefore: true);
        ConsoleUtils.Message($"Periodo: {dataLimite:dd/MM/yyyy} ate {DateTime.Now:dd/MM/yyyy}\n", ConsoleColor.DarkMagenta);

        foreach (var servico in servicosDaSemana)
        {
            ConsoleUtils.Message(servico.ToString(), ConsoleColor.DarkYellow);
        }
        
        ConsoleUtils.Message($"\nO lucro liquido do funcionario nesta semana foi de: {totalComissao.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))}", ConsoleColor.Green);
        ConsoleUtils.Pause();
    }
}