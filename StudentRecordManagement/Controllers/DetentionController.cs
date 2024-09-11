using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentRecordManagement.Data;
using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.Entities.People;
using StudentRecordManagement.Models.ViewModels;
using StudentRecordManagement.Repositories.FormRecordRepository;
using StudentRecordManagement.Repositories.StudentDetailsRepository;

namespace StudentRecordManagement.Controllers
{
    public class DetentionController : Controller
    {
        private readonly FormRecordRepositoryFactory _factory;
        private readonly IStudentDetailsRepository _studentDetailsRepository;

        public DetentionController(FormRecordRepositoryFactory factory, IStudentDetailsRepository studentDetailsRepository)
        {
            _factory = factory;
            _studentDetailsRepository = studentDetailsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var students = await _studentDetailsRepository.GetAllAsync();
            IEnumerable<SelectListItem> selectList = from s in students
                                                     select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };
            ViewBag.StudentList = new SelectList(selectList, "Value", "Text");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddDetentionRequest entity)
        {
            var detention = new Detention
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                RecordDate = DateTime.Now,
                StudentId = entity.StudentId,
                PredefinedReasons = entity.PredefinedReasons,
                DetentionTime = entity.DetentionTime,
            };
            var detentionRepository = _factory.CreateRepository<Detention>();
            await detentionRepository.CreateAsync(detention);
            return RedirectToAction("List", "Detention");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var detentionRepository = _factory.CreateRepository<Detention>();
            var detentions = await detentionRepository.GetAllAsync();
            return View(detentions);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var detentionRepository = _factory.CreateRepository<Detention>();
            var detentionRecord = await detentionRepository.GetAsync(id);

            var students = await _studentDetailsRepository.GetAllAsync();
            IEnumerable<SelectListItem> selectList = from s in students
                                                     select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };
            ViewBag.StudentList = new SelectList(selectList, "Value", "Text");

            return View(detentionRecord);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(Detention viewModel)
        {
            viewModel.Status = DetentionStatus.DetentionCompleted;
            var detentionRepository = _factory.CreateRepository<Detention>();
            var updatedDetention = await detentionRepository.UpdateAsync(viewModel);
            return RedirectToAction("List", "Detention");
        }

        [HttpPost]
        public async Task<IActionResult> CompletedToNonProcessed(Detention viewModel)
        {
            viewModel.Status = DetentionStatus.NonProcessed;
            var detentionRepository = _factory.CreateRepository<Detention>();
            var updatedDetention = await detentionRepository.UpdateAsync(viewModel);
            return RedirectToAction("List", "Detention");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Detention viewModel)
        {
            var detentionRepository = _factory.CreateRepository<Detention>();
            var updatedDetention = await detentionRepository.UpdateAsync(viewModel);
            return RedirectToAction("List", "Detention");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Detention viewModel)
        {
            var detentionRepository = _factory.CreateRepository<Detention>();
            var deletedDetention = await detentionRepository.DeleteAsync(viewModel.Id);

            if (deletedDetention != null)
            {
                // show success notification
                return RedirectToAction("List", "Detention");
            }

            // show error notification
            return RedirectToAction("Edit", new { id = viewModel.Id });
        }
    }
}
