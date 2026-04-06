namespace Joaoaalves.Tiny.Abstractions.Entities.Orders;

/// <summary>
/// The customer associated with an order, including contact and address details.
/// </summary>
public sealed class OrderCustomer
{
    /// <summary>The customer's internal code in Tiny.</summary>
    public string? Code { get; init; }

    /// <summary>The customer's full legal name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>The customer's trade name or DBA name.</summary>
    public string? TradeName { get; init; }

    /// <summary>Person type: "F" for individual, "J" for legal entity.</summary>
    public string? PersonType { get; init; }

    /// <summary>CPF (individual) or CNPJ (company) tax identification number.</summary>
    public string? TaxId { get; init; }

    /// <summary>State tax registration number (Inscrição Estadual).</summary>
    public string? StateRegistration { get; init; }

    /// <summary>National identity document number (RG).</summary>
    public string? NationalId { get; init; }

    /// <summary>Street address.</summary>
    public string? Address { get; init; }

    /// <summary>Street number.</summary>
    public string? AddressNumber { get; init; }

    /// <summary>Address complement (apartment, floor, etc.).</summary>
    public string? Complement { get; init; }

    /// <summary>Neighbourhood or district.</summary>
    public string? Neighborhood { get; init; }

    /// <summary>Brazilian postal code (CEP).</summary>
    public string? PostalCode { get; init; }

    /// <summary>City name.</summary>
    public string? City { get; init; }

    /// <summary>Two-letter state abbreviation (UF), e.g. "SP", "RS".</summary>
    public string? State { get; init; }

    /// <summary>Primary phone number.</summary>
    public string? Phone { get; init; }
}
