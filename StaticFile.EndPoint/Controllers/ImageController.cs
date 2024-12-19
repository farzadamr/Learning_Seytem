using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
	private readonly IWebHostEnvironment _environment;
	public ImagesController(IWebHostEnvironment environment)
	{
		_environment = environment;
	}

	[HttpPost]
	public async Task<IActionResult> Post(string ApiKey, string category)
	{
		if (ApiKey != "mySecretKey")
		{
			return BadRequest();
		}
		try
		{
			var file = Request.Form.Files.FirstOrDefault();
			if (file != null)
			{
				var result = await UploadFileAsync(file, category);
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

	private async Task<UploadDto> UploadFileAsync(IFormFile file, string category)
	{
		string newName = Guid.NewGuid().ToString();
		var date = DateTime.Now;
		var folder = $@"Resources/{category}/{date.Year}-{date.Month}/";
		var UploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);

		if (!Directory.Exists(UploadsRootFolder))
		{
			Directory.CreateDirectory(UploadsRootFolder);
		}
		string address = "";
		if (file != null && file.Length > 0)
		{
			string filename = newName + file.FileName;
			var filePath = Path.Combine(UploadsRootFolder, filename);
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}
			address = folder + filename;
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
		public string FileNameAddress { get; set; }
	}
}
