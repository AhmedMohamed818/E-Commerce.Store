﻿namespace Domain.Models.OrderModels
{
    public enum OrderPaymentStatus
    {
        Pending = 0,
        PaymentReceived = 1,
        PaymentFailed = 2,
        PaymentCanceled = 3,


    }
}