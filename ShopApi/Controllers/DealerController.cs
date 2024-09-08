using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using DtoLayer.DealerDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DealerController : ControllerBase
{
    private readonly IDealerService _dealerService;
    private readonly IMapper _mapper;
    private readonly string _uiRootPath;

    public DealerController(IDealerService dealerService, IMapper mapper, IConfiguration configuration)
    {
        _dealerService = dealerService;
        _mapper = mapper;
        _uiRootPath = configuration["UIRootPath"];
    }

    [HttpGet]
    public IActionResult DealerList()
    {
        var dealers = _dealerService.TGetListAll();

        var value = _mapper.Map<List<ResultDealerDto>>(dealers);
        return Ok(value);
    }

    [HttpGet("DealerListWithCategory")]
    public IActionResult DealerListWithCategory()
    {
        var context = new ShopContext();
        var values = context.Dealers.Include(x => x.DealerCategory).Select(y => new ResultDealerWithCategory
        {
            DealerID = y.DealerID,
            DealerName = y.DealerName,
            DealerAddress = y.DealerAddress,
            DealerOwner = y.DealerOwner,
            DealerCity = y.DealerCity,
            CategoryName = y.DealerCategory.CategoryName,
            DealerDistrict = y.DealerDistrict,
            Phone1 = y.Phone1,
            Phone2 = y.Phone2
        }).ToList();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateDealer(CreateDealerDto createDealerDto)
    {
        try
        {
            var dealer = new Dealer
            {
                DealerName = createDealerDto.DealerName,
                DealerOwner = createDealerDto.DealerOwner,
                DealerAddress = createDealerDto.DealerAddress,
                DealerDistrict = createDealerDto.DealerDistrict,
                DealerCity = createDealerDto.DealerCity,
                Phone1 = createDealerDto.Phone1,
                Phone2 = createDealerDto.Phone2,
                DealerCategoryID = createDealerDto.DealerCategoryID
            };

            _dealerService.TAdd(dealer);
            return Ok("Firma Bilgisi Eklendi");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"İşlem başarısız: {ex.Message}");
        }
    }
   


  
    [HttpDelete("{id}")]
    public IActionResult DeleteDealer(int id)
    {
        var dealer = _dealerService.TGetByID(id);
        if (dealer == null)
            return NotFound("Firma bulunamadı");

        _dealerService.TDelete(dealer);
        return Ok("Firma Bilgisi Silindi");
    }

    [HttpGet("{id}")]
    public IActionResult GetDealer(int id)
    {
        var dealer = _dealerService.TGetByID(id);
        if (dealer == null)
            return NotFound("Firma bulunamadı");

        return Ok(dealer);
    }

    [HttpPut]
    public IActionResult UpdateDealer(UpdateDealerDto updateDealerDto)
    {
        var dealer = _dealerService.TGetByID(updateDealerDto.DealerID);
        if (dealer == null)
            return NotFound("Firma bulunamadı");

        dealer.DealerName = updateDealerDto.DealerName;
        dealer.DealerOwner = updateDealerDto.DealerOwner;
        dealer.DealerAddress = updateDealerDto.DealerAddress;
        dealer.DealerDistrict = updateDealerDto.DealerDistrict;
        dealer.DealerCity = updateDealerDto.DealerCity;
        dealer.Phone1 = updateDealerDto.Phone1;
        dealer.Phone2 = updateDealerDto.Phone2;
        dealer.DealerCategoryID = updateDealerDto.DealerCategoryID;
        
        _dealerService.TUpdate(dealer);
        return Ok("Firma Bilgisi Güncellendi");
    }
}
