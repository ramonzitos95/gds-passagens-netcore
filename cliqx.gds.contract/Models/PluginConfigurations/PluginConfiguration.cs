using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Global;

namespace cliqx.gds.contract.Models.PluginConfigurations;

public class PluginConfiguration : BasicObjectGuid
{
    [Column(TypeName ="varchar(36) COLLATE ascii_general_ci")]
    public Guid PluginId { get; set; }
    public Plugin Plugin { get; set; }
    public string ApiBaseUrl { get; set; }

    [MaxLength(8192)]
    public string CredentialsJsonObject { get; set; }

    [StringLength(255)]
    public string? Options { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(255)]
    public string TransactionName { get; set; }

    [StringLength(255)]
    public string TransactionLocator { get; set; }
    public long StoreId { get; set; }
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public int PaymentServiceId { get; set; }
    public PaymentService PaymentService { get; set; }

    [StringLength(4000)]
    public string? ExtraData { get; set; }

    public List<UserPlugin> UsersPlugins { get; set; }

}
