using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Core.Infrastructure.Models
{
	public class LoggedUserCommand
	{
		[BindNever]
		public int UserId { get; set; }
		[BindNever]
		public int EmpRecordId { get; set; }
		[BindNever]
		public string EmployeeId { get; set; }
		[BindNever]
		public IEnumerable<Group> Groups { get; set; }
		[BindNever]
		public IEnumerable<Role> Roles { get; set; }
		[BindNever]
		public IEnumerable<Pop> Pops { get; set; }

		[BindNever]
		public string Email { get; set; }

		[BindNever]
		public string UserFullName { get; set; }

		

		[BindNever]
		public int DepartmentId { get; set; }

		[BindNever]
		public int CompanyId { get; set; }

		[BindNever]
		public int LocationId { get; set; }

		[BindNever]
		public int CounterId { get; set; }

		[BindNever]
		public bool IsManager { get;  set; }

		[BindNever]
		public bool IsAdmin { get; set; }

		[BindNever]
		public bool IsExecutive { get;  set; }


		//public string LoginId { get; set; }
		//public string Username { get; set; }
	}

	public class Group
	{
		public int GroupId { get; set; }
		public string GroupName { get; set; }

	}
	public class Role
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; }

	}
	public class Pop
	{
		public int PopId { get; set; }
		public string PopName { get; set; }
		public int DivisionId { get; set; }
		public int DistrictId { get; set; }
		public int ThanaId { get; set; }




	}
}
