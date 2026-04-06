using Joaoaalves.Tiny.Abstractions.Entities.Orders;
using Joaoaalves.Tiny.Abstractions.Enums;
using Joaoaalves.Tiny.Core.DTOs.Orders;

namespace Joaoaalves.Tiny.Core.Mappers;

internal static class OrderMapper
{
    internal static Order ToEntity(TinyOrderJson j) => new()
    {
        Id = ProductMapper.ParseLong(j.Id),
        Number = ProductMapper.NullIfEmpty(j.Number) ?? string.Empty,
        OrderDate = ProductMapper.ParseDate(j.OrderDate) ?? DateTime.MinValue,
        ExpectedDate = ProductMapper.ParseDate(j.ExpectedDate),
        InvoiceDate = ProductMapper.ParseDate(j.InvoiceDate),
        Customer = j.Customer is null ? new OrderCustomer() : ToCustomer(j.Customer),
        Items = j.Items?
            .Where(i => i.Item is not null)
            .Select(i => ToItem(i.Item!))
            .ToList() ?? [],
        Installments = j.Installments?
            .Where(p => p.Installment is not null)
            .Select(p => ToInstallment(p.Installment!))
            .ToList() ?? [],
        Markers = j.Markers?
            .Where(m => m.Marker is not null)
            .Select(m => ToMarker(m.Marker!))
            .ToList() ?? [],
        PaymentCondition = ProductMapper.NullIfEmpty(j.PaymentCondition),
        PaymentMethod = ProductMapper.NullIfEmpty(j.PaymentMethod),
        PaymentMedium = ProductMapper.NullIfEmpty(j.PaymentMedium),
        CarrierName = ProductMapper.NullIfEmpty(j.CarrierName),
        FreightResponsibility = MapFreightResponsibility(j.FreightResponsibility),
        FreightValue = ProductMapper.ParseDecimal(j.FreightValue),
        DiscountValue = ProductMapper.ParseDecimal(j.DiscountValue),
        ProductsTotal = ProductMapper.ParseDecimal(j.ProductsTotal),
        OrderTotal = ProductMapper.ParseDecimal(j.OrderTotal),
        PurchaseOrderNumber = ProductMapper.NullIfEmpty(j.PurchaseOrderNumber),
        Warehouse = ProductMapper.NullIfEmpty(j.Warehouse),
        ShippingMethod = ProductMapper.NullIfEmpty(j.ShippingMethod),
        ShippingService = ProductMapper.NullIfEmpty(j.ShippingService),
        Status = j.Status ?? string.Empty,
        Notes = ProductMapper.NullIfEmpty(j.Notes),
        SellerId = ProductMapper.ParseNullableLong(j.SellerId),
        SellerName = ProductMapper.NullIfEmpty(j.SellerName),
        TrackingCode = ProductMapper.NullIfEmpty(j.TrackingCode),
        TrackingUrl = ProductMapper.NullIfEmpty(j.TrackingUrl),
        InvoiceId = ProductMapper.ParseNullableLong(j.InvoiceId),
        IntegratedPayments = j.IntegratedPayments?
            .Where(p => p.Payment is not null)
            .Select(p => ToIntegratedPayment(p.Payment!))
            .ToList() ?? []
    };

    internal static OrderSummary ToSummary(TinyOrderSummaryJson j) => new()
    {
        Id = ProductMapper.ParseLong(j.Id),
        Number = ProductMapper.ParseLong(j.Number),
        EcommerceNumber = ProductMapper.NullIfEmpty(j.EcommerceNumber),
        OrderDate = ProductMapper.ParseDate(j.OrderDate) ?? DateTime.MinValue,
        ExpectedDate = ProductMapper.ParseDate(j.ExpectedDate),
        CustomerName = j.CustomerName ?? string.Empty,
        Value = ProductMapper.ParseDecimal(j.Value),
        SellerId = ProductMapper.ParseNullableLong(j.SellerId),
        SellerName = ProductMapper.NullIfEmpty(j.SellerName),
        Status = j.Status ?? string.Empty,
        TrackingCode = ProductMapper.NullIfEmpty(j.TrackingCode)
    };

    private static OrderCustomer ToCustomer(TinyOrderCustomerJson j) => new()
    {
        Code = ProductMapper.NullIfEmpty(j.Code),
        Name = j.Name ?? string.Empty,
        TradeName = ProductMapper.NullIfEmpty(j.TradeName),
        PersonType = ProductMapper.NullIfEmpty(j.PersonType),
        TaxId = ProductMapper.NullIfEmpty(j.TaxId),
        StateRegistration = ProductMapper.NullIfEmpty(j.StateRegistration),
        NationalId = ProductMapper.NullIfEmpty(j.NationalId),
        Address = ProductMapper.NullIfEmpty(j.Address),
        AddressNumber = ProductMapper.NullIfEmpty(j.AddressNumber),
        Complement = ProductMapper.NullIfEmpty(j.Complement),
        Neighborhood = ProductMapper.NullIfEmpty(j.Neighborhood),
        PostalCode = ProductMapper.NullIfEmpty(j.PostalCode),
        City = ProductMapper.NullIfEmpty(j.City),
        State = ProductMapper.NullIfEmpty(j.State),
        Phone = ProductMapper.NullIfEmpty(j.Phone)
    };

    private static OrderItem ToItem(TinyOrderItemJson j) => new()
    {
        Sku = ProductMapper.NullIfEmpty(j.Sku),
        Description = j.Description ?? string.Empty,
        Unit = ProductMapper.NullIfEmpty(j.Unit),
        Quantity = ProductMapper.ParseDecimal(j.Quantity),
        UnitPrice = ProductMapper.ParseDecimal(j.UnitPrice)
    };

    private static OrderInstallment ToInstallment(TinyOrderInstallmentJson j) => new()
    {
        Days = ProductMapper.ParseNullableInt(j.Days) ?? 0,
        Date = ProductMapper.ParseDate(j.Date) ?? DateTime.MinValue,
        Value = ProductMapper.ParseDecimal(j.Value),
        Notes = ProductMapper.NullIfEmpty(j.Notes)
    };

    private static OrderMarker ToMarker(TinyOrderMarkerJson j) => new()
    {
        Id = ProductMapper.ParseLong(j.Id),
        Description = j.Description ?? string.Empty,
        Color = ProductMapper.NullIfEmpty(j.Color)
    };

    private static OrderIntegratedPayment ToIntegratedPayment(TinyOrderIntegratedPaymentJson j) => new()
    {
        Value = ProductMapper.ParseDecimal(j.Value),
        PaymentType = ProductMapper.ParseNullableInt(j.PaymentType) ?? 0,
        IntermediaryTaxId = ProductMapper.NullIfEmpty(j.IntermediaryTaxId),
        AuthorizationCode = ProductMapper.NullIfEmpty(j.AuthorizationCode),
        BrandCode = ProductMapper.ParseNullableInt(j.BrandCode)
    };

    private static FreightResponsibility? MapFreightResponsibility(string? value) => value switch
    {
        "E" => FreightResponsibility.Sender,
        "D" => FreightResponsibility.Recipient,
        "T" => FreightResponsibility.ThirdParty,
        _ => null
    };
}
