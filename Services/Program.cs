using Models;
using Service;
using Services;

//----------------------------------------TABLE USER--------------------------------//
UserService.CreateDatabase();

UserService userservice = new UserService();

User newUser = new User()
{
    Age = 30,
    UserName = "yusuff",
    Password = "yuusuff",
    Email = "yusuff@gmail.com"
};
bool usercreated = userservice.CreateUser(newUser);
if (usercreated == true)
{
    Console.WriteLine("User created successfully");
}
else
{
    Console.WriteLine("User not created successfully");
}

List<User> users = userservice.GetUsers();
foreach (var user in users)
{
    Console.WriteLine($"Id: {user.Id},Name : {user.UserName},Email : {user.Email}");
}

User userbyid = userservice.GetUserById(1);
if (userbyid == null)
{
    Console.WriteLine("User not found");
}
else
{
    Console.WriteLine($"user id is: {userbyid.Id},Name : {userbyid.UserName},Age :{userbyid.Age} ,Email : {userbyid.Email}");
}

if (userbyid == null)
{
    Console.WriteLine("It is not possible to update");
}
else
{
    userbyid.Age = 31;
    bool userupdate = userservice.UpdateUser(userbyid);
    Console.WriteLine("User updated successfully");
}

bool userdeleted = userservice.DeleteUser(1);
if (userdeleted == true)
{
    Console.WriteLine("User deleted successfully");
}
else
{
    Console.WriteLine("user not found");
}

UserService.Droptable();

//----------------------------------------TABLE DEVICE-----------------------------------//

DeviceService.CreateTable();

DeviceService deviceService = new DeviceService();

Device device = new Device()
{
    TypeId = 1,
    BrandId = 1,
    Name  = "Phone",
    Price = 6999.99m,
    Stock = 2,
    CreatedAt = DateTime.Now
};

bool devicecreeated = deviceService.CreateDevice(device);
if (devicecreeated==true)
{
    Console.WriteLine("Device created successfully");
}
else
{
    Console.WriteLine("device not found");
}

List<Device> devices = deviceService.GetDevices();
foreach (var devicee in devices)
{
    Console.WriteLine($"Id: {devicee.Id}, Name: {devicee.Name}, Price: {devicee.Price}, Stock: {devicee.Stock}, CreatedAt: {devicee.CreatedAt}");
}

Device devicebyid = deviceService.GetDeviceById(1);
if (devicebyid!=null)
{
    Console.WriteLine($"By Id: {devicebyid.Id}, Name: {devicebyid.Name}, Price: {devicebyid.Price}, Stock: {devicebyid.Stock}, CreatedAt: {devicebyid.CreatedAt}");
}
else
{
    Console.WriteLine("Device not found");
}

if (devicebyid!=null)
{
    devicebyid.Stock = 5;
    bool deviceupdate = deviceService.UpdateDevice(devicebyid);
    if (deviceupdate==true)
    {
        Console.WriteLine("Updated device");
    }
    else
    {
        Console.WriteLine("Device not updated");
    }
}

bool devicedelete = deviceService.DeleteDevice(1);
if (devicedelete==true)
{
    Console.WriteLine("Deleted device");
}
else
{
    Console.WriteLine("Device not found");
}

DeviceService.Droptable();

//------------------------------------------TABLE ORDER---------------------------------//

OrderService.CreateTable();

OrderService orderservice = new OrderService();

Order order = new Order()
{
    UserId = 1,
    OrderDate = DateTime.Now,
    Status = "In line",
    TotalAmount = 299.99m,
    Discount = null,
};

bool ordercreated = orderservice.CreateOrder(order);
if (ordercreated==true)
{
    Console.WriteLine("Order created");
}
else
{
    Console.WriteLine("Order not created");
}
List<Order> orders = orderservice.GetOrders();
foreach (var oreder in orders)
{
    Console.WriteLine($"Id :{order.Id}, User id:{order.UserId},Order date :{order.OrderDate}, Status: {order.Status}, Total amount :{order.TotalAmount}, Discount :{order.Discount}");
}

Order orderbyid = orderservice.GetOrderById(1);
if (orderbyid!=null)
{
    Console.WriteLine($"Order Id :{orderbyid.Id},Iser id :{orderbyid.UserId},Order date :{orderbyid.OrderDate}, Status: {orderbyid.Status}, Total amount :{orderbyid.TotalAmount}, Discount :{orderbyid.Discount}");
}
else
{
    Console.WriteLine("Order not found");
}

if (orderbyid != null)
{
    orderbyid.Status = "Completed"; 
    bool orderupdate=orderservice.UpdateOrder(orderbyid);
    if (orderupdate==true)
    {
        Console.WriteLine("Order updated");
    }
    else
    {
        Console.WriteLine("Order not updated");
    }
}

bool orderdelete = orderservice.DeleteOrder(1);
if (orderdelete==true)
{
    Console.WriteLine("Order deleted");
}
else
{
    Console.WriteLine("Order not deleted");
}

OrderService.DropTable();

//------------------------------------------TABLE PAYMENT------------------------------------------//

PaymentService.CreateTable();

PaymentService paymentservice = new PaymentService();

Payment payment = new Payment()
{
    OrderId = 1,
    Amount = 499.99m,
    PaymentDate = DateTime.Now,
    PaymentMethod = "Credit Card"
};

bool paymentcreated = paymentservice.CreatePayment(payment);
if (paymentcreated==true)
{
    Console.WriteLine("Payment created");
}
else
{
    Console.WriteLine("Payment not created");
}

List<Payment> payments = paymentservice.Getpayments();
foreach (var paymentt in payments)
{
    Console.WriteLine($"Id :{paymentt.Id},Order id :{paymentt.OrderId},Amount :{paymentt.Amount},Payment method :{paymentt.PaymentMethod},Payment date :{paymentt.PaymentDate}");
}

Payment paymentbyid = paymentservice.GetPaymentbyId(1);
if (paymentbyid != null)
{
    Console.WriteLine($"Payment by id : {paymentbyid.Id},Order id : {paymentbyid.OrderId},Amount : {paymentbyid.Amount},Payment method : {paymentbyid.PaymentMethod},Payment date : {paymentbyid.PaymentDate}");
}
else
{
    Console.WriteLine("Payment not found");
}

if (paymentbyid != null)
{
    paymentbyid.PaymentMethod = "Visa";
    bool paymentupdate = paymentservice.UpdatePayment(paymentbyid);
    if (paymentupdate == true)
    {
        Console.WriteLine("Payment updated");
    }
    else
    {
        Console.WriteLine("Payment not updated");
    }
}
bool paymentdelete = paymentservice.DeletePayment(1);
if (paymentdelete == true)
{
    Console.WriteLine("Payment deleted");
}
else
{
    Console.WriteLine("Payment not deleted");
}

PaymentService.DropTable();
UserService.DropDatabase();

//------THE END--------//


