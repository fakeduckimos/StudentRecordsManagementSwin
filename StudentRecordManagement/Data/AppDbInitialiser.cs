using StudentRecordManagement.Models.Entities;
using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.Entities.People;

namespace StudentRecordManagement.Data
{
    public class AppDbInitialiser
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();

                context.Database.EnsureCreated();

                // Student
                if (context.Students.Any() || context.Staff.Any() || context.ParentContacts.Any()) return;

                var parentContacts = new List<ParentContact>
                {
                    new ParentContact
                    {
                        Firstname = "Jorge",
                        Surname = "Messi",
                        Email = "",
                        Phone = "",
                        Address = ""
                    },
                    new ParentContact
                    {
                        Firstname = "Veronica",
                        Surname = "Fernandez",
                        Email = "",
                        Phone = "",
                        Address = ""
                    },
                    new ParentContact
                    {
                        Firstname = "Abdoulaye",
                        Surname = "Mbappe",
                        Email = "",
                        Phone = "",
                        Address = ""
                    },
                    new ParentContact
                    {
                        Firstname = "Tina",
                        Surname = "Modric",
                        Email = "",
                        Phone = "",
                        Address = ""
                    }
                };

                foreach (ParentContact p in parentContacts)
                {
                    p.Email = "104358130@student.swin.edu.au";
                }

                var students = new List<Student>
                {
                    new Student
                    {
                        Firstname = "Lionel",
                        Surname = "Messi",
                        Email = "",
                        Phone = "",
                        Address = "",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        PreferredName = "Leo",
                        Year = "12",
                        DOB = DateTime.Parse("24/06/1987"),
                        Gender = 'M',
                        ParentContact = parentContacts[0]
                    },
                    new Student
                    {
                        Firstname = "Cristiano",
                        Surname = "Ronaldo",
                        Email = "",
                        Phone = "",
                        Address = "",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        PreferredName = "CR7",
                        Year = "12",
                        DOB = DateTime.Parse("05/02/1985"),
                        Gender = 'M',
                        ParentContact = parentContacts[1]
                    },
                    new Student
                    {
                        Firstname = "Kylian",
                        Surname = "Mbappe",
                        Email = "",
                        Phone = "",
                        Address = "",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        PreferredName = "Kylian",
                        Year = "11",
                        DOB = DateTime.Parse("20/12/1998"),
                        Gender = 'M',
                        ParentContact = parentContacts[2]
                    },
                    new Student
                    {
                        Firstname = "Luka",
                        Surname = "Modric",
                        Email = "",
                        Phone = "",
                        Address = "",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        PreferredName = "Luka",
                        Year = "12",
                        DOB = DateTime.Parse("09/09/1985"),
                        Gender = 'M',
                        ParentContact = parentContacts[3]
                    }
                };

                var staff = new List<Staff>
                {
                    new Staff
                    {
                        Firstname = "Zinedine",
                        Surname = "Zidane",
                        Email = "zinedine.zidane@school.com",
                        Phone = "",
                        Address = "",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        StaffGroupMask = (int)StaffGroupBit.Teacher + (int)StaffGroupBit.HOD + (int)StaffGroupBit.MedicalOfficer
                    },
                    new Staff
                    {
                        Firstname = "Pep",
                        Surname = "Guardiola",
                        Email = "pep.guardiola@school.com",
                        Phone = "",
                        Address = "",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        StaffGroupMask = (int)StaffGroupBit.Teacher
                    }
                };

                // Original Detentions, SickBay, LeavePass, LatePass records
                var detentions = new List<Detention>
                {
                    new Detention
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[0],
                        PredefinedReasons = PredefinedReasons.Uniform,
                        DetentionTime = DetentionTime.BeforeSchool,
                        Status = DetentionStatus.NonProcessed
                    },
                    new Detention
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[1],
                        PredefinedReasons = PredefinedReasons.Behaviour,
                        BreachReason = "say bad words to a teacher",
                        DetentionTime = DetentionTime.LunchTime,
                        Status = DetentionStatus.NonProcessed
                    },
                    new Detention
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[2],
                        PredefinedReasons = PredefinedReasons.Late,
                        DetentionTime = DetentionTime.LunchTime,
                        Status = DetentionStatus.DetentionCompleted
                    }
                };

                var leavePassRecords = new List<LeavePass>
                {
                    new LeavePass
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[0],
                        Reason = AbsentReason.Family,
                        SignOutDateTime = new DateTime(2024,05,13,9,30,00)
                    },
                    new LeavePass
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[1],
                        Reason = AbsentReason.Unexplained,
                        SignOutDateTime = new DateTime(2024,05,14,11,30,00)
                    },
                    new LeavePass
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[2],
                        Reason = AbsentReason.Sick,
                        SignOutDateTime = new DateTime(2024,05,15,10,45,00)
                    }
                };

                var latePassRecords = new List<LatePass>
                {
                    new LatePass
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[0],
                        Reason = AbsentReason.Unexplained,
                        SignInDateTime = new DateTime(2024,05,13,9,00,00)
                    },
                    new LatePass
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[1],
                        Reason = AbsentReason.Sick,
                        SignInDateTime = new DateTime(2024,05,14,9,00,00)
                    },
                    new LatePass
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[2],
                        Reason = AbsentReason.Family,
                        SignInDateTime = new DateTime(2024,05,15,9,00,00)
                    }
                };

                var sickBayRecords = new List<SickBay>
                {
                    new SickBay
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[3],
                        Status = SickBayStatus.SickBayIn,
                        TimeIn = new DateTime(2024,05,12,9,30,00),
                        SickBayReason = SickBayReason.Injured
                    },
                    new SickBay
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[1],
                        Status = SickBayStatus.SickBayOut,
                        TimeIn = new DateTime(2024,05,12,11,30,00),
                        SickBayReason = SickBayReason.Sick,
                        Treatment = "Panadol Extra",
                        TimeOut = new DateTime(2024,05,12,14,20,00),
                        SickBayOutAction = SickBayOutAction.GoingHome,
                        ParentContacted = true,
                        MedicalOfficer = staff[0]
                    },
                    new SickBay
                    {
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RecordDate = DateTime.Now,
                        Student = students[2],
                        Status = SickBayStatus.SickBayOut,
                        TimeIn = new DateTime(2024,05,12,12,20,00),
                        SickBayReason = SickBayReason.Injured,
                        TimeOut = new DateTime(2024,05,12,15,40,00),
                        SickBayOutAction = SickBayOutAction.ReturnToClass,
                        ParentContacted = false,
                        MedicalOfficer = staff[0]
                    }
                };

                context.Students.AddRange(students);
                context.Staff.AddRange(staff);
                context.ParentContacts.AddRange(parentContacts);
                context.DetentionRecords.AddRange(detentions);
                context.SickBayRecords.AddRange(sickBayRecords);
                context.LeavePassRecords.AddRange(leavePassRecords);
                context.LatePassRecords.AddRange(latePassRecords);
                context.SaveChanges();
            }
        }
    }
}
