using System;
using System.Collections.Generic;
using Lab6.Models.EntityMetadata;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Models.DataAccess
{
    [ModelMetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {

    }
}