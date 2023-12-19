using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class BookAttribute:BasicAttribute
    {
        [JsonProperty("departure_time", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartureTime { get; set; }

        [JsonProperty("arrival_time", NullValueHandling = NullValueHandling.Ignore)]
        public string ArrivalTime { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public int Duration { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("zip_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ZipCode { get; set; }

        [JsonProperty("street_and_number", NullValueHandling = NullValueHandling.Ignore)]
        public string StreetAndNumber { get; set; }

        [JsonProperty("payment_method", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentMethod { get; set; }

        [JsonProperty("payment_token", NullValueHandling = NullValueHandling.Ignore)]
        public object PaymentToken { get; set; }

        [JsonProperty("payer_id", NullValueHandling = NullValueHandling.Ignore)]
        public object PayerId { get; set; }

        [JsonProperty("total_price", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPrice { get; set; }

        [JsonProperty("pax", NullValueHandling = NullValueHandling.Ignore)]
        public int Pax { get; set; }

        [JsonProperty("flight_number", NullValueHandling = NullValueHandling.Ignore)]
        public object FlightNumber { get; set; }

        [JsonProperty("distribusion_booking_number", NullValueHandling = NullValueHandling.Ignore)]
        public string DistribusionBookingNumber { get; set; }

        [JsonProperty("marketing_carrier_booking_number", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketingCarrierBookingNumber { get; set; }

        [JsonProperty("terms_accepted", NullValueHandling = NullValueHandling.Ignore)]
        public bool TermsAccepted { get; set; }

        [JsonProperty("send_customer_email", NullValueHandling = NullValueHandling.Ignore)]
        public bool SendCustomerEmail { get; set; }

        [JsonProperty("retailer_partner_number", NullValueHandling = NullValueHandling.Ignore)]
        public string RetailerPartnerNumber { get; set; }

        [JsonProperty("connection_reference", NullValueHandling = NullValueHandling.Ignore)]
        public object ConnectionReference { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedAt { get; set; }
    }
}
