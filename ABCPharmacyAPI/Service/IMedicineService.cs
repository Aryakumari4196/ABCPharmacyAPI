using ABCPharmacyAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ABCPharmacyAPI.Service
{
    public interface IMedicineService
    {
        Task<(List<Medicine> Data, int TotalCount)> GetAllMedicinesAsync(string? name, int pageNumber, int pageSize);

        Task<Medicine> AddMedicine(Medicine medicine);
    }
}
