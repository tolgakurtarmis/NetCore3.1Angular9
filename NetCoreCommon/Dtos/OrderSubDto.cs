﻿namespace NetCoreCommon.Dtos
{
    public class OrderSubDto
    {
        public int? OrderId { get; set; }
        public string OrderNo { get; set; }
        public decimal? GrandTotal { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }


    }
}