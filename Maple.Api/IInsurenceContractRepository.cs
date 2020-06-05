using Maple.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maple.Api
{
    public interface IInsurenceContractRepository
    {
        IEnumerable<ContractsModel> GetAll();
    }
    public class InsurenceContractRepository : IInsurenceContractRepository
    {
        private readonly DatabaseContext _context;


        public InsurenceContractRepository(DatabaseContext context)
        {
            _context = context;
        }
        public IEnumerable<ContractsModel> GetAll()
        {
            return _context.Contracts;
        }
    }
}
