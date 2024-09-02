using System.Data;
using Models;
using Npgsql;
using Service;

namespace Service;

class UserService : IUserService
{
    #region CreateDatabase
    public static void CreateDatabase()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.DefaultConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.CreateDatabase,connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion

    #region DropDatabase

    public static void DropDatabase()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.DefaultConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.DropDatabase, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion

    #region Createtable
    public static void Createtable()
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
    
    #region GetUsers
    public List<User> GetUsers()
    {
        try
        {
            List<User> users = new();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = SqlCommands.ReadUsers;
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = reader.GetInt32(0);
                            user.Age = reader.GetInt32(1);
                            user.UserName = reader.GetString(2);
                            user.Password = reader.GetString(3);
                            user.Email = reader.GetString(4);
                            
                            users.Add(user);
                        }
                    }
                }
                return users;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region GetUserById
    public User GetUserById(int id)
    {
        try
        {
            User user = new User();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = SqlCommands.ReadUserByid;
                    cmd.Parameters.AddWithValue("id", id);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = reader.GetInt32(0);
                            user.Age = reader.GetInt32(1);
                            user.UserName = reader.GetString(2);
                            user.Password = reader.GetString(3);
                            user.Email = reader.GetString(4);
                        }
                        return user;
                    }
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
    
    #region CreateUser

    public bool CreateUser(User user)
        {
            try
            {
                int res = 0;
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.CommandText = SqlCommands.InsertUser;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@age", user.Age);
                        command.Parameters.AddWithValue("@username", user.UserName);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@email", user.Email);

                        res = command.ExecuteNonQuery();
                    }
                }
                return res > 0;
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    #endregion

    #region UpdateUser
    public bool UpdateUser(User user)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommands.UpdateUser;
                    
                    command.Parameters.AddWithValue("@age", user.Age);
                    command.Parameters.AddWithValue("@username", user.UserName);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@email", user.Email);
                    
                    int res = command.ExecuteNonQuery();
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

    #region DeleteUser

    public bool DeleteUser(int id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = SqlCommands.DeleteUser;
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