
using AutoMapper;
using Azure;
using ECommerce.Models;
using ECommerce.Models.DTO;
using ECommerce.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    [Route("api/Order")]
    [ApiController]

    public class OrderController : Controller
    {
        private readonly IRepositoryorder _orderRepository;
        private readonly IItemUpdateRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public OrderController(IRepositoryorder orderRepository, IMapper mapper, IItemUpdateRepository itemRepository,IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> GetOrders()
        {
            try
            {
                IEnumerable<Order> orders = await _orderRepository.GetOrdersAsync();
                List<OrderDTO> ordersDTO = _mapper.Map<List<OrderDTO>>(orders);

                _response.result = ordersDTO;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "All Orders";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("User/{username}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<APIResponse>> GetOrdersByUsernameAsync(string username)
        {
            try
            {
                var orders = await _userRepository.GetOrdersByUsernameAsync(username);
                var ordersDTO = _mapper.Map<List<OrderDTO>>(orders);

                _response.result = ordersDTO;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
                _response.Message = "My Orders";
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        //[HttpGet("User")]
        //[Authorize(Roles = "User")]
        //public async Task<List<Order>> GetOrdersByUsernameAsync(string username)
        //{
        //    return await _userRepository.GetOrdersByUsernameAsync(username);
        //}

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<APIResponse>> AddOrder(OrderCreateDTO newOrder)
        {
            try
            {
                if (newOrder == null)
                {
                    return BadRequest();
                }

                if (string.IsNullOrEmpty(newOrder.Username))
                {
                    return BadRequest("Username is required.");
                }

                
                int? userId = await _userRepository.GetUserIdByUsernameAsync(newOrder.Username);

                if (!userId.HasValue)
                {
                    return BadRequest($"User '{newOrder.Username}' not found.");
                }

                var order = _mapper.Map<Order>(newOrder);
                order.UserId = userId.Value;

                await _orderRepository.AddOrderAsync(order);
                _response.IsSuccess = true;
                _response.result = _mapper.Map<OrderDTO>(order);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Message = "Order Placed Successfully";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<APIResponse>> DeleteOrder(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                await _orderRepository.DeleteOrderAsync(id);
                _response.result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.Message = "Order Removed";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            

        }
    }
}

