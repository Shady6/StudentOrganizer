﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Mongo
{
    public class MongoConfiguration
    {
        private static bool _initialized;
        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            _initialized = true;
            RegisterConventions();
        }
        private static void RegisterConventions()
        {
            ConventionRegistry.Register("Conventions", new MongoConvention(), x => true);
        }
        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions
                => new List<IConvention>
                {
                    new IgnoreExtraElementsConvention(true),
                    new EnumRepresentationConvention(BsonType.String)
                };
        }
    }
}
