using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IFormRepository
    {
        Task<bool> Approve(Form form);
        Task<bool> Create(Form form);
        Task<Form> Get(Guid StudentId);
        Task<bool> Update(Form form);
        Task<bool> Delete(Guid Id);
    }
    public class FormRepository : IFormRepository
    {
        private readonly TFContext tFContext;
        private ICurrentContext CurrentContext;
        public FormRepository(TFContext tFContext, ICurrentContext CurrentContext)
        {
            this.tFContext = tFContext;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> Create(Form form)
        {
            FormDAO formDAO = new FormDAO
            {
                Id = form.Id,
                Graduated = form.Graduated,
                ClusterContestId = form.ClusterContestId,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                Maths = form.Maths,
                Literature = form.Literature,
                Languages = form.Languages,
                NaturalSciences = form.NaturalSciences,
                SocialSciences = form.SocialSciences,
                Physics = form.Physics,
                Chemistry = form.Chemistry,
                Biology = form.Biology,
                History = form.History,
                Geography = form.Geography,
                CivicEducation = form.CivicEducation,

                ExceptLanguages = form.ExceptLanguages,
                Mark = form.Mark,
                ReserveMaths = form.ReserveMaths,
                ReserveLiterature = form.ReserveLiterature,
                ReserveLanguages = form.ReserveLanguages,
                ReservePhysics = form.ReservePhysics,
                ReserveChemistry = form.ReserveChemistry,
                ReserveBiology = form.ReserveBiology,
                ReserveHistory = form.ReserveHistory,
                ReserveGeography = form.ReserveGeography,
                ReserveCivicEducation = form.ReserveCivicEducation,

                PriorityType = form.PriorityType,
                Area = form.Area,
                Status = form.Status,
                StudentId = CurrentContext.StudentId,
            };

            tFContext.Form.Add(formDAO);
            if (form.Aspirations.Any())
            {
            await BulkCreateAspirations(form);
            }
            //else
            //{
            //    form.Aspirations = new List<Aspiration>()
            //}
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkCreateAspirations(Form form)
        {
            await tFContext.Aspiration.Where(d => d.FormId.Equals(form.Id)).DeleteFromQueryAsync();
            if(form.Aspirations != null)
            {
                List<AspirationDAO> aspirationDAOs = form.Aspirations.Select(a => new AspirationDAO
                {
                    Id = a.Id,
                    Sequence = a.Sequence,
                    FormId = a.FormId,
                    MajorsId = a.MajorsId,
                    UniversityId = a.UniversityId,
                    SubjectGroupId = a.SubjectGroupId
                }).ToList();
                await tFContext.Aspiration.AddRangeAsync(aspirationDAOs);
            }
            
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Aspiration.Where(d => d.FormId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Form.Where(b => b.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Form> Get(Guid StudentId)
        {
            Form form = await tFContext.Form.Where(f => f.StudentId.Equals(StudentId)).Include(f => f.Aspirations).Select(f => new Form
            {
                Id = f.Id,
                StudentId = f.StudentId,

                Graduated = f.Graduated,
                ClusterContestId = f.ClusterContestId,
                ClusterContestCode = f.ClusterContest.Code,
                ClusterContestName = f.ClusterContest.Name,
                RegisterPlaceOfExamId = f.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = f.RegisterPlaceOfExam.Code,
                RegisterPlaceOfExamName = f.RegisterPlaceOfExam.Name,
                Biology = f.Biology,
                Chemistry = f.Chemistry,
                CivicEducation = f.CivicEducation,
                Geography = f.Geography,
                History = f.History,
                Languages = f.Languages,
                Literature = f.Literature,
                Maths = f.Maths,
                NaturalSciences = f.NaturalSciences,
                Physics = f.Physics,
                SocialSciences = f.SocialSciences,

                ExceptLanguages = f.ExceptLanguages,
                Mark = f.Mark,
                ReserveBiology = f.ReserveBiology,
                ReserveChemistry = f.ReserveChemistry,
                ReserveCivicEducation = f.ReserveCivicEducation,
                ReserveGeography = f.ReserveGeography,
                ReserveHistory = f.ReserveHistory,
                ReserveLanguages = f.ReserveLanguages,
                ReserveLiterature = f.ReserveLiterature,
                ReserveMaths = f.ReserveMaths,
                ReservePhysics = f.ReservePhysics,

                Area = f.Area,
                PriorityType = f.PriorityType,
                Status = f.Status,
                Aspirations = f.Aspirations.Select(a => new Aspiration
                {
                    Id = a.Id,
                    FormId = a.FormId,
                    MajorsId = a.MajorsId,
                    MajorsCode = a.Majors.Code,
                    MajorsName = a.Majors.Name,
                    UniversityId = a.UniversityId,
                    UniversityCode = a.University.Code,
                    UniversityName = a.University.Name,
                    UniversityAddress = a.University.Address,
                    SubjectGroupId = a.SubjectGroupId,
                    SubjectGroupCode = a.SubjectGroup.Code,
                    SubjectGroupName = a.SubjectGroup.Name,
                    Sequence = a.Sequence
                }).OrderBy(a => a.Sequence).ToList(),
            }).FirstOrDefaultAsync();

            return form;
        }

        public async Task<bool> Update(Form form)
        {
            await tFContext.Form.Where(f => f.Id.Equals(form.Id)).UpdateFromQueryAsync(f => new FormDAO
            {
                Id = f.Id,

                Graduated = form.Graduated,
                ClusterContestId = form.ClusterContestId,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                Maths = form.Maths,
                Literature = form.Literature,
                Languages = form.Languages,
                Physics = form.Physics,
                Chemistry = form.Chemistry,
                Biology = form.Biology,
                History = form.History,
                Geography = form.Geography,
                CivicEducation = form.CivicEducation,
                NaturalSciences = form.NaturalSciences,
                SocialSciences = form.SocialSciences,

                Mark = form.Mark,
                ExceptLanguages = form.ExceptLanguages,
                ReserveMaths = form.ReserveMaths,
                ReserveLiterature = form.ReserveLiterature,
                ReserveLanguages = form.ReserveLanguages,
                ReservePhysics = form.ReservePhysics,
                ReserveChemistry = form.ReserveChemistry,
                ReserveBiology = form.ReserveBiology,
                ReserveHistory = form.ReserveHistory,
                ReserveGeography = form.ReserveGeography,
                ReserveCivicEducation = form.ReserveCivicEducation,

                Area = form.Area,
                PriorityType = form.PriorityType,
                Status = form.Status
            });
            await BulkCreateAspirations(form);

            return true;
        }

        public async Task<bool> Approve(Form form)
        {
            await tFContext.Form.Where(f => f.Id == form.Id).UpdateFromQueryAsync(f => new FormDAO
            {
                Status = form.Status
            });
            return true;
        }
    }
}
