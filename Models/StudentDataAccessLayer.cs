using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
namespace MvcCoreDemo.Models
{
    public class StudentDataAccessLayer
    {
        string connectionString = "server=127.0.0.1;uid=root;pwd=;database=mvccoredemo";

        //To View all Student details      
        public IEnumerable<Student> GetAllStudent()
        {
            List<Student> studList = new List<Student>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("spGetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student stud = new Student();

                    int studentIdOrdinal = rdr.GetOrdinal("StudId");
                    stud.StudId = Convert.ToInt32(rdr.GetValue(studentIdOrdinal));
                    stud.Name = rdr["Name"].ToString();
                    stud.Gender = rdr["Gender"].ToString();
                    stud.Department = rdr["Department"].ToString();
                    stud.City = rdr["City"].ToString();

                    studList.Add(stud);
                }
                con.Close();
            }
            return studList;
        }

        //To Add new student record      
        public void AddStudent(Student student)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("spAddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.WriteLine("Adding parameters to command:");
                Console.WriteLine($"- StudId: {student.StudId}");
                Console.WriteLine($"- Name: {student.Name}");
                Console.WriteLine($"- Gender: {student.Gender}");
                Console.WriteLine($"- Department: {student.Department}");
                Console.WriteLine($"- City: {student.City}");

                cmd.Parameters.AddWithValue("@p_Name", student.Name);
                cmd.Parameters.AddWithValue("@p_Gender", student.Gender);
                cmd.Parameters.AddWithValue("@p_Department", student.Department);
                cmd.Parameters.AddWithValue("@p_City", student.City);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a individual student    
        public void UpdateStudent(Student student)
        {
            if (student == null)
            {
                Console.WriteLine("Error: The student parameter is null.");
                return;
            }
            Console.WriteLine("UpdateStudent method called.");
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                Console.WriteLine("update called");
                con.Open();
                MySqlCommand cmd = new MySqlCommand("spUpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.WriteLine("Adding parameters to command:");
               // Console.WriteLine($"- StudId: {student.StudId}");
                Console.WriteLine($"- Name: {student.Name}");
                Console.WriteLine($"- Gender: {student.Gender}");
                Console.WriteLine($"- Department: {student.Department}");
                Console.WriteLine($"- City: {student.City}");


                cmd.Parameters.AddWithValue("@p_StudId", student.StudId);
                cmd.Parameters.AddWithValue("@p_Name", student.Name);
                cmd.Parameters.AddWithValue("@p_Gender", student.Gender);
                cmd.Parameters.AddWithValue("@p_Department", student.Department);
                cmd.Parameters.AddWithValue("@p_City", student.City);



                cmd.ExecuteNonQuery();
                Console.WriteLine("Student updated successfully.");
                con.Close();
            }
        }

        //Get the details of a individual student    
        public Student GetStudentData(int? id)
        {
            Student student = new Student();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblStudent WHERE StudId= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int studentIdOrdinal = rdr.GetOrdinal("StudId");
                    student.StudId = Convert.ToInt32(rdr.GetValue(studentIdOrdinal));
                    student.Name = rdr["Name"].ToString();
                    student.Gender = rdr["Gender"].ToString();
                    student.Department = rdr["Department"].ToString();
                    student.City = rdr["City"].ToString();
                }
            }
            return student;
        }

        //To Delete the record on a particular student    
        public void DeleteStudent(int? id)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("spDeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_StudId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}