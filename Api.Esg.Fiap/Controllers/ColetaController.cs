using Api.Esg.Fiap.ViewModel;
using Api.Esg.Fiap.Models;
using Api.Esg.Fiap.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Esg.Fiap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColetaController : ControllerBase
    {
        private readonly IColetaService _service;
        private readonly IMapper _mapper;

        public ColetaController(IColetaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ColetaViewModel>> Get()
        {
            var coletas = _service.ListarColetas();
            var viewModelList = _mapper.Map<IEnumerable<ColetaViewModel>>(coletas);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public ActionResult<ColetaViewModel> Get(int id)
        {
            var coleta = _service.ObterColetaPorId(id);
            if (coleta == null)
                return NotFound();

            var viewModel = _mapper.Map<ColetaViewModel>(coleta);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ColetaCreateViewModel viewModel)
        {
            var coleta = _mapper.Map<ColetaModel>(viewModel);
            _service.CriarColeta(coleta);
            return CreatedAtAction(nameof(Get), new { id = coleta.ColetaId }, coleta);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ColetaCreateViewModel viewModel)
        {
            var coletaExistente = _service.ObterColetaPorId(id);
            if (coletaExistente == null)
                return NotFound();

            _mapper.Map(viewModel, coletaExistente);
            _service.AtualizarColeta(coletaExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeletarColeta(id);
            return NoContent();
        }
    }
}
