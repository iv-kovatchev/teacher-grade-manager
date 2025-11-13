using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;

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

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, FirstName, LastName, FacultyNumber FROM Students ORDER BY Id";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
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
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, FirstName, LastName, FacultyNumber FROM Students WHERE Id = @Id";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
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
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = @"
                INSERT INTO Students (FirstName, LastName, FacultyNumber)
                VALUES (@FirstName, @LastName, @FacultyNumber);
                SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@FacultyNumber", student.FacultyNumber);

                    student.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Update(Student student)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string updateQuery = @"
                    UPDATE Students 
                    SET FirstName = @FirstName, 
                        LastName = @LastName, 
                        FacultyNumber = @FacultyNumber 
                    WHERE Id = @Id";

                using (var command = new SQLiteCommand(updateQuery, connection))
                {
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
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Students WHERE Id = @Id";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InitiliazeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Students (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    FacultyNumber TEXT NOT NULL UNIQUE
                );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
