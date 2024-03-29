﻿using System;
using StudentOrganizer.Core.Common;

namespace StudentOrganizer.Core.Models
{
	public class Location
	{
		public Address Address { get; protected set; }
		public string Link { get; protected set; }
		public string Room { get; protected set; }

		public Location(Address address, string room, string link = "")
		{
			SetLink(link);
			SetRoom(room);
			SetAddress(address);
		}

		public Location()
		{
		}

		public void SetLink(string link)
		{
			Link = link;
		}

		public void SetRoom(string room)
		{
			if (string.IsNullOrWhiteSpace(room))
			{
				throw new AppException("Room can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			Room = room;
		}

		public void SetAddress(Address address)
		{
			if (address == null)
			{
				throw new AppException("Address can not be null.", AppErrorCode.VALIDATION_ERROR);
			}
			Address = address;
		}

		public override bool Equals(object obj)
		{
			var other = obj as Location;

			return Link == other.Link &&
				Room == other.Room &&
				Address.Equals(other.Address);
		}
	}
}