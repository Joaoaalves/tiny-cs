# Entities

All entities are read-only `sealed class` types with `init`-only properties. They are produced by the mapper layer and returned directly to callers.

## Common

### PagedResult\<T\>

Generic wrapper for any paginated endpoint.

| Property | Type | Description |
|---|---|---|
| `Page` | `int` | Current page (1-based) |
| `TotalPages` | `int` | Total number of pages available |
| `Items` | `IReadOnlyList<T>` | Items on this page |

### UpsertResult

Outcome of a single item in a batch create or update operation.

| Property | Type | Description |
|---|---|---|
| `Sequence` | `int` | Position of this item in the input list |
| `Success` | `bool` | `true` when Tiny accepted the record |
| `Id` | `long?` | ID assigned or updated by Tiny |
| `Errors` | `IReadOnlyList<string>` | Error messages when `Success` is `false` |
| `VariationIds` | `IReadOnlyList<long>` | IDs of any product variations created alongside this record |

---

## Products

### Product

Full product representation returned by `GetByIdAsync`.

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Tiny product ID |
| `CreatedAt` | `DateTime?` | Creation timestamp |
| `Name` | `string` | Product name |
| `Sku` | `string?` | Internal SKU / code |
| `Unit` | `string?` | Unit of measure (e.g. `UN`, `CX`) |
| `Price` | `decimal` | Sale price |
| `PromotionalPrice` | `decimal` | Promotional sale price |
| `Ncm` | `string?` | Fiscal classification code |
| `Origin` | `string?` | Origin code (0–8) |
| `Gtin` | `string?` | EAN/GTIN barcode |
| `PackagingGtin` | `string?` | Packaging GTIN |
| `Location` | `string?` | Warehouse location label |
| `NetWeight` | `decimal` | Net weight in kg |
| `GrossWeight` | `decimal` | Gross weight in kg |
| `MinimumStock` | `decimal` | Minimum stock alert threshold |
| `MaximumStock` | `decimal` | Maximum stock limit |
| `SupplierId` | `long?` | Tiny supplier contact ID |
| `SupplierCode` | `string?` | Supplier code |
| `SupplierProductCode` | `string?` | Supplier's own product code |
| `UnitsPerBox` | `string?` | Units per shipping box |
| `CostPrice` | `decimal` | Last purchase cost |
| `AverageCostPrice` | `decimal` | Weighted average cost |
| `Status` | `ProductStatus` | Active, Inactive, or Deleted |
| `Type` | `ProductType` | Product or Service |
| `IpiClass` | `string?` | IPI tax class |
| `FixedIpiValue` | `decimal` | Fixed IPI amount |
| `ServiceListCode` | `string?` | Service list code (LC 116) |
| `AdditionalDescription` | `string?` | Extended description |
| `Notes` | `string?` | Internal notes |
| `Warranty` | `string?` | Warranty description |
| `Cest` | `string?` | CEST fiscal code |
| `VariationType` | `VariationType` | Normal, Parent, or Variation |
| `Variations` | `IReadOnlyList<ProductVariation>` | Child variations (when `VariationType = Parent`) |
| `ParentProductId` | `long?` | Parent product ID (when `VariationType = Variation`) |
| `MadeToOrder` | `bool` | Whether the product is made to order |
| `PreparationDays` | `int?` | Lead time in days |
| `Grid` | `Dictionary<string, string>` | Variation grid attributes (size, color, etc.) |
| `Brand` | `string?` | Brand name |
| `PackagingType` | `PackagingType?` | Envelope, box, or roll/cylinder |
| `PackagingHeight` | `decimal?` | Packaging height in cm |
| `PackagingWidth` | `decimal?` | Packaging width in cm |
| `PackagingLength` | `decimal?` | Packaging length in cm |
| `PackagingDiameter` | `decimal?` | Packaging diameter in cm |
| `Category` | `string?` | Category name |
| `Attachments` | `IReadOnlyList<string>` | Attachment URLs |
| `ExternalImages` | `IReadOnlyList<string>` | External image URLs |
| `ProductClass` | `ProductClass` | Simple, Kit, WithVariations, Manufactured, or RawMaterial |
| `KitItems` | `IReadOnlyList<ProductKitItem>` | Component products when `ProductClass = Kit` |
| `Seo` | `ProductSeo?` | SEO metadata (null when all fields are empty) |

