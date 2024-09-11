using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.People;
using StudentRecordManagement.Models.ViewModels;
using StudentRecordManagement.Repositories.StudentDetailsRepository;
using System.Numerics;
using System.Reflection;

namespace StudentRecordManagement.Controllers
{
    public class StudentDetailsController : Controller
    {
        private readonly IStudentDetailsRepository _studentDetailsRepository;

        public StudentDetailsController(IStudentDetailsRepository studentDetailsRepository)
        {
            _studentDetailsRepository = studentDetailsRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentRequest viewModel)
        {
            var student = new Student
            {
                Firstname = viewModel.Firstname,
                Surname = viewModel.Surname,
                PreferredName = viewModel.PreferredName,
                Year = viewModel.Year,
                DOB = viewModel.DOB,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Gender = viewModel.Gender,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            await _studentDetailsRepository.AddAsync(student);

            return RedirectToAction("List", "StudentDetails");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _studentDetailsRepository.GetAllAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentDetailsRepository.GetAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var updatedStudent = await _studentDetailsRepository.UpdateAsync(viewModel);

            if (updatedStudent != null)
            {
                // show success notification
            } else
            {
                // show error notification
            }

            return RedirectToAction("List", "StudentDetails");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var deletedStudent = await _studentDetailsRepository.DeleteAsync(viewModel.Id);

            if (deletedStudent != null)
            {
                // show success notification
                return RedirectToAction("List", "StudentDetails");
            }

            // show error notification
            return RedirectToAction("Edit", new { id = viewModel.Id});
        }
    }
}