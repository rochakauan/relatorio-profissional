namespace RelatorioProfissional.Erros;

public class ServicoNaoEncontradoExc : Exception
{
    public ServicoNaoEncontradoExc(int id)
        : base($"Serviço com ID {id} nao encontrado.")
    {}
}