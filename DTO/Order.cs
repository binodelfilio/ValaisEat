using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Order
    {
        public const int IN_PROGRESS = 0;
        public const int PENDING = 1;
        public const int TO_DELIVERY = 2;
        public const int DELIVERED = 3;
        public const int UNABLE_TO_DELIVER = 4;

        public int IdOrder { get; set; }
        public int Status { get; set; }
        public int IdCustomer { get; set; }
        public int IdStaff { get; set; }
        public DateTime DatetimeCreated { get; set; }
        public Nullable<DateTime> DatetimeDelivered { get; set; }
        public Nullable<DateTime> DatetimeConfirmed { get; set; }
        public int NbrDish { get; set; }
        public int TotalPrice { get; set; }
        public int TimeToDelivery { get; set; }
        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "En cours";
                        break;
                    case 1:
                        return "Validée";
                        break;
                    case 2:
                        return "A livrer";
                        break;
                    case 3:
                        return "Livrée";
                        break;
                    case 4:
                        return "Pas de livreur disponible";
                        break;
                    default:
                        return "";
                        break;
                };
                
            }
            set { }
        }

    }
}
