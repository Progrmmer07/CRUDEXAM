using Models;

namespace Service;

public interface IOrderService
{
    List<Order> GetOrders();
    Order GetOrderbyId(int id);
    bool CreateOrder(Order order);
    bool UpdateOrder(Order order);
    bool DeleteOrder(int id);
}

