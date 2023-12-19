
namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
public class ClientResponse
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string NomeCompleto { get; set; }
    public string Cpf { get; set; }
    public string Ativo { get; set; }
    public DateTime? DataCadastro { get; set; }
    public DateTime? DataUltimoAcesso { get; set; }
    public DateTime? DataUltimoPedido { get; set; }
    public DateTime? DataExclusao { get; set; }
    public Loja Loja { get; set; }
    public Contato Contato { get; set; }
}
