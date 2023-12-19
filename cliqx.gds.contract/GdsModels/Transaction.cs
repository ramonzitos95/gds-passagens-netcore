using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.contract.GdsModels
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string LocatorId { get; set; }
        public DateTime TransactionExpiresAt { get; set; }

        public string ReservationId { get; set; }
        public DateTime? ReservationExpiresAt { get; set; }

        public long ReservationTotalPrice { get; set; }

        public decimal ReservationTotalPriceAsDecimal => ReservationTotalPrice / 100M;

        ///Número da reserva para pegar condições de cancelamento
        public string BookingNumber { get; set; }
        public decimal CancelationFee { get; set; } = 0;
        public decimal CancelationTotalOrder { get; set; } = 0;
        public decimal CancelationRefund { get; set; } = 0;
        public string CancelationCreatedAt { get; set; } = string.Empty;
    }
}