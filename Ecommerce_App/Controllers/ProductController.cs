using AutoMapper;
using Ecommerce_App.Services.IServices;
using ECommerce_App;
using ECommerce_App.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce_App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductservice _pdtservice;
        private readonly IMapper _mapper;
        public ProductController(IProductservice pdtservice, IMapper mapper)
        {
            _pdtservice = pdtservice;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexProduct()
        {
            List<ItemsDTO> list = new();
            var response = await _pdtservice.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ItemsDTO>>(Convert.ToString(response.result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ItemCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _pdtservice.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProduct));
                }

            }
            return View(model);
        }
        public async Task<IActionResult> UpdateProduct(int villaId)
        {
            var response = await _pdtservice.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                ItemsDTO model = JsonConvert.DeserializeObject<ItemsDTO>(Convert.ToString(response.result));
                return View(_mapper.Map<ItemUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ItemUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _pdtservice.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProduct));
                }

            }
            return View(model);
        }
        public async Task<IActionResult> DeleteProduct(int villaId)
        {
            var response = await _pdtservice.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                ItemsDTO model = JsonConvert.DeserializeObject<ItemsDTO>(Convert.ToString(response.result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(ItemsDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _pdtservice.DeleteAsync<APIResponse>(model.Id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProduct));
                }

            }
            return View(model);
        }
    }
}
