using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;

namespace cliqx.gds.plugins._SmartbusPlugin.Extensions
{
    public static class OrderExtensions
    {
        public static Ticket AsTicket(this ConfirmacaoResponse confirmacao)
        {
            var ticket = new Ticket();
            var data = confirmacao.Data;

            ticket.Locator = data.Localizador;
            ticket.Id = data.Bilhete;
            ticket.SeatNumber = data.Assento.ToString();

            ticket.ServiceType = data.TipoServico;
            ticket.ServiceClass = data.ClasseServico;

            ticket.AgencyCompanyName = data.RazaoSocialAgencia;
            ticket.AgencyCnpj = data.CnpjAgencia;
            ticket.AgencyStateRegistration = data.InscrEstadualAgencia;
            ticket.AgencyPhone = data.TelefoneAgencia;
            ticket.AgencyAddress = data.EnderecoAgencia;
            ticket.AgencyDistrict = data.BairroAgencia;
            ticket.AgencyNumber = data.NumeroAgencia;
            ticket.AgencyCity = data.CidadeAgencia;
            ticket.AgencyState = data.UfAgencia;
            ticket.AgencyZipCode = data.CepAgencia;


            ticket.CompanyName = data.RazaoSocialEmpresa;
            ticket.CompanyCnpj = data.CnpjEmpresa;
            ticket.CompanyStateRegistration = data.InscrEstadualEmpresa;
            ticket.CompanyPhone = data.TelefoneEmpresa;
            ticket.CompanyAddress = data.EnderecoEmpresa;
            ticket.CompanyDistrict = data.BairroEmpresa;
            ticket.CompanyNumber = data.NumeroEmpresa;
            ticket.CompanyCity = data.CidadeEmpresa;
            ticket.CompanyState = data.UfEmpresa;
            ticket.CompanyZipCode = data.CepEmpresa;


            ticket.BpeKey = data.ChaveBpe;
            ticket.OriginAnttCode = data.CodigoAnttOrigem;
            ticket.DestinationAnttCode = data.CodigoAnttDestino;
            ticket.MonitriipCode = data.CodigoMonitriip;
            ticket.Contingency = data.Contingencia;
            ticket.BpeAuthorizationDate = data.DataAutorizacaoBpe;
            ticket.PaymentMethod = data.FormaPagamento;
            ticket.Route = data.Linha;
            ticket.BpeAuthorizationNumber = data.NumeroAutorizacaoBpe;

            ticket.BpeSystemNumber = data.NumeroSistemaBpe;

            ticket.OtherTaxes = data.OutrosTributos;

            ticket.BoardingPlatform = data.PlataformaEmbarque;

            ticket.BusStationAccess = data.AcessoRodoviaria;
            ticket.DiscountPrice = data.PrecoDesconto;

            ticket.OtherPrice = data.PrecoOutros;
            ticket.TollPrice = data.PrecoPedagio;

            ticket.MandatoryInsurancePrice = data.PrecoSeguroObrigatorio;
            ticket.OptionalInsurancePrice = data.PrecoSeguroOpcional;

            ticket.FarePrice = data.PrecoTarifa;

            ticket.BoardingFee = data.PrecoTaxaEmbarque;

            ticket.ServicePrefix = data.PrefixoLinha;
            
            ticket.AuthorizationProtocol = data.ProtocoloAutorizacao;
            ticket.BpeQrCode = data.QrCodeBpe;
            ticket.BpeSeries = data.SerieBpe;
            ticket.DiscountType = data.TipoDesconto;
            ticket.Change = data.Troco;
            ticket.PaymentAmount = data.ValorFormaPagamento;
            ticket.TotalAmount = data.ValorPagar;
            ticket.SeatValue = (decimal)data.ValorAssento;
            ticket.SeatTicketNumber = data.NumeroBilheteAssento.ToString();
            ticket.Currency = data.Moeda;
            ticket.BpeByteArray = data.DabpeByteArray;

            return ticket;
        }
    }
}