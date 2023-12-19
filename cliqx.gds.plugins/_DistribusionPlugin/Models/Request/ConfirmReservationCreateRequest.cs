using System;
using System.Collections.Generic;
using distribusion.api.client.Models;
using Newtonsoft.Json;

namespace distribusion.api.client.Request
{
    public class ConfirmReservationCreateRequest
    {
        [JsonProperty("reservation_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ReservationId { get; set; }

        [JsonProperty("retailer_partner_number", NullValueHandling = NullValueHandling.Ignore)]
        public string RetailerPartnerNumber { get; set; }

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

        [JsonProperty("execute_payment", NullValueHandling = NullValueHandling.Ignore)]
        public bool ExecutePayment { get; set; }

        [JsonProperty("payment_method", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentMethod { get; set; }

        [JsonProperty("total_price", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPrice { get; set; }

        [JsonProperty("pax", NullValueHandling = NullValueHandling.Ignore)]
        public int Pax { get; set; }

        [JsonProperty("terms_accepted", NullValueHandling = NullValueHandling.Ignore)]
        public bool TermsAccepted { get; set; }

        [JsonProperty("locale", NullValueHandling = NullValueHandling.Ignore)]
        public string Locale { get; set; }

        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("send_customer_email", NullValueHandling = NullValueHandling.Ignore)]
        public bool SendCustomerEmail { get; set; }

        [JsonProperty("government_id", NullValueHandling = NullValueHandling.Ignore)]
        public string GovernmentId { get; set; }

        [JsonProperty("fare_class", NullValueHandling = NullValueHandling.Ignore)]
        public string FareClass { get; set; }

        [JsonProperty("passengers", NullValueHandling = NullValueHandling.Ignore)]
        public List<Passenger> Passengers { get; set; }
    }
}
