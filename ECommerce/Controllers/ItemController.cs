using AutoMapper;
using Azure;
using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Models.DTO;
using ECommerce.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.Controllers
{
    [Route("api/Item")]
    [ApiController]
    
    public class ItemController : ControllerBase 
    {
        private readonly IItemUpdateRepository _item;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public ItemController(IItemUpdateRepository item,IMapper mapper)
        {
            _item = item;
            _mapper=mapper;
            this._response = new ();
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<APIResponse>> Getdetails()
        {
            try
            {
                IEnumerable<Items> itemlist = await _item.GetAllAsync();
                _response.result = _mapper.Map<List<ItemsDTO>>(itemlist);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Item Details";
                return Ok(_response);
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("{Id:int}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Getdetails(int Id)
        {
            try
            {
                if (Id == 0)
                {

                    return BadRequest();
                }
                var v = await _item.GetAsync(u => u.Id == Id);
                if (v == null)
                {
                    return NotFound();
                }
                _response.result = _mapper.Map<ItemsDTO>(v);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Items By Id";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Createdetails(ItemCreateDTO vd)
        {
            try
            {
                if (await _item.GetAsync(u => u.Name.ToLower() == vd.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessage", "Already Exists");
                    return BadRequest(ModelState);
                }
                if (vd == null)
                {
                    return BadRequest();
                }
                Items model = _mapper.Map<Items>(vd);
                await _item.CreateAsync(model);
                _response.result = _mapper.Map<ItemsDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Message = "New Orders";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;


        }
        [HttpDelete("{Id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Deletedetails(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return BadRequest();
                }
                var v = await _item.GetAsync(v => v.Id == Id);
                if (v == null)
                {
                    return NotFound();
                }
                await _item.RemoveAsync(v);
                _response.result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.Message = "Items Deleted";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{Id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDetails(int Id, ItemUpdateDTO vd)
        {
            try
            {
                if (vd == null || Id != vd.Id)
                {
                    return BadRequest();
                }
                Items model = _mapper.Map<Items>(vd);
                await _item.UpdateAsync(model);
                _response.result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.Message = "Updated Items";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }




    }
}
