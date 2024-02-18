using System;
using Hygie.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Hygie.Infrastructure.Common.Interfaces
{
    public interface IPatientService
    {
        Task<bool> SetHospitalExitDateAsync(string userId, DateTime exitDate);

        Task<DateTime?> GetHospitalExitDateAsync(string userId);

        Task<List<Document>> GetDocumentsAsync(string userId);

        Task<bool> AddDocument(string userId, IFormFile file, string type, DateTime? expirationDate);
    }
}

