using System.Globalization;
 
 namespace RelatorioProfissional.Chaveiro;
 
 public class Services
 {
     private static int _ultimoId = 0;
 
     public int Id { get; set; }
     public string Servico { get; set; }
     public decimal ValorUnitario { get; set; }
     public decimal CustoUnitario { get; set; }
     public decimal Quantidade { get; set; }
     public EMeioDePagamento Pagamento { get; set; }
     public DateTime DataCadastro {get; set;}

     private decimal ValorTotalBruto => ValorUnitario * Quantidade;
     private decimal CustoTotal => CustoUnitario * Quantidade;
     private decimal ValorTotal => ValorTotalBruto - CustoTotal;
     public decimal ComissaoFuncionario  => ValorTotal / 2;
     
     public Services()
     {
         Id = ++_ultimoId;
         DataCadastro = DateTime.Now;
     }

     public Services(string servico, decimal valorUnitario, decimal custoUnitario, int quantidade,
         EMeioDePagamento eMeioDePagamento, DateTime? dataCadastro = null) : this()
     {
         Servico = servico;
         ValorUnitario = valorUnitario;
         CustoUnitario = custoUnitario;
         Quantidade = quantidade;
         Pagamento = eMeioDePagamento;
         DataCadastro = dataCadastro ?? DateTime.Now;
 
     }

     public static void AtualizarUltimoId(int ultimoIdExistente)
     {
         _ultimoId = ultimoIdExistente;
     }
     public override string ToString()
     {
         return $"| Serviço: {Servico} " +
                $"| Valor unitario: {ValorUnitario.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))} " +
                $"| Custo unitario: {CustoUnitario.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))} " +
                $"\n- Valor total bruto: {ValorTotalBruto.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))} " +
                $"| Valor Total: {ValorTotal.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))} " +
                $"| Meio de pagamento: {Pagamento}" +
                $"\n| Comissão do funcionario: {ComissaoFuncionario.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))}" +
                $"\n------------ {DataCadastro:f}\n";
     }
 }
 
 public enum EMeioDePagamento
 {
     Pix,
     Cartao,
     Dinheiro
 }