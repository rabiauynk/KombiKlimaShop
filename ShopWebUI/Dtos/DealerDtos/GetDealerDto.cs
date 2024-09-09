using ShopWebUI.Dtos.DealerImageDtos;

namespace ShopWebUI.Dtos.DealerDtos
{
    public class GetDealerDto
    {

        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string DealerOwner { get; set; }
        public string DealerAddress { get; set; }
        public string DealerCity { get; set; }
        public string DealerDistrict { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string CategoryName { get; set; }
        public List<ResultDealerImageDto> Images { get; set; }

    }
}
