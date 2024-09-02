using Models;

namespace Service;

public interface IPaymentService
{
    List<Payment> Getpayments();
    Payment GetPaymentbyId(int id);
    bool CreatePayment(Payment payment);
    bool UpdatePayment(Payment payment);
    bool DeletePayment(int id);
}