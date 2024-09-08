using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCoreDemo.Models;

namespace MvcCoreDemo.Controllers
{
    public class StudentController : Controller
    {
        StudentDataAccessLayer objstudent = new StudentDataAccessLayer();

        public IActionResult Index()
        {
            List<Student> lststudent = new List<Student>();
            lststudent = objstudent.GetAllStudent().ToList();
            return View(lststudent);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Student student)
        {
            if (ModelState.IsValid)
            {
                objstudent.AddStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Console.Write("edit called");
            if (id == null)
            {
                Console.Write("id null");
                return NotFound();
            }
            Student student = objstudent.GetStudentData(id);
            // Console.Write(student.ToString());

            if (student == null)
            {
                Console.Write("not edit called");
                return NotFound();
            }
            Console.WriteLine($"Student found: {student}");
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Student student)
        {
            Console.Write("edit called");
            if (id != student.StudId)
            {
                Console.WriteLine("ID mismatch.");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                Console.Write("valid called");
                objstudent.UpdateStudent(student);
                return RedirectToAction("Index");
            }
             Console.WriteLine("Model is not valid.");
            return View(student);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Student student = objstudent.GetStudentData(id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Student student = objstudent.GetStudentData(id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            objstudent.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}