### ProductSummary

Lightweight projection returned by `SearchAsync`. Contains the most commonly needed fields without the full product detail.

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Tiny product ID |
| `Name` | `string` | Product name |
| `Sku` | `string?` | Internal SKU |
| `Price` | `decimal` | Sale price |
| `PromotionalPrice` | `decimal` | Promotional price |
| `CostPrice` | `decimal?` | Last purchase cost |
| `AverageCostPrice` | `decimal?` | Weighted average cost |
| `Unit` | `string?` | Unit of measure |
| `Gtin` | `string?` | EAN/GTIN barcode |
| `VariationType` | `VariationType` | Normal, Parent, or Variation |
| `Location` | `string?` | Warehouse location label |
| `Status` | `ProductStatus?` | Active, Inactive, or Deleted |
| `CreatedAt` | `DateTime?` | Creation date |

### ProductVariation

A specific variation of a parent product (size/colour combination).

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Tiny variation ID |
| `Sku` | `string?` | Variation SKU |
| `Price` | `decimal` | Variation-specific price |
| `Grid` | `Dictionary<string, string>` | Attributes such as `{ "Tamanho": "G", "Cor": "Branco" }` |

### ProductKitItem

A component product inside a kit.

| Property | Type | Description |
|---|---|---|
| `ProductId` | `long` | Tiny ID of the component product |
| `Quantity` | `decimal` | Quantity of this component in the kit |

### ProductSeo

SEO metadata. Returned as `null` on `Product` when all fields are empty.

| Property | Type | Description |
|---|---|---|
| `Title` | `string?` | Page title |
| `Keywords` | `string?` | Meta keywords |
| `VideoUrl` | `string?` | YouTube or Vimeo URL |
| `Description` | `string?` | Meta description |
| `Slug` | `string?` | URL slug |

---

## Orders

### Order

Full order representation returned by `GetByIdAsync`.

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Tiny order ID |
| `Number` | `string` | Human-readable order number |
| `OrderDate` | `DateTime` | Date the order was placed |
| `ExpectedDate` | `DateTime?` | Expected delivery date |
| `InvoiceDate` | `DateTime?` | Invoice issuance date |
| `Customer` | `OrderCustomer` | Customer details |
| `Items` | `IReadOnlyList<OrderItem>` | Line items |
| `Installments` | `IReadOnlyList<OrderInstallment>` | Payment installments |
| `Markers` | `IReadOnlyList<OrderMarker>` | Labels/tags attached to the order |
| `PaymentCondition` | `string?` | Payment condition description |
| `PaymentMethod` | `string?` | Payment method description |
| `PaymentMedium` | `string?` | Payment medium (card, PIX, etc.) |
| `CarrierName` | `string?` | Carrier/transporter name |
| `FreightResponsibility` | `FreightResponsibility?` | Sender, Recipient, or ThirdParty |
| `FreightValue` | `decimal` | Freight cost |
| `DiscountValue` | `decimal` | Total discount applied |
| `ProductsTotal` | `decimal` | Sum of all line items before freight and discounts |
| `OrderTotal` | `decimal` | Final order total |
| `PurchaseOrderNumber` | `string?` | Buyer's PO number |
| `Warehouse` | `string?` | Fulfillment warehouse |
| `ShippingMethod` | `string?` | Shipping method name |
| `ShippingService` | `string?` | Carrier service level |
| `Status` | `string` | Tiny status label (open-ended string) |
| `Notes` | `string?` | Internal notes |
| `SellerId` | `long?` | Seller ID |
| `SellerName` | `string?` | Seller name |
| `TrackingCode` | `string?` | Shipment tracking code |
| `TrackingUrl` | `string?` | Carrier tracking URL |
| `InvoiceId` | `long?` | Linked invoice ID |
| `IntegratedPayments` | `IReadOnlyList<OrderIntegratedPayment>` | Gateway payment records |

### OrderSummary

