﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class OrdersVm
    {
        public string? UserName { get; set; }

        public decimal TotalPrice { get; set; }


        //BillingAddress

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? EmailAddress { get; set; }

        public string? AddressLine { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

        //Payment

        public string? CardName { get; set; }

        public string? CardNumber { get; set; }

        public string? Expiration { get; set; }

        public string? CVV { get; set; }

        public int Paymethod { get; set; }
    }
}
