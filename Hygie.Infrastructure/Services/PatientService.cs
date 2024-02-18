using System;
using System.Linq.Expressions;
using Azure.Core;
using Hygie.Core.Entities;
using Hygie.Core.Repositories.Command.Base;
using Hygie.Core.Repositories.Query.Base;
using Hygie.Infrastructure.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Hygie.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IQueryRepository<PatientExitDate> _patientExitDateQueryRepository;
        private readonly IQueryRepository<Document> _documentQueryRepository;
        private readonly ICommandRepository<PatientExitDate> _patientExitDateCommandRepository;
        private readonly ICommandRepository<Document> _documentCommandRepository;

        public PatientService(IQueryRepository<PatientExitDate> patientExitDateQueryRepository,
            ICommandRepository<PatientExitDate> patientExitDateCommandRepository,
            IQueryRepository<Document> documentQueryRepository,
            ICommandRepository<Document> documentCommandRepository)
        {
            _patientExitDateQueryRepository = patientExitDateQueryRepository;
            _documentQueryRepository = documentQueryRepository;
            _patientExitDateCommandRepository = patientExitDateCommandRepository;
            _documentCommandRepository = documentCommandRepository; 
        }

        public async Task<bool> SetHospitalExitDateAsync(string userId, DateTime exitDate)
        {
            var existingPatientExitDate = await _patientExitDateQueryRepository.FindAsync(i => i.UserId == userId);

            if (existingPatientExitDate == null)
            {
                var patientExitDate = new PatientExitDate
                {
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    ExitDate = exitDate
                };

                await _patientExitDateCommandRepository.AddAsync(patientExitDate);
            }
            else
            {
                existingPatientExitDate!.ExitDate = exitDate;
                await _patientExitDateCommandRepository.UpdateAsync(existingPatientExitDate);
            }

            return true;
        }
        public async Task<bool> AddDocument(string userId, IFormFile file, string type, DateTime? expirationDate)
        {
            var existingDocument = await _documentQueryRepository.FindAsync(i => i.UserId == userId && i.Type == type);

            if(existingDocument == null) {
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                var document = new Document
                {
                    CreatedDate = DateTime.Now,
                    Data = fileBytes,
                    Name = file.FileName,
                    Size = file.Length,
                    Type = file.ContentType,
                    UserId = userId,
                    DocumentType = type,
                    ExpirationDate = expirationDate,
                    Verified = false,
                };

                await _documentCommandRepository.AddAsync(document);
            }
            else
            {
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                existingDocument.ExpirationDate = expirationDate;
                existingDocument.Data = fileBytes;
                existingDocument.Name = file.Name;
                existingDocument.Size = file.Length;
                existingDocument.Type = file.ContentType;
                existingDocument.Verified = false;

                await _documentCommandRepository.UpdateAsync(existingDocument);

            }

            return true;

        }
        public async Task<DateTime?> GetHospitalExitDateAsync(string userId)
        {
            var patientExitDate = await _patientExitDateQueryRepository.FindAsync(i => i.UserId == userId);
            return patientExitDate?.ExitDate;
        }

        public async Task<List<Document>> GetDocumentsAsync(string userId)
        {
            var document = await _documentQueryRepository.FindAllAsync(d => d.UserId == userId);
            return document;
        }
    }
}

