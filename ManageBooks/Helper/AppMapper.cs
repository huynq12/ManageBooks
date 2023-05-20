using AutoMapper;
using ManageBooks.Dtos;
using ManageBooks.Models;

namespace ManageBooks.Helper
{
	public class AppMapper : Profile
	{
		public AppMapper()
		{
			CreateMap<Order, OrderDto>();
			CreateMap<Book, CreateBookRequest>();
		}
	}
}
