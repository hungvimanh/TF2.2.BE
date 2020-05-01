using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.form;
using TwelveFinal.Controller.majors;
using TwelveFinal.Controller.subject_group;
using TwelveFinal.Controller.university;
using TwelveFinal.Controller.university_majors;

namespace TwelveFinal.Controller
{
    public class Root
    {
        public const string Base = "api/TF";
    }

    public class AdminRoute : Root
    {
        public const string Default = Base + "/ad";

        public const string CreateStudent = Default + "/student/create";
        public const string ImportStudent = Default + "/student/import";
        public const string ListStudent = Default + "/student/list";
        public const string GetByIdentify = Default + "/student/get-by-identify";
        public const string MarkInputStudent = Default + "/student/mark-input";
        public const string ViewForm = Default + "/student/view-form";
        public const string ApproveAccept = Default + "/student/accept";
        public const string ApproveDeny = Default + "/student/deny";
        public const string DeleteStudent = Default + "/student/delete";

        public const string CreateMajors = Default + "/majors/create";
        public const string UpdateMajors = Default + "/majors/update";
        public const string DeleteMajors = Default + "/majors/delete";

        public const string CreateSubjectGroup = Default + "/subject-group/create";
        public const string UpdateSubjectGroup = Default + "/subject-group/update";
        public const string DeleteSubjectGroup = Default + "/subject-group/delete";

        public const string CreateUniversity = Default + "/university/create";
        public const string UpdateUniversity = Default + "/university/update";
        public const string DeleteUniversity = Default + "/university/delete";

        public const string CreateUniversity_Majors = Default + "/university-majors/create";
        public const string UpdateUniversity_Majors = Default + "/university-majors/update";
        public const string DeleteUniversity_Majors = Default + "/university-majors/delete";

        public const string CreateUniversity_Majors_SubjectGroup = Default + "/university-majors-subject-group/create";
        public const string DeleteUniversity_Majors_SubjectGroup = Default + "/university-majors-subject-group/delete";
    }

    public class StudentRoute : Root
    {
        public const string Default = Base + "/st";
        public const string GetForm = Default + "/form/get";
        public const string SaveForm = Default + "/form/save";

        public const string UpdateProfile = Default + "/student/update";
        public const string UploadAvatar = Default + "/student/upload-avatar";
        public const string GetProfile = Default + "/student/get";
        public const string ViewMark = Default + "/student/view-mark";
    }

    public class CommonRoute : Root
    {
        public const string GetProvince = Base + "/province/get";
        public const string GetDistrict = Base + "/district/get";
        public const string GetTown = Base + "/town/get";
        public const string GetHighSchool = Base + "/high-school/get";
        public const string GetEthnic = Base + "/ethnic/get";

        public const string ListProvince = Base + "/province/list";
        public const string ListDistrict = Base + "/district/list";
        public const string ListTown = Base + "/town/list";
        public const string ListHighSchool = Base + "/high-school/list";
        public const string ListEthnic = Base + "/ethnic/list";

        public const string GetUniversity = Base + "/university/get";
        public const string ListUniversity = Base + "/university/list";

        public const string GetMajors = Base + "/majors/get";
        public const string ListMajors = Base + "/majors/list";

        public const string GetSubjectGroup = Base + "/subject-group/get";
        public const string ListSubjectGroup = Base + "/subject-group/list";

        public const string GetUniversity_Majors = Base + "/university-majors/get";
        public const string ListUniversity_Majors = Base + "/university-majors/list";

        public const string GetUniversity_Majors_SubjectGroup = Base + "/university-majors-subject-group/get";
        public const string ListUniversity_Majors_SubjectGroup = Base + "/university-majors-subject-group/list";
    }

    [Authorize]
    [Authorize(Policy = "Permission")]
    public class ApiController : ControllerBase
    {

    }
}
