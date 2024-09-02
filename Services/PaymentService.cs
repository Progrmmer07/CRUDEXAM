using System;
using System.Collections.Generic;
using Models;
using Npgsql;
using Service;

namespace Services;

public class PaymentService : IPaymentService
{
    private const string DefaultConnectionString =
        "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=saburovmanu190907;";

    private const string ConnectionString =
        "Server=localhost;Port=5432;Database=user_db;Username=postgres;Password=saburovmanu190907;";

    #region CreateTable

    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    CREATE TABLE IF NOT EXISTS payments (
                        id SERIAL PRIMARY KEY, 
                        order_id INT REFERENCES orders(id),
                        amount DECIMAL(10, 2) NOT NULL,
                        payment_date DATE,
                        payment_method VARCHAR(50) NOT NULL
                    )", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
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
                using (NpgsqlCommand cmd = new NpgsqlCommand("DROP TABLE IF EXISTS payments", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion

    #region GetPayments

    public List<Payment> Getpayments()
    {
        try
        {
            List<Payment> payments = new List<Payment>();
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM payments";

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Payment payment = new Payment
                            {
                                Id = reader.GetInt32(0),
                                OrderId = reader.GetInt32(1),
                                Amount = reader.GetDecimal(2),
                                PaymentDate = reader.GetDateTime(3),
                                PaymentMethod = reader.GetString(4)
                            };
                            payments.Add(payment);
                        }
                    }
                }
            }

            return payments;
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion

    #region GetPaymentById

    public Payment GetPaymentbyId(int id)
    {
        try
        {
            Payment payment = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM payments WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            payment = new Payment
                            {
                                Id = reader.GetInt32(0),
                                OrderId = reader.GetInt32(1),
                                Amount = reader.GetDecimal(2),
                                PaymentDate = reader.GetDateTime(3),
                                PaymentMethod = reader.GetString(4)
                            };
                        }
                    }
                }
            }

            return payment;
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion
    
    #region CreatePayment
    public bool CreatePayment(Payment payment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText =
                        "INSERT INTO payments (order_id, amount, payment_date, payment_method) VALUES (@order_id, @amount, @payment_date, @payment_method)";
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@order_id", payment.OrderId);
                    command.Parameters.AddWithValue("@amount", payment.Amount);
                    command.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
                    command.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);

                    int res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    #endregion

    #region UpdatePayment

    public bool UpdatePayment(Payment payment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText =
                        "UPDATE payments SET order_id = @order_id, amount = @amount, payment_date = @payment_date, payment_method = @payment_method WHERE id = @id";

                    command.Parameters.AddWithValue("@id", payment.Id);
                    command.Parameters.AddWithValue("@order_id", payment.OrderId);
                    command.Parameters.AddWithValue("@amount", payment.Amount);
                    command.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
                    command.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);

                    int res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    #endregion

    #region DeletePayment

    public bool DeletePayment(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM payments WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    int res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    #endregion
}