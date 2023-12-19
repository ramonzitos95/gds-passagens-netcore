using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models
{
    public class Conexao
    {
        public long PoltronasLivres { get; set; }
        public long PoltronasTotal { get; set; }
        public string Classe { get; set; }
        public string DataCorrida { get; set; }
        public string DataSaida { get; set; }
        public string DataChegada { get; set; }
        public string HoraSaida { get; set; }
        public string HoraChegada { get; set; }
        public string Preco { get; set; }
        public string PrecoOriginal { get; set; }
        public string Servico { get; set; }
        public long Linha { get; set; }
        public string Empresa { get; set; }
        public long EmpresaId { get; set; }
        public long Marca { get; set; }
        public long Origem { get; set; }
        public string OrigemDescricao { get; set; }
        public long Destino { get; set; }
        public string DestinoDescricao { get; set; }
        public bool Vende { get; set; }
        public bool IsBpe { get; set; }
        public long Sequencia { get; set; }
    }
}