using System;
using StudentOrganizer.Core.Common;

namespace StudentOrganizer.Core.Models
{
	public class Address
	{
		public string StreetName { get; protected set; }
		public string BuildingNumber { get; protected set; }
		public string City { get; protected set; }

		public Address(string streetName, string buildingNumber, string city)
		{
			SetBuildingNumber(buildingNumber);
			SetStreetName(streetName);
			SetCity(city);
		}

		public void SetStreetName(string streetName)
		{
			if (string.IsNullOrWhiteSpace(streetName))
			{
				throw new AppException("Street name can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			StreetName = streetName;
		}

		public void SetBuildingNumber(string buildingNumber)
		{
			if (string.IsNullOrWhiteSpace(buildingNumber))
			{
				throw new AppException("Building number can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			BuildingNumber = buildingNumber;
		}

		public void SetCity(string city)
		{
			if (string.IsNullOrWhiteSpace(city))
			{
				throw new AppException("City can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			City = city;
		}

		public override bool Equals(object obj)
		{
			var other = obj as Address;

			return StreetName == other.StreetName &&
				BuildingNumber == other.BuildingNumber &&
				City == other.City;
		}
	}
}