using AppOwnsData.Models;
using AppOwnsData.Models.ViewModels;
using AppOwnsData.Services;
using AppOwnsData.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AppOwnsData.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        private readonly PowerBiServiceApi _powerBiServiceApi;

        public SellersController(SellerService sellerService, DepartmentService departmentService, PowerBiServiceApi powerBiServiceApi)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
            _powerBiServiceApi = powerBiServiceApi;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return  RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        [AllowAnonymous]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [AllowAnonymous]
        public IActionResult Error(string messsage)
        {
            var viewModel = new ErrorViewModel
            {
                Message = messsage,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Embed()
        {


            Guid workspaceId = new Guid("074ed360-d99c-4ff0-961a-9471d6fad229");
            Guid reportId = new Guid("86957652-2a88-4657-9029-11624caa9521");

            var viewModel = await _powerBiServiceApi.GetReport(workspaceId, reportId);

            return View(viewModel);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
