using System;
using System.Collections.Generic;
using Eventmanagement.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Eventmanagement.Repositories
{
    public class EventRepository
    {
        private readonly string connectionString;

        public EventRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void Insert(Event item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
INSERT INTO Events (Name,Location,StartDate,EndDate) 
VALUES (@Name,@Location,@StartDate,@EndDate) 
";
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = item.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Location";
            parameter.Value = item.Location;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@StartDate";
            parameter.Value = item.StartDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@EndDate";
            parameter.Value = item.EndDate;
            command.Parameters.Add(parameter);


            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        private void Update(Event item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
UPDATE Events 
SET 
    Name=@Name, 
    Location=@Location, 
    StartDate=@StartDate,
    EndDate=@EndDate
WHERE Id=@Id
";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = item.Id;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = item.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Location";
            parameter.Value = item.Location;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@StartDate";
            parameter.Value = item.StartDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@EndDate";
            parameter.Value = item.EndDate;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public Event GetById(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                using (connection)
                {
                    IDbCommand command = connection.CreateCommand();
                    command.CommandText =
@"
SELECT * FROM Events
WHERE 
Id=@Id 
";

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@Id";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    IDataReader reader = command.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            Event ev = new Event();
                            ev.Id = (int)reader["Id"];
                            ev.Name = (string)reader["Name"];
                            ev.Location = (string)reader["Location"];
                            ev.StartDate = (DateTime)reader["StartDate"];
                            ev.EndDate = (DateTime)reader["EndDate"];


                            return ev;
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public List<Event> GetAll()
        {
            List<Event> resultSet = new List<Event>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"
SELECT * FROM Events 
";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new Event()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Location = (string)reader["Location"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"]
                        });
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return resultSet;
        }

        public void Delete(Event item)
        {
            IDbConnection connection = new SqlConnection(connectionString);


            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
DELETE FROM Events
WHERE Id=@Id
";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = item.Id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Save(Event item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}
