
using System.Globalization;
using System.Reflection;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.plugins._RjConsultoresPlugin.Models;
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Response;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using cliqx.gds.plugins.Util;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Extensions;

public static class TripExtensions
{
    public static List<Trip> AsTrip(this Corrida consultaRespons, TripQuery query)
    {
        var trips = new List<Trip>();

        if (consultaRespons != null)
        {
            foreach (var item in consultaRespons.LsServicos)
            {
                var trip = new Trip
                {
                    Id = item.ServicoId,
                    Value = Convert.ToInt64(item.Preco * 100),
                    Distance = item.Km.ToString()
                };

                DateTimeOffset departureTime = item.Saida;

                TimeSpan timeOfDay = departureTime.TimeOfDay;
                trip.DepartureTime = new DateTime(query.TravelDate.Year, query.TravelDate.Month, query.TravelDate.Day, timeOfDay.Hours, timeOfDay.Minutes, 0);


                DateTimeOffset arrivalTime = item.Chegada;
                TimeSpan timeOfDayArrival = arrivalTime.TimeOfDay;
                trip.ArrivalTime = new DateTime(item.Chegada.Year, item.Chegada.Month, item.Chegada.Day, timeOfDayArrival.Hours, timeOfDayArrival.Minutes, 0);

                trip.Class = new TripClass();
                trip.Class.Id = trip.Id + "-" + item.Classe;
                trip.Class.Name = item.Classe;

                trip.Company = new Company();
                trip.Company.Id = item.EmpresaId.ToString();
                trip.Company.Name = item.Empresa;

                trip.Origin = query.Origin.AsTrip();
                trip.Destination = query.Destination.AsTrip();

                trip.Station = new TripStation();

                trip.IsBpe = item.Bpe;

                trip.AvailableSeats = item.PoltronasLivres;

                if (item.Conexao != null)
                {
                    var conn = TratarConexao((Dictionary<string, object>)item.Conexao);
                    trip.Connections = conn.Select(x => x.AsConnection()).ToList();
                }

                trip.Display =
                    $"📍 Origem:  {query.Origin.Name} \n" +
                    $"🏁 Destino: {query.Destination.Name} \n" +
                    $"⏰ Saída: {trip.DepartureTime.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                    $"🏁 Chegada: {trip.ArrivalTime.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                    $"💰 R$ {trip.ValueAsDecimal.ToString("N2", new CultureInfo("pt-BR"))} \n" +
                    $"💺 Assentos Livres: {trip.AvailableSeats} \n" +
                    $"🚌 Classe: {trip.Class.Name} \n" +
                    $"⚙️ Empresa: {trip.Company.Name} {(trip.TotalConnections > 0 ? "\n" : "")}";

                trip.Display = trip.Display +
                    $"* +{trip.TotalConnections} Conexão *";

                trip.Key = KeyGenerator.Generate(query.Origin.NormalizedName, query.Destination.NormalizedName, trip.Company.Name, trip.DepartureTime, trip.ArrivalTime, trip.Value);

                trips.Add(trip);
            }
        }


        return trips;
    }

    private static List<Conexao> TratarConexao(Dictionary<string, object> obj)
    {

        var conexoes = new List<Conexao>();
        var servico = new Conexao();

        Conexao c1 = new Conexao();
        Conexao c2 = new Conexao();
        Conexao c3 = new Conexao();
        Conexao c4 = new Conexao();
        Conexao c5 = new Conexao();

        foreach (KeyValuePair<string, object> attribute in obj)
        {

            if (ContainsFirst(attribute.Key, "primeiroTrecho"))
            {
                var name = attribute.Key.Replace("primeiroTrecho", "");

                PropertyInfo field = c1.GetType().GetProperty(name);

                if (field != null)
                    field.SetValue(c1, attribute.Value);

            }
            else if (ContainsFirst(attribute.Key, "segundoTrecho"))
            {
                var name = attribute.Key.Replace("segundoTrecho", "");

                PropertyInfo field = c2.GetType().GetProperty(name);

                if (field != null)
                    field.SetValue(c2, attribute.Value);

            }
            else if (ContainsFirst(attribute.Key, "terceiroTrecho"))
            {
                var name = attribute.Key.Replace("terceiroTrecho", "");

                PropertyInfo field = c3.GetType().GetProperty(name);

                if (field != null)
                    field.SetValue(c3, attribute.Value);

            }
            else if (ContainsFirst(attribute.Key, "quartoTrecho"))
            {
                var name = attribute.Key.Replace("quartoTrecho", "");

                PropertyInfo field = c4.GetType().GetProperty(name);

                if (field != null)
                    field.SetValue(c4, attribute.Value);

            }
            else if (ContainsFirst(attribute.Key, "quintoTrecho"))
            {
                var name = attribute.Key.Replace("quintoTrecho", "");

                PropertyInfo field = c5.GetType().GetProperty(name);

                if (field != null)
                    field.SetValue(c5, attribute.Value);

            }
            else
            {
                var name = attribute.Key;

                name = char.ToUpper(name[0]) + name.Substring(1);

                PropertyInfo field = servico.GetType().GetProperty(name);

                if (field != null)
                    field.SetValue(servico, attribute.Value);

            }
        }

        if (c1.Vende)
            conexoes.Add(c1);
        if (c2.Vende)
            conexoes.Add(c2);
        if (c3.Vende)
            conexoes.Add(c3);
        if (c4.Vende)
            conexoes.Add(c4);
        if (c5.Vende)
            conexoes.Add(c5);


        return conexoes;
    }

    private static bool ContainsFirst(string obj, string key)
    {

        if (obj.Contains(key))
        {
            return true;
        }

        return false;
    }

    private static Connection AsConnection(this Conexao conexao)
    {
        string dataSaida = conexao.DataSaida;
        string horaSaida = conexao.HoraSaida;

        string dataChegada = conexao.DataChegada;
        string horaChegada = conexao.HoraChegada;

        string formatoDataHora = "dd/MM/yyyy HH:mm";

        DateTime dataHoraSaida;
        DateTime dataHoraChegada;

        DateTime.TryParseExact($"{dataSaida} {horaSaida}", formatoDataHora, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataHoraSaida);
        DateTime.TryParseExact($"{dataChegada} {horaChegada}", formatoDataHora, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataHoraChegada);


        var conn = new Connection()
        {
            Id = conexao.Servico.ToString(),
            DepartureTime = dataHoraSaida,
            ArrivalTime = dataHoraChegada,
            Company = new Company() { Id = conexao.EmpresaId.ToString(), Name = conexao.Empresa },
            Class = new TripClass() { Name = conexao.Classe },
            IsBpe = conexao.IsBpe,
            Origin = new TripOriginDestination()
            {
                CityId = conexao.Origem.ToString(),
                CityName = conexao.OrigemDescricao
            },
            Destination = new TripOriginDestination()
            {
                CityId = conexao.Destino.ToString(),
                CityName = conexao.DestinoDescricao
            },
            AvailableSeats = conexao.PoltronasLivres,
            Value = Convert.ToInt64(double.Parse(conexao.Preco) * 100)
        };

        return conn;
    }
}
