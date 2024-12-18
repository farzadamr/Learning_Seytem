﻿using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IPersonService
	{
		Task<ResultDto<int?>> AddPerson(PersonDto person);
	}
}
