﻿using E6K_NetAng_GestContact.Api.Models.Entities;
using System.Data;

namespace E6K_NetAng_GestContact.Api.Models.Mappers
{
    internal static class DataRecordExtensions
    {
        internal static Contact ToContact(this IDataRecord record)
        {
            return new Contact
            {
                Id = (int)record["Id"],
                Nom = (string)record["Nom"],
                Prenom = (string)record["Prenom"]
            };
        }
    }
}
