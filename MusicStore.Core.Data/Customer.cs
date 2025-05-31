using System;
using MusicStore.Shared.Models;
namespace MusicStore.Core.Data;
public class Customer : BaseModel
{
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
}
