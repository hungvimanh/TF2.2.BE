using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MForm;
using TwelveFinal.Services.MStudentService;

namespace TwelveFinal.Controller.student
{
    public class StudentController : ApiController
    {
        private IStudentService StudentService;
        private IFormService FormService;
        public StudentController(IStudentService StudentService, IFormService FormService)
        {
            this.StudentService = StudentService;
            this.FormService = FormService;
        }

        #region Create
        [Route(AdminRoute.CreateStudent), HttpPost]
        public async Task<ActionResult<RegisterDTO>> Create([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null) registerDTO = new RegisterDTO();

            Student student = new Student
            {
                Dob = registerDTO.Dob,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                EthnicId = registerDTO.EthnicId,
                PlaceOfBirth = registerDTO.PlaceOfBirth,
                HighSchoolId = registerDTO.HighSchoolId,
                Gender = registerDTO.Gender,
                Identify = registerDTO.Identify,
                Phone = registerDTO.Phone
            };
            student = await StudentService.Register(student);

            registerDTO = new RegisterDTO
            {
                Id = student.Id,
                Dob = student.Dob.Date,
                Email = student.Email,
                Name = student.Name,
                EthnicId = student.EthnicId.HasValue ? student.EthnicId.Value : Guid.Empty,
                EthnicName = student.EthnicName,
                PlaceOfBirth = student.PlaceOfBirth,
                HighSchoolId = student.HighSchoolId.HasValue ? student.HighSchoolId.Value : Guid.Empty,
                HighSchoolName = student.HighSchoolName,
                Gender = student.Gender,
                Identify = student.Identify,
                Phone = student.Phone,
                Errors = student.Errors
            };
            if (student.HasError)
                return BadRequest(registerDTO);
            return Ok(registerDTO);
        }
        #endregion

        #region BulkInsert
        [Route(AdminRoute.ImportStudent), HttpPost]
        public async Task ImportExcel([FromForm]IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            Request.Body.CopyTo(memoryStream);
            await this.StudentService.ImportExcel(memoryStream.ToArray());
        }
        #endregion

        #region Update
        [Route(StudentRoute.UpdateProfile), HttpPost]
        public async Task<ActionResult<StudentDTO>> Update([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) studentDTO = new StudentDTO();

            Student student = new Student
            {
                Id = studentDTO.Id,
                Dob = studentDTO.Dob,
                Name = studentDTO.Name,
                Gender = studentDTO.Gender,
                Identify = studentDTO.Identify,
                Image = studentDTO.Image != null ? Encoding.UTF8.GetBytes(studentDTO.Image) : null,
                Phone = studentDTO.Phone,
                Address = studentDTO.Address,
                EthnicId = studentDTO.EthnicId,
                HighSchoolId = studentDTO.HighSchoolId,
                PlaceOfBirth = studentDTO.PlaceOfBirth,
                ProvinceId = studentDTO.ProvinceId,
                DistrictId = studentDTO.DistrictId,
                TownId = studentDTO.TownId
            };
            student = await StudentService.Update(student);

            studentDTO = new StudentDTO
            {
                Id = student.Id,
                Dob = student.Dob.Date,
                Name = student.Name,
                Gender = student.Gender,
                Identify = student.Identify,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                Image = student.Image == null ? null : Encoding.UTF8.GetString(student.Image),
                EthnicId = student.EthnicId,
                EthnicCode = student.EthnicCode,
                EthnicName = student.EthnicName,
                HighSchoolId = student.HighSchoolId,
                HighSchoolCode = student.HighSchoolCode,
                HighSchoolName = student.HighSchoolName,
                PlaceOfBirth = student.PlaceOfBirth,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
            };
            if (student.HasError)
                return BadRequest(studentDTO);
            return Ok(studentDTO);
        }

        [Route(StudentRoute.UploadAvatar), HttpPost]
        public async Task<bool> UploadAvatar([FromBody] StudentDTO studentDTO)
        {
            Student student = new Student
            {
                Image = studentDTO.Image != null ? Encoding.UTF8.GetBytes(studentDTO.Image) : null
            };
            student = await StudentService.UploadAvatar(student);
            return student == null ? false : true;
        }
        #endregion

        #region Get
        [Route(StudentRoute.GetProfile), HttpPost]
        public async Task<StudentDTO> Get()
        {
            Student student = await StudentService.Get();

            return student == null ? null : new StudentDTO()
            {
                Id = student.Id,
                Address = student.Address,
                Dob = student.Dob.Date,
                Gender = student.Gender,
                Email = student.Email,
                Identify = student.Identify,
                PlaceOfBirth = student.PlaceOfBirth,
                Name = student.Name,
                Phone = student.Phone,
                Image = student.Image == null ? null : Encoding.UTF8.GetString(student.Image),
                EthnicId = student.EthnicId,
                EthnicName = student.EthnicName,
                EthnicCode = student.EthnicCode,
                HighSchoolId = student.HighSchoolId,
                HighSchoolName = student.HighSchoolName,
                HighSchoolCode = student.HighSchoolCode,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
                Status = student.Status
            };
        }
        #endregion

