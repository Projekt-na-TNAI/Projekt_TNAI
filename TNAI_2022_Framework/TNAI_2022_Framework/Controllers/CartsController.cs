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
    public class CartsController : ApiController
    {
        private readonly ICartRepository _CartRepository;
        private readonly IMapper _mapper;

        public CartsController(ICartRepository CartRepository, IMapper mapper)
        {
            _CartRepository = CartRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Pobierz wybrany koszyk
        /// </summary>
        /// <param name="id">Identyfikator koszyka</param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Description = "Found Cart")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Couldn't find Cart")]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id");

            var Cart = await _CartRepository.GetCartAsync(id);
            if (Cart == null)
                return NotFound();

            var result = _mapper.Map<CartOutputModel>(Cart);

            return Ok(result);
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var Carts = await _CartRepository.GetAllCartsAsync();
            if (Carts == null)
                return NotFound();

            var result = _mapper.Map<List<CartOutputModel>>(Carts);

            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(CartInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Cart = new Cart()
            {
            };

            var result = await _CartRepository.SaveCartAsync(Cart);
            if (!result)
                return InternalServerError();

            var CartOutputModel = _mapper.Map<CartOutputModel>(Cart);

            return Ok(CartOutputModel);
        }

        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] CartInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Cart = await _CartRepository.GetCartAsync(id);
            if (Cart == null)
                return NotFound();

            var result = await _CartRepository.SaveCartAsync(Cart);
            if (!result)
                return InternalServerError();

            var CartOutputModel = _mapper.Map<CartOutputModel>(Cart);

            return Ok(CartOutputModel);
        }

        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = await _CartRepository.DeleteCartAsync(id);

            return Ok(result);
        }
    }
}
