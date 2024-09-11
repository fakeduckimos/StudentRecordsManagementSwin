using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.Entities.People;
using StudentRecordManagement.Models.ViewModels;
using StudentRecordManagement.Repositories.FormRecordRepository;
using StudentRecordManagement.Repositories.StudentDetailsRepository;

namespace StudentRecordManagement.Controllers
{

    public class LeavePassController : Controller
    {
        private readonly FormRecordRepositoryFactory _factory;
        private readonly IStudentDetailsRepository _studentDetailsRepository;

        public LeavePassController(FormRecordRepositoryFactory factory, IStudentDetailsRepository studentDetailsRepository)
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
        public async Task<IActionResult> Create(AddLeavePassRequest entity)
        {
            var leavePass = new LeavePass
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                RecordDate = DateTime.Now,
                StudentId = entity.StudentId,
                Reason = entity.Reason,
                SignOutDateTime = entity.SignOutDateTime
            };

            var leavePassRepository = _factory.CreateRepository<LeavePass>();

            // TODO: if reason is unexplained, create a detention record for this student

            if (leavePass.Reason == AbsentReason.Unexplained)
            {
                DetentionTime detentionTime;

                if (leavePass.SignOutDateTime.TimeOfDay < new TimeSpan(8, 0, 0))
                {
                    detentionTime = DetentionTime.BeforeSchool;
                }
                else if (leavePass.SignOutDateTime.TimeOfDay >= new TimeSpan(8, 0, 0) && leavePass.SignOutDateTime.TimeOfDay < new TimeSpan(12, 0, 0))
                {
                    detentionTime = DetentionTime.AM_Periods;
                }
                else if (leavePass.SignOutDateTime.TimeOfDay >= new TimeSpan(12, 0, 0) && leavePass.SignOutDateTime.TimeOfDay < new TimeSpan(13, 0, 0))
                {
                    detentionTime = DetentionTime.LunchTime;
                }
                else if (leavePass.SignOutDateTime.TimeOfDay >= new TimeSpan(13, 0, 0) && leavePass.SignOutDateTime.TimeOfDay < new TimeSpan(14, 30, 0))
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
                    StudentId = leavePass.StudentId,
                    PredefinedReasons = PredefinedReasons.UnexplainedLeftEarly,
                    Status = DetentionStatus.NonProcessed,
                    BreachReason = "Left early without explanation at " + leavePass.SignOutDateTime.ToString(),
                    DetentionTime = detentionTime
                };

                var detentionRepository = _factory.CreateRepository<Detention>();
                await detentionRepository.CreateAsync(detention);
            }

            await leavePassRepository.CreateAsync(leavePass);
            return RedirectToAction("List", "LeavePass");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var leavePassRepository = _factory.CreateRepository<LeavePass>();
            var leavePassRecords = await leavePassRepository.GetAllAsync();
            return View(leavePassRecords);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var leavePassRepository = _factory.CreateRepository<LeavePass>();
            var leavePassRecord = await leavePassRepository.GetAsync(id);

            var students = await _studentDetailsRepository.GetAllAsync();
            IEnumerable<SelectListItem> selectList = from s in students
                                                     select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };

            ViewBag.StudentList = new SelectList(selectList, "Value", "Text");


            return View(leavePassRecord);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LeavePass entity)
        {
            var leavePassRepository = _factory.CreateRepository<LeavePass>();
            var leavePassRecord = await leavePassRepository.UpdateAsync(entity);
            return RedirectToAction("List", "LeavePass");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LeavePass entity)
        {
            var leavePassRepository = _factory.CreateRepository<LeavePass>();
            var deletedItem = await leavePassRepository.DeleteAsync(entity.Id);

            if (deletedItem != null)
            {
                // show success notification
                return RedirectToAction("List", "LeavePass");
            }

            // show error notification
            return RedirectToAction("Edit", new { id = entity.Id });
        }
    }
}
