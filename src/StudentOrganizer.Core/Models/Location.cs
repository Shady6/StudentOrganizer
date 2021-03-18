using System;

namespace StudentOrganizer.Core.Models
{
	public class Location
	{
		public Address Address { get; protected set; }
		public string Link { get; protected set; }
		public string Room { get; protected set; }

        public Location(Address address, string room, string link="")
        {
            SetLink(link);
            SetRoom(room);
            SetAddress(address);
        }

		public void SetLink(string link)
        {
            Link = link;
        }

        public void SetRoom(string room)
        {
            if (string.IsNullOrWhiteSpace(room))
            {
                throw new Exception("Room can not be empty.");
            }
            Room = room;
        }

        public void SetAddress(Address address)
        {
            if(address == null)
            {
                throw new Exception("Address can not be null.");
            }
            Address = address;
        }
    }
}