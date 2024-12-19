using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.StaticFile.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;
		public ImagesController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public async Task<IActionResult> Post(string ApiKey)
		{
			if (ApiKey != "mySecretKey")
			{
				return BadRequest();
			}
			try
			{
				var files = Request.Form.Files;
				var folderName = Path.Combine("Resources", "Images");
				var PathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
				if (files != null)
				{
					var result = await UploadFileAsync(files);
					return Ok(result);
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error");
				throw new Exception("upload image error", ex);
			}


		}
		private async Task<UploadDto> UploadFileAsync(IFormFileCollection files)
		{
			string newName = Guid.NewGuid().ToString();
			var date = DateTime.Now;
			var folder = $@"Resources/images/{date.Year}-{date.Month}/";
			var UploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);

			if (!Directory.Exists(UploadsRootFolder))
			{
				Directory.CreateDirectory(UploadsRootFolder);
			}
			List<string> address = new List<string>();
			foreach (var file in files)
			{
				if (file != null && file.Length > 0)
				{
					string filename = newName + file.FileName;
					var filePath = Path.Combine(UploadsRootFolder, filename);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}
					address.Add(folder + filename);
				}
			}
			return new UploadDto()
			{
				FileNameAddress = address,
				Status = true
			};

		}

	public class UploadDto
	{

		public bool Status { get; set; }
		public List<string> FileNameAddress { get; set; }
	}
}
