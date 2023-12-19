using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.contract.GdsModels.Enum
{
    public enum CheckoutType
    {
        Undefined, // nao configurado
        Native, // o plugin processa todo o pagamento (inclusive gerar link para captuar dados do cartao)
        Zappag, // o todo pagamento efetuado do lado do zappag (captura de dados e execucao/transacao do pagamento)
        Hybrid // apenas tela de checkout fica do lado do zappag (captura de dados do cartao)
    }
}