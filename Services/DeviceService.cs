
using System.Data;
using Models;
using Npgsql;
using Service;

namespace Service;

public class DeviceService : IDevicesService
{
    #region CreateTable
    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
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
    
    #region Droptable
    public static void Droptable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
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
    
    #region CreateDevice
    public bool CreateDevice(Device device)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"insert into device (type_id,brand_id,name,price,stock,created_at)
                                        values(@type_id,@brand_id,@name,@price,@stock,@created_at)";
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@type_id", device.TypeId);
                    cmd.Parameters.AddWithValue("@brand_id", device.BrandId);
                    cmd.Parameters.AddWithValue("@name", device.Name);
                    cmd.Parameters.AddWithValue("@price", device.Price);
                    cmd.Parameters.AddWithValue("@stock", device.Stock);
                    cmd.Parameters.AddWithValue("@created_at", device.CreatedAt);
                    
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

    #region UpdateDevice
    public bool UpdateDevice(Device device)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.ConnectionString))
                {
                    cmd.CommandText =
                        "update device set type_id =@type_id,brand_id =@brand_id,name=@name,pice=@price,stock=@stock,created_at=@created_at where id =@id";
                    cmd.Connection=connection;
                    
                    cmd.Parameters.AddWithValue("@type_id", device.TypeId);
                    cmd.Parameters.AddWithValue("@brand_id", device.BrandId);
                    cmd.Parameters.AddWithValue("@name", device.Name);
                    cmd.Parameters.AddWithValue("@price", device.Price);
                    cmd.Parameters.AddWithValue("@stock", device.Stock);
                    cmd.Parameters.AddWithValue("@created_at", device.CreatedAt);
                    
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

    #region DeleteDevice
    public bool DeleteDevice(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.ConnectionString, connection))
                {
                    cmd.CommandText = "DELETE FROM devices WHERE id=@id";
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

    #region GetDevices
    public List<Device> GetDevices() 
    {
        try
        {
            List<Device> devices = new();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.ConnectionString, connection))
                {
                    cmd.CommandText = "SELECT * FROM devices";
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Device device = new Device()
                            {
                                Id = reader.GetInt32(0),
                                TypeId = reader.GetInt32(1),
                                BrandId = reader.GetInt32(2),
                                Name = reader.GetString(3),
                                Price = reader.GetDecimal(4),
                                Stock = reader.GetInt32(5),
                                CreatedAt = reader.GetDateTime(6)
                            };
                            devices.Add(device);
                        }
                    }
                }
            }
            return devices;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion
    

    #region GetDeviceById
    public Device GetDeviceById(int id) 
    {
        try
        {
            Device device = new();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.ConnectionString, connection))
                {
                    cmd.CommandText = "SELECT * FROM devices WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            device.Id = reader.GetInt32(0);
                            device.TypeId= reader.GetInt32(1);
                            device.BrandId= reader.GetInt32(2);
                            device.Name = reader.GetString(3);
                            device.Price = reader.GetDecimal(4);
                            device.Stock = reader.GetInt32(5);
                            device.CreatedAt = reader.GetDateTime(6);
                        }
                    }
                }
            }
            return device;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion
    
}

file class SqlCommands
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=saburovmanu190907;";
    public const string ConnectionString = 
        "Server=localhost;Port=5432;Database=online_store;Username=postgres;Password=saburovmanu190907;";
    public const string CreateDatabase = "create database online_store";
    public const string DropDatabase = "drop database online_store";

    public const string CreateTable = @"create table if not exists users(
                                        id serial primary key,
                                        age int,
                                        username varchar(255) unique not null,
                                        password varchar(255) unique not null
                                        email varchar(255) unique not null)";

    public const string DropTable = "drop table if exists users";
    public const string InsertUser = @"insert into users (id, age, username, password)
                                     values(@age, @username, @password, @email);";
    public const string UpdateUser = @"update users set age = @age, username = @username, password = @password, email = @email where id = @id";
    public const string DeleteUser = "Delete from users where id = @id";
    public const string ReadUsers = "Select * from users";
    public const string ReadUserByid = "Select * from users where id = @id";
}