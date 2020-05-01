using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Form : DataEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }

        public bool? Graduated { get; set; }        //Đã tốt nghiệp THPT/TTGDTX?
        public Guid ClusterContestId { get; set; }      //Cụm thi(tỉnh/thành phố)
        public string ClusterContestCode { get; set; }
        public string ClusterContestName { get; set; }
        public Guid RegisterPlaceOfExamId { get; set; }     //Địa điểm đăng kí thi(trường THPT)
        public string RegisterPlaceOfExamCode { get; set; }
        public string RegisterPlaceOfExamName { get; set; }

        //Các môn dự thi
        public bool? Maths { get; set; }
        public bool? Literature { get; set; }
        public string Languages { get; set; }
        public bool? NaturalSciences { get; set; }      //Tổ hợp KHTN
        public bool? SocialSciences { get; set; }       //Tổ hợp KHXH
        public bool? Physics { get; set; }
        public bool? Chemistry { get; set; }
        public bool? Biology { get; set; }
        public bool? History { get; set; }
        public bool? Geography { get; set; }
        public bool? CivicEducation { get; set; }

        //Phần điểm miễn thi ngoại ngữ (nếu có)
        public string ExceptLanguages { get; set; }
        //Các môn học muốn bảo lưu điểm
        public int? Mark { get; set; }
        public int? ReserveMaths { get; set; }
        public int? ReservePhysics { get; set; }
        public int? ReserveChemistry { get; set; }
        public int? ReserveLiterature { get; set; }
        public int? ReserveHistory { get; set; }
        public int? ReserveGeography { get; set; }
        public int? ReserveBiology { get; set; }
        public int? ReserveCivicEducation { get; set; }
        public int? ReserveLanguages { get; set; }

        public string PriorityType { get; set; }        //Đối tượng ưu tiên
        public string Area { get; set; }        //Khu vực tuyển sinh
        public int Status { get; set; }
        
        public List<Aspiration> Aspirations { get; set; }       //Danh sách các nguyện vọng thi sinh đăng kí
    }

   
}
