namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
public class ClientRequest
{
    public long Id { get; set; }

    public string Nome { get; set; }

    public string Sobrenome { get; set; }

    public string Cpf { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public long LojaId { get; set; }
}

