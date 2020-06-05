using Maple.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maple.Api
{
    public interface IInsurenceContractService
    {
        IAsyncEnumerable<ContractsModel> GetAll();
        Task Save(ContractDTO dto);
        Task Update(ContractDTO dto);
        Task Delete(ContractDTO dto);
    }

    public class InsurenceContractService : IInsurenceContractService
    {
        private readonly IInsurenceContractRepository _repository;
        public InsurenceContractService(IInsurenceContractRepository repository)
        {
            _repository = repository;
        }
        public async IAsyncEnumerable<ContractsModel> GetAll()
        {
            await foreach (var item in _repository.GetAll())
                yield return item;
        }

        public async Task Save(ContractDTO dto)
        {            
            await _repository.Save(dto);
        }

        public Task Update(ContractDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(ContractDTO dto)
        {
            await _repository.Delete(dto);
        }

    }
}
