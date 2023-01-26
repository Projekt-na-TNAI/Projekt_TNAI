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
    public class CategoriesController : ApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Pobierz wybraną kategorię
        /// </summary>
        /// <param name="id">Identyfikator kategorii</param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Description = "Found category")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Couldn't find category")]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id");

            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            var result = _mapper.Map<CategoryOutputModel>(category);

            return Ok(result);
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if (categories == null)
                return NotFound();

            var result = _mapper.Map<List<CategoryOutputModel>>(categories);

            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(CategoryInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = new Category()
            {
                Name = inputModel.Name
            };

            var result = await _categoryRepository.SaveCategoryAsync(category);
            if (!result)
                return InternalServerError();

            var categoryOutputModel = _mapper.Map<CategoryOutputModel>(category);

            return Ok(categoryOutputModel);
        }

        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] CategoryInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepository.GetCategoryAsync(id);
            if(category == null)
                return NotFound();

            category.Name = inputModel.Name;

            var result = await _categoryRepository.SaveCategoryAsync(category);
            if (!result)
                return InternalServerError();

            var categoryOutputModel = _mapper.Map<CategoryOutputModel>(category);

            return Ok(categoryOutputModel);
        }

        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = await _categoryRepository.DeleteCategoryAsync(id);

            return Ok(result);
        }
    }
}
