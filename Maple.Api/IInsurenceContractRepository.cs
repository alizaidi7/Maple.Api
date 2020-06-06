using Maple.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maple.Api
{
    public interface IInsurenceContractRepository
    {
        IAsyncEnumerable<ContractsModel> GetAll();
        Task Save(ContractDTO dto);
        Task Update(ContractDTO dto);
        Task Delete(ContractDTO dto);
    }
    public class InsurenceContractRepository : IInsurenceContractRepository
    {
        private readonly DatabaseContext _context;


        public InsurenceContractRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Update(ContractDTO dto)
        {
            var item = _context.Contracts.FirstOrDefault(o => o.Address == dto.Address && o.Country == dto.Country &&
                                 o.DateOfBirth == dto.DateOfBirth && o.Gender == dto.Gender && o.SaleDate == dto.SaleDate && o.Name == dto.Name);
            if (item != null)
            {
                item.CoveragePlan = GetCoveragePlan(dto.Country, dto.SaleDate);
                item.NetPrice = GetNetRate(dto.DateOfBirth, dto.Gender, item.CoveragePlan);
                await _context.SaveChangesAsync();
            }
        }


        public async Task Delete(ContractDTO dto)
        {
            var item = _context.Contracts.FirstOrDefault(o => o.Address == dto.Address && o.Country == dto.Country &&
                                 o.DateOfBirth == dto.DateOfBirth && o.Gender == dto.Gender && o.SaleDate == dto.SaleDate && o.Name == dto.Name);
            if (item != null)
            {
                _context.Contracts.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async IAsyncEnumerable<ContractsModel> GetAll()
        {
            await foreach (var item in _context.Contracts)
                yield return item;
        }

        private string GetCoveragePlan(string country, DateTime saleDate)
        {
            return _context.CoveragePlan.SingleOrDefault(o => o.EligibilityCountry== country
                     && o.EligibilityDateTo >= saleDate && o.EligibilityDateFrom <= saleDate)?.CoveragePlan
                     ?? _context.CoveragePlan.Single(o => o.EligibilityCountry == "*").CoveragePlan;
        }

        private decimal GetNetRate(DateTime dob, string gender, string coveragePlan)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dob.Year;
            decimal rate = 0.0m;
            if ((dob.Month == currentDate.Month && currentDate.Day < dob.Day) || currentDate.Month < dob.Month)
            {
                age--;
            }
            if (coveragePlan == "Gold" && gender == "M" && age <= 40) rate = 1000;
            if (coveragePlan == "Gold" && gender == "M" && age > 40) rate = 2000;
            if (coveragePlan == "Gold" && gender == "F" && age <= 40) rate = 1200;
            if (coveragePlan == "Gold" && gender == "F" && age > 40) rate = 2500;

            if (coveragePlan == "Silver" && gender == "M" && age <= 40) rate = 1500;
            if (coveragePlan == "Silver" && gender == "M" && age > 40) rate = 2600;
            if (coveragePlan == "Silver" && gender == "F" && age <= 40) rate = 1900;
            if (coveragePlan == "Silver" && gender == "F" && age > 40) rate = 2800;

            if (coveragePlan == "Platinum" && gender == "M" && age <= 40) rate = 1900;
            if (coveragePlan == "Platinum" && gender == "M" && age > 40) rate = 2900;
            if (coveragePlan == "Platinum" && gender == "F" && age <= 40) rate = 2100;
            if (coveragePlan == "Platinum" && gender == "F" && age > 40) rate = 3200;
            return rate;
        }

        public async Task Save(ContractDTO dto)
        {
            var contract = new ContractsModel
            {
                Address = dto.Address,
                Country = dto.Country,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                SaleDate = dto.SaleDate,
                Name = dto.Name,
            };
            contract.CoveragePlan = GetCoveragePlan(dto.Country, dto.SaleDate);
            contract.NetPrice = GetNetRate(dto.DateOfBirth, dto.Gender, contract.CoveragePlan);
            contract.Id = _context.Contracts.Count() + 1;
            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();
        }
    }
}
