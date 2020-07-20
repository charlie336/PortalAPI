using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;
namespace LIMSWebPortalAPIApp.Contracts
{
    public class IContactRepository : IRepositoryBase<ContactModel>
    {
        public Task<bool> Create(ContactModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(ContactModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ContactModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<ContactModel> FindAllById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ContactModel> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> isExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ContactModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
