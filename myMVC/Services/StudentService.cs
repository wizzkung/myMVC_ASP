using myMVC.Abstract;
using myMVC.Models;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace myMVC.Services
{
    public class StudentService : IStudent
    {
        IConfiguration _configuration;
       public StudentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AddStudent(Student std)
        {
            string connection = _configuration["db"];
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Execute
                    ("pStudent", new { @name = std.Name, @PhoneNumber = std.PhoneNumber }, commandType: CommandType.StoredProcedure);
            }
            return "Студент добавлен";
        }

        public string DeleteStudent(int id)
        {
            string connection = _configuration["db"];
            using (SqlConnection con = new SqlConnection(connection))
            {
                var param = new { _id = id };
                con.Execute
                    ("pStudent;4", param, commandType: CommandType.StoredProcedure);
            }
            return "Студент удален";
        }
         
        public IEnumerable<Student> getAllStudents()
        {
            string connection = _configuration["db"];
            using (SqlConnection con = new SqlConnection(connection))
            {
                return con.Query<Student>("pStudent;2", commandType: CommandType.StoredProcedure);
            }
        }

        public Student GetStudent(int id)
        {
            string connection = _configuration["db"];
            using (SqlConnection con = new SqlConnection(connection))
            {
                var param = new { id };
                return con.QueryFirstOrDefault<Student>("pStudent;3", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
