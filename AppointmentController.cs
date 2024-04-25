
namespace Hairry.HDR.Controllers
{
    [ServiceFilter(typeof(SessionTimeoutAttribute))]
    public class AppointmentController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IHubContext<AppointmentHub> _hubContext_messageRoom;
        public AppointmentController(IUnitOfWork unitOfWork, IHubContext<AppointmentHub> hubContext_messageRoom)
        {
            _unitOfWork = unitOfWork;
            _hubContext_messageRoom = hubContext_messageRoom;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetLatestAppointment()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            if (!string.IsNullOrEmpty(SessionData.Email))
            {
                //if (_unitOfWork.User.IsSalon(Convert.ToInt32(HttpContext.Session.GetString("Id"))))
                if (_unitOfWork.User.IsSalon(SessionData.Id))
                {
                    return View("~/Views/Salon/Appointment/GetLatestAppointment.cshtml");
                }
                else
                {
                    return View("~/Views/Employee/Appointment/GetLatestAppointment.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult Appointment() 
        {
            return View("~/Views/Salon/Appointment/Appointment.cshtml");
        }
        //public IActionResult AppointmentOverview()
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //    {
        //        if(objUserController.IsHairdresser(Convert.ToInt32(HttpContext.Session.GetString("Id"))))
        //        {
        //            List<User> Employee = new List<User>();
        //            var employee = _unitOfWork.Hairdressers.GetEmployees(Convert.ToInt32(HttpContext.Session.GetString("Id")));
        //            if (employee.Item2 == true)
        //            {
        //                Employee = employee.Item1;
        //            }
        //            ViewData["Employee"] = Employee;
        //            return View("~/Views/Hairdresser/Appointment/AppointmentOverview.cshtml");
        //        }
        //        else
        //        {
        //            List<User> Employee = new List<User>();
        //            var employee = _unitOfWork.Hairdressers.GetEmployees(Convert.ToInt32(HttpContext.Session.GetString("Id")));
        //            if (employee.Item2 == true)
        //            {
        //                Employee = employee.Item1;
        //            }
        //            ViewData["EmployeeID"] = Convert.ToInt32(HttpContext.Session.GetString("Id"));
        //            ViewData["Employee"] = Employee;
        //            return View("~/Views/Employee/Appointment/AppointmentOverview.cshtml");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login","Home");
        //    }
        //}

        private static DateTime ConvertDate(string datetime)
        {
            try
            {
                return DateTime.ParseExact(datetime, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return DateTime.ParseExact(datetime, "d/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }


        }

        //public JsonResult CurrentDayAppointment(string EmployeeID, string DayDate)
        //{
        //    Tuple<List<AppointmentOverviewViewModel>, bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            Response = _unitOfWork.Appointments.GetCurrentDayAppointment(Convert.ToInt32(EmployeeID), ConvertDate(DayDate));
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public JsonResult InsertAndUpdateAppointment(Appointments appoint, string[] employeeIds,string dateOfAppointment)
        //{
        //    // Tuple<IQueryable<Appointment>, bool, string, string> AddAppointment(Appointment appoint)
        //    Tuple<List<Appointments>, bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            //DateTime parsedDateTime = DateTime.Parse(dateOfAppointment, new CultureInfo("en-GB"));
        //            //appoint.Date = parsedDateTime;
        //            //appoint.HairdresserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));

        //            //DateTime startDateTime = new DateTime(parsedDateTime.Year, parsedDateTime.Month, parsedDateTime.Day, appoint.StartTime.Hour, appoint.StartTime.Minute, appoint.StartTime.Second);
        //            //DateTime endDateTime = new DateTime(parsedDateTime.Year, parsedDateTime.Month, parsedDateTime.Day, appoint.EndTime.Hour, appoint.EndTime.Minute, appoint.EndTime.Second);
        //            //appoint.StartTime = startDateTime;
        //            //appoint.EndTime = endDateTime;

        //            //Response = _unitOfWork.Appointments.InsertAndUpdateAppointment(appoint, employeeIds);
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        //public JsonResult InsertAndUpdateAppointmentForEmployee(Appointments appoint, string[] employeeIds, string dateOfAppointment)
        //{
        //    // Tuple<IQueryable<Appointment>, bool, string, string> AddAppointment(Appointment appoint)
        //    Tuple<List<Appointments>, bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            DateTime parsedDateTime = DateTime.Parse(dateOfAppointment, new CultureInfo("en-GB"));
        //            appoint.Date = parsedDateTime;

        //            //var employee = _unitOfWork.Employees.GetSpecificEmployee(Convert.ToInt32(HttpContext.Session.GetString("Id")));

        //            //employee.Item1.UserRole.

        //            appoint.HairdresserId = _unitOfWork.Employees.GetHairdresserId(Convert.ToInt32(HttpContext.Session.GetString("Id")));//4;// Convert.ToInt32(HttpContext.Session.GetString("Id"));

        //            //appoint.HairdresserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));

        //            DateTime startDateTime = new DateTime(parsedDateTime.Year, parsedDateTime.Month, parsedDateTime.Day, appoint.StartTime.Hour, appoint.StartTime.Minute, appoint.StartTime.Second);
        //            DateTime endDateTime = new DateTime(parsedDateTime.Year, parsedDateTime.Month, parsedDateTime.Day, appoint.EndTime.Hour, appoint.EndTime.Minute, appoint.EndTime.Second);
        //            appoint.StartTime = startDateTime;
        //            appoint.EndTime = endDateTime;

        //            Response = _unitOfWork.Appointments.InsertAndUpdateAppointment(appoint, employeeIds);
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        //public JsonResult EditAppointment(string Id)
        //{
        //    Tuple<EditBookingViewModel, List<AppointmentProducts>, List<Product>, List<User>, Int32, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            Int32 appointmentId =  Convert.ToInt32(_unitOfWork.EncryptOrDecrypt.Decrypt(Id));
        //            Response = _unitOfWork.Appointments.EditBooking(appointmentId, Convert.ToInt32(HttpContext.Session.GetString("Id")));
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public JsonResult UpdateAppointment(Appointment appoint, string[] employeeIds)
        //{
        //    // Tuple<IQueryable<Appointment>, bool, string, string> AddAppointment(Appointment appoint)
        //    Tuple<List<Appointment>, bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            appoint.HairdresserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
        //            Response = _unitOfWork.Hairdressers.AddAppointment(appoint, employeeIds);
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public JsonResult DeleteAppointment(int id)
        //{
        //    Tuple<bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            Response = _unitOfWork.Appointments.DelteAppointment(id);
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public JsonResult LatestAppointment()
        //{
        //    Tuple<List<GetLatestAppointmentViewModel>, bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            Response = _unitOfWork.Appointments.GetLatestAppointment(Convert.ToInt32(HttpContext.Session.GetString("Id")));
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public JsonResult LatestAppointmentForEmployee()
        //{
        //    Tuple<List<GetLatestAppointmentViewModel>, bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            Response = _unitOfWork.Appointments.GetLatestAppointmentByEmployeeID(Convert.ToInt32(HttpContext.Session.GetString("Id")));
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public IActionResult PassAvailabilty()
        {
            return View();
        }
        //public JsonResult CurrentDayAppointments(string DayDate)
        //{
        //    Tuple<List<AppointmentsOverviewViewModel>, bool, string, string> Response;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        //        {
        //            //Response = _unitOfWork.Appointments.GetCurrentDayAppointments(Convert.ToInt32(HttpContext.Session.GetString("Id")), ConvertDate(DayDate));
        //            Response = _unitOfWork.Appointments.GetCurrentDayAppointments(Convert.ToInt32(HttpContext.Session.GetString("Id")), ConvertDate(DayDate));
        //            return Json(Response);
        //        }
        //        else
        //        {
        //            var data = new
        //            {
        //                Item3 = "203",
        //                Item4 = "Je moet eerst inloggen"
        //            };
        //            return Json(data);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpPost]
        public JsonResult UpdateAppointmentNoShow(int AppointmentId,bool Noshow)
        {
            try
            {
                int salonId = SessionData.Id;
                var Response=_unitOfWork.Appointments.UpdateAppointmentNoShow(salonId,AppointmentId, Noshow);
                return Json(Response);
            }
            catch(Exception)
            {
                throw;
            }
           
        }
        [HttpPost]
        public JsonResult SaveComment(int AppointmentId, string Description)
        {
            try
            {
                int salonId = SessionData.Id;//Convert.ToInt32(HttpContext.Session.GetString("Id"));
                var Response = _unitOfWork.Appointments.SaveComment(AppointmentId, Description, salonId);
                return Json(Response);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult UpdateAppointmentOptin(int AppointmentId, bool OptinStatus)
        {
            try
            {
                var Response = _unitOfWork.Appointments.UpdateAppointmentOptin(AppointmentId, OptinStatus);
                return Json(Response);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        public JsonResult UpdateComment(int CommentId, string Description)
        {
            try
            {
                var Response = _unitOfWork.Appointments.UpdateComment(CommentId, Description);
                return Json(Response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult DeleteComment(int AppointmentId,int CommentId)
        {
            try
            {
                var Response = _unitOfWork.Appointments.DeleteComment(AppointmentId,CommentId);
                return Json(Response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public JsonResult GetAppointmentNotification(int appointmentId)
        {
            try
            {
                var Response = _unitOfWork.Appointments.GetAppointmentNotification(appointmentId);
                return Json(Response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool API_Broadcasting_For_Messages(long AppointmentId)
        {
            bool Response = true;
            string GroupName = "AppointmentId" + AppointmentId;
            _hubContext_messageRoom.Clients.Group(GroupName).SendAsync("AppointmentHubNotification", AppointmentId);

            return Response;
        }

        [HttpPost]
        public JsonResult SetCheckStatus(int Id,bool Status)
        {
            try
            {
                int LoginUserId = SessionData.Id;
                var Response = _unitOfWork.Appointments.SetCheckStatus(Id, LoginUserId, Status);
                return Json(Response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult GetCustomerLast12Appointments(int CustomerId)
        {
            try
            {
                int LoginUserId = SessionData.Id;
                var Response = _unitOfWork.Appointments.GetCustomerLast12Appointments(CustomerId, LoginUserId);
                return Json(Response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}