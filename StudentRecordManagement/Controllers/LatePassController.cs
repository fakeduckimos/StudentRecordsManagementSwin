using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.ViewModels;
using StudentRecordManagement.Repositories.FormRecordRepository;
using StudentRecordManagement.Repositories.StudentDetailsRepository;

namespace StudentRecordManagement.Controllers
{
    public class LatePassController : Controller
    {
        private readonly FormRecordRepositoryFactory _factory;
        private readonly IStudentDetailsRepository _studentDetailsRepository;

        public LatePassController(FormRecordRepositoryFactory factory, IStudentDetailsRepository studentDetailsRepository)
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
        public async Task<IActionResult> Create(AddLatePassRequest entity)
        {
            var latePass = new LatePass
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                RecordDate = DateTime.Now,
                StudentId = entity.StudentId,
                Reason = entity.Reason,
                SignInDateTime = entity.SignInDateTime
            };

            var latePassRepository = _factory.CreateRepository<LatePass>();

            if (latePass.Reason == AbsentReason.Unexplained)
            {
                
                DetentionTime detentionTime;

                if (latePass.SignInDateTime.TimeOfDay < new TimeSpan(8, 0, 0))
                {
                    detentionTime = DetentionTime.BeforeSchool;
                }
                else if (latePass.SignInDateTime.TimeOfDay >= new TimeSpan(8, 0, 0) && latePass.SignInDateTime.TimeOfDay < new TimeSpan(12, 0, 0))
                {
                    detentionTime = DetentionTime.AM_Periods;
                }
                else if (latePass.SignInDateTime.TimeOfDay >= new TimeSpan(12, 0, 0) && latePass.SignInDateTime.TimeOfDay < new TimeSpan(13, 0, 0))
                {
                    detentionTime = DetentionTime.LunchTime;
                }
                else if (latePass.SignInDateTime.TimeOfDay >= new TimeSpan(13, 0, 0) && latePass.SignInDateTime.TimeOfDay < new TimeSpan(14, 30, 0))
                {
                    detentionTime = DetentionTime.PM_Periods;
                }
                else
                {
                    detentionTime = DetentionTime.AfterSchool;
                }

                var detention = new Detention
                {
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    RecordDate = DateTime.Now,
                    StudentId = entity.StudentId,
                    PredefinedReasons = PredefinedReasons.Late,
                    Status = DetentionStatus.NonProcessed,
                    BreachReason = "Late without proper reason at " + latePass.SignInDateTime.ToString(),
                    DetentionTime = detentionTime
                };

                var detentionRepository = _factory.CreateRepository<Detention>();
                await detentionRepository.CreateAsync(detention);
            }

            await latePassRepository.CreateAsync(latePass);
            return RedirectToAction("List", "LatePass");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var latePassRepository = _factory.CreateRepository<LatePass>();
            var latePassRecords = await latePassRepository.GetAllAsync();
            return View(latePassRecords);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var latePassRepository = _factory.CreateRepository<LatePass>();
            var latePassRecord = await latePassRepository.GetAsync(id);

            var students = await _studentDetailsRepository.GetAllAsync();
            IEnumerable<SelectListItem> selectList = from s in students
                                                     select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };
            ViewBag.StudentList = new SelectList(selectList, "Value", "Text");

            return View(latePassRecord);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LatePass entity)
        {
            var latePassRepository = _factory.CreateRepository<LatePass>();
            var latePassRecord = await latePassRepository.UpdateAsync(entity);
            return RedirectToAction("List", "LatePass");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LatePass entity)
        {
            var latePassRepository = _factory.CreateRepository<LatePass>();
            var deletedItem = await latePassRepository.DeleteAsync(entity.Id);

            if (deletedItem != null)
            {
                // show success notification
                return RedirectToAction("List", "LatePass");
            }

            // show error notification
            return RedirectToAction("Edit", new { id = entity.Id });
        }
    }
}
