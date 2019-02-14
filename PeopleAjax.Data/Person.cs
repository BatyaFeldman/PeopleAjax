using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleAjax.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

    }

    public class PeopleDb
    {
        private string _connectionString;
        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person>GetAllPeople()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
                using(SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "select * from Persons";
                List<Person>results = new List<Person>();
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }
                return results;
            }
        }

        public void AddPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
                using(SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "insert into persons values(@firstName, @lastName, @age)";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "update persons set FirstName=@firstName, LastName=@lastName, Age=@age where id=@personId";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                cmd.Parameters.AddWithValue("@personId", person.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePerson(int personId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "delete from persons where id=@personId";
                cmd.Parameters.AddWithValue("@personId", personId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
