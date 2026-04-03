using ABCPharmacyAPI.Entities;
using System.Text.Json;

namespace ABCPharmacyAPI.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly string filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "medicine.json"
        );

        public async Task<(List<Medicine> Data, int TotalCount)> GetAllMedicinesAsync(string ? name, int pageNumber, int pageSize)
        {
            if (!File.Exists(filePath))
                return (new List<Medicine>(), 0);

            var json = await File.ReadAllTextAsync(filePath);

            var medicines = JsonSerializer.Deserialize<List<Medicine>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Medicine>();

            if (!string.IsNullOrEmpty(name))
            {
                medicines = medicines
                    .Where(m => m.FullName != null &&
                                m.FullName.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            // Validation
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Total count 
            var totalCount = medicines.Count;

            // Pagination
            var pagedData = medicines
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (pagedData, totalCount);
        }

        public async Task<Medicine> AddMedicine(Medicine medicine)
        {
            if (!File.Exists(filePath))
            {
                var emptyJson = JsonSerializer.Serialize(new List<Medicine>());
                await File.WriteAllTextAsync(filePath, emptyJson);
            }

            var json = await File.ReadAllTextAsync(filePath);

            var medicines = JsonSerializer.Deserialize<List<Medicine>>(json)
                            ?? new List<Medicine>();

            medicines.Add(medicine);

            var updatedJson = JsonSerializer.Serialize(medicines, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(filePath, updatedJson);

            return medicine;
        }

    }
}
