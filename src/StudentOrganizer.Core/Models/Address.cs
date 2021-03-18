using System;

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
                throw new Exception("Street name can not be empty.");
            }
            StreetName = streetName;
        }

        public void SetBuildingNumber(string buildingNumber)
        {
            if (string.IsNullOrWhiteSpace(buildingNumber))
            {
                throw new Exception("Building number can not be empty.");
            }
            BuildingNumber = buildingNumber;
        }

        public void SetCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new Exception("City can not be empty.");
            }
            City = city;
        }
    }
}