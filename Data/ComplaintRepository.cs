using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sem_project.Models;

namespace Sem_project.Data
{
    public class ComplaintRepository
    {
        private readonly string _connectionString;

        public ComplaintRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing connection string: DefaultConnection");
        }

        public List<Complaint> GetAll()
        {
            var complaints = new List<Complaint>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"SELECT c.Complaint_ID, c.Description, c.Date, c.Status,
                                          s.Name AS StudentName,
                                          cc.Category_Name
                                   FROM Complaint c
                                   JOIN Student s ON c.Student_ID = s.Student_ID
                                   JOIN ComplaintCategory cc ON c.Category_ID = cc.Category_ID
                                   ORDER BY c.Date DESC";

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                complaints.Add(new Complaint
                {
                    Complaint_ID = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    Date = reader.GetDateTime(2),
                    Status = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    StudentName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    CategoryName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                });
            }

            return complaints;
        }

        public Complaint? GetById(int complaintId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"SELECT Complaint_ID, Description, Date, Status, Student_ID, Category_ID
                                 FROM Complaint
                                 WHERE Complaint_ID = @Complaint_ID";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Complaint_ID", complaintId);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new Complaint
            {
                Complaint_ID = reader.GetInt32(0),
                Description = reader.GetString(1),
                Date = reader.GetDateTime(2),
                Status = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                Student_ID = reader.GetInt32(4),
                Category_ID = reader.GetInt32(5)
            };
        }

        public void Create(Complaint complaint)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"INSERT INTO Complaint (Description, Date, Status, Student_ID, Category_ID)
                                 VALUES (@Description, @Date, @Status, @Student_ID, @Category_ID)";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Description", complaint.Description);
            command.Parameters.AddWithValue("@Date", complaint.Date);
            command.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(complaint.Status) ? DBNull.Value : complaint.Status);
            command.Parameters.AddWithValue("@Student_ID", complaint.Student_ID);
            command.Parameters.AddWithValue("@Category_ID", complaint.Category_ID);

            command.ExecuteNonQuery();
        }

        public void Update(Complaint complaint)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"UPDATE Complaint
                                 SET Description = @Description,
                                     Date = @Date,
                                     Status = @Status,
                                     Student_ID = @Student_ID,
                                     Category_ID = @Category_ID
                                 WHERE Complaint_ID = @Complaint_ID";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Description", complaint.Description);
            command.Parameters.AddWithValue("@Date", complaint.Date);
            command.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(complaint.Status) ? DBNull.Value : complaint.Status);
            command.Parameters.AddWithValue("@Student_ID", complaint.Student_ID);
            command.Parameters.AddWithValue("@Category_ID", complaint.Category_ID);
            command.Parameters.AddWithValue("@Complaint_ID", complaint.Complaint_ID);

            command.ExecuteNonQuery();
        }

        public void Delete(int complaintId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sql = @"DELETE FROM Complaint WHERE Complaint_ID = @Complaint_ID";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Complaint_ID", complaintId);

            command.ExecuteNonQuery();
        }
    }
}
