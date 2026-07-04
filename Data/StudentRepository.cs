using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sem_project.Models;

namespace Sem_project.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing connection string: DefaultConnection");
        }

        public List<Student> GetAll()
        {
            var students = new List<Student>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"SELECT Student_ID, Name, Email, Phone, Dept_ID
                                 FROM Student
                                 ORDER BY Student_ID DESC";

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student
                {
                    Student_ID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    Phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    Dept_ID = reader.GetInt32(4)
                });
            }

            return students;
        }

        public Student? GetById(int studentId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"SELECT Student_ID, Name, Email, Phone, Dept_ID
                                 FROM Student
                                 WHERE Student_ID = @Student_ID";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Student_ID", studentId);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return new Student
            {
                Student_ID = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                Phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                Dept_ID = reader.GetInt32(4)
            };
        }

        public void Create(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"INSERT INTO Student (Name, Email, Phone, Dept_ID)
                                 VALUES (@Name, @Email, @Phone, @Dept_ID)";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(student.Email) ? DBNull.Value : student.Email);
            command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(student.Phone) ? DBNull.Value : student.Phone);
            command.Parameters.AddWithValue("@Dept_ID", student.Dept_ID);

            command.ExecuteNonQuery();
        }

        public void Update(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"UPDATE Student
                                 SET Name = @Name,
                                     Email = @Email,
                                     Phone = @Phone,
                                     Dept_ID = @Dept_ID
                                 WHERE Student_ID = @Student_ID";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(student.Email) ? DBNull.Value : student.Email);
            command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(student.Phone) ? DBNull.Value : student.Phone);
            command.Parameters.AddWithValue("@Dept_ID", student.Dept_ID);
            command.Parameters.AddWithValue("@Student_ID", student.Student_ID);

            command.ExecuteNonQuery();
        }

        public void Delete(int studentId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"DELETE FROM Student
                                 WHERE Student_ID = @Student_ID";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Student_ID", studentId);

            command.ExecuteNonQuery();
        }
    }
}
