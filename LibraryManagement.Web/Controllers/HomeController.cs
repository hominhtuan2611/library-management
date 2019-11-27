﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Web.Models;
using LibraryManagement.API.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using LibraryManagement.Application.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace LibraryManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryDBContext _context;

        public IConfiguration _configuration;
        private readonly IHostingEnvironment _appEnvironment;

        private HttpClient _apiService;
        private readonly string apiAddress;

        public HomeController(LibraryDBContext context, IConfiguration configuration, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _appEnvironment = appEnvironment;

            apiAddress = _configuration.GetSection("ApiAddress").GetSection("Url").Value;
            _apiService = ApiService.GetAPI(apiAddress);
        }
        public async Task<IActionResult> Index()
        {
            var loaisach = await _apiService.GetAsync("api/Loaisach").Result.Content.ReadAsAsync<List<LoaiSach>>();
            var list_sach = new List<Sach>();
            var sach = new Sach();
            var tuple = new Tuple<List<LibraryManagement.API.Models.LoaiSach>, List<LibraryManagement.API.Models.Sach>, LibraryManagement.API.Models.Sach>(loaisach, list_sach, sach);
            return View(tuple);
        }
        public async Task<IActionResult> Login()
        {
            var loaisach = await _apiService.GetAsync("api/Loaisach").Result.Content.ReadAsAsync<List<LoaiSach>>();
            var list_sach = new List<Sach>();
            var sach = new Sach();
            var tuple = new Tuple<List<LibraryManagement.API.Models.LoaiSach>, List<LibraryManagement.API.Models.Sach>, LibraryManagement.API.Models.Sach>(loaisach, list_sach, sach);
            return View(tuple);
        }

        public async void CheckLogin(string name, string pass)
        {
            HttpContext.Session.SetString(CommonConstants.Docgia_Session,"");
            string password = Password_Encryptor.HashSHA1(pass);
            var docGia = _context.DocGia.Where(m => m.Username == name && m.Password == pass).FirstOrDefault();
            if (docGia == null)
            {
                Response.Redirect("Error");
            }
            else
            {
                HttpContext.Session.SetObject<int>(CommonConstants.Docgia_Session, docGia.Id);
                Response.Redirect("index");
            }
            /*return Index();*/
            //return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
