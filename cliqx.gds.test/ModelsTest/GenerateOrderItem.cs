using cliqx.gds.contract.GdsModels;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.test.ModelsTest
{
    public static class GenerateDataPreOrdem
    {
        public static ItemPedido Generate(this ItemPedido itemPedido)
        {
            itemPedido = new ItemPedido()
            {
                Nome = "IDA",
                Quantidade = 1,
                ValorUnitario = 125,
                Descricoes = new List<Descricao>()
            };

            itemPedido.Descricoes.GenerateDescricoes();
            return itemPedido;
        }

        public static List<PreOrderItem> GetGeneratePreOrderItens()
        {
            var itens = new List<PreOrderItem>();
            var itemPedido = new PreOrderItem()
            {
                TravelDirectionType = contract.GdsModels.Enum.TravelDirectionEnum.IDA,
                TotalValue = 250,
                Value = 250,
                PluginId = Guid.NewGuid(),
                PedagogueValue = 150,
                // Seat = new Seat()
                // {
                //     Id = "1",
                //     Class = "EXECUTIVO",
                //     Key = "2",
                //     Number = "3",
                //     DiscountValue = 10,
                //     Display = $"📍 Origem: SÃO PAULO \n" +
                //                   $"🏁 Destino: BLUMENAU \n" +
                //                   $"⏰ Saída: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                //                   $"🏁 Chegada: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} \n",
                //     Value = 125,
                //     Layout = new SeatLayout(),
                //     ArrivalTax = 11
                // }
            };

            itens.Add(itemPedido);

            return itens;
        }

        public static List<OrderItem> GetGenerateOrderItens()
        {
            var itens = new List<OrderItem>();
            // var itemPedido = new OrderItem()
            // {
            //     Id = "207",
            //     //TravelDirection = "IDA",
            //     TotalValue = 250,
            //     Value = 250,
            //     PluginId = Guid.NewGuid(),
            //     PedagogueValue = 150,
            //     Seat = new Seat()
            //     {
            //         Id = "1",
            //         Class = "EXECUTIVO",
            //         Key = "2",
            //         Number = "3",
            //         DiscountValue = 10,
            //         Display = $"📍 Origem: SÃO PAULO \n" +
            //                       $"🏁 Destino: BLUMENAU \n" +
            //                       $"⏰ Saída: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
            //                       $"🏁 Chegada: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} \n",
            //         Value = 125,
            //         Layout = new SeatLayout(),
            //         ArrivalTax = 11
            //     }
            // };

            // itens.Add(itemPedido);

            return itens;
        }


        public static Trip GenerateTrip()
        {
            var trip = new Trip()
            {
                ArrivalTime = DateTime.Now,
                DepartureTime = DateTime.Now.AddDays(5),
                AvailableSeats = 20,
                Class = new TripClass()
                {
                    Description = "teste",
                    Id = "1",
                    Name = "Executiva"
                },
                Station = new TripStation() 
                { 
                    DestinationId = "1",
                    DestinationName = "Estação teste",
                    OriginId = "2",
                    OriginName  = "Origem teste"
                },
                PluginId = Guid.NewGuid(),
                Company = new Company() 
                { 
                    PluginId = Guid.NewGuid(),
                    Name = "Viação garcia",
                    ExternalId = Guid.NewGuid(),
                    Key = "2",
                    Description = "teste"
                },
                Origin = new TripOriginDestination()
                {
                    CityId = "1",
                    CityName = "São Paulo",
                    StationId = "1",
                    StationName = "Estação"
                },
                Destination = new TripOriginDestination()
                {
                    CityId = "1",
                    CityName = "São Paulo",
                    StationId = "1",
                    StationName = "Estação"
                },
                PassengerType = "NORMAL",
                DiscountValue = 0,
                Value = 125,
                Key = "3"
            };
            
            return trip;
        }

        public static List<Descricao> GenerateDescricoes(this List<Descricao> descricoes)
        {
            descricoes = new List<Descricao>
            {
                new Descricao
                {
                    Chave = "PLUGIN_TRANSACTION",
                    Valor = "FFFTTTRRR",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Chave Transacao"
                },
                new Descricao
                {
                    Chave = "POLTRONA",
                    Valor = "25000",
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Poltrona"
                },
                new Descricao
                {
                    Chave = "CIDADE_ORIGEM_ID",
                    Valor = "15000",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Origem ID"
                },
                new Descricao
                {
                    Chave = "ORIGEM_CIDADE",
                    Valor = "SÃO PAULO",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Origem Nome"
                },
                new Descricao
                {
                    Chave = "ESTACAO_ORIGEM_ID",
                    Valor = "12",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Estacao Origem ID"
                },
                new Descricao
                {
                    Chave = "ESTACAO_ORIGEM_NOME",
                    Valor = "ESTAÇÃO DO RODOANEL",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Estacao Origem Nome"
                },
                new Descricao
                {
                    Chave = "ORIGEM_DATA_SAIDA",
                    Valor = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Origem data saída"
                },
                new Descricao
                {
                    Chave = "CIDADE_DESTINO_ID",
                    Valor = "15500",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Destino ID"
                },
                new Descricao
                {
                    Chave = "DESTINO_CIDADE",
                    Valor = "Blumenau",
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Destino cidade"
                },
                new Descricao
                {
                    Chave = "ESTACAO_DESTINO_ID",
                    Valor = "15",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Estação destino ID"
                },
                new Descricao
                {
                    Chave = "ESTACAO_DESTINO_NOME",
                    Valor = "BLUMENAU CENTRAL",
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Estação destino nome"
                },
                new Descricao
                {
                    Chave = "DESTINO_DATA_CHEGADA",
                    Valor = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Destino data chegada"
                },
                new Descricao
                {
                    Chave = "CORRIDA_ID",
                    Valor = "25000",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Corrida ID"
                },
                new Descricao
                {
                    Chave = "CORRIDA_CLASSE",
                    Valor = "EXECUTIVA",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Corrida Classe"
                },
                new Descricao
                {
                    Chave = "CORRIDA_CLASSE_ID",
                    Valor = "2500",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Corrida Classe ID"
                },
                new Descricao
                {
                    Chave = "CORRIDA_EMPRESA",
                    Valor = "VIAÇÃO GARCIA",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Corrida empresa"
                },
                new Descricao
                {
                    Chave = "CORRIDA_EMPRESA_ID",
                    Valor = "13",
                    Exibir = "N",
                    Posicao = 0,
                    Titulo = "Corrida empresa ID"
                },
                new Descricao
                {
                    Chave = "VALOR_COM_TAXA_IDA",
                    Valor = "2500",
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Valor com Taxa"
                },
                new Descricao
                {
                    Chave = "TAXA_EMBARQUE",
                    Valor = true.ToString(),
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Passageiro"
                },
                new Descricao
                {
                    Chave = "PASSAGEIRO_NOME",
                    Valor = "JOÃO FERREIRA DA SILVA",
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Passageiro"
                },
                new Descricao
                {
                    Chave = "PASSAGEIRO_DOCUMENTO",
                    Valor = "08390890909",
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Passageiro documento"
                },
                new Descricao
                {
                    Chave = "PASSAGEIRO_TIPO",
                    Valor = "PADRÃO",
                    Exibir = "S",
                    Posicao = 0,
                    Titulo = "Passageiro tipo"
                }
            };

            return descricoes;
        }
    }
}
