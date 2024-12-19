using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.ExternalApi
{
	public interface IFileUploadService
	{
		Task<string> UploadAsync(string Category, IFormFile file);
	}
	public class FileUploadService : IFileUploadService
	{
		public async Task<string> UploadAsync(string Category, IFormFile file)
		{
			var options = new RestClientOptions("https://localhost:44373")
			{
				MaxTimeout = -1,
			};
			var client = new RestClient(options);
			var request = new RestRequest($"/api/images?ApiKey=mySecretKey&category={Category}", method: Method.Post);
			request.AlwaysMultipartFormData = true;


			byte[] bytes;
			using (var ms = new MemoryStream())
			{
				await file.CopyToAsync(ms);
				bytes = ms.ToArray();
			}
			request.AddFile(file.FileName, bytes, file.FileName, file.ContentType);

			RestResponse response = await client.ExecuteAsync(request);
			UploadDto upload = JsonConvert.DeserializeObject<UploadDto>(response.Content);
			return upload.FileNameAddress;
		}
	}
	public class UploadDto
	{
		public bool Status { get; set; }
		public string FileNameAddress { get; set; }
	}
}
