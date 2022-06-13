using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
    {
        _platformRepo = platformRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
    {
        Console.WriteLine("=--> Getting Platforms...");

        var platformService = await _platformRepo.GetAllPlatformsAsync();

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformService));
    }

    [HttpGet("{id:int}", Name = "GetPlatformById")]
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
    {
        var platformItem = await _platformRepo.GetPlatformByIdAsync(id);

        if (platformItem is not null)
        {
            return Ok(_mapper.Map<PlatformReadDto>(platformItem));
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        await _platformRepo.CreatePlatformAsync(platformModel);
        await _platformRepo.SaveChangesAsync();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
        return CreatedAtRoute(
            nameof(GetPlatformById), new {Id = platformModel.Id}, platformReadDto);
    }
}