Lightweight projection returned by `SearchAsync`.

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Tiny order ID |
| `Number` | `long` | Order number |
| `EcommerceNumber` | `string?` | Marketplace reference number |
| `OrderDate` | `DateTime` | Date the order was placed |
| `ExpectedDate` | `DateTime?` | Expected delivery date |
| `CustomerName` | `string` | Customer full name |
| `Value` | `decimal` | Order total value |
| `SellerId` | `long?` | Seller ID |
| `SellerName` | `string?` | Seller name |
| `Status` | `string` | Tiny status label |
| `TrackingCode` | `string?` | Shipment tracking code |

### OrderCustomer

Customer details embedded in the full `Order`.

| Property | Type | Description |
|---|---|---|
| `Code` | `string?` | Customer code in Tiny |
| `Name` | `string` | Full name |
| `TradeName` | `string?` | Trade name (razão social) |
| `PersonType` | `string?` | `"F"` (individual) or `"J"` (company) |
| `TaxId` | `string?` | CPF or CNPJ |
| `StateRegistration` | `string?` | Inscrição Estadual |
| `NationalId` | `string?` | RG (individual document) |
| `Address` | `string?` | Street address |
| `AddressNumber` | `string?` | Street number |
| `Complement` | `string?` | Address complement |
| `Neighborhood` | `string?` | Neighborhood (bairro) |
| `PostalCode` | `string?` | CEP |
| `City` | `string?` | City |
| `State` | `string?` | State abbreviation (UF) |
| `Phone` | `string?` | Phone number |

### OrderItem

A line item within an order.

| Property | Type | Description |
|---|---|---|
| `Sku` | `string?` | Product SKU |
| `Description` | `string` | Product description |
| `Unit` | `string?` | Unit of measure |
| `Quantity` | `decimal` | Quantity ordered |
| `UnitPrice` | `decimal` | Unit sale price |

### OrderInstallment

A payment installment.

| Property | Type | Description |
|---|---|---|
| `Days` | `int` | Days until this installment is due |
| `Date` | `DateTime` | Due date |
| `Value` | `decimal` | Installment amount |
| `Notes` | `string?` | Installment notes |

### OrderMarker

A label/tag attached to an order for visual classification.

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Marker ID |
| `Description` | `string` | Marker label |
| `Color` | `string?` | Hex color string (e.g. `"#FF5733"`) |

### OrderIntegratedPayment

A payment gateway transaction linked to an order.

| Property | Type | Description |
|---|---|---|
| `Value` | `decimal` | Transaction amount |
| `PaymentType` | `int` | Gateway payment type code |
| `IntermediaryTaxId` | `string?` | Intermediary CNPJ |
| `AuthorizationCode` | `string?` | Authorization or NSU code |
| `BrandCode` | `int?` | Card brand code |

---

## Stock

### ProductStock

Current stock position for a product, returned by `GetByProductIdAsync`.

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Tiny product ID |
| `Name` | `string` | Product name |
| `Sku` | `string?` | Product SKU |
| `Unit` | `string?` | Unit of measure |
| `Balance` | `decimal` | Total available stock |
| `ReservedBalance` | `decimal?` | Stock reserved for open orders |
| `Warehouses` | `IReadOnlyList<StockWarehouse>` | Per-warehouse breakdown |

### StockUpdateEntry

A product from the real-time stock update queue, returned by `ListUpdatesAsync`.

| Property | Type | Description |
|---|---|---|
| `Id` | `long` | Tiny product ID |
| `Name` | `string` | Product name |
| `Sku` | `string?` | Product SKU |
| `Unit` | `string?` | Unit of measure |
| `VariationType` | `VariationType` | Normal, Parent, or Variation |
| `Location` | `string?` | Warehouse location label |
| `UpdatedAt` | `DateTime?` | Timestamp of the stock change |
| `Balance` | `decimal` | Current available stock |
| `ReservedBalance` | `decimal?` | Stock reserved for open orders |
| `Warehouses` | `IReadOnlyList<StockWarehouse>` | Per-warehouse breakdown |

### StockWarehouse

Stock balance for a single warehouse.

| Property | Type | Description |
|---|---|---|
| `Name` | `string?` | Warehouse name |
| `Exclude` | `bool` | Whether this warehouse is excluded from the main balance |
| `Balance` | `decimal` | Balance in this warehouse |
| `Company` | `string?` | Company alias for this warehouse |
