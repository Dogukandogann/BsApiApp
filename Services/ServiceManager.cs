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
        private readonly Lazy<IBookServices> _bookServices;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper,IBookLinks bookLinks,UserManager<User> usermanager,IConfiguration configuration)
        {
            _bookServices = new Lazy<IBookServices>(() => new BookManager(repositoryManager,logger, mapper, bookLinks));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, usermanager, configuration));
        }
        public IBookServices BookServices => _bookServices.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
