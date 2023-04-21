using AutoMapper;
using ManageBooks.Dtos;
using ManageBooks.Models;

namespace ManageBooks.Helper
{
	public class AppMapper : Profile
	{
		public AppMapper()
		{
			CreateMap<Order, CreateOrderRequest>();
			CreateMap<Book, CreateBookRequest>();
		}
	}
}
