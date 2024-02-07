﻿using AutoMapper;
using Entities.DataTransferObjects;
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
        public ServiceManager(IRepositoryManager repositoryManager,ILoggerService logger,IMapper mapper,IDataShaper<BookDto> shaper)
        {
            _bookServices = new Lazy<IBookServices>(() => new BookManager(repositoryManager,logger, mapper,shaper));
        }
        public IBookServices BookServices => _bookServices.Value; 
    }
}
