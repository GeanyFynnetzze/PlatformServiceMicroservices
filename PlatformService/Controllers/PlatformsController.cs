using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private IMapper _mapper;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult <IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("-----Getting Platform-----");

            var platformItem = _platformRepo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platformItem));
        }
        [HttpGet("{id}", Name = "GetPlatformById")]

        public ActionResult<PlatformReadDTO> GetPlatformById(int id)
        {
            Console.WriteLine("-----Getting One Platform-----");

            var platformItem = _platformRepo.GetPlatformById(id);

            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDTO>(platformItem));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult <PlatformReadDTO> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            Console.WriteLine("-----Creating Platform-----");

            var platformModel = _mapper.Map<Platform>(platformCreateDTO);
            _platformRepo.CreatePlatform(platformModel);
            _platformRepo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDTO>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platformReadDto.Id}, platformReadDto);
        }
    }
}
