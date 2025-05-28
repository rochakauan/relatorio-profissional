using System.ComponentModel;
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
}