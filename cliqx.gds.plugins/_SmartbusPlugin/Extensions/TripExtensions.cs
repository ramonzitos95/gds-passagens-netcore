
using System.Globalization;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using cliqx.gds.plugins.Util;

namespace cliqx.gds.plugins._SmartbusPlugin.Extensions;

public static class TripExtensions
{
    public static List<Trip> AsTrip(this ConsultaResponse consultaRespons, TripQuery query)
    {
        var trips = new List<Trip>();

        if(consultaRespons != null)
        {
            foreach (var item in consultaRespons.Data)
            {
                var trip = new Trip();

                trip.Id = item.Servico.ToString();

                trip.Value = Convert.ToInt64(item.Preco * 100);
                //trip.DepartureTax = (decimal)item.DetalhePreco.TaxaEmbarque;

                DateTimeOffset departureTime = item.HoraSaida;

                TimeSpan timeOfDay = departureTime.TimeOfDay;
                trip.DepartureTime = new DateTime(query.TravelDate.Year, query.TravelDate.Month, query.TravelDate.Day, timeOfDay.Hours, timeOfDay.Minutes, 0);


                DateTimeOffset arrivalTime = item.HoraChegada;
                TimeSpan timeOfDayArrival = arrivalTime.TimeOfDay;
                trip.ArrivalTime = new DateTime(query.TravelDate.Year, query.TravelDate.Month, query.TravelDate.Day, timeOfDayArrival.Hours, timeOfDayArrival.Minutes, 0).AddDays(item.DiaChegada);

                trip.Class = new TripClass();
                trip.Class.Id = trip.Id + "-" + item.ClasseServico;
                trip.Class.Name = item.ClasseServico;

                trip.Company = new Company();
                trip.Company.Id = item.EmpresaId.ToString();
                trip.Company.Name = item.NomeEmpresa;

                trip.Origin = query.Origin.AsTrip();
                trip.Destination = query.Destination.AsTrip();

                trip.Station = new TripStation();

                trip.IsBpe = item.Bpe;
                trip.AvailableSeats = item.AssentosLivres;

                trip.Connections = item.TrechosConexao.Select(x => x.AsConnection(trip)).ToList();

                trip.Display = $"📍 Origem:  {query.Origin.Name} \n" +
                                      $"🏁 Destino: {query.Destination.Name} \n" +
                                      $"⏰ Saída: {trip.DepartureTime.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                                      $"🏁 Chegada: {trip.ArrivalTime.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                                      $"💰 R$ {trip.ValueAsDecimal.ToString("N2", new CultureInfo("pt-BR"))} \n" +
                                      $"💺 Assentos Livres: {item.AssentosLivres} \n" +
                                      $"🚌 Classe: {trip.Class.Name} \n" +
                                      $"⚙️ Empresa: {trip.Company.Name}";

                trip.Key = KeyGenerator.Generate(query.Origin.NormalizedName, query.Destination.NormalizedName, trip.Company.Name, trip.DepartureTime, trip.ArrivalTime, trip.Value);

                trips.Add(trip);
            }
        }
        

        return trips;
    }

    private static Connection AsConnection(this TrechosConexao conexao, Trip trip)
    {
        var conn = new Connection()
        {
            Id = conexao.Servico.ToString()
            ,
            DepartureTime = trip.DepartureTime
            ,
            ArrivalTime = trip.ArrivalTime
            ,
            Company = new Company() { Id = conexao.EmpresaId.ToString(), Name = conexao.NomeEmpresa }
            ,
            Class = new TripClass() { Name = conexao.ClasseServico }
            ,
            IsBpe = conexao.Bpe
            ,
            Origin = trip.Origin
            ,
            Destination = trip.Destination
        };

        return conn;
    }
}
