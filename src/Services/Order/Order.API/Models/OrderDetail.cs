using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Order.API.Models;

[Serializable, BsonIgnoreExtraElements]
public class OrderDetail
{
    [BsonElement("catalog_item_id"), BsonRepresentation(BsonType.Int32)]
    public int CatalogItemId { get; set; }

    [BsonElement("quantity"), BsonRepresentation(BsonType.Decimal128)]
    public decimal Quantity { get; set; }

    [BsonElement("unit_price"), BsonRepresentation(BsonType.Decimal128)]
    public decimal UnitPrice { get; set; }
}
