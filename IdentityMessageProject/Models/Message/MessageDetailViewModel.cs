using IdentityMessageProject.EntityLayer.Concrete;

namespace IdentityMessageProject.Models.Message
{
	public class MessageDetailViewModel
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public int SenderId { get; set; }
		public AppUser AppUser { get; set; }
	}
}
