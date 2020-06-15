using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Models;
using LIMSWebPortalAPIApp.Data.DTOs;

namespace LIMSWebPortalAPIApp.Data.Mappings
{
    public class Maps:Profile
    {
        public Maps()
        {
            CreateMap<CustomerModel, CustomerDTO>().ReverseMap();
            CreateMap<ProjectModel, ProjectDTO>().ReverseMap();
            CreateMap<ContractQuoteModel, ContractQuoteDTO>().ReverseMap();
            CreateMap<ContactModel, ContactDTO>().ReverseMap();
        }
    }
}
