using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.test.ModelsTest
{
    public static class GenerateConnections
    {
        public static List<Connection> Generate()
        {
            var Connections = new List<contract.GdsModels.Connection>
            {
                new contract.GdsModels.Connection()
                {
                    Id = "2",
                    PluginId = Guid.NewGuid(),
                    Key = "Teste",
                    ArrivalTime = DateTime.Now,
                    DepartureTime = DateTime.Now,
                    AvailableSeats=15,
                    Class = new TripClass()
                    {
                        Description = "Teste",
                        Id = "2",
                        Name = "Teste"
                    },
                    Company = new Company()
                    {
                        Description = "Teste",
                        Id = "25",
                        ExternalId = Guid.NewGuid(),
                        Key = "Teste",
                        Name = "CompanyName",
                    },
                    Seats = new List<contract.GdsModels.Connection.ConnectionItem>()
                    {
                        new contract.GdsModels.Connection.ConnectionItem()
                        {
                            Client = InstanceCliente(),
                            Seat = new contract.GdsModels.Seat()
                            {
                                Class = "Executivo",
                                ArrivalTax = 10,
                                Description = "Teste",
                                DiscountValue = 0,
                                Image = string.Empty,
                                Number = "20",
                                Display = string.Empty,
                                Vacant = false,
                                PluginId = Guid.NewGuid(),
                                Type = "Normal",
                                Id = "20",
                            },
                            Ticket = new contract.GdsModels.Ticket()
                            {
                                Id = "Teste",
                                Key = "Teste",
                                PluginId= Guid.NewGuid(),
                                Display = string.Empty,
                                AgencyAddress = "Agency",
                                AgencyCity = "Palmas",
                                AgencyCnpj = "00000000000000"
                            },
                            TravelDirectionType = contract.GdsModels.Enum.TravelDirectionEnum.IDA,
                            Transaction = new Transaction()
                            {
                                LocatorId = Guid.NewGuid().ToString(),
                                ReservationId = Guid.NewGuid().ToString(),
                                TransactionId = Guid.NewGuid().ToString(),
                            }
                        }
                    }
                }
            };

            return Connections;
        }

        public static List<Connection> GenerateManyConnections()
        {
            var Connections = new List<contract.GdsModels.Connection>
            {
                new contract.GdsModels.Connection()
                {
                    Id = "2",
                    PluginId = Guid.NewGuid(),
                    Key = "Teste",
                    ArrivalTime = DateTime.Now,
                    DepartureTime = DateTime.Now,
                    AvailableSeats=15,
                    Class = new TripClass()
                    {
                        Description = "Teste",
                        Id = "2",
                        Name = "Teste"
                    },
                    Company = new Company()
                    {
                        Description = "Teste",
                        Id = "25",
                        ExternalId = Guid.NewGuid(),
                        Key = "Teste",
                        Name = "CompanyName",
                    },
                    Seats = new List<contract.GdsModels.Connection.ConnectionItem>()
                    {
                        new contract.GdsModels.Connection.ConnectionItem()
                        {
                            Client = InstanceCliente(),
                            Seat = new contract.GdsModels.Seat()
                            {
                                Class = "Executivo",
                                ArrivalTax = 10,
                                Description = "Teste",
                                DiscountValue = 0,
                                Image = string.Empty,
                                Number = "20",
                                Display = string.Empty,
                                Vacant = false,
                                PluginId = Guid.NewGuid(),
                                Type = "Normal",
                                Id = "20",
                            },
                            Ticket = new contract.GdsModels.Ticket()
                            {
                                Id = "Teste",
                                Key = "Teste",
                                PluginId= Guid.NewGuid(),
                                Display = string.Empty,
                                AgencyAddress = "Agency",
                                AgencyCity = "Palmas",
                                AgencyCnpj = "00000000000000"
                            },
                            TravelDirectionType = contract.GdsModels.Enum.TravelDirectionEnum.IDA_CONEXAO,
                            Transaction = new Transaction()
                            {
                                LocatorId = Guid.NewGuid().ToString(),
                                ReservationId = Guid.NewGuid().ToString(),
                                TransactionId = Guid.NewGuid().ToString(),
                            }
                        }
                    }
                },
                new contract.GdsModels.Connection()
                {
                    Id = "2",
                    PluginId = Guid.NewGuid(),
                    Key = "Teste",
                    ArrivalTime = DateTime.Now,
                    DepartureTime = DateTime.Now,
                    AvailableSeats=15,
                    Class = new TripClass()
                    {
                        Description = "Teste",
                        Id = "2",
                        Name = "Teste"
                    },
                    Company = new Company()
                    {
                        Description = "Teste",
                        Id = "25",
                        ExternalId = Guid.NewGuid(),
                        Key = "Teste",
                        Name = "CompanyName",
                    },
                    Seats = new List<contract.GdsModels.Connection.ConnectionItem>()
                    {
                        new contract.GdsModels.Connection.ConnectionItem()
                        {
                            Client = InstanceCliente(),
                            Seat = new contract.GdsModels.Seat()
                            {
                                Class = "Executivo",
                                ArrivalTax = 10,
                                Description = "Teste",
                                DiscountValue = 0,
                                Image = string.Empty,
                                Number = "20",
                                Display = string.Empty,
                                Vacant = false,
                                PluginId = Guid.NewGuid(),
                                Type = "Normal",
                                Id = "20",
                            },
                            Ticket = new contract.GdsModels.Ticket()
                            {
                                Id = "Teste",
                                Key = "Teste",
                                PluginId= Guid.NewGuid(),
                                Display = string.Empty,
                                AgencyAddress = "Agency",
                                AgencyCity = "Palmas",
                                AgencyCnpj = "00000000000000"
                            },
                            TravelDirectionType = contract.GdsModels.Enum.TravelDirectionEnum.VOLTA_CONEXAO,
                            Transaction = new Transaction()
                            {
                                LocatorId = Guid.NewGuid().ToString(),
                                ReservationId = Guid.NewGuid().ToString(),
                                TransactionId = Guid.NewGuid().ToString(),
                            }
                        }
                    }
                }
            };

            return Connections;
        }

        private static contract.Models.Client InstanceCliente()
        {
            var documento = new Document() { Value = "95057818066", DocumentType = contract.GdsModels.Enum.DocumentTypeEnum.CPF };
            var documents = new List<Document>() { documento };
            var contatoTelefone = new Contact() { Value = "5511981617830", ContactType = contract.GdsModels.Enum.ContactTypeEnum.CelularPessoal };
            var contatoEmail = new Contact() { Value = "rodrigosilva@gmail.com", ContactType = contract.GdsModels.Enum.ContactTypeEnum.EmailPessoal };
            var contacts = new List<Contact>() { contatoEmail, contatoTelefone };
            return new contract.Models.Client()
            {
                Id = "261",
                Documents = documents,
                FullName = "Rodrigo da Silva",
                Contacts = contacts
            };
        }
    }
}
