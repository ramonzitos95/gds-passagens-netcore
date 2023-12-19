using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.Enums;

namespace cliqx.gds.contract.GdsModels;

public class Payment : BaseObjectWithOutPlugin
{
    public PaymentModeEnum PaymentMechanism { get; set; }
    
    /// <summary>
    /// Nome do método de pagamento
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; }

    /// <summary>
    /// Descrição do método de pagamento
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; set; }

    /// <summary>
    /// Url da imagem do método de pagamento
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Image { get; set; }

    /// <summary>
    /// Quantidade maxima de parcelas
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int MaxInstalments { get; set; }

    /// <summary>
    /// Parcelamento selecionado pelo cliente
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? Instalments { get; set; }

    /// <summary>
    /// link para pagamento da ordem
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Url { get; set; }

    /// <summary>
    /// mensagem que sera enviada para o cliente sobre o processo de pagamento
    /// ex: a partir de agora você tem 5min para efetuar o pagamento, caso não seja
    /// efetuado no prazo, sua compra sera cancelada
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Instruction { get; set; }

    /// <summary>
    /// data e hora para validade do link de pagamento
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime ExpirationDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<PaymentTax> PaymentsTax { get; set; }
}

public class PaymentTax
{
    public PaymentModeEnum PaymentMechanism { get; set; }
    public string MechanismDescription { get; set; }
    public decimal Value { get; set; }
}
