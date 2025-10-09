using Api.Esg.Fiap.Models;
using Api.Esg.Fiap.Services;
using Api.Esg.Fiap.ViewModel;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Esg.Fiap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResiduoController : ControllerBase
    {
        private readonly IResiduoService _service;
        private readonly IMapper _mapper;

        public ResiduoController(IResiduoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResiduoViewModel>> Get()
        {
            var residuo = _service.ListarResiduos();
            var viewModelList = _mapper.Map<IEnumerable<ResiduoViewModel>>(residuo);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public ActionResult<ResiduoViewModel> Get(int id)
        {
            var residuo = _service.ObterResiduoPorId(id);
            if (residuo == null)
                return NotFound();

            var viewModel = _mapper.Map<ResiduoViewModel>(residuo);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ResiduoCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var residuo = _mapper.Map<ResiduoModel>(viewModel);
            _service.CriarResiduo(residuo);
            return CreatedAtAction(nameof(Get), new { id = residuo.ResiduoId }, viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ResiduoCreateViewModel viewModel)
        {
            var residuoExistente = _service.ObterResiduoPorId(id);
            if (residuoExistente == null)
                return NotFound();

            _mapper.Map(viewModel, residuoExistente);
            _service.AtualizarResiduo(residuoExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeletarResiduo(id);
            return NoContent();
        }
    }
}
