using System;
using MusicStore.Shared.Models;
namespace MusicStore.Core.Data;
public class Employee : BaseModel
{
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public decimal Salary { get; set; }
}
