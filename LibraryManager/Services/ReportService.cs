using ManageBooks.Dtos;
using ManageBooks.Models;
using System.Net;

namespace LibraryManager.Services
{
	public class ReportService : IReportService
	{
		private readonly HttpClient _httpClient;

		public ReportService(HttpClient http)
		{
			_httpClient = http;
		}

		public List<Report> Reports { get ; set ; }  = new List<Report>();

		public async Task GetReport()
		{
			var report = await _httpClient.GetFromJsonAsync<Report>("/api/Report");
			if(report != null)
			{
				Reports.Add(report);
			}

		}
	}
}
