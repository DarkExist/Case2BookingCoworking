using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Core.Domain.Entities
{
	public class AudienceType : ValueObject
	{
		public static readonly List<string> validAudienceType =
			new List<string> { 
				"Лекционная",
				"Для практических работ",
				"С проектором",
				"С экраном",
				"Компьютерный класс",
				"С обычной доской",
				"С интерактивной доской"
			};
		public string Type { get; set; }

		public AudienceType() { }
		public AudienceType(string audienceType)
		{
			if (!validAudienceType.Contains(audienceType))
			{
				throw new ArgumentException("Wrong audience type");
			}
			Type = audienceType;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Type;
		}
	}
}
