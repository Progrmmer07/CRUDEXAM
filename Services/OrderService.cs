using System;
using System.Collections.Generic;
using Models;
using Npgsql;

namespace Service;

public class OrderService : IOrderService
{
    private const string ConnectionString =
        "Server=localhost;Port=5432;Database=online_store;Username=postgres;Password=saburovmanu190907;";

    #region CreateTable

    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.CreateTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region DropTable

    public static void DropTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.DropTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region CreateOrder

    public Order GetOrderbyId(int id)
    {
        throw new NotImplementedException();
    }

    public bool CreateOrder(Order order)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"insert into orders (user_id, order_date, status, total_amount, discount_id)
                                        values(@user_id, @order_date, @status, @total_amount, @discount_id)";
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@user_id", order.UserId);
                    cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
                    cmd.Parameters.AddWithValue("@status", order.Status);
                    cmd.Parameters.AddWithValue("@total_amount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@discount_id", order.Discount);

                    int res = cmd.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region UpdateOrder

    public bool UpdateOrder(Order order)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText =
                        "Update orders Set user_id = @user_id, order_date = @order_date, status = @status, total_amount = @total_amount, discount_id = @discount_id where id = @id";
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@id", order.Id);
                    cmd.Parameters.AddWithValue("@user_id", order.UserId);
                    cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
                    cmd.Parameters.AddWithValue("@status", order.Status);
                    cmd.Parameters.AddWithValue("@total_amount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@discount_id", order.Discount);

                    int res = cmd.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region DeleteOrder

    public bool DeleteOrder(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = "DELETE FROM orders WHERE id = @id";
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@id", id);
                    int res = cmd.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region GetOrders

    public List<Order> GetOrders()
    {
        try
        {
            List<Order> orders = new List<Order>();
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM orders";
                    cmd.Connection = connection;
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order()
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                OrderDate = reader.GetDateTime(2),
                                Status = reader.GetString(3),
                                TotalAmount = reader.GetDecimal(4),
                                Discount = reader.GetInt32(5),
                            };
                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region GetOrderById

    public Order GetOrderById(int id)
    {
        try
        {
            Order order = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM orders WHERE id = @id";
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order = new Order()
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                OrderDate = reader.GetDateTime(2),
                                Status = reader.GetString(3),
                                TotalAmount = reader.GetDecimal(4),
                                Discount = reader.GetInt32(5),
                            };
                        }
                    }
                }
            }

            return order;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion
}

file static class SqlCommands
{
    public const string CreateTable = @"create table if not exists orders(
                                        id serial primary key,
                                        user_id int not null,
                                        order_date timestamp not null,
                                        status varchar(255) not null,
                                        total_amount decimal not null,
                                        discount_id int not null)";

    public const string DropTable = "drop table if exists orders";
}