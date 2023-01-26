using AutoMapper;

using Swashbuckle.Swagger.Annotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using TNAI.Model.Entities;
using TNAI.Repository.Abstract;

using TNAI_2022_Framework.Models.InputModels;
using TNAI_2022_Framework.Models.OutputModels;

namespace TNAI_2022_Framework.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository OrderRepository, IMapper mapper)
        {
            _OrderRepository = OrderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Pobierz wybrane zamówienie
        /// </summary>
        /// <param name="id">Identyfikator zamówienia</param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Description = "Found order")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Couldn't find order")]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id");

            var Order = await _OrderRepository.GetOrderAsync(id);
            if (Order == null)
                return NotFound();

            var result = _mapper.Map<OrderOutputModel>(Order);

            return Ok(result);
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var Orders = await _OrderRepository.GetAllOrdersAsync();
            if (Orders == null)
                return NotFound();

            var result = _mapper.Map<List<OrderOutputModel>>(Orders);

            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(OrderInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Order = new Order()
            {
                Name = inputModel.Name
            };

            var result = await _OrderRepository.SaveOrderAsync(Order);
            if (!result)
                return InternalServerError();

            var OrderOutputModel = _mapper.Map<OrderOutputModel>(Order);

            return Ok(OrderOutputModel);
        }

        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] OrderInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Order = await _OrderRepository.GetOrderAsync(id);
            if (Order == null)
                return NotFound();

            Order.Name = inputModel.Name;

            var result = await _OrderRepository.SaveOrderAsync(Order);
            if (!result)
                return InternalServerError();

            var OrderOutputModel = _mapper.Map<OrderOutputModel>(Order);

            return Ok(OrderOutputModel);
        }

        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = await _OrderRepository.DeleteOrderAsync(id);

            return Ok(result);
        }
    }
}
