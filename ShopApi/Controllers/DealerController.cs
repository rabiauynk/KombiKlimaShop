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

        // Her bir dealer için null olan ImageUrls kontrolü yapılıyor
        foreach (var dealer in dealers)
        {
            dealer.ImageUrls = dealer.ImageUrls ?? new List<string>();
        }

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
            ImageUrls = y.ImageUrls,
            Phone = y.Phone,
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
                Phone = createDealerDto.Phone,
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
    [HttpPost("{id}/upload")]
    public async Task<IActionResult> Upload(int id, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Dosya seçilmedi");

        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            return BadRequest("Yalnızca görüntü dosyaları kabul edilir (jpg, jpeg, png, gif).");

        if (string.IsNullOrEmpty(_uiRootPath))
            return StatusCode(StatusCodes.Status500InternalServerError, "UIRootPath is not configured.");

        if (string.IsNullOrEmpty(file.FileName))
            return BadRequest("Dosya adı belirtilmedi.");

        var uploadPath = Path.Combine(_uiRootPath, "images");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid().ToString() + extension;
        var path = Path.Combine(uploadPath, uniqueFileName);

        try
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var dealer = _dealerService.TGetByID(id);
            if (dealer == null)
                return NotFound("Firma bulunamadı");

            if (dealer.ImageUrls == null)
            {
                dealer.ImageUrls = new List<string>();
            }

            dealer.ImageUrls.Add(uniqueFileName);
            _dealerService.TUpdate(dealer);

            return Ok(new { path });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"İşlem başarısız: {ex.Message}");
        }
    }



    [HttpGet("{id}/images")]
    public IActionResult GetImages(int id)
    {
        var dealer = _dealerService.TGetByID(id);
        if (dealer == null)
            return NotFound("Firma bulunamadı");

        return Ok(dealer.ImageUrls);
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
        dealer.Phone = updateDealerDto.Phone;
        dealer.DealerCategoryID = updateDealerDto.DealerCategoryID;
        dealer.ImageUrls= updateDealerDto.ImageUrls;
        _dealerService.TUpdate(dealer);
        return Ok("Firma Bilgisi Güncellendi");
    }
}
