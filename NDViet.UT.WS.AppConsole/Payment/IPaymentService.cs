namespace NDViet.UT.WS.AppConsole.Payment
{
    public interface IPaymentService
    {
        PaymentResult Create(PaymentDto payment);
    }
}