using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MediaMonkey.Models;
using MediaMonkey.DataAccess.Models;

namespace MediaMonkey.Controllers
{
    [Route("File")]
    public class FileController : Controller
    {
        private readonly AppConfig _appConfig;

        private readonly DataService _dataService;

        public FileController(IOptions<AppConfig> appConfig, DataContext dataContext){
            _appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        // GET: /File/
        [HttpGet, Route("")]
        public ActionResult Index(int id)
        {
            var fileToRetrieve = _dataService.GetAvatar(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}