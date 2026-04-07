# Request DTOs

Request DTOs are input objects passed to service methods. All properties are `init`-only and optional unless marked required.

## Products

### SearchProductsRequest

Passed to `ITinyProductService.SearchAsync`.

| Property | Type | Default | Description |
|---|---|---|---|
| `Query` | `string?` | `null` | Free-text search across name and SKU |
| `Status` | `ProductStatus?` | `null` | Filter by product status |
| `Page` | `int` | `1` | Page number (1-based) |

### UpsertProductData

Passed to `ITinyProductService.CreateAsync` and `UpdateAsync`. All fields are optional except for `Name` and `Price` on create.

| Property | Type | Description |
|---|---|---|
| `Sequence` | `int` | Position in the batch (used to match `UpsertResult`) |
| `Id` | `long?` | Tiny product ID — required for updates |
| `Sku` | `string?` | Internal SKU / code |
| `Name` | `string?` | Product name |
| `Unit` | `string?` | Unit of measure |
| `Price` | `decimal` | Sale price |
| `PromotionalPrice` | `decimal?` | Promotional price |
| `Ncm` | `string?` | NCM fiscal classification |
| `Origin` | `string?` | Origin code (0–8) |
| `Gtin` | `string?` | EAN/GTIN barcode |
| `PackagingGtin` | `string?` | Packaging GTIN |
| `Location` | `string?` | Warehouse location label |
| `NetWeight` | `decimal?` | Net weight in kg |
| `GrossWeight` | `decimal?` | Gross weight in kg |
| `MinimumStock` | `decimal?` | Minimum stock threshold |
| `MaximumStock` | `decimal?` | Maximum stock limit |
| `SupplierId` | `long?` | Tiny supplier ID |
| `SupplierCode` | `string?` | Supplier code |
| `SupplierProductCode` | `string?` | Supplier's product code |
| `UnitsPerBox` | `string?` | Units per shipping box |
| `CostPrice` | `decimal?` | Purchase cost |
| `Status` | `ProductStatus` | Active, Inactive, or Deleted (default: Active) |
| `Type` | `ProductType` | Product or Service (default: Product) |
| `IpiClass` | `string?` | IPI tax class |
| `FixedIpiValue` | `decimal?` | Fixed IPI amount |
| `ServiceListCode` | `string?` | LC 116 service list code |
| `AdditionalDescription` | `string?` | Extended description |
| `Notes` | `string?` | Internal notes |
| `Warranty` | `string?` | Warranty description |
| `Cest` | `string?` | CEST fiscal code |
| `PreparationDays` | `int?` | Lead time in days |
| `Brand` | `string?` | Brand name |
| `PackagingType` | `PackagingType?` | Envelope, box, or roll/cylinder |
| `PackagingHeight` | `decimal?` | Packaging height in cm |
| `PackagingWidth` | `decimal?` | Packaging width in cm |
| `PackagingLength` | `decimal?` | Packaging length in cm |
| `PackagingDiameter` | `decimal?` | Packaging diameter in cm |
| `Category` | `string?` | Category name |
| `ProductClass` | `ProductClass?` | Simple, Kit, WithVariations, Manufactured, or RawMaterial |

---

## Orders

### SearchOrdersRequest

Passed to `ITinyOrderService.SearchAsync`. All filter properties are optional.

| Property | Type | Default | Description |
|---|---|---|---|
| `Number` | `string?` | `null` | Exact order number |
| `CustomerName` | `string?` | `null` | Partial customer name |
| `CustomerTaxId` | `string?` | `null` | CPF or CNPJ |
| `StartDate` | `DateOnly?` | `null` | Order date range start |
| `EndDate` | `DateOnly?` | `null` | Order date range end |
| `UpdatedSince` | `DateTime?` | `null` | Filter by last update timestamp |
| `Status` | `string?` | `null` | Tiny status label (e.g. `"Aberto"`, `"Aprovado"`) |
| `EcommerceNumber` | `string?` | `null` | Marketplace reference number |
| `SellerId` | `long?` | `null` | Seller ID |
| `SellerName` | `string?` | `null` | Partial seller name |
| `Marker` | `string?` | `null` | Marker label |
| `OccurrenceStartDate` | `DateOnly?` | `null` | Occurrence date range start |
| `OccurrenceEndDate` | `DateOnly?` | `null` | Occurrence date range end |
| `Page` | `int` | `1` | Page number (1-based) |
| `SortOrder` | `SortOrder?` | `null` | Ascending or Descending |

> Searching without any filter on accounts with large order volumes may trigger a server-side error from Tiny. Provide at least a date range when possible.

---

## Stock

### UpdateStockData

Passed to `ITinyStockService.UpdateAsync`.

| Property | Type | Required | Description |
|---|---|---|---|
| `ProductId` | `long` | Yes | Tiny product ID |
| `Type` | `StockUpdateType` | Yes | Entry, Exit, or Balance |
| `Quantity` | `decimal` | Yes | Movement quantity |
| `Date` | `DateTime?` | No | Movement date (defaults to now in Tiny) |
| `UnitPrice` | `decimal?` | No | Unit cost for this movement |
| `Notes` | `string?` | No | Free-text observation |
| `WarehouseName` | `string?` | No | Target warehouse name |

### ListStockUpdatesRequest

Passed to `ITinyStockService.ListUpdatesAsync`.

| Property | Type | Required | Description |
|---|---|---|---|
| `UpdatedSince` | `DateTime` | Yes | Only products changed at or after this timestamp |
| `Page` | `int` | No | Page number, 1-based (default: 1) |
