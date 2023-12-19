using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels;

public abstract class OrderBase : BaseObjectPlugin
{
    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Client Client { get; set; }

    /// <summary>
    /// Meio de pagamento
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Payment Payment { get; set; }

    /// <summary>
    /// CPF que sera usado na nota fiscal
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string InvoiceCpfCnpj { get; set; }

    /// <summary>
    /// Nome que sera usado na nota fiscal
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string InvoiceName { get; set; }

    /// <summary>
    /// Observações da ordem
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Notes { get; set; }

    /// <summary>
    /// Estado da ordem de serviço. Usado para orientar operações de atualização (update)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Status { get; set; }

    /// <summary>
    /// Cupom de desconto
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string DiscountCoupon { get; set; }

    /// <summary>
    /// Data de emissao da ordem
    /// </summary>
    public DateTime IssuedAt { get; set; }

    /// <summary>
    /// Identificacao do marketplace
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MarketPlace MarketPlace { get; set; }

    /// <summary>
    /// Informacoes da loja
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Store Store { get; set; }

    /// <summary>
    /// Id da Loja
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string StoreId { get; set; }

    /// <summary>
    /// Parceiro Id
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ParceiroId { get; set; }

    /// <summary>
    /// Token do parceiro, vinculo da empresa com o pedido
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string TokenParceiro { get; set; }

    /// <summary>
    /// Emdereco da ordem de compra
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Address Address { get; set; }

    /// <summary>
    /// Contratou seguro
    /// </summary>
    public bool HasInsurance { get; set; }

    public long TotalValue { get; set; }
    public long DiscountValue { get; set; }
    public long Value { get; set; }
    public decimal ValueAsDecimal => Value / 100M;
    public decimal TotalValueAsDecimal => TotalValue / 100M;
    public long ValueServiceTax { get; set; }
    public decimal ValueServiceTaxAsDecimal => ValueServiceTax / 100M;
    
    public decimal PartnerFee { get; set; }

    public long ValuePartnerFee { get; set; }
    public decimal ValuePartnerFeeAsDecimal => ValuePartnerFee / 100M;


    public DateTime ExpirationDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Summary { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Channel { get; set; }

    public long? StatusPedidoId { get; set; }
}
