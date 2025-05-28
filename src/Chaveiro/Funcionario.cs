namespace RelatorioProfissional.Chaveiro;

public class Funcionario
{
    public string Nome;
    public decimal? Salario = null;

    public Funcionario(string nome)
    {
        Nome = nome;
    }

    public string GetNome()
    {
        return Nome;
    }

    public override string ToString()
    {
        return Nome;
    }
}