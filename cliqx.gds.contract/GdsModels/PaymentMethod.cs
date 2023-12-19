using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.contract.GdsModels;

public enum PaymentMethod
    {
        /// <summary>
        /// nao definido
        /// </summary>
        Undefined,
        
        /// <summary>
        /// por ex. pagamento na loja, pessoalmente
        /// </summary>
        Offline,
        
        /// <summary>
        /// link gerado por alguma api e enviado para o cliente via bot
        /// </summary>
        Link,
        
        /// <summary>
        /// um codigo gerado para ser aplicado em algum aplicativo, ex: picpay 
        /// </summary>
        Code,
    }
