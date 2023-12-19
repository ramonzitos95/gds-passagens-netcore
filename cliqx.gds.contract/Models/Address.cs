using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.contract.Models;

public class Address
{
    public Guid ExternalId { get; set; } = Guid.NewGuid();

    public bool IsDefault { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Street { get; set; }
    public string ReferencePoint { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string District { get; set; }

    public string FullAddress => string.Join(", ",
        (new[] { Street, Number, ReferencePoint, Country, $"{City}/{State}" }).Where(s =>
            !string.IsNullOrEmpty(s)));

    public Address(Address address)
    {
        
    }

    public Address()
    {
        
    }

}
