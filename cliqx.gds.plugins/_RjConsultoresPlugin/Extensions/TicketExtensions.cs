using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Response;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Extensions
{
    public static class TicketExtensions
    {
        public static Ticket AsTicket(this ConfirmaVendaResponse response)
        {
            var ticket = new Ticket();

            ticket.Locator = response.Localizador;
            ticket.Id = response.Servico;

            ticket.SeatNumber = response.Poltrona;
            ticket.ServiceType = response.Bpe.TipoServico;
            ticket.ServiceClass = response.Bpe.Classe;

            //ticket.AgencyCompanyName = response.Bpe.CabecalhoAgencia.RazaoSocialAgencia;
            //ticket.AgencyCnpj = response.Bpe.CabecalhoAgencia.CnpjAgencia;
            //ticket.AgencyStateRegistration = response.Bpe.CabecalhoAgencia.InscrEstadualAgencia;
            //ticket.AgencyPhone = response.Bpe.CabecalhoAgencia.TelefoneAgencia;
            ticket.AgencyAddress = response.Bpe.CabecalhoAgencia.Endereco;
            ticket.AgencyDistrict = response.Bpe.CabecalhoAgencia.Bairro;
            ticket.AgencyNumber = response.Bpe.CabecalhoAgencia.Numero;
            ticket.AgencyCity = response.Bpe.CabecalhoAgencia.Cidade;
            ticket.AgencyState = response.Bpe.CabecalhoAgencia.Uf;
            //ticket.AgencyZipCode = response.Bpe.CabecalhoAgencia.ce;

            ticket.CompanyName = response.Bpe.CabecalhoEmitente.RazaoSocial;
            ticket.CompanyCnpj = response.Bpe.CnpjEmpresa;
            ticket.CompanyStateRegistration = response.Bpe.CnpjEmpresa;
            ticket.CompanyPhone = response.Bpe.TelefoneEmpresa;
            ticket.CompanyAddress = response.Bpe.CabecalhoEmitente.Endereco;
            ticket.CompanyDistrict = response.Bpe.CabecalhoEmitente.Bairro;
            ticket.CompanyNumber = response.Bpe.CabecalhoEmitente.Numero;
            ticket.CompanyCity = response.Bpe.CabecalhoEmitente.Cidade;
            ticket.CompanyState = response.Bpe.CabecalhoEmitente.Uf;
            ticket.CompanyZipCode = response.Bpe.CabecalhoEmitente.Cep;

            ticket.BpeKey = response.Bpe.ChaveBpe;
            ticket.OriginAnttCode = response.Bpe.CodigoANTTOrigem == null ? response.Bpe.CodigoANTTOrigem.ToString() : response.Bpe.CodigoAnttOrigem.ToString();
            ticket.DestinationAnttCode = response.Bpe.CodigoANTTDestino == null ? response.Bpe.CodigoANTTDestino.ToString() : response.Bpe.CodigoAnttDestino.ToString();
            ticket.MonitriipCode = response.Bpe.CodigoMonitriipBPe;
            ticket.Contingency = response.Bpe.Contingencia;
            ticket.BpeAuthorizationDate = response.Bpe.DataAutorizacao;
            ticket.BpeAuthorizationNumber = response.Bpe.ProtocoloAutorizacao;
            ticket.BpeSystemNumber = response.Bpe.NumeroBpe;
            ticket.Route = response.Bpe.Linha;

            ticket.OtherTaxes = response.Bpe.TaxaEmbarque;

            ticket.BoardingPlatform = response.Bpe.Plataforma;
            ticket.DiscountPrice = response.Bpe.Desconto;
            ticket.TollPrice = response.Bpe.Pedagio;

            ticket.MandatoryInsurancePrice = response.Bpe.Seguro;

            ticket.FarePrice = (response.Bpe.Tarifa);

            ticket.BoardingFee = double.Parse(response.Bpe.TaxaEmbarque);
            ticket.ServicePrefix = response.Bpe.Prefixo;

            ticket.BpeQrCode = new Uri(response.Bpe.QrcodeBpe);

            ticket.BpeSeries = response.Bpe.Serie;

            ticket.DiscountType = response.Bpe.TipoDesconto;
            ticket.Change = response.Bpe.Troco;
            ticket.PaymentAmount = response.Bpe.ValorFormaPagamento;
            ticket.TotalAmount = response.Bpe.ValorPagar;
            ticket.SeatTicketNumber = response.NumeroBilhete;
            ticket.Currency = "BRL";
            ticket.BpeByteArray = response.XmlBPE;
            
            return ticket;
        }
    }
}