using Dapper;
using MediatorAngularDapper.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        void Delete(int id);
        User Get(int id);
        IEnumerable<User> GetUsers();
        void Update(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserRepository(string conn)
        {
            connectionString = conn;
        }
        public IEnumerable<User> GetUsers()
        {
            using var db = new SqlConnection(connectionString);

            return db.Query<User>("SELECT * FROM Users");//.AsEnumerable();
        }

        public User Get(int id)
        {
            using var db = new SqlConnection(connectionString);

            return db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
        }

        public void Create(User user)
        {
            using var db = new SqlConnection(connectionString);
            const string sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age)";
            db.Execute(sqlQuery, user);

            // если мы хотим получить id добавленного пользователя
            //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
            //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
            //user.Id = userId.Value;
        }

        public void Update(User user)
        {
            using var db = new SqlConnection(connectionString);
            const string sqlQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
            db.Execute(sqlQuery, user);
        }

        public void Delete(int id)
        {
            using var db = new SqlConnection(connectionString);
            const string sqlQuery = "DELETE FROM Users WHERE Id = @id";
            db.Execute(sqlQuery, new { id });
        }
    }
}
