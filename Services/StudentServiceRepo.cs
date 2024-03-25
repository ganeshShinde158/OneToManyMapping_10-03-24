using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OneToManyMapping_10_03_24.Models;
using System;
using System.Data;

namespace OneToManyMapping_10_03_24.Services
{
    public class StudentServiceRepo
    {
        private readonly string _connectionString;

        public StudentServiceRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConn");
        }

        public ResponseModel InsertStudentWithQualifications(StudentModels student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_InsertStudentWithQualifications", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@name", student.Name);
                command.Parameters.AddWithValue("@email", student.Email);
                command.Parameters.AddWithValue("@mobile", student.Mobile);
                command.Parameters.AddWithValue("@city", student.City);
                command.Parameters.AddWithValue("@qualification", student.Qualification);
                command.Parameters.AddWithValue("@university", student.University);
                command.Parameters.AddWithValue("@passing_year", student.PassingYear);
                command.Parameters.AddWithValue("@percentage", student.Percentage);

                // Output parameters
                command.Parameters.Add("@ResponseMsg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
                command.Parameters.Add("@OperationSuccess", SqlDbType.Bit).Direction = ParameterDirection.Output;
                command.Parameters.Add("@InsertedStudentID", SqlDbType.Int).Direction = ParameterDirection.Output;

                // Execute the stored procedure
                command.ExecuteNonQuery();

                // Retrieve output parameters
                var response = new ResponseModel
                {
                    ResponseMsg = command.Parameters["@ResponseMsg"].Value.ToString(),
                    OperationSuccess = Convert.ToBoolean(command.Parameters["@OperationSuccess"].Value),
                    InsertedStudentID = Convert.ToInt32(command.Parameters["@InsertedStudentID"].Value)
                };

                return response;
            }
        }
        public List<StudentModels> GetAllStudents()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_GetAllStudents", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Execute the stored procedure
                using (var reader = command.ExecuteReader())
                {
                    var students = new List<StudentModels>();
                    while (reader.Read())
                    {
                        var student = new StudentModels
                        {
                            Name = reader["StudentName"].ToString(),
                            Email = reader["StudentEmail"].ToString(),
                            Mobile = reader["StudentMobile"].ToString(),
                            City = reader["StudentCity"].ToString(),
                            Qualification = reader["Qualification"].ToString(),
                            University = reader["University"].ToString(),
                            PassingYear = Convert.ToInt32(reader["PassingYear"]),
                            Percentage = Convert.ToSingle(reader["Percentage"])
                        };

                        students.Add(student);
                    }

                    return students;
                }
            }
        }

    }
}

