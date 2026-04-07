# Enums

All enums map Tiny API V2 string codes to typed C# values. The mapping is handled internally by the mappers; callers always work with these named values.

## ProductStatus

Status of a product record.

| Member | Tiny API value | Description |
|---|---|---|
| `Active` | `"A"` | Product is active and available |
| `Inactive` | `"I"` | Product is inactive |
| `Deleted` | `"E"` | Product is marked as deleted |

## ProductType

Whether the record is a physical product or a service.

| Member | Tiny API value | Description |
|---|---|---|
| `Product` | `"P"` | Physical product |
| `Service` | `"S"` | Service |

## VariationType

Role of a product within a variation group.

| Member | Tiny API value | Description |
|---|---|---|
| `Normal` | `""` | Standalone product with no variations |
| `Parent` | `"P"` | Parent product that groups variations |
| `Variation` | `"V"` | Child variation of a parent |

## ProductClass

Classification of the product's composition or structure.

| Member | Tiny API value | Description |
|---|---|---|
| `Simple` | `"S"` | Regular product |
| `Kit` | `"K"` | Bundle of other products |
| `WithVariations` | `"V"` | Product with size/colour variations |
| `Manufactured` | `"F"` | Manufactured in-house |
| `RawMaterial` | `"M"` | Raw material for manufacturing |

## PackagingType

Shape of the product's packaging, used for freight calculation.

| Member | Tiny API value | Description |
|---|---|---|
| `Envelope` | `"1"` | Flat envelope |
| `PackageOrBox` | `"2"` | Box or package |
| `RollOrCylinder` | `"3"` | Roll or cylinder |

## StockUpdateType

Direction of a stock movement recorded via `ITinyStockService.UpdateAsync`.

| Member | Tiny API value | Description |
|---|---|---|
| `Entry` | `"E"` | Incoming stock (purchase, production) |
| `Exit` | `"S"` | Outgoing stock (sale, waste) |
| `Balance` | `"B"` | Absolute balance adjustment |

## FreightResponsibility

Who is responsible for freight costs on an order.

| Member | Tiny API value | Description |
|---|---|---|
| `Sender` | `"E"` | Sender (remetente) pays freight |
| `Recipient` | `"D"` | Recipient (destinatário) pays freight |
| `ThirdParty` | `"T"` | Third party pays freight |

## SortOrder

Sort direction for `SearchOrdersRequest`.

| Member | Description |
|---|---|
| `Ascending` | Oldest first |
| `Descending` | Newest first |
