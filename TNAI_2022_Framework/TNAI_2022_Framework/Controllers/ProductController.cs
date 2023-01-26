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
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Pobierz wybrany produkt
        /// </summary>
        /// <param name="id">Identyfikator produktu</param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Description = "Found product")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Couldn't find product")]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id");

            var Product = await _ProductRepository.GetProductAsync(id);
            if (Product == null)
                return NotFound();

            var result = _mapper.Map<ProductOutputModel>(Product);

            return Ok(result);
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var Products = await _ProductRepository.GetAllProductsAsync();
            if (Products == null)
                return NotFound();

            var result = _mapper.Map<List<ProductOutputModel>>(Products);

            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(ProductInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Product = new Product()
            {
                Name = inputModel.Name,

                Price = inputModel.Price
            };

            var result = await _ProductRepository.SaveProductAsync(Product);
            if (!result)
                return InternalServerError();

            var ProductOutputModel = _mapper.Map<ProductOutputModel>(Product);

            return Ok(ProductOutputModel);
        }

        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] ProductInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Product = await _ProductRepository.GetProductAsync(id);
            if (Product == null)
                return NotFound();

            Product.Name = inputModel.Name;

            var result = await _ProductRepository.SaveProductAsync(Product);
            if (!result)
                return InternalServerError();

            var ProductOutputModel = _mapper.Map<ProductOutputModel>(Product);

            return Ok(ProductOutputModel);
        }

        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = await _ProductRepository.DeleteProductAsync(id);

            return Ok(result);
        }
    }
}
