using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IBookServices _bookServices;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICategoryService _categoryService;

        public ServiceManager(IBookServices bookServices, IAuthenticationService authenticationService, ICategoryService categoryService)
        {
            _bookServices = bookServices;
            _authenticationService = authenticationService;
            _categoryService = categoryService;
        }

        public IBookServices BookServices => _bookServices;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public ICategoryService CategoryServices => _categoryService;
    }
}
