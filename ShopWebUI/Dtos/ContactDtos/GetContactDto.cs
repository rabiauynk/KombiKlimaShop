namespace ShopWebUI.Dtos.ContactDtos
{
	public class GetContactDto
	{
		public int ContactID { get; set; }
		public string Location { get; set; }
		public string Phone { get; set; }
		public string Mail { get; set; }
		public string FooterDescription { get; set; }
		public string BankAccounts { get; set; }
	}
}
