namespace Joaoaalves.Tiny.Abstractions.Enums;

/// <summary>
/// Indicates who is responsible for paying the freight on an order.
/// Maps to the <c>frete_por_conta</c> field in the API.
/// </summary>
public enum FreightResponsibility
{
    /// <summary>The sender (seller) is responsible for freight. API value: "E".</summary>
    Sender,

    /// <summary>The recipient (buyer) is responsible for freight. API value: "D".</summary>
    Recipient,

    /// <summary>A third party is responsible for freight. API value: "T".</summary>
    ThirdParty
}
