using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.ViewModels;
using StudentRecordManagement.Repositories.FormRecordRepository;
using StudentRecordManagement.Repositories.StaffRepository;
using StudentRecordManagement.Repositories.StudentDetailsRepository;
using StudentRecordManagement.Services;

namespace StudentRecordManagement.Controllers
{
    public class SickBayController : Controller
    {
        private readonly FormRecordRepositoryFactory _factory;
        private readonly IStudentDetailsRepository _studentDetailsRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IEmailSender _emailSender;

        public SickBayController(FormRecordRepositoryFactory factory, IStudentDetailsRepository studentDetailsRepository, IStaffRepository staffRepository, IEmailSender emailSender)
        {
            _factory = factory;
            _studentDetailsRepository = studentDetailsRepository;
            _staffRepository = staffRepository;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var students = await _studentDetailsRepository.GetAllAsync();
            IEnumerable<SelectListItem> selectList = from s in students
                                                     select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };
            ViewBag.StudentList = new SelectList(selectList, "Value", "Text");
            var staff = await _staffRepository.GetAllMedicalStaffAsync();
            IEnumerable<SelectListItem> selectList2 = from s in staff
                                                      select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };
            ViewBag.StaffList = new SelectList(selectList2, "Value", "Text");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddSickBayRequest entity)
        {
            var sickBay = new SickBay
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                RecordDate = DateTime.Now,
                StudentId = entity.StudentId,
                TimeIn = entity.TimeIn,
                SickBayReason = entity.SickBayReason,
                OtherReasons = entity.OtherReasons,
                ParentContacted = entity.ParentContacted,
                MedicalOfficerId = entity.MedicalOfficerId
            };
            var sickBayRepository = _factory.CreateRepository<SickBay>();
            await sickBayRepository.CreateAsync(sickBay);
            return RedirectToAction("List", "SickBay");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var sickBayRepository = _factory.CreateRepository<SickBay>();
            var sickBayRecords = await sickBayRepository.GetAllAsync();
            return View(sickBayRecords);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var sickBayRepository = _factory.CreateRepository<SickBay>();
            var sickBay = await sickBayRepository.GetAsync(id);

            var students = await _studentDetailsRepository.GetAllAsync();
            IEnumerable<SelectListItem> selectList = from s in students
                                                     select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };
            ViewBag.StudentList = new SelectList(selectList, "Value", "Text");
            var staff = await _staffRepository.GetAllMedicalStaffAsync();
            IEnumerable<SelectListItem> selectList2 = from s in staff
                                                      select new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Surname };
            ViewBag.StaffList = new SelectList(selectList2, "Value", "Text");
            return View(sickBay);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SickBay viewModel)
        {
            var sickBayRepository = _factory.CreateRepository<SickBay>();

            await sickBayRepository.UpdateAsync(viewModel);
            return RedirectToAction("List", "SickBay");
        }

        [HttpPost]
        public async Task<IActionResult> Process(SickBay viewModel)
        {
            viewModel.Status = SickBayStatus.SickBayIn;
            var sickBayRepository = _factory.CreateRepository<SickBay>();
            await sickBayRepository.UpdateAsync(viewModel);
            return RedirectToAction("List", "SickBay");
        }

        [HttpPost]
        public async Task<IActionResult> ReturnToClass(SickBay viewModel)
        {
            viewModel.Status = SickBayStatus.SickBayOut;
            viewModel.SickBayOutAction = SickBayOutAction.ReturnToClass;
            viewModel.TimeOut = DateTime.Now;
            viewModel.ParentContacted = true;
            var sickBayRepository = _factory.CreateRepository<SickBay>();

            await sickBayRepository.UpdateAsync(viewModel);

            var latePassViewModel = new AddLatePassRequest
            {
                StudentId = viewModel.StudentId,
                Reason = AbsentReason.Sick,
                SignInDateTime = DateTime.Now
            };

            var latePass = new LatePass
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                RecordDate = DateTime.Now,
                StudentId = latePassViewModel.StudentId,
                Reason = latePassViewModel.Reason,
                SignInDateTime = latePassViewModel.SignInDateTime
            };

            var latePassRepository = _factory.CreateRepository<LatePass>();
            await latePassRepository.CreateAsync(latePass);

            var student = await _studentDetailsRepository.GetAsync(viewModel.StudentId);

            string receiver;

            if (student.ParentContact != null)
            {
                receiver = student.ParentContact.Email;
            }
            else
            {
                receiver = "104358130@student.swin.edu.au";
            }

            string subject = "Sick Notification";
            string message = $"{student.Firstname} {student.Surname} has been sent to sickbay and delivered back to class.\nReason: {viewModel.SickBayReason}";

            await _emailSender.SendEmailAsync(receiver, subject, message);

            return RedirectToAction("List", "SickBay");
        }

        [HttpPost]
        public async Task<IActionResult> GoingHome(SickBay viewModel)
        {
            viewModel.Status = SickBayStatus.SickBayOut;
            viewModel.SickBayOutAction = SickBayOutAction.GoingHome;
            viewModel.TimeOut = DateTime.Now;
            viewModel.ParentContacted = true;
            var leavePass = new LeavePass
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                RecordDate = DateTime.Now,
                StudentId = viewModel.StudentId,
                Reason = AbsentReason.Sick,
                SignOutDateTime = DateTime.Now
            };

            var leavePassRepository = _factory.CreateRepository<LeavePass>();
            await leavePassRepository.CreateAsync(leavePass);

            // TODO 2: send an email to parent student.ParentContact.Email
            var student = await _studentDetailsRepository.GetAsync(viewModel.StudentId);

            string receiver;
            if (student.ParentContact != null)
            {
                receiver = student.ParentContact.Email;
            } else
            {
                receiver = "104358130@student.swin.edu.au";
            }

            string subject = "Sick Notification";
            string message = $"{student.Firstname} {student.Surname} has been sent home.\nReason: {viewModel.SickBayReason}";

            await _emailSender.SendEmailAsync(receiver, subject, message);

            var sickBayRepository = _factory.CreateRepository<SickBay>();
            await sickBayRepository.UpdateAsync(viewModel);
            return RedirectToAction("List", "SickBay");
        }

        [HttpPost]
        public async Task<IActionResult> BackToSickBayIn (SickBay viewModel)
        {
            viewModel.Status = SickBayStatus.SickBayIn;
            viewModel.TimeOut = null;
            var sickBayRepository = _factory.CreateRepository<SickBay>();
            await sickBayRepository.UpdateAsync(viewModel);
            return RedirectToAction("List", "SickBay");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SickBay viewModel)
        {
            var sickBayRepository = _factory.CreateRepository<SickBay>();
            var deletedSickBay = await sickBayRepository.DeleteAsync(viewModel.Id);

            if (deletedSickBay != null)
            {
                // show success notification
                return RedirectToAction("List", "SickBay");
            }

            // show error notification
            return RedirectToAction("Edit", new { id = viewModel.Id });
        }
    }
}
