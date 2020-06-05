using Maple.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maple.Api
{
    public interface IInsurenceContractService
    {
        IEnumerable<ContractsModel> GetAll();
    }

    public class InsurenceContractService : IInsurenceContractService
    {
        private readonly IInsurenceContractRepository _repository;
        public InsurenceContractService(IInsurenceContractRepository repository )
        {
            _repository = repository;
        }
        public IEnumerable<ContractsModel> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
