using myMVC.Models;

namespace myMVC.Abstract
{
    public interface IStudent
    {
        IEnumerable<Student> getAllStudents();
        string AddStudent(Student std);
        Student GetStudent(int id);
        string DeleteStudent(int id);
    }
}
