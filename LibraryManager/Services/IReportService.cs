using ManageBooks.Dtos;

namespace LibraryManager.Services
{
	public interface IReportService
	{
		List<Report> Reports { get; set; }
		Task GetReport();
	}
}
