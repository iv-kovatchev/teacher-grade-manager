using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherGradeManager.Models;

namespace TeacherGradeManager.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly string _connectionString;

        public CourseRepository(string databasePath = "teachers_management.db")
        {
            _connectionString = $"Data Source={databasePath}";
            InitiliazeDatabase();
        }

        public List<Course> GetAll()
        {
            var courses = new List<Course>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, Name, DayOfWeek, Time, [Type] FROM Courses ORDER BY DayOfWeek, Time";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                DayOfWeek = (DayOfWeek)reader.GetInt32(2),
                                Time = TimeSpan.Parse(reader.GetString(3)),
                                Type = (CourseType)reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return courses;
        }

        public Course? GetById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, Name, DayOfWeek, Time, [Type] FROM Courses WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Course
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                DayOfWeek = (DayOfWeek)reader.GetInt32(2),
                                Time = TimeSpan.Parse(reader.GetString(3)),
                                Type = (CourseType)reader.GetInt32(4)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void Add(Course course)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"
                INSERT INTO Courses (Name, DayOfWeek, Time, [Type])
                VALUES (@Name, @DayOfWeek, @Time, @Type);
                SELECT last_insert_rowid();";

                command.Parameters.AddWithValue("@Name", course.Name);
                command.Parameters.AddWithValue("@DayOfWeek", (int)course.DayOfWeek);
                command.Parameters.AddWithValue("@Time", course.Time.ToString());
                command.Parameters.AddWithValue("@Type", (int)course.Type);
                
                var result = command.ExecuteNonQuery();
                course.Id = Convert.ToInt32(result);
            }
        }

        public void Update(Course course)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string updateQuery = @"
                    UPDATE Courses
                    SET Name = @Name,
                        DayOfWeek = @DayOfWeek,
                        Time = @Time
                        [Type] = @Type
                    WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = updateQuery;

                    command.Parameters.AddWithValue("@Id", course.Id);
                    command.Parameters.AddWithValue("@Name", course.Name);
                    command.Parameters.AddWithValue("@DayOfWeek", (int)course.DayOfWeek);
                    command.Parameters.AddWithValue("@Time", course.Time.ToString(@"hh\:mm"));
                    command.Parameters.AddWithValue("@Type", (int)course.Type);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Courses WHERE Id = @Id";

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
                CREATE TABLE IF NOT EXISTS Courses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE,
                    DayOfWeek INTEGER NOT NULL,
                    Time TEXT NOT NULL,
                    [Type] INTEGER NOT NULL
                );";

                createTableCommand.ExecuteNonQuery();
            }

        }
    }
}
