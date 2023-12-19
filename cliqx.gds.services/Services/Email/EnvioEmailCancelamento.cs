using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.GdsModels.Enum;

namespace cliqx.gds.services.Services.Email
{
    public class EnvioEmailCancelamento
    {
        public static async void EnviarEmailNotificandoCancelamento(Order order, IEnumerable<OrderItem> listaProduto, AuthenticationData authenticationData)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
            {
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress("cancelamento@snog.com.br", "SNOG - CANCELAMENTO PASSAGEM");

                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Host = "smtp.office365.com";
                    smtpClient.Port = 587;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("cancelamento@snog.com.br", "eyAasdas123");

                    message.From = fromAddress;

                    string Assunto = "SNOG - CANCELAMENTO PASSAGEM - PEDIDO: " + order.Id;
                    message.Subject = Assunto;

                    long totalMulta = 0;


                    //string chamado = "http://chamados.facilitabots.com.br/front/ticket.form.php?id=" + numerochamadoglpi;
                    message.IsBodyHtml = true;
                    //message.Body = "Novo cancelamento, pedido:  " + numeroPedido; // + "<br>" + chamado ;

                    string campoEmail = "";
                    var email = order.Client.Contacts.FirstOrDefault(x => x.ContactType == ContactTypeEnum.EmailPessoal).Value;
                    if (!String.IsNullOrEmpty(email))
                    {
                        campoEmail = $"<b>Email Cliente: </b> {email} <br/>";
                    }

                    string corpo = "CLIENTE SOLICITA O CANCELAMENTO DE PASSAGEM <br/><br/>" +
                        $"<b>Cliente: </b>{order.Client.FullName} <br/>" +
                        $"<b>Número do Pedido: </b> {order.Id} <br/>" +
                        $"<b>Data da Compra: </b> {order.IssuedAt.ToString("dd/MM/yyyy HH:mm")} <br/>" +
                        campoEmail;
                    foreach (var item in order.Items)
                    {
                        foreach (var seat in item.Trip.Seats)
                        {
                            corpo += $"<b>Nome do Passageiro:</b> {seat.Client.FullName} <br/>";
                            corpo += $"<b>Localizador:</b> {seat.Transaction.LocatorId} <br/>";
                        }
                        //var ced = Packer.Unpack<SeatReservationCreateExtraData>(produto.ExtraData);
                        //var nomeEmpresa = order?.Trip?.Company?.Name ?? string.Empty;
                        //corpo += $"<br/><b>Poltrona:</b> {produto.Seat.Number} <br/>";
                        
                        //corpo += $"<b>Booking: </b> {produto.BookingNumber} <br/>";
                        //corpo += $"<b>Empresa: </b> {nomeEmpresa} <br/>";
                        //totalMulta += (int)produto.CancelationFee;
                    }

                    corpo += $"<br/><b>Valor da Compra: </b> {order.TotalValueAsDecimal}  <br/>" +
                        $"<b>Valor Taxa de Conveniência: </b> {order.ValueServiceTax} <br/>" +
                        $"<b>Valor da Multa de Cancelamento: </b> {(totalMulta / 100M)} <br/>" +
                        $"<b>Data e Hora Cancelamento: </b> {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                    message.Body = corpo;

                    message.To.Add("financeiro@snog.com.br");
                    message.To.Add("cancelamento@snog.com.br");
                    message.To.Add("alex.bonfim@cliqx.com.br");
                    message.To.Add("marcus@cliqx.com.br");
                    message.To.Add("adilson@cliqx.com.br");
                    message.To.Add("guilherme.pires@cliqx.com.br");

                    try
                    {
                        await smtpClient.SendMailAsync(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                    }
                }
            }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao tentar enviar email para o financeiro. Error: " + e.Message);
                return;
            }
            
        }
    }
}
