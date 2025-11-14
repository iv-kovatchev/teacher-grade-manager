using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;
using Microsoft.Data.Sqlite;

namespace TeacherGradeManager.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly string _connectionString;

        public StudentRepository(string databasePath = "teacher_grade_manager.db")
        {
            _connectionString = $"Data Source={databasePath}";
            InitiliazeDatabase();
        }

        public List<Student> GetAll()
        {
            var students = new List<Student>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, FirstName, LastName, FacultyNumber FROM Students ORDER BY Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                FacultyNumber = reader.GetString(3)
                            });
                        }
                    }
                }
            }

            return students;
        }

        public Student GetById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, FirstName, LastName, FacultyNumber FROM Students WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                FacultyNumber = reader.GetString(3)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void Add(Student student)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                INSERT INTO Students (FirstName, LastName, FacultyNumber)
                VALUES (@FirstName, @LastName, @FacultyNumber);
                SELECT last_insert_rowid();";


                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@FacultyNumber", student.FacultyNumber);

                var result = command.ExecuteScalar();
                student.Id = Convert.ToInt32(result);
            }
        }

        public void Update(Student student)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string updateQuery = @"
                    UPDATE Students 
                    SET FirstName = @FirstName, 
                        LastName = @LastName, 
                        FacultyNumber = @FacultyNumber 
                    WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = updateQuery;
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@FacultyNumber", student.FacultyNumber);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Students WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InitiliazeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var createTableCommand = connection.CreateCommand();

                createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Students (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    FacultyNumber TEXT NOT NULL UNIQUE
                );";

                createTableCommand.ExecuteNonQuery();
            }
        }
    }
}
