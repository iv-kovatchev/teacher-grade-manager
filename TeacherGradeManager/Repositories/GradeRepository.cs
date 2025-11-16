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
    internal class GradeRepository : IRepository<Grade>
    {
        private readonly string _connectionString;

        public GradeRepository(string databasePath = "teachers_management.db")
        {
            _connectionString = $"Data Source={databasePath}";
            InitializeDatabase();
        }

        public List<Grade> GetAll()
        {
            var grades = new List<Grade>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, StudentId, CourseId, GradeValue FROM Grades ORDER BY StudentId, CourseId";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            grades.Add(new Grade
                            {
                                Id = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                CourseId = reader.GetInt32(2),
                                GradeValue = reader.GetDecimal(3)
                            });
                        }
                    }
                }
            }

            return grades;
        }

        public Grade? GetById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, StudentId, CourseId, GradeValue FROM Grades WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Grade
                            {
                                Id = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                CourseId = reader.GetInt32(2),
                                GradeValue = reader.GetDecimal(3)
                            };
                        }
                    }
                }
            }

            return null;
        
        }

        public void Add(Grade grade)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"
                INSERT INTO Grades (StudentId, CourseId, GradeValue)
                VALUES (@StudentId, @CourseId, @GradeValue);
                SELECT last_insert_rowid();";

                command.Parameters.AddWithValue("@StudentId", grade.StudentId);
                command.Parameters.AddWithValue("@CourseId", grade.CourseId);
                command.Parameters.AddWithValue("@GradeValue", grade.GradeValue);

                var result = command.ExecuteScalar();
                grade.Id = Convert.ToInt32(result);
            }
        }

        public void Update(Grade grade)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string updateQuery = @"
                    UPDATE Grades
                    SET StudentId = @StudentId,
                        CourseId = @CourseId,
                        GradeValue = @GradeValue
                    WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = updateQuery;

                    command.Parameters.AddWithValue("@Id", grade.Id);
                    command.Parameters.AddWithValue("@StudentId", grade.StudentId);
                    command.Parameters.AddWithValue("@CourseId", grade.CourseId);
                    command.Parameters.AddWithValue("@GradeValue", grade.GradeValue);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Grades WHERE Id = @Id";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Grade> GetGradesByStudent(int studentId)
        {
            var grades = new List<Grade>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, StudentId, CourseId, GradeValue FROM Grades WHERE StudentId = @StudentId ORDER BY CourseId";
                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            grades.Add(new Grade
                            {
                                Id = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                CourseId = reader.GetInt32(2),
                                GradeValue = reader.GetDecimal(3)
                            });
                        }
                    }
                }
            }

            return grades;
        }

        public List<Grade> GetGradesByCourse(int courseId)
        {
            var grades = new List<Grade>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                
                string selectQuery = "SELECT Id, StudentId, CourseId, GradeValue FROM Grades WHERE CourseId = @CourseId ORDER BY StudentId";
                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@CourseId", courseId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            grades.Add(new Grade
                            {
                                Id = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                CourseId = reader.GetInt32(2),
                                GradeValue = reader.GetDecimal(3)
                            });
                        }
                    }
                }
            }

            return grades;
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var createTableCommand = connection.CreateCommand();

                createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Grades (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentId INTEGER NOT NULL,
                    CourseId INTEGER NOT NULL,
                    GradeValue REAL NOT NULL,
                    FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE CASCADE,
                    FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE,
                    UNIQUE(StudentId, CourseId)
                );";


                createTableCommand.ExecuteNonQuery();
            }
        }
    }
}