        #region List
        [Route(AdminRoute.ListStudent), HttpPost]
        public async Task<List<StudentDTO>> List([FromBody] StudentFilterDTO studentFilterDTO)
        {
            StudentFilter studentFilter = new StudentFilter
            {
                Identify = new StringFilter { StartsWith = studentFilterDTO.Identify },
                Name = new StringFilter { Contains = studentFilterDTO.Name },
                ProvinceId = new GuidFilter { Equal = studentFilterDTO.ProvinceId },
                HighSchoolId = new GuidFilter { Equal = studentFilterDTO.HighSchoolId },
                Gender = studentFilterDTO.Gender,
                Dob = new DateTimeFilter { Equal = studentFilterDTO.Dob },
                Status = studentFilterDTO.Status,
                Skip = studentFilterDTO.Skip,
                Take = int.MaxValue
            };

            var students = await StudentService.List(studentFilter);
            if (students == null) return new List<StudentDTO>();
            return students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Address = s.Address,
                Dob = s.Dob.Date,
                Gender = s.Gender,
                Email = s.Email,
                Identify = s.Identify,
                PlaceOfBirth = s.PlaceOfBirth,
                Name = s.Name,
                Phone = s.Phone,
                EthnicId = s.EthnicId,
                EthnicName = s.EthnicName,
                EthnicCode = s.EthnicCode,
                HighSchoolId = s.HighSchoolId,
                HighSchoolName = s.HighSchoolName,
                HighSchoolCode = s.HighSchoolCode,
                TownId = s.TownId,
                TownCode = s.TownCode,
                TownName = s.TownName,
                DistrictId = s.DistrictId,
                DistrictCode = s.DistrictCode,
                DistrictName = s.DistrictName,
                ProvinceId = s.ProvinceId,
                ProvinceCode = s.ProvinceCode,
                ProvinceName = s.ProvinceName,
                Status = s.Status
            }).ToList();
        }
        #endregion

        #region Get By Identify
        [Route(AdminRoute.GetByIdentify), HttpPost]
        public async Task<ActionResult<Student_IdentifyDTO>> GetByIdentify([FromBody]Student_IdentifyDTO student_IdentifyDTO)
        {
            if (student_IdentifyDTO == null) student_IdentifyDTO = new Student_IdentifyDTO();

            var student = await StudentService.GetByIdentify(student_IdentifyDTO.Identify);
            student_IdentifyDTO = new Student_IdentifyDTO
            {
                StudentId = student.Id,
                Identify = student.Identify,
                Dob = student.Dob.Date,
                Name = student.Name,
                Biology = student.Biology,
                Chemistry = student.Chemistry,
                CivicEducation = student.CivicEducation,
                Geography = student.Geography,
                History = student.History,
                Languages = student.Languages,
                Literature = student.Literature,
                Maths = student.Maths,
                Physics = student.Physics,
            };
            if (student == null)
            {
                return null;
            }
            return Ok(student_IdentifyDTO);
        }
        #endregion

        #region Mark Input
        [Route(AdminRoute.MarkInputStudent), HttpPost]
        public async Task<ActionResult<MarkDTO>> MarkInput([FromBody] MarkDTO markDTO)
        {
            if (markDTO == null) markDTO = new MarkDTO();

            Student student = new Student
            {
                Id = markDTO.StudentId,
                Biology = markDTO.Biology,
                Chemistry = markDTO.Chemistry,
                CivicEducation = markDTO.CivicEducation,
                Geography = markDTO.Geography,
                History = markDTO.History,
                Languages = markDTO.Languages,
                Literature = markDTO.Literature,
                Maths = markDTO.Maths,
                Physics = markDTO.Physics,
            };

            student = await StudentService.MarkInput(student);

            markDTO = new MarkDTO
            {
                StudentId = student.Id,
                Biology = student.Biology,
                Chemistry = student.Chemistry,
                CivicEducation = student.CivicEducation,
                Geography = student.Geography,
                History = student.History,
                Languages = student.Languages,
                Literature = student.Literature,
                Maths = student.Maths,
                Physics = student.Physics,
                Errors = student.Errors
            };

            if (student.HasError)
            {
                return BadRequest(markDTO);
            }
            return Ok(markDTO);
        }
        #endregion

        #region View Mark
        [Route(StudentRoute.ViewMark), HttpPost]
        public async Task<MarkDTO> ViewMark()
        {
            var student = await StudentService.ViewMark();

            return student == null ? null : new MarkDTO
            {
                StudentId = student.Id,
                Dob = student.Dob,
                Name = student.Name,
                Email = student.Email,
                Identify = student.Identify,
                Biology = student.Biology,
                Chemistry = student.Chemistry,
                CivicEducation = student.CivicEducation,
                Geography = student.Geography,
                History = student.History,
                Languages = student.Languages,
                Literature = student.Literature,
                Maths = student.Maths,
                Physics = student.Physics,
                Graduated = student.Graduated,
                GraduationMark = student.GraduationMark,
                Errors = student.Errors
            };
        }
        #endregion

        #region View Form Register
        [Route(AdminRoute.ViewForm), HttpPost]
        public async Task<FormDTO> ViewForm([FromBody] ViewRegisterFormDTO viewRegisterFormDTO)
        {
            if (viewRegisterFormDTO == null) viewRegisterFormDTO = new ViewRegisterFormDTO();
            Student student = new Student { Id = viewRegisterFormDTO.Id };
            student = await StudentService.GetById(student.Id);

            Form form = new Form { StudentId = viewRegisterFormDTO.Id };

            form = await FormService.Get(form.StudentId);
            return new FormDTO
            {
                Id = form.Id,
                StudentId = form.StudentId,
                Address = student.Address,
                Dob = student.Dob.Date,
                Gender = student.Gender,
                Email = student.Email,
                Identify = student.Identify,
                PlaceOfBirth = student.PlaceOfBirth,
                Name = student.Name,
                Phone = student.Phone,
                Image = student.Image == null ? null : Encoding.UTF8.GetString(student.Image),
                EthnicId = student.EthnicId,
                EthnicName = student.EthnicName,
                EthnicCode = student.EthnicCode,
                HighSchoolId = student.HighSchoolId,
                HighSchoolName = student.HighSchoolName,
                HighSchoolCode = student.HighSchoolCode,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
                ClusterContestId = form.ClusterContestId,
                ClusterContestCode = form.ClusterContestCode,
                ClusterContestName = form.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterPlaceOfExamName,
                Biology = form.Biology,
                Chemistry = form.Chemistry,
                CivicEducation = form.CivicEducation,
                Geography = form.Geography,
                History = form.History,
                Languages = form.Languages,
                Literature = form.Literature,
                Maths = form.Maths,
                NaturalSciences = form.NaturalSciences,
                Physics = form.Physics,
                SocialSciences = form.SocialSciences,
                Graduated = form.Graduated,

                ExceptLanguages = form.ExceptLanguages,
                Mark = form.Mark,
                ReserveBiology = form.ReserveBiology,
                ReserveChemistry = form.ReserveChemistry,
                ReserveCivicEducation = form.ReserveCivicEducation,
                ReserveGeography = form.ReserveGeography,
                ReserveHistory = form.ReserveHistory,
                ReserveLanguages = form.ReserveLanguages,
                ReserveLiterature = form.ReserveLiterature,
                ReserveMaths = form.ReserveMaths,
                ReservePhysics = form.ReservePhysics,

                Area = form.Area,
                PriorityType = form.PriorityType,
                Status = form.Status,
                Aspirations = form.Aspirations.Select(m => new AspirationDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };
        }
        #endregion

        #region Accept
        [Route(AdminRoute.ApproveAccept), HttpPost]
        public async Task<ActionResult<bool>> ApproveAccept([FromBody] ApproveDTO approveDTO)
        {
            if (approveDTO == null) approveDTO = new ApproveDTO();
            Form form = new Form
            {
                Id = approveDTO.Id,
                StudentId = approveDTO.StudentId,
                Status = approveDTO.Status
            };
            form = await FormService.ApproveAccept(form);
            if (form.Errors != null && form.Errors.Count > 0)
            {
                return BadRequest(form.Errors);
            }

            return Ok(true);
        }
        #endregion

        #region Deny
        [Route(AdminRoute.ApproveDeny), HttpPost]
        public async Task<ActionResult<bool>> ApproveDeny([FromBody] ApproveDTO approveDTO)
        {
            if (approveDTO == null) approveDTO = new ApproveDTO();
            Form form = new Form
            {
                Id = approveDTO.Id,
                StudentId = approveDTO.StudentId,
                Status = approveDTO.Status
            };
            form = await FormService.ApproveDeny(form);
            if (form.Errors != null && form.Errors.Count > 0)
            {
                return BadRequest(form.Errors);
            }

            return Ok(true);
        }
        #endregion

        #region Delete
        [Route(AdminRoute.DeleteStudent), HttpPost]
        public async Task<ActionResult<StudentDTO>> Delete([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) studentDTO = new StudentDTO();

            Student student = new Student { Id = studentDTO.Id };
            student = await StudentService.Delete(student);

            studentDTO = new StudentDTO
            {
                Id = student.Id,
                Address = student.Address,
                Dob = student.Dob.Date,
                Gender = student.Gender,
                Email = student.Email,
                Identify = student.Identify,
                PlaceOfBirth = student.PlaceOfBirth,
                Name = student.Name,
                Phone = student.Phone,
                EthnicId = student.EthnicId,
                EthnicName = student.EthnicName,
                EthnicCode = student.EthnicCode,
                HighSchoolId = student.HighSchoolId,
                HighSchoolName = student.HighSchoolName,
                HighSchoolCode = student.HighSchoolCode,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
                Status = student.Status,
                Errors = student.Errors
            };
            if (student.HasError)
                return BadRequest(studentDTO);
            return Ok(studentDTO);
        }
        #endregion
    }
